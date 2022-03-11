using Microsoft.Extensions.DependencyInjection;
using consoleTest;
using EasyNetworker.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddSingleton<ConsoleApp>()
            .AddEasyNetworker()
            .RegisterPacketHandler<TestHandler, TestPacket>()
            .RegisterPacketHandler<FilepacketHandler, FilePacket>()
            .BuildServiceProvider();
serviceProvider.GetService<ConsoleApp>()?.Run();
