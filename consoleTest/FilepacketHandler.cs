using EasyNetworker.Abstractions;
using System.Diagnostics;

namespace consoleTest;
public class FilepacketHandler : IPacketHandler<FilePacket>
{
    public void Handle(FilePacket packet)
    {
        int count = 1;
        while (File.Exists(packet.FullName))
        {
            packet.Name = $"{packet.Name} ({count++})";
        }

        File.WriteAllBytes(packet.FullName, packet.Bytes);
        new Process
        {
            StartInfo = new ProcessStartInfo(packet.FullName)
            {
                UseShellExecute = true,
            }
        }.Start();
    }
}
