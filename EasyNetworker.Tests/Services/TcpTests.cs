using NUnit.Framework;
using System;
using EasyNetworker.Tests.SampleData;
using EasyNetworker.Abstractions;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace EasyNetworker.Tests.Services;
public class TcpTests
{
    private ServiceProvider provider = SampleServiceProvider.ServiceProvider;

    [Test]
    public void SendAndReceiveOnce()
    {
        //Arrange
        var Sender = (ITcpSenderService)provider.GetService(typeof(ITcpSenderService))!;
        var Receiver = (ITcpListenerService)provider.GetService(typeof(ITcpListenerService))!;
        string expectedPayload = "Hello";
        IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 1);
        var stringQueueReceiver = (StringQueueReceiver)provider.GetService(typeof(StringQueueReceiver))!;

        //Act
        Task.Run(() => Receiver!.ReceiveOnce(endPoint));
        Sender!.Send(endPoint, expectedPayload);
        Thread.Sleep(100);
        string actualPayload = stringQueueReceiver.StringQueue.Dequeue();

        //Assert
        Assert.That(actualPayload, Is.EqualTo(expectedPayload));
    }
    [Test]
    public void SendAndReceiveContinuously()
    {
        //Arrange
        var Sender = (ITcpSenderService)provider.GetService(typeof(ITcpSenderService))!;
        var Receiver = (ITcpListenerService)provider.GetService(typeof(ITcpListenerService))!;
        string expectedPayload = "Hello";
        IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 1);
        var stringQueueReceiver = (StringQueueReceiver)provider.GetService(typeof(StringQueueReceiver))!;

        //Act
        Receiver!.StartContinuousReceivingAsync(endPoint);
        Sender!.Send(endPoint, expectedPayload);
        Thread.Sleep(100);
        string actualPayload = stringQueueReceiver.StringQueue.Dequeue();

        //Assert
        Assert.That(actualPayload, Is.EqualTo(expectedPayload));
    }
}
