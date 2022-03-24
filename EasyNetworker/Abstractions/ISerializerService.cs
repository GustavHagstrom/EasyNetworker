using EasyNetworker.Models;

namespace EasyNetworker.Abstractions;

public interface ISerializerService
{
    T Deserialize<T>(byte[] dataBytes);
    object? Deserialize(byte[] dataBytes, Type returnType);
    byte[] Serialize<T>(T data);
}