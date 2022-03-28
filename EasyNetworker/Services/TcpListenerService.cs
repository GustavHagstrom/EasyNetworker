using EasyNetworker.Abstractions;
using EasyNetworker.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EasyNetworker.Services;
public class TcpListenerService : ITcpListenerService
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly IPacketGeneratorService packetGeneratorService;

    private TcpListener? Client { get; set; }
    public TcpListenerService(IHandlerInvokerService handlerInvokerService, IPacketGeneratorService packetGeneratorService)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.packetGeneratorService = packetGeneratorService;
    }
    public void ReceiveOnce(IPEndPoint localEndPoint)
    {
        RestartListener(localEndPoint);
        var client = Client!.AcceptTcpClient();
        ReceiveAndInvokeHandler(client);
        Client?.Stop();
    }
    public async Task StartContinuousReceivingAsync(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        RestartListener(localEndPoint);
        while (cancellationToken?.IsCancellationRequested == false || cancellationToken == null)
        {
            var client = await Client!.AcceptTcpClientAsync();
            ReceiveAndInvokeHandler(client);
        }
        Client?.Stop();
    }
    private void ReceiveAndInvokeHandler(TcpClient client)
    {
        byte[] receivedBytes = GetReceivedBytes(client);
        var packet = packetGeneratorService.Generate(receivedBytes);
        handlerInvokerService.Invoke(packet, client.Client.RemoteEndPoint!);
        client.Dispose();
    }
    private byte[] GetReceivedBytes(TcpClient client)
    {
        List<byte> receivedBytes = new();
        var stream = client.GetStream();
        receivedBytes.AddRange(FillBuffer(stream));
        while (stream.DataAvailable)
        {
            receivedBytes.AddRange(FillBuffer(stream));
        }
        return receivedBytes.ToArray();
    }
    private byte[] FillBuffer(NetworkStream stream)
    {
        byte[] buffer = new byte[8192];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }
    private void RestartListener(IPEndPoint localEndPoint)
    {
        Client?.Stop();
        Client = new TcpListener(localEndPoint);
        Client.Start();
    }
}
