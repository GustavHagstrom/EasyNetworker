using EasyNetworker.Models;

namespace EasyNetworker.Abstractions;

public interface IHandlerInvokerService
{
    void Invoke(BasePacket basePacket);
}