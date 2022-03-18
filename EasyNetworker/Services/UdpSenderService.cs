using EasyNetworker.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpSenderService : IUdpSenderService
{
    private readonly ISerializerService serializerService;
    private readonly Logger<UdpSenderService> logger;

    public UdpSenderService(ISerializerService serializerService, Logger<UdpSenderService> logger)
    {
        this.serializerService = serializerService;
        this.logger = logger;
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await Task.Run(() => Send(remoteEndPoint, paylaod));
    }
    public void Send<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        using (var udp = new UdpClient())
        {
            var bytesToSend = serializerService.SerializePayload(paylaod);
            logger.LogInformation($"Sending Udp payload");
            udp.Send(bytesToSend, bytesToSend.Length, remoteEndPoint);
        }
    }
}
