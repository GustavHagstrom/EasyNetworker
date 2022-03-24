using EasyNetworker.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class TcpSenderService : ITcpSenderService
{
    private readonly IPacketGeneratorService packetGeneratorService;

    public TcpSenderService(IPacketGeneratorService packetGeneratorService)
    {
        this.packetGeneratorService = packetGeneratorService;
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        
        await Task.Run(() => Send(remoteEndPoint, paylaod));
    }
    public void Send<T>(IPEndPoint remoteEndPoint, T payload)
    {
        using (var tcp = new TcpClient())
        {
            var packetBytes = packetGeneratorService.GenerateAsByteArray(payload);
            tcp.Connect(remoteEndPoint);
            var stream = tcp.GetStream();
            stream.Write(packetBytes, 0, packetBytes.Length);
        }
    }
}
