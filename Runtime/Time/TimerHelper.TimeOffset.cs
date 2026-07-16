// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class TimerHelper
    {
        /// <summary>
        /// 服务器与本地的时间差（秒）。服务器时间戳 - 本地 UTC 时间戳。
        /// </summary>
        /// <remarks>
        /// Server-to-local time delta in seconds. Server timestamp - local UTC timestamp.
        /// </remarks>
        /// <value>服务器时间差（秒） / Server time delta in seconds</value>
        [Preserve]
        public static long TimeOffsetSeconds { get; private set; }

        /// <summary>
        /// 服务器与本地的时间差（毫秒）。服务器时间戳 - 本地 UTC 时间戳。
        /// </summary>
        /// <remarks>
        /// Server-to-local time delta in milliseconds. Server timestamp - local UTC timestamp.
        /// </remarks>
        /// <value>服务器时间差（毫秒） / Server time delta in milliseconds</value>
        [Preserve]
        public static long TimeOffsetMilliseconds { get; private set; }

        /// <summary>
        /// 通过服务器秒级时间戳同步时间差。
        /// </summary>
        /// <remarks>
        /// Synchronizes the time delta using a server second-level timestamp.
        /// Call this on each heartbeat to keep the client in sync with the server.
        /// </remarks>
        /// <param name="serverTimestampSeconds">服务器当前秒级时间戳 / Server current second-level timestamp</param>
        [Preserve]
        public static void SyncServerTimeSeconds(long serverTimestampSeconds)
        {
            var localSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TimeOffsetSeconds = serverTimestampSeconds - localSeconds;
            TimeOffsetMilliseconds = TimeOffsetSeconds * 1000;
        }

        /// <summary>
        /// 通过服务器毫秒级时间戳同步时间差。
        /// </summary>
        /// <remarks>
        /// Synchronizes the time delta using a server millisecond-level timestamp.
        /// Call this on each heartbeat to keep the client in sync with the server.
        /// </remarks>
        /// <param name="serverTimestampMilliseconds">服务器当前毫秒级时间戳 / Server current millisecond-level timestamp</param>
        [Preserve]
        public static void SyncServerTimeMilliseconds(long serverTimestampMilliseconds)
        {
            var localMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            TimeOffsetMilliseconds = serverTimestampMilliseconds - localMilliseconds;
            TimeOffsetSeconds = TimeOffsetMilliseconds / 1000;
        }

        /// <summary>
        /// 获取当前服务器时间（秒）。
        /// </summary>
        /// <remarks>
        /// Gets the current server time in seconds.
        /// Returns local UTC timestamp + time delta synced from the server.
        /// Call <see cref="SyncServerTimeSeconds"/> first to establish the delta.
        /// </remarks>
        /// <returns>服务器当前秒级时间戳 / Server current second-level timestamp</returns>
        [Preserve]
        public static long ServerNowSeconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前服务器时间（毫秒）。
        /// </summary>
        /// <remarks>
        /// Gets the current server time in milliseconds.
        /// Returns local UTC timestamp + time delta synced from the server.
        /// Call <see cref="SyncServerTimeMilliseconds"/> first to establish the delta.
        /// </remarks>
        /// <returns>服务器当前毫秒级时间戳 / Server current millisecond-level timestamp</returns>
        [Preserve]
        public static long ServerNowMilliseconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }

        /// <summary>
        /// 重置时间偏移值为默认值（0）。
        /// </summary>
        /// <remarks>
        /// Resets the time offset values to their defaults (0).
        /// This method resets both second-level and millisecond-level offsets to zero,
        /// restoring time calculations to the local UTC time.
        /// </remarks>
        [Preserve]
        public static void ResetTimeOffset()
        {
            TimeOffsetSeconds = 0;
            TimeOffsetMilliseconds = 0;
        }
    }
}