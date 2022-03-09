namespace EasyNetworker.Shared.Abstractions;

public interface ISerializerService
{
    object DeserializeReceivedBytes(byte[] receivedBytes);
    byte[] SerializePayload<T>(T payload);
}