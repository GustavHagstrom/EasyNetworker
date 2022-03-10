using EasyNetworker.Abstractions;
using EasyNetworker.Services;
using EasyNetworker.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasyNetworker.DependencyInjection;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEasyNetworker(this IServiceCollection services)
    {
        services.TryAddTransient<ServiceFactory>(p => p.GetRequiredService);
        services.AddTransient<IHandlerInvokerService, HandlerInvokerService>();
        services.AddTransient<ISerializerService, SerializerService>();
        services.AddTransient<ITcpListenerService, TcpListenerService>();
        services.AddTransient<ITcpSenderService, TcpSenderService>();
        services.AddTransient<IUdpListenerService, UdpListenerService>();
        services.AddTransient<IUdpSenderService, UdpSenderService>();
        services.AddTransient<INetworkerClient, NetworkerClient>();
        return services;
    }
    public static IServiceCollection RegisterPacketHandler<THandler, Payload>(this IServiceCollection services) where THandler : IPacketHandler<Payload>
    {
        Mappings.Instance.Register<THandler, Payload>();
        services.AddTransient(typeof(THandler));
        services.AddTransient(typeof(Payload));
        return services;
    }
}
