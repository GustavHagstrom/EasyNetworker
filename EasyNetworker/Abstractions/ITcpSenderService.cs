using System.Net;

namespace EasyNetworker.Abstractions;

public interface ITcpSenderService
{
    void SendTcp<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task SendTcpAsync<T>(IPEndPoint remoteEndPoint, T paylaod);
}