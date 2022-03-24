using EasyNetworker.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpSenderService : IUdpSenderService
{
    private readonly IPacketGeneratorService packetGeneratorService;

    public UdpSenderService(IPacketGeneratorService packetGeneratorService)
    {
        this.packetGeneratorService = packetGeneratorService;
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await Task.Run(() => Send(remoteEndPoint, paylaod));
    }
    public void Send<T>(IPEndPoint remoteEndPoint, T payload)
    {
        using (var udp = new UdpClient())
        {
            var packetBytes = packetGeneratorService.GenerateAsByteArray(payload);
            udp.Send(packetBytes, packetBytes.Length, remoteEndPoint);
        }
    }
}
