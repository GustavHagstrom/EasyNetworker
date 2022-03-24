using EasyNetworker.Models;

namespace EasyNetworker.Abstractions;

public interface IPacketGeneratorService
{
    Packet Generate(byte[] receivedBytes);
    Packet Generate<T>(T payload);
    byte[] GenerateAsByteArray<T>(T payload);
}