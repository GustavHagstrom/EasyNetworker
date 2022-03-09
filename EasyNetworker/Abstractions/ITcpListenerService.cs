using System.Net;

namespace EasyNetworker.Abstractions;

public interface ITcpListenerService
{
    void ReceiveOnce(IPEndPoint localEndPoint);
    Task StartContinuousReceivingAsync(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null);
}