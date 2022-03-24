using EasyNetworker.Abstractions;
using EasyNetworker.Models;

namespace EasyNetworker.Utilities;
public class Mappings
{
    public static Mappings Instance { get; } = new();
    private Mappings() { }
    private Dictionary<string, HandlePayloadDescription> DescriptionMap { get; } = new();
    private Dictionary<Type, string> PayloadIdMap { get; } = new();
    private Dictionary<string, Type> IdPayloadMap { get; } = new();
    public void Register<THandler, Payload>() where THandler : IPacketHandler<Payload>
    {
        string id = typeof(THandler).FullName!;
        DescriptionMap.Add(id, new() {PayloadType = typeof(Payload), HandlerType = typeof(THandler) });
        PayloadIdMap.Add(typeof(Payload), id);
        IdPayloadMap.Add(id, typeof(Payload));
    }
    public HandlePayloadDescription GetPacketsDescription(string packetId)
    {
        return DescriptionMap[packetId];
    }
    public string GetPayloadId<Payload>()
    {
        return PayloadIdMap[typeof(Payload)];
    }
    public Type GetPayloadType(string id)
    {
        return IdPayloadMap[id];
    }
}
