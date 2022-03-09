using EasyNetworker.Abstractions;
using EasyNetworker.Models;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpListenerService : IUdpListenerService
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly ISerializerService serializerService;

    public UdpListenerService(IHandlerInvokerService handlerInvokerService, ISerializerService serializerService)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.serializerService = serializerService;
    }
    public void ReceiveOnce(IPEndPoint localEndPoint)
    {
        using (var client = new UdpClient(localEndPoint))
        {
            var bytes = client.Receive(ref localEndPoint);
            InvokeHandler(bytes);
        }
    }
    public void BeginContinuousReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        using (var client = new UdpClient(localEndPoint))
        {
            if (cancellationToken?.IsCancellationRequested == false)
            {
                client.BeginReceive(ContinuousReceivingCallback, new UdpState { Client = client, EndPoint = localEndPoint, CancelToken = cancellationToken });
            }
        }
    }
    private void ContinuousReceivingCallback(IAsyncResult ar)
    {
        var state = (UdpState)ar.AsyncState!;
        var client = state.Client!;
        var endPoint = state.EndPoint!;
        BeginContinuousReceiving(endPoint, state.CancelToken);
        var bytes = client.EndReceive(ar, ref endPoint);
        InvokeHandler(bytes);
    }
    private void InvokeHandler(byte[] receivedBytes)
    {
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray());
        object receivedObject = serializerService.DeserializeReceivedBytes(receivedBytes);
        handlerInvokerService.Invoke(receivedObject, id);
    }
}
