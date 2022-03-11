using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest;
public class FilePacket
{
    public byte[] Bytes { get; set; } = new byte[0];
    public string Name { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}
