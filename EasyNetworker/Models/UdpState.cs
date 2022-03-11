using System.Net;
using System.Net.Sockets;

namespace EasyNetworker.Models;
public class UdpState
{
    public UdpClient? Client { get; set; }
    public IPEndPoint? EndPoint { get; set; }
    public CancellationToken? CancelToken { get; set; }
}
