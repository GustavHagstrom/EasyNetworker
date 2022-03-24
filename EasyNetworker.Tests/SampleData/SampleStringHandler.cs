using EasyNetworker.Abstractions;
using System;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandler : IPacketHandler<string>
{
    public SampleStringHandler(StringQueueReceiver stringQueueReceiver)
    {
        StringQueueReceiver = stringQueueReceiver;
    }

    public StringQueueReceiver StringQueueReceiver { get; }

    public void Handle(string packet)
    {
        StringQueueReceiver.StringQueue.Enqueue(packet);
    }
}
