using EasyNetworker.Services;
using EasyNetworker.Tests.SampleData;
using EasyNetworker.Utilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EasyNetworker.Tests.Services;
public class SerializerServiceTests
{
    private ServiceProvider provider = SampleServiceProvider.ServiceProvider;
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
