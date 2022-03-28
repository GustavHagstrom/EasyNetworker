using EasyNetworker.Abstractions;
using System;
using System.Net;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandler : IPayloadHandler<string>
{
    public SampleStringHandler(StringQueueReceiver stringQueueReceiver)
    {
        StringQueueReceiver = stringQueueReceiver;
    }

    public StringQueueReceiver StringQueueReceiver { get; }

    public void Handle(string payload, EndPoint senderEndPoint)
    {
        StringQueueReceiver.StringQueue.Enqueue(payload);
    }
}
