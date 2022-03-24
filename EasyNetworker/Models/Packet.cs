using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Models;
public class Packet
{
    public string Id { get; set; } = string.Empty;  
    public byte[] Payload { get; set; } = Array.Empty<byte>();
}
