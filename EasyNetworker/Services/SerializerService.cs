using EasyNetworker.Abstractions;
using EasyNetworker.Exceptions;
using EasyNetworker.Models;
using EasyNetworker.Utilities;
using System.Text;
using System.Text.Json;

namespace EasyNetworker.Services;
public class SerializerService : ISerializerService
{
    public byte[] SerializePayload<T>(T payload)
    {
        string id = Mappings.Instance.GetPayloadId<T>();
        var basePacket = new BasePacket { Id = id,  PayloadAsJson = JsonSerializer.Serialize(payload) };
        var jsonBasePacket = JsonSerializer.Serialize(basePacket);

        return Encoding.UTF8.GetBytes(jsonBasePacket);
    }
    public BasePacket DeserializeReceivedBytes(byte[] receivedBytes)
    {
        var jsonBasePacket = Encoding.UTF8.GetString(receivedBytes);
        return JsonSerializer.Deserialize<BasePacket>(jsonBasePacket)!;
    }
}
