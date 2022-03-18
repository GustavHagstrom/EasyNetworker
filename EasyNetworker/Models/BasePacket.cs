using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Models;
public class BasePacket
{
    public string Id { get; set; } = string.Empty;  
    public string PayloadAsJson { get; set; } = string.Empty;
}
