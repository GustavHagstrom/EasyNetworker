using EasyNetworker.Abstractions;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class TcpSenderService : ITcpSenderService
{
    private readonly ISerializerService serializerService;

    public TcpSenderService(ISerializerService serializerService)
    {
        this.serializerService = serializerService;
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await Task.Run(() => Send(remoteEndPoint, paylaod));
    }
    public void Send<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        using (var tcp = new TcpClient())
        {
            var bytesToSend = serializerService.SerializePayload(paylaod);
            tcp.Connect(remoteEndPoint);
            var stream = tcp.GetStream();
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}
