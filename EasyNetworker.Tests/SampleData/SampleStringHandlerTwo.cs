using EasyNetworker.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandlerTwo : IPacketHandler<string>
{
    public SampleStringHandlerTwo(StringQueueReceiver stringQueueReceiver)
    {
        StringQueueReceiver = stringQueueReceiver;
    }

    public StringQueueReceiver StringQueueReceiver { get; }

    public void Handle(string packet)
    {
        StringQueueReceiver.StringQueue.Enqueue(packet);
    }
}
