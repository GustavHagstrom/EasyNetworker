using EasyNetworker.Abstractions;
using EasyNetworker.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpListenerService : IUdpListenerService
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly IPacketGeneratorService packetGeneratorService;

    public UdpListenerService(IHandlerInvokerService handlerInvokerService, IPacketGeneratorService packetGeneratorService)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.packetGeneratorService = packetGeneratorService;
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
        var packet = packetGeneratorService.Generate(receivedBytes);
        handlerInvokerService.Invoke(packet);
    }
}
