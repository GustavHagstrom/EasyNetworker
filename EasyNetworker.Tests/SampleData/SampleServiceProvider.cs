using Microsoft.Extensions.DependencyInjection;
using EasyNetworker.DependencyInjection;

namespace EasyNetworker.Tests.SampleData;
public static class SampleServiceProvider
{
    public static ServiceProvider ServiceProvider { get; } = new ServiceCollection()
        .AddEasyNetworker()
        .RegisterPacketHandler<SampleStringHandler, string>()
        .AddSingleton<StringQueueReceiver>()
        .BuildServiceProvider();
}
