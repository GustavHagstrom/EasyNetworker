using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Shared.Exceptions;
public class PacketLossException : Exception
{
    public PacketLossException(Exception e) : base(e.Message, e)
    {
    }
    public PacketLossException(string message) : base(message)
    {

    }
}
