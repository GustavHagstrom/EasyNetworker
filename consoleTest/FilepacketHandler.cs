using EasyNetworker.Abstractions;
using System.Diagnostics;

namespace consoleTest;
public class FilepacketHandler : IPacketHandler<FilePacket>
{
    public void Handle(FilePacket packet)
    {
        int count = 1;
        string originalName = packet.Name;
        while (File.Exists(packet.FullName))
        {
            packet.Name = $"{originalName} ({count++})";
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
