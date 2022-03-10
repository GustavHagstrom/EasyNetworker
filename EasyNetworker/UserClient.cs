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
    public void Send<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        udpSenderService.Send(remoteEndPoint, paylaod);
    }
    public async Task SendAsync<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        await udpSenderService.SendAsync(remoteEndPoint, paylaod);
    }
}
