using EasyNetworker.Models;
using System.Net;

namespace EasyNetworker.Abstractions;

public interface IHandlerInvokerService
{
    void Invoke(Packet packet, EndPoint senderEndPoint);
}