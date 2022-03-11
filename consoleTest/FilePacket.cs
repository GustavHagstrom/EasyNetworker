using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest;
public class FilePacket
{
    public byte[] FileBytes { get; set; } = new byte[0];
    public string FileName { get; set; } = string.Empty;
}
