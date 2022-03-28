using System.Net;

namespace EasyNetworker.Abstractions;
public interface IPayloadHandler<Payload>
{
    void Handle(Payload payload, EndPoint senderEndPoint);
}
