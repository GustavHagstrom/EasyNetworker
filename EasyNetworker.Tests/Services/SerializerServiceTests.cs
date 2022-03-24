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
        var expectedPaylaod = "Hello";
        var serializerService = new SerializerService();
        byte[] buffer = new byte[8192];

        //Act
        var seriBytes = serializerService.Serialize(expectedPaylaod);
        seriBytes.CopyTo(buffer, 0);
        var actualPayload = serializerService.Deserialize<string>(buffer);
        

        //Assert
        Assert.That(actualPayload, Is.EqualTo(expectedPaylaod));
    }
}
