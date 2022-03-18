using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetworker.DependencyInjection;

namespace EasyNetworker.Tests.SampleData;
public static class SampleServiceProvider
{
    public static ServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddEasyNetworker()
            .BuildServiceProvider();
}
