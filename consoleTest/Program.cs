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
    //private readonly IHandlerInvokerService handlerInvokerService;
    //private readonly ISerializerService serializerService;
    private readonly TestPacket packet = new();
    private readonly ITcpListenerService tcpListenerService1;
    private readonly ITcpListenerService tcpListenerService2;
    private readonly ITcpSenderService tcpSenderService;
    private readonly IUdpListenerService udpListenerService;
    private readonly IUdpSenderService udpSenderService;

    public ConsoleApp(ITcpListenerService tcpListenerService1, ITcpListenerService tcpListenerService2, ITcpSenderService tcpSenderService, IUdpListenerService udpListenerService, IUdpSenderService udpSenderService)
    {
        //this.handlerInvokerService = handlerInvokerService;
        //this.serializerService = serializerService;
        this.tcpListenerService1 = tcpListenerService1;
        this.tcpListenerService2 = tcpListenerService2;
        this.tcpSenderService = tcpSenderService;
        this.udpListenerService = udpListenerService;
        this.udpSenderService = udpSenderService;
    }
    public void Run()
    {
        var address = IPAddress.Any;
        //var address = IPAddress.Parse("127.0.0.1");
        var endPoint = new IPEndPoint(address, 50000);
        //while (true)
        //{
        //    //Console.ReadKey();
        //    tcpListenerService1.ReceiveOnce(endPoint);
        //    //tcpSenderService.SendTcp(endPoint, packet);
        //}
        tcpListenerService1.StartContinuousReceivingAsync(endPoint, null);
        //tcpSenderService.SendTcp(endPoint, packet);
        while (true)
        {
            Console.ReadKey();
        }
        //Console.ReadKey();
        //handlerInvokerService.Invoke(packet, 1);
        //var bytes = serializerService.SerializePayload(packet);
        //var obj = serializerService.DeserializeReceivedBytes(bytes);
    }
}