using System.Net;

namespace EasyNetworker.Abstractions;

public interface IUdpSenderService
{
    void SendUdp<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task SendUdpAsync<T>(IPEndPoint remoteEndPoint, T paylaod);
}