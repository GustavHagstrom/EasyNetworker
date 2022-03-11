using EasyNetworker.Abstractions;
using System.Diagnostics;

namespace consoleTest;
public class FilepacketHandler : IPacketHandler<FilePacket>
{
    public void Handle(FilePacket packet)
    {
        string filename = packet.Name + packet.FileExtension;
        int count = 1;
        while (File.Exists(filename+packet.FileExtension))
        {
            filename = $"{packet.Name} ({count++})";
        }
        packet.Name = filename;

        File.WriteAllBytes(packet.Name, packet.Bytes);
        new Process
        {
            StartInfo = new ProcessStartInfo(packet.Name+packet.FileExtension)
            {
                UseShellExecute = true,
            }
        }.Start();
    }
}
