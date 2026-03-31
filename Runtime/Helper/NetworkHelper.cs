using System.Collections.Generic;
using System.Net;
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
    }
}