using EasyNetworker.Abstractions;
using EasyNetworker.Exceptions;
using EasyNetworker.Models;
using EasyNetworker.Utilities;
using System.Text.Json;

namespace EasyNetworker.Services;
public class HandlerInvokerService : IHandlerInvokerService
{
    private readonly ServiceFactory serviceFactory;
    private readonly ISerializerService serializerService;
    private const string HandleMethodName = "Handle";

    public HandlerInvokerService(ServiceFactory serviceFactory, ISerializerService serializerService)
    {
        this.serviceFactory = serviceFactory;
        this.serializerService = serializerService;
    }
    public void Invoke(Packet packet)
    {
        var payload = serializerService.Deserialize(packet.Payload, Mappings.Instance.GetPayloadType(packet.Id)); 
        var description = Mappings.Instance.GetPacketsDescription(packet.Id);
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
