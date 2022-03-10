using EasyNetworker.Abstractions;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class UdpSenderService : IUdpSenderService
{
    private readonly ISerializerService serializerService;

    public UdpSenderService(ISerializerService serializerService)
    {
        this.serializerService = serializerService;
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
            udp.Send(bytesToSend, bytesToSend.Length, remoteEndPoint);
        }
    }
}
