namespace EasyNetworker.Abstractions;
public interface IPacketHandler<Payload>
{
    void Handle(Payload packet);
}
