using NUnit.Framework;
using System;
using EasyNetworker.Tests.SampleData;
using EasyNetworker.Abstractions;

namespace EasyNetworker.Tests.Services;
public class TcpTests
{
    public IServiceProvider Provider { get; set; } = SampleServiceProvider.ServiceProvider;
    public ITcpSenderService? Sender { get; set; }
    public ITcpListenerService? Receiver { get; set; }
    [SetUp]
    public void Setup()
    {
        Sender = (ITcpSenderService)Provider.GetService(typeof(ITcpSenderService))!;
        Receiver = (ITcpListenerService)Provider.GetService(typeof(ITcpListenerService))!;
    }

    [Test]
    public void ReceiveOnceTest()
    {
        //Arrange


        //Act


        //Assert

    }
}
