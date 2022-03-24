using EasyNetworker.Models;

namespace EasyNetworker.Abstractions;

public interface ISerializerService
{
    T Deserialize<T>(byte[] dataBytes);
    byte[] Serialize<T>(T data);
}