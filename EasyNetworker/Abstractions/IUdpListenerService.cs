using System.Net;

namespace EasyNetworker.Abstractions;

public interface IUdpListenerService
{
    void BeginContinuousReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null);
    void ReceiveOnce(IPEndPoint localEndPoint);
}