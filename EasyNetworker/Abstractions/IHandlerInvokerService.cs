namespace EasyNetworker.Abstractions;

public interface IHandlerInvokerService
{
    void Invoke(object payload, int id);
}