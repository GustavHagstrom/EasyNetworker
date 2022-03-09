using EasyNetworker.Shared.Abstractions;

namespace consoleTest;
public class TestHandler : IPacketHandler<TestPacket>
{
    public void Handle(TestPacket packet)
    {
        Console.WriteLine(packet.MyString);
    }
}
