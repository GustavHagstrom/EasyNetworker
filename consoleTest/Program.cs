using Microsoft.Extensions.DependencyInjection;
using consoleTest;
using EasyNetworker.Shared.Abstractions;
using EasyNetworker.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddSingleton<ConsoleApp>()
            .AddEasyNetworker()
            .RegisterPacketHandler<TestHandler, TestPacket>()
            .BuildServiceProvider();
serviceProvider.GetService<ConsoleApp>()?.Run();



public class ConsoleApp
{
    private readonly IHandlerInvokerService handlerInvokerService;
    private readonly ISerializerService serializerService;
    private readonly TestPacket packet = new();
    public ConsoleApp(IHandlerInvokerService handlerInvokerService, ISerializerService serializerService)
    {
        this.handlerInvokerService = handlerInvokerService;
        this.serializerService = serializerService;
    }
    public void Run()
    {
        handlerInvokerService.Invoke(packet, 1);
        var bytes = serializerService.SerializePayload(packet);
        var obj = serializerService.DeserializeReceivedBytes(bytes);
    }
}