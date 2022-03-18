using EasyNetworker.Models;
using EasyNetworker.Services;
using EasyNetworker.Tests.SampleData;
using EasyNetworker.Utilities;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace EasyNetworker.Tests.Services;
public class SerializerServiceTests
{
    [SetUp]
    public void Setup()
    {
        Mappings.Instance.Register<SampleStringHandler, string>();
    }

    [Test]
    public void SerializeAndDeserializeTest()
    {
        //Arrange
        var expectedId = typeof(SampleStringHandler).FullName;
        var expectedPaylaod = "Hello";
        var serializerService = new SerializerService();

        //Act
        var seriBytes = serializerService.SerializePayload(expectedPaylaod);
        var basePacket = JsonSerializer.Deserialize<BasePacket>(seriBytes);
        var actualPayload = JsonSerializer.Deserialize(basePacket!.PayloadAsJson, Mappings.Instance.GetPayloadType(basePacket.Id));

        //Assert
        Assert.That(basePacket!.Id, Is.EqualTo(expectedId));
        Assert.That(actualPayload, Is.EqualTo(expectedPaylaod));
    }
}
