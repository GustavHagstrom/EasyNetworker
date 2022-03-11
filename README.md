# EasyNetworker

A simple to use TCP and UDP networking library.
Used with dependency injection using Micrsoft.Extensions.DependencyInjection.ServiceCollection

# Get started
* Create packethandlers inheriting IPacketHandler\<Payload\> interface
 ````csharp
public class MessagePacketHandler : IPacketHandler<MessagePacket>
{
    public void Handle(MessagePacket packet)
    {
        Console.WriteLine(packet.Message);
    }
}
````
* Add EasyNetworker to your ServiceCollection
 ````csharp
 Services.AddEasyNetworker();
````
* Register PacketHandler with associated payload in your ServiceCollection
 ````csharp
 Services.RegisterPacketHandler<MessagePacketHandler, MessagePacket>();
````
* Sending
````csharp
public class SampleClass
{
  
  private readonly IPEndPoint _sampleEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
  private readonly _networkerClient;
  
  public SampleClass(INetworkerClient networkerClient)
  {
    _networkerClient = networkerClient;
  }
  public async Task SendSampleTcpMessageAsync(string message)
  {
    await _networkerClient.SendTcpAsync(_sampleEndPoint, new MessagePacket{ Message = "Hello world!" });
  }
  public void SendSampleUdpMessage(string message)
  {
    _networkerClient.SendUdp(_sampleEndPoint, new MessagePacket{ Message = "Hello world!" });
  }
}
````
* Receiving
````csharp
public class SampleClass
{
  
  private readonly IPEndPoint _sampleEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
  private readonly _networkerClient;
  
  public SampleClass(INetworkerClient networkerClient)
  {
    _networkerClient = networkerClient;
  }
  public async Task StartContinuousTcpReceiving()
  {
    await _networkerClient.StartContinuousTcpReceiving(_sampleEndPoint);
  }
  public void ReceiveSampleUdpMessage()
  {
    _networkerClient.ReceiveUdpOnce(_sampleEndPoint);
  }
}
````
