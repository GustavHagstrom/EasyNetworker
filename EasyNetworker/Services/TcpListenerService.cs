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
        while (cancellationToken?.IsCancellationRequested == false)
        {
            var client = await Client!.AcceptTcpClientAsync();
            ReceiveAndInvokeHandler(client);
        }
        Client?.Stop();
    }
    private void ReceiveAndInvokeHandler(TcpClient client)
    {
        var stream = client.GetStream();
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        int id = BitConverter.ToInt32(buffer.Take(4).ToArray());
        object receivedObject = serializerService.DeserializeReceivedBytes(buffer);
        handlerInvokerService.Invoke(receivedObject, id);
    }
    private void RestartListener(IPEndPoint localEndPoint)
    {
        Client?.Stop();
        Client = new TcpListener(localEndPoint);
        Client.Start();
    }
}
