using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 网络帮助类。
    /// </summary>
    /// <remarks>
    /// Network helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class NetworkHelper
    {
        /// <summary>
        /// 获取本地的IP列表。
        /// </summary>
        /// <remarks>
        /// Gets the local IP address list.
        /// </remarks>
        /// <returns>本地IP地址数组 / Array of local IP addresses</returns>
        [UnityEngine.Scripting.Preserve]
        public static string[] GetAddressIPs()
        {
            //获取本地的IP地址
            var list = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            string[] addressIPs = new string[list.Length];
            for (var index = 0; index < list.Length; index++)
            {
                IPAddress address = list[index];
                addressIPs[index] = address.ToString();
            }

            return addressIPs;
        }

        /// <summary>
        /// 获取当前是否有网络连接。
        /// </summary>
        /// <remarks>
        /// Gets whether the device has network connectivity.
        /// </remarks>
        /// <value>如果有网络连接则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if network is reachable; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsReachable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        /// <summary>
        /// 获取当前是否通过WiFi连接网络。
        /// </summary>
        /// <remarks>
        /// Gets whether the device is connected via WiFi (local area network).
        /// </remarks>
        /// <value>如果是WiFi连接则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on WiFi; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWifi()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }

        /// <summary>
        /// 获取当前是否通过移动数据网络连接。
        /// </summary>
        /// <remarks>
        /// Gets whether the device is connected via cellular data network.
        /// </remarks>
        /// <value>如果是移动网络则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on cellular data; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsViaCarrierData()
        {
            //当用户使用移动网络时
            return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
        }

        /// <summary>
        /// 获取第一个可用的端口号（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the first available port number.
        /// </remarks>
        /// <param name="startPort">起始端口号 / The starting port number</param>
        /// <param name="maxPort">结束端口号 / The ending port number</param>
        /// <returns>返回第一个可用的端口号，如果没有可用端口则返回 -1 / The first available port number, or -1 if none is available</returns>
        [UnityEngine.Scripting.Preserve]
        public static int GetFirstAvailablePort(int startPort = 667, int maxPort = 65535)
        {
            for (int i = startPort; i < maxPort; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        /// <summary>
        /// 获取操作系统已用的端口号（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the port numbers already used by the operating system.
        /// </remarks>
        /// <returns>返回一个包含所有已用端口号的列表 / A list containing all used port numbers</returns>
        [UnityEngine.Scripting.Preserve]
        public static List<int> PortIsUsed()
        {
            // 获取本地计算机的网络连接和通信统计数据的信息
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            // 返回本地计算机上的所有Tcp监听程序
            var ipsTcp = ipGlobalProperties.GetActiveTcpListeners();

            // 返回本地计算机上的所有UDP监听程序
            var ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            // 返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
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
        /// 检查指定端口是否可用（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Checks whether the specified port is available.
        /// </remarks>
        /// <param name="port">要检查的端口号 / The port number to check</param>
        /// <returns>如果端口可用则返回 true，否则返回 false / true if the port is available; otherwise, false</returns>
        [UnityEngine.Scripting.Preserve]
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
        /// 获取域名的 IpV4 地址（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the IPv4 address of a domain name.
        /// </remarks>
        /// <param name="domainName">域名 / The domain name</param>
        /// <returns>返回域名的IPv4地址，如果没有则返回空字符串 / The IPv4 address of the domain, or an empty string if none exists</returns>
        [UnityEngine.Scripting.Preserve]
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
        /// 获取域名的 IpV6 地址（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the IPv6 address of a domain name.
        /// </remarks>
        /// <param name="domainName">域名 / The domain name</param>
        /// <returns>返回域名的IPv6地址，如果没有则返回空字符串 / The IPv6 address of the domain, or an empty string if none exists</returns>
        [UnityEngine.Scripting.Preserve]
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
        /// 获取本机 ip 地址（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the local IP address.
        /// </remarks>
        /// <returns>返回本机的IPv4地址，如果没有则返回空字符串 / The local IPv4 address, or an empty string if none exists</returns>
        [UnityEngine.Scripting.Preserve]
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

        /// <summary>
        /// 获取指定主机的地址，优先返回 IPv6 地址（C27 自旧网络门面迁入）。
        /// </summary>
        /// <remarks>
        /// Gets the address of the specified host, preferring an IPv6 address. Falls back to an IPv4 address, and finally to the host string itself.
        /// </remarks>
        /// <param name="host">主机名或域名 / The host name or domain name</param>
        /// <returns>地址族与地址字符串组成的元组 / A tuple of the address family and the address string</returns>
        [UnityEngine.Scripting.Preserve]
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