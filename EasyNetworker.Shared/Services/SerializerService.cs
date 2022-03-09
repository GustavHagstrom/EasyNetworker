using EasyNetworker.Shared.Abstractions;
using EasyNetworker.Shared.Utilities;
using System.Text;
using System.Text.Json;

namespace EasyNetworker.Shared.Services;
public class SerializerService : ISerializerService
{
    public byte[] SerializePayload<T>(T payload)
    {
        int id = Mappings.Instance.GetPayloadId<T>();
        var jsonData = JsonSerializer.Serialize(payload);
        return BitConverter.GetBytes(id).Concat(Encoding.UTF8.GetBytes(jsonData)).ToArray();
    }
    public object DeserializeReceivedBytes(byte[] receivedBytes)
    {
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray(), 0);
        var jsonString = Encoding.UTF8.GetString(receivedBytes.Skip(4).ToArray());
        return JsonSerializer.Deserialize(jsonString, Mappings.Instance.GetPayloadType(id))!;
    }
}
