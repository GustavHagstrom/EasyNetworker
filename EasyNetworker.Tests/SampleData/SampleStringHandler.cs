using EasyNetworker.Abstractions;
using System;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandler : IPacketHandler<string>
{
    public void Handle(string packet)
    {
        Console.WriteLine(packet);
    }
}
