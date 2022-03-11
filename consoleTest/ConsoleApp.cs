using consoleTest;
using EasyNetworker.Abstractions;
using System.Net;

public class ConsoleApp
{
    private readonly TestPacket packet = new();
    private readonly INetworkerClient networkerClient;

    public ConsoleApp(INetworkerClient networkerClient)
    {
        this.networkerClient = networkerClient;
    }
    public void Run()
    {
        //var address = IPAddress.Any;// .Parse("192.168.0.109");
        var localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.131"), 60000);
        var remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.109"), 60000);
        networkerClient.StartContinuousUdpReceiving(localEndPoint);
        while (true)
        {
            //networkerClient.ReceiveUdpOnce(localEndPoint);
            packet.MyString = Console.ReadKey().KeyChar.ToString()!;
            //networkerClient.SendTcp(remoteEndPoint, packet);
            networkerClient.SendUdpAsync(remoteEndPoint, packet);
        }
    }
}
