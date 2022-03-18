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
    private readonly ISerializerService serializerService;
    private readonly ILogger<TcpListenerService> logger;

    private TcpListener? Client { get; set; }
    public TcpListenerService(IHandlerInvokerService handlerInvokerService, ISerializerService serializerService, ILogger<TcpListenerService> logger)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.serializerService = serializerService;
        this.logger = logger;
    }
    public void ReceiveOnce(IPEndPoint localEndPoint)
    {
        RestartListener(localEndPoint);
        logger.LogInformation($"Starting to listen for incoming Tcp data");
        var client = Client!.AcceptTcpClient();
        logger.LogInformation($"Incoming Tcp connection from remote endpoint: {client.Client.RemoteEndPoint}");
        ReceiveAndInvokeHandler(client);
        Client?.Stop();
    }
    public async Task StartContinuousReceivingAsync(IPEndPoint localEndPoint, CancellationToken? cancellationToken = null)
    {
        RestartListener(localEndPoint);
        while (cancellationToken?.IsCancellationRequested == false || cancellationToken == null)
        {
            logger.LogInformation($"Starting to listen for incoming Tcp data");
            var client = await Client!.AcceptTcpClientAsync();
            logger.LogInformation($"Incoming Tcp connection from remote endpoint: {client.Client.RemoteEndPoint}");
            ReceiveAndInvokeHandler(client);
        }
        Client?.Stop();
    }
    private void ReceiveAndInvokeHandler(TcpClient client)
    {
        byte[] receivedBytes = GetReceivedBytes(client);
        var basePacket = serializerService.DeserializeReceivedBytes(receivedBytes);
        logger.LogInformation($"Data received with Tcp. PacketId: {basePacket.Id}. Passing to Handler...");
        handlerInvokerService.Invoke(basePacket);
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
