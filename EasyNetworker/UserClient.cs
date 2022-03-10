using EasyNetworker.Abstractions;
using System.Net;

namespace EasyNetworker;
public class UserClient : ITcpSenderService, IUdpSenderService
{
    private readonly ITcpSenderService tcpSenderService;
    private readonly IUdpSenderService udpSenderService;

    public UserClient(ITcpSenderService tcpSenderService, IUdpSenderService udpSenderService)
    {
        this.tcpSenderService = tcpSenderService;
        this.udpSenderService = udpSenderService;
    }

    public void Send<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        tcpSenderService.Send(remoteEndPoint, paylaod);
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await tcpSenderService.SendAsync(remoteEndPoint, paylaod);
    }
    public void SendUdp<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        udpSenderService.SendUdp(remoteEndPoint, paylaod);
    }
    public async Task SendUdpAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await udpSenderService.SendUdpAsync(remoteEndPoint, paylaod);
    }
}
