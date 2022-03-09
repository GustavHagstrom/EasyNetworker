namespace EasyNetworker.Shared.Abstractions;
public interface IPacketHandler<Payload>
{
    void Handle(Payload packet);
}
