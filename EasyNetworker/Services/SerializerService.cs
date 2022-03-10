using EasyNetworker.Abstractions;
using EasyNetworker.Utilities;
using System.Text;
using System.Text.Json;

namespace EasyNetworker.Services;
public class SerializerService : ISerializerService
{
    public byte[] SerializePayload<T>(T payload)
    {
        int id = Mappings.Instance.GetPayloadId<T>();
        var jsonData = JsonSerializer.Serialize(payload);
        byte[] idBytes = BitConverter.GetBytes(id);
        byte[] jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);
        int length = idBytes.Length + jsonDataBytes.Length + 4;
        byte[] lengthBytes = BitConverter.GetBytes(length);
        //length = BitConverter.ToInt32(lengthBytes, 0);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(idBytes);
        bytes.AddRange(lengthBytes);
        bytes.AddRange(jsonDataBytes);
        

        return bytes.ToArray();
    }
    public object DeserializeReceivedBytes(byte[] receivedBytes)
    {
        int id = BitConverter.ToInt32(receivedBytes.Take(4).ToArray(), 0);
        int length = BitConverter.ToInt32(receivedBytes.Skip(4).Take(4).ToArray(), 0);
        var jsonString = Encoding.UTF8.GetString(receivedBytes.Skip(8).Take(length-8).ToArray());
        return JsonSerializer.Deserialize(jsonString, Mappings.Instance.GetPayloadType(id))!;
    }
}
