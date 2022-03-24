using EasyNetworker.Abstractions;
using System.Text.Json;

namespace EasyNetworker.Services;
public class SerializerService : ISerializerService
{
    private const int EndValue = 1;
    public byte[] Serialize<T>(T payload)
    {
        //string id = Mappings.Instance.GetPayloadId<T>();
        //var basePacket = new Packet { Id = id,  PayloadAsJson = JsonSerializer.Serialize(payload) };
        return JsonSerializer.SerializeToUtf8Bytes(payload).Concat(BitConverter.GetBytes(EndValue)).ToArray();
    }
    //public Packet DeserializeReceivedBytes(byte[] receivedBytes)
    //{
    //    var index = Array.LastIndexOf(receivedBytes, Convert.ToByte(125));
    //    var span = receivedBytes.Take(index + 1).ToArray();
    //    return JsonSerializer.Deserialize<Packet>(span)!;
    //}

    public T Deserialize<T>(byte[] dataBytes)
    {
        var index = Array.LastIndexOf(dataBytes, Convert.ToByte(EndValue));
        var usedBytes = dataBytes.Take(index).ToArray();
        //return JsonSerializer.Deserialize<T>(usedBytes)!;
        return JsonSerializer.Deserialize<T>(usedBytes)!;
    }
}
