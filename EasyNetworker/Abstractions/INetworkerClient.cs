using System.Net;

namespace EasyNetworker.Abstractions;

public interface INetworkerClient
{
    void ReceiveTcpOnce(IPEndPoint localEndPoint);
    void ReceiveUdpOnce(IPEndPoint localEndPoint);
    void SendTcp<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task SendTcpAsync<T>(IPEndPoint remoteEndPoint, T paylaod);
    void SendUdp<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task SendUdpAsync<T>(IPEndPoint remoteEndPoint, T paylaod);
    Task StartContinuousTcpReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null);
    Task StartContinuousUdpReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null);
}