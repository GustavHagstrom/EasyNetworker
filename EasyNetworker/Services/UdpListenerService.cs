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
    public async Task StartContinuousReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        using (var client = new UdpClient(localEndPoint))
        {
            while (cancellationToken?.IsCancellationRequested == false || cancellationToken == null)
            {
                var result = await client.ReceiveAsync();
                _ = Task.Run(() => InvokeHandler(result.Buffer));
            }
        }
    }
    private void InvokeHandler(byte[] receivedBytes)
    {
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray());
        object receivedObject = serializerService.DeserializeReceivedBytes(receivedBytes);
        handlerInvokerService.Invoke(receivedObject, id);
    }
}
