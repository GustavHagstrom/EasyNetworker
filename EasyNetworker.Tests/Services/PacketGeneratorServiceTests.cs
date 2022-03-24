using EasyNetworker.Services;
using EasyNetworker.Tests.SampleData;
using EasyNetworker.Utilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EasyNetworker.Tests.Services;
public class PacketGeneratorServiceTests
{
    private ServiceProvider provider = SampleServiceProvider.ServiceProvider;

    [Test]
    public void GeneratePacketTest()
    {
        //Arrange
        var serializer = new SerializerService();
        var generator = new PacketGeneratorService(serializer);
        var expectedId = typeof(SampleStringHandler).FullName;
        var payload = "Hello";
        var expectedPayload = serializer.Serialize(payload);
        
        //Act
        var packet = generator.Generate(payload);

        //Assert
        Assert.That(packet.Id, Is.EqualTo(expectedId));
        Assert.That(packet.Payload, Is.EqualTo(expectedPayload));
    }
}
