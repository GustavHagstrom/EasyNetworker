using EasyNetworker.Abstractions;
using EasyNetworker.Exceptions;
using EasyNetworker.Models;
using EasyNetworker.Utilities;
using System.Text.Json;

namespace EasyNetworker.Services;
public class HandlerInvokerService : IHandlerInvokerService
{
    private readonly ServiceFactory serviceFactory;
    private const string HandleMethodName = "Handle";

    public HandlerInvokerService(ServiceFactory serviceFactory)
    {
        this.serviceFactory = serviceFactory;
    }
    public void Invoke(BasePacket basePacket)
    {
        var payload = JsonSerializer.Deserialize(basePacket.PayloadAsJson, Mappings.Instance.GetPayloadType(basePacket.Id));
        var description = Mappings.Instance.GetPacketsDescription(basePacket.Id);
        var handler = serviceFactory(description.HandlerType!);
        try
        {
            handler?.GetType().GetMethod(HandleMethodName)?.Invoke(handler, new object[] { payload! });
        }
        catch (Exception e)
        {
            throw new PacketLossException(e);
        }
        
    }
}
