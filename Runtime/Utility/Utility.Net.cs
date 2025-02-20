using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 网络相关的对象工具类
        /// </summary>
        [Preserve]
        public static class Net
        {
            /// <summary>
            /// 获取第一个可用的端口号
            /// </summary>
            /// <param name="startPort">起始端口号</param>
            /// <param name="maxPort">结束端口号</param>
            /// <returns>返回第一个可用的端口号，如果没有可用端口则返回-1</returns>
            [Preserve]
            public static int GetFirstAvailablePort(int startPort = 667, int maxPort = 65535)
            {
                for (int i = startPort; i < maxPort; i++)
                {
                    if (PortIsAvailable(i)) return i;
                }

                return -1;
            }

            /// <summary>
            /// 获取操作系统已用的端口号
            /// </summary>
            /// <returns>返回一个包含所有已用端口号的列表</returns>
            [Preserve]
            public static List<int> PortIsUsed()
            {
                //获取本地计算机的网络连接和通信统计数据的信息
                var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

                //返回本地计算机上的所有Tcp监听程序
                var ipsTcp = ipGlobalProperties.GetActiveTcpListeners();

                //返回本地计算机上的所有UDP监听程序
                var ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

                //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
                var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

                var allPorts = new List<int>();
                foreach (var ep in ipsTcp)
                {
                    allPorts.Add(ep.Port);
                }

                foreach (var ep in ipsUDP)
                {
                    allPorts.Add(ep.Port);
                }

                foreach (var conn in tcpConnInfoArray)
                {
                    allPorts.Add(conn.LocalEndPoint.Port);
                }

                return allPorts;
            }

            /// <summary>
            /// 检查指定端口是否已用
            /// </summary>
            /// <param name="port">要检查的端口号</param>
            /// <returns>如果端口可用则返回true，否则返回false</returns>
            [Preserve]
            public static bool PortIsAvailable(int port)
            {
                var isAvailable = true;

                var portUsed = PortIsUsed();

                foreach (int p in portUsed)
                {
                    if (p == port)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                return isAvailable;
            }

            /// <summary>
            /// 获取域名的IpV4 地址
            /// </summary>
            /// <param name="domainName">域名</param>
            /// <returns>返回域名的IPv4地址，如果没有则返回空字符串</returns>
            [Preserve]
            public static string GetHostIPv4(string domainName)
            {
                var iPHostEntry = Dns.GetHostEntry(domainName);
                foreach (var address in iPHostEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }

                return string.Empty;
            }

            /// <summary>
            /// 获取域名的IpV6 地址
            /// </summary>
            /// <param name="domainName">域名</param>
            /// <returns>返回域名的IPv4地址，如果没有则返回空字符串</returns>
            [Preserve]
            public static string GetHostIPv6(string domainName)
            {
                var iPHostEntry = Dns.GetHostEntry(domainName);
                foreach (var address in iPHostEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        return address.ToString();
                    }
                }

                return string.Empty;
            }

            /// <summary>
            /// 获取本机ip地址
            /// </summary>
            /// <returns>返回本机的IPv4地址，如果没有则返回空字符串</returns>
            [Preserve]
            public static string GetIP()
            {
                var hostName = Dns.GetHostName();
                var iPHostEntry = Dns.GetHostEntry(hostName);
                foreach (var address in iPHostEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }

                return string.Empty;
            }

            [Preserve]
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