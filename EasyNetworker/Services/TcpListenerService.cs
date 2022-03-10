using EasyNetworker.Abstractions;
using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Services;
public class TcpListenerService : ITcpListenerService
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly ISerializerService serializerService;

    private TcpListener? Client { get; set; }
    public TcpListenerService(IHandlerInvokerService handlerInvokerService, ISerializerService serializerService)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.serializerService = serializerService;
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
        List<byte> receivedBytes = new();
        receivedBytes.AddRange(FillBuffer(client));
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray());
        int length = BitConverter.ToInt32(receivedBytes.Skip(4).Take(4).ToArray());
        while (receivedBytes.Count < length)
        {
            receivedBytes.AddRange(FillBuffer(client));
        }
        object receivedObject = serializerService.DeserializeReceivedBytes(receivedBytes.ToArray());
        handlerInvokerService.Invoke(receivedObject, id);
        client.Dispose();
    }
    private byte[] FillBuffer(TcpClient client)
    {
        var stream = client.GetStream();
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
