using EasyNetworker.Abstractions;
using EasyNetworker.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpListenerService : IUdpListenerService
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly ISerializerService serializerService;
    private readonly ILogger<UdpListenerService> logger;

    public UdpListenerService(IHandlerInvokerService handlerInvokerService, ISerializerService serializerService, ILogger<UdpListenerService> logger)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.serializerService = serializerService;
        this.logger = logger;
    }
    public void ReceiveOnce(IPEndPoint localEndPoint)
    {
        using (var client = new UdpClient(localEndPoint))
        {
            logger.LogInformation($"Starting to listen for incoming Udp data");
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
                logger.LogInformation($"Starting to listen for incoming Udp data");
                var result = await client.ReceiveAsync();
                _ = Task.Run(() => InvokeHandler(result.Buffer));
            }
        }
    }
    private void InvokeHandler(byte[] receivedBytes)
    {
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray());
        var basePacket = serializerService.DeserializeReceivedBytes(receivedBytes);
        logger.LogInformation($"Data received with Udp. PacketId: {basePacket.Id}. Passing to Handler...");
        handlerInvokerService.Invoke(basePacket);
    }
}
