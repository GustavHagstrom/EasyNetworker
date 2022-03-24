using EasyNetworker.Abstractions;
using EasyNetworker.Models;
using EasyNetworker.Utilities;

namespace EasyNetworker.Services;
public class PacketGeneratorService : IPacketGeneratorService
{
    private readonly ISerializerService serializerService;

    public PacketGeneratorService(ISerializerService serializerService)
    {
        this.serializerService = serializerService;
    }
    public Packet Generate<T>(T payload)
    {
        string id = Mappings.Instance.GetPayloadId<T>();
        return new Packet { Id = id, Payload = serializerService.Serialize(payload) };
    }
    public Packet Generate(byte[] receivedBytes)
    {
        return serializerService.Deserialize<Packet>(receivedBytes);
    }
    public byte[] GenerateAsByteArray<T>(T payload)
    {
        var packet = Generate(payload);
        return serializerService.Serialize(packet);
    }
}
