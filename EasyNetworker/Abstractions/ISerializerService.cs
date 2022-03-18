using EasyNetworker.Models;

namespace EasyNetworker.Abstractions;

public interface ISerializerService
{
    BasePacket DeserializeReceivedBytes(byte[] receivedBytes);
    byte[] SerializePayload<T>(T payload);
}