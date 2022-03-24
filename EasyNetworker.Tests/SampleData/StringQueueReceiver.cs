using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Tests.SampleData;
public class StringQueueReceiver
{
    public Queue<string> StringQueue { get; set; } = new();
}
