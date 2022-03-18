using EasyNetworker.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Tests.SampleData;
public class SampleStringHandlerTwo : IPacketHandler<string>
{
    public void Handle(string packet)
    {
        Console.WriteLine(packet);
    }
}
