using EasyNetworker.Abstractions;
using System.Net;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandlerTwo : IPayloadHandler<string>
{
    public SampleStringHandlerTwo(StringQueueReceiver stringQueueReceiver)
    {
        StringQueueReceiver = stringQueueReceiver;
    }

    public StringQueueReceiver StringQueueReceiver { get; }


    public void Handle(string payload, EndPoint senderEndPoint)
    {
        StringQueueReceiver.StringQueue.Enqueue(payload);
    }
}
