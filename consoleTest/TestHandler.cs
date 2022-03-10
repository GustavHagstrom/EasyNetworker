﻿using EasyNetworker.Abstractions;

namespace consoleTest;
public class TestHandler : IPacketHandler<TestPacket>
{
    public void Handle(TestPacket packet)
    {
        Console.Write(packet.MyString);
    }
}
