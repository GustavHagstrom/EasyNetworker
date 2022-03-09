using EasyNetworker.Shared.Abstractions;
using EasyNetworker.Shared.Services;
using EasyNetworker.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasyNetworker.Shared.DependencyInjection;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEasyNetworker(this IServiceCollection services)
    {
        services.TryAddTransient<ServiceFactory>(p => p.GetRequiredService);
        services.AddTransient<IHandlerInvokerService, HandlerInvokerService>();
        services.AddTransient<ISerializerService, SerializerService>();
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