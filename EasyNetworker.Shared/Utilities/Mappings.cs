using EasyNetworker.Shared.Abstractions;
using EasyNetworker.Shared.Models;

namespace EasyNetworker.Shared.Utilities;
internal class Mappings
{
    public static Mappings Instance { get; } = new();
    private Mappings() { }
    private int Count { get; set; } = 0;
    private Dictionary<int, HandlePayoadDescription> DescriptionMap { get; } = new();
    private Dictionary<Type, int> PayloadIdMap { get; } = new();
    private Dictionary<int, Type> IdPayloadMap { get; } = new();

    public void Register<THandler, Payload>() where THandler : IPacketHandler<Payload>
    {
        Count += 1;
        DescriptionMap.Add(Count, new() {PayloadType = typeof(Payload), HandlerType = typeof(THandler) });
        PayloadIdMap.Add(typeof(Payload), Count);
        IdPayloadMap.Add(Count, typeof(Payload));
    }
    public HandlePayoadDescription GetPacketsDescription(int packetId)
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
