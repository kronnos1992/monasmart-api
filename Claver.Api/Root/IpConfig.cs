using System.Net;
using System.Net.Sockets;

namespace Claver.Api.Root
{
    public static class IpConfig
    {
        public static string LocalIPAddress() {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)) {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                if (endPoint != null) {
                    return endPoint.Address.ToString();
                } else {
                    return "https://localhost:5000";
                }
            }
        }
    }
}