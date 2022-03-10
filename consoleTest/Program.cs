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
        var address = IPAddress.Parse("127.0.0.1");
        var localEndPoint = new IPEndPoint(address, 50001);
        var remoteEndPoint = new IPEndPoint(address, 50001);
        networkerClient.StartContinuousUdpReceiving(localEndPoint);
        while (true)
        {
            //networkerClient.ReceiveUdpOnce(localEndPoint);
            packet.MyString = Console.ReadKey().KeyChar.ToString();
            //networkerClient.SendUdp(remoteEndPoint, packet);
        }
    }
}