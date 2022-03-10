using System.Net;

namespace EasyNetworker.Abstractions;

public interface ITcpSenderService
{
    void Send<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod);
}