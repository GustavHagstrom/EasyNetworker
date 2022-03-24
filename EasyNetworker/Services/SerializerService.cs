using EasyNetworker.Abstractions;
using System.Text.Json;

namespace EasyNetworker.Services;
public class SerializerService : ISerializerService
{
    private const int EndValue = 1;
    public byte[] Serialize<T>(T payload)
    {
        return JsonSerializer.SerializeToUtf8Bytes(payload).Concat(BitConverter.GetBytes(EndValue)).ToArray();
    }
    public T Deserialize<T>(byte[] dataBytes)
    {
        var index = Array.LastIndexOf(dataBytes, Convert.ToByte(EndValue));
        var usedBytes = dataBytes.Take(index).ToArray();
        return JsonSerializer.Deserialize<T>(usedBytes)!;
    }
    public object? Deserialize(byte[] dataBytes, Type returnType)
    {
        var index = Array.LastIndexOf(dataBytes, Convert.ToByte(EndValue));
        var usedBytes = dataBytes.Take(index).ToArray();
        return JsonSerializer.Deserialize(usedBytes, returnType);
    }
}
