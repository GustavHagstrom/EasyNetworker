using EasyNetworker.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class TcpSenderService : ITcpSenderService
{
    private readonly ISerializerService serializerService;
    private readonly ILogger<TcpSenderService> logger;

    public TcpSenderService(ISerializerService serializerService, ILogger<TcpSenderService> logger)
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
        using (var tcp = new TcpClient())
        {
            var bytesToSend = serializerService.SerializePayload(paylaod);
            logger.LogInformation($"Establishing Tcp connection to {remoteEndPoint}");
            tcp.Connect(remoteEndPoint);
            logger.LogInformation("Sending Tcp payload");
            var stream = tcp.GetStream();
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}
