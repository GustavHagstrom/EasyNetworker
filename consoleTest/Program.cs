using Microsoft.Extensions.DependencyInjection;
using consoleTest;
using EasyNetworker.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddSingleton<ConsoleApp>()
            .AddEasyNetworker()
            .RegisterPacketHandler<TestHandler, TestPacket>()
            .BuildServiceProvider();
serviceProvider.GetService<ConsoleApp>()?.Run();
