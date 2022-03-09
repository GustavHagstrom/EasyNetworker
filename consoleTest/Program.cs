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

    public ConsoleApp(ITcpListenerService tcpListenerService1, ITcpListenerService tcpListenerService2, ITcpSenderService tcpSenderService)
    {
        //this.handlerInvokerService = handlerInvokerService;
        //this.serializerService = serializerService;
        this.tcpListenerService1 = tcpListenerService1;
        this.tcpListenerService2 = tcpListenerService2;
        this.tcpSenderService = tcpSenderService;
    }
    public void Run()
    {
        //var endPoint = new IPEndPoint(new IPAddress()"", 5050);
        tcpListenerService1.StartContinuousReceivingAsync(endPoint);
        tcpSenderService.SendTcp(endPoint, packet);
        Console.ReadKey();
        //handlerInvokerService.Invoke(packet, 1);
        //var bytes = serializerService.SerializePayload(packet);
        //var obj = serializerService.DeserializeReceivedBytes(bytes);
    }
}