using System.Net;

namespace EasyNetworker.Abstractions;

public interface IUdpListenerService
{
    Task StartContinuousReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null);
    void ReceiveOnce(IPEndPoint localEndPoint);
}