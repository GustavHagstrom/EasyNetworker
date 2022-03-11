using EasyNetworker.Abstractions;
using System.Diagnostics;

namespace consoleTest;
public class FilepacketHandler : IPacketHandler<FilePacket>
{
    public void Handle(FilePacket packet)
    {
        File.WriteAllBytes(packet.FileName, packet.FileBytes);
        new Process
        {
            StartInfo = new ProcessStartInfo(packet.FileName)
            {
                UseShellExecute = true,
            }
        }.Start();
    }
}
