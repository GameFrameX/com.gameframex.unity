using System.Net;
using System.Net.Sockets;

namespace GameFrameX
{
    public static partial class Utility
    {
        /// <summary>
        /// 网络相关的对象工具类
        /// </summary>
        public static class Net
        {
            public static (AddressFamily, string) GetIPv6Address(string host)
            {
                var addresses = Dns.GetHostAddresses(host);

                foreach (var ipAddress in addresses)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        return (AddressFamily.InterNetworkV6, ipAddress.ToString());
                    }
                }

                foreach (var ipAddress in addresses)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return (AddressFamily.InterNetwork, ipAddress.ToString());
                    }
                }

                return (AddressFamily.InterNetwork, host);
            }
        }
    }
}