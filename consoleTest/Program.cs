using Microsoft.Extensions.DependencyInjection;
using consoleTest;
using EasyNetworker.DependencyInjection;
using EasyNetworker.Abstractions;
using System.Net;

var serviceProvider = new ServiceCollection()
            .AddSingleton<ConsoleApp>()
            .AddEasyNetworker()
            .RegisterPacketHandler<TestHandler, TestPacket>()
            .BuildServiceProvider();
serviceProvider.GetService<ConsoleApp>()?.Run();



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
        var localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.131"), 50000);
        var remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.131"), 50000);
        networkerClient.StartContinuousTcpReceiving(localEndPoint);
        while (true)
        {
            //networkerClient.ReceiveUdpOnce(localEndPoint);
            packet.MyString = Console.ReadKey().KeyChar.ToString()!;
            //networkerClient.SendTcp(remoteEndPoint, packet);
            networkerClient.SendTcpAsync(remoteEndPoint, packet);
        }
    }
}