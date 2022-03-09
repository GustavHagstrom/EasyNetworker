using System.Net;
using System.Net.Sockets;

namespace EasyNetworker;
public class UserClient
{
    public UserClient()
    {

    }
    public void SendTcp<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        using (var tcp = new TcpClient())
        {
            tcp.Connect(remoteEndPoint);
        }
    }
    public void SendUdp<T>(IPEndPoint remoteEndPoint, T paylaod)
    {
        using (var udp = new UdpClient())
        {

        }
    }
}
