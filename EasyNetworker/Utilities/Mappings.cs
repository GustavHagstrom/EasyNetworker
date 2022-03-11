using EasyNetworker.Abstractions;
using EasyNetworker.Models;

namespace EasyNetworker.Utilities;
public class Mappings
{
    public static Mappings Instance { get; } = new();
    private Mappings() { }
    private int Count { get; set; } = 0;
    private Dictionary<int, HandlePayloadDescription> DescriptionMap { get; } = new();
    private Dictionary<Type, int> PayloadIdMap { get; } = new();
    private Dictionary<int, Type> IdPayloadMap { get; } = new();
    public void Register<THandler, Payload>() where THandler : IPacketHandler<Payload>
    {
        Count += 1;
        DescriptionMap.Add(Count, new() {PayloadType = typeof(Payload), HandlerType = typeof(THandler) });
        PayloadIdMap.Add(typeof(Payload), Count);
        IdPayloadMap.Add(Count, typeof(Payload));
    }
    public HandlePayloadDescription GetPacketsDescription(int packetId)
    {
        return DescriptionMap[packetId];
    }
    public int GetPayloadId<Payload>()
    {
        return PayloadIdMap[typeof(Payload)];
    }
    public Type GetPayloadType(int id)
    {
        return IdPayloadMap[id];
    }
}
