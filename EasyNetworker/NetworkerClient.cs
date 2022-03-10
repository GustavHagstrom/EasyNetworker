using EasyNetworker.Abstractions;
using System.Net;

namespace EasyNetworker;
public class NetworkerClient : INetworkerClient
{
    private readonly ITcpSenderService tcpSenderService;
    private readonly IUdpSenderService udpSenderService;
    private readonly ITcpListenerService tcpListenerService;
    private readonly IUdpListenerService udpListenerService;

    public NetworkerClient(ITcpSenderService tcpSenderService, IUdpSenderService udpSenderService, ITcpListenerService tcpListenerService, IUdpListenerService udpListenerService)
    {
        this.tcpSenderService = tcpSenderService;
        this.udpSenderService = udpSenderService;
        this.tcpListenerService = tcpListenerService;
        this.udpListenerService = udpListenerService;
    }

    public async Task StartContinuousTcpReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        await tcpListenerService.StartContinuousReceivingAsync(localEndPoint, cancellationToken);
    }
    public void ReceiveTcpOnce(IPEndPoint localEndPoint)
    {
        tcpListenerService.ReceiveOnce(localEndPoint);
    }
    public async Task StartContinuousUdpReceiving(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        await udpListenerService.StartContinuousReceiving(localEndPoint, cancellationToken);
    }
    public void ReceiveUdpOnce(IPEndPoint localEndPoint)
    {
        udpListenerService.ReceiveOnce(localEndPoint);
    }
    public void SendTcp<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        tcpSenderService.Send(remoteEndPoint, paylaod);
    }
    public async Task SendTcpAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await tcpSenderService.SendAsync(remoteEndPoint, paylaod);
    }
    public void SendUdp<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        udpSenderService.Send(remoteEndPoint, paylaod);
    }
    public async Task SendUdpAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await udpSenderService.SendAsync(remoteEndPoint, paylaod);
    }
}
