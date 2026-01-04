// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
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
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 时间辅助工具类
    /// </summary>
    public partial class TimerHelper
    {
        /// <summary>
        /// Unix 纪元时间：1970-01-01 00:00:00 本地时间。
        /// </summary>
        /// <value>
        /// 表示 Unix 纪元开始时间的 <see cref="DateTime"/> 对象，时区为本地时间。
        /// </value>
        /// <remarks>
        /// 此常量用于本地时间与 Unix 时间戳之间的转换计算。
        /// Unix 纪元是计算机系统中时间戳的起始参考点。
        /// </remarks>
        /// <seealso cref="EpochUtc"/>
        public static readonly DateTime EpochLocal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        /// <summary>
        /// Unix 纪元时间：1970-01-01 00:00:00 UTC 时间。
        /// </summary>
        /// <value>
        /// 表示 Unix 纪元开始时间的 <see cref="DateTime"/> 对象，时区为 UTC。
        /// </value>
        /// <remarks>
        /// 此常量用于 UTC 时间与 Unix 时间戳之间的转换计算。
        /// UTC 时间是国际标准时间，不受时区影响，推荐在跨时区应用中使用。
        /// </remarks>
        /// <seealso cref="EpochLocal"/>
        public static readonly DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly long Epoch = EpochUtc.Ticks;

        /// <summary>
        /// 时间差
        /// </summary>
        private static long _differenceTime;

        private static bool _isSecLevel = true;

        /// <summary>
        /// 微秒
        /// </summary>
        public const long TicksPerMicrosecond = 1; //100微秒

        /// <summary>
        /// 10微秒
        /// </summary>
        public const long TicksPer = 10 * TicksPerMicrosecond;

        /// <summary>
        /// 1毫秒
        /// </summary>
        public const long TicksMillisecondUnit = TicksPer * 1000; //毫秒

        /// <summary>
        /// 1秒
        /// </summary>
        public const long TicksSecondUnit = TicksMillisecondUnit * 1000; // 秒  //10000000

        /// <summary>
        /// 设置时间差
        /// </summary>
        /// <param name="timeSpan"></param>
        [UnityEngine.Scripting.Preserve]
        public static void SetDifferenceTime(long timeSpan)
        {
            if (timeSpan > 1000000000000)
            {
                _isSecLevel = false;
            }
            else
            {
                _isSecLevel = true;
            }

            if (_isSecLevel)
            {
                _differenceTime = timeSpan - ClientNowSeconds();
            }
            else
            {
                _differenceTime = timeSpan - ClientNowMillisecond();
            }
        }

        /// <summary>
        /// 设置时间差
        /// </summary>
        /// <param name="serverTime">服务器时间</param>
        /// <param name="isUtc">是否使用UTC时间</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetDifferenceTime(DateTime serverTime, bool isUtc = false)
        {
            if (isUtc)
            {
                _differenceTime = (long)(serverTime - DateTime.UtcNow).TotalSeconds;
            }
            else
            {
                _differenceTime = (long)(serverTime - DateTime.Now).TotalSeconds;
            }
        }

        /// <summary>
        /// 毫秒级
        /// </summary>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long ClientNowMillisecond()
        {
            return (DateTime.UtcNow.Ticks - Epoch) / TicksMillisecondUnit;
        }

        [UnityEngine.Scripting.Preserve]
        public static long ServerToday()
        {
            if (_isSecLevel)
            {
                return _differenceTime + ClientToday();
            }

            return (_differenceTime + ClientTodayMillisecond()) / 1000;
        }

        [UnityEngine.Scripting.Preserve]
        public static long ClientTodayMillisecond()
        {
            return (DateTime.Now.Date.ToUniversalTime().Ticks - Epoch) / 10000;
        }

        /// <summary>
        /// 服务器当前时间 单位 秒
        /// </summary>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long ServerNow()
        {
            if (_isSecLevel)
            {
                return _differenceTime + ClientNowSeconds();
            }

            return (_differenceTime + ClientNowMillisecond()) / 1000;
        }

        /// <summary>
        /// 将秒数转换成TimeSpan
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static TimeSpan FromSeconds(int seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        /// 今天的客户端时间
        /// </summary>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long ClientToday()
        {
            return (DateTime.Now.Date.ToUniversalTime().Ticks - Epoch) / TicksSecondUnit;
        }

        /// <summary>
        /// 客户端时间，毫秒
        /// </summary>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - Epoch) / 10000;
        }

        /// <summary>
        /// 客户端时间。秒
        /// </summary>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long ClientNowSeconds()
        {
            return (DateTime.UtcNow.Ticks - Epoch) / 10000000;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="server">是否使用服务器时间</param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static DateTime DateTimeNow(bool server = true)
        {
            if (server)
            {
                return DateTimeOffset.FromUnixTimeSeconds(ServerNow()).DateTime;
            }

            return DateTimeOffset.FromUnixTimeSeconds(ClientNowSeconds()).DateTime;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="server">是否使用服务器时间</param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static DateTime Now(bool server = true)
        {
            if (server)
            {
                return DateTimeOffset.FromUnixTimeSeconds(ServerNow()).DateTime;
            }

            return DateTimeOffset.FromUnixTimeSeconds(ClientNowSeconds()).DateTime;
        }

        /// <summary>
        /// 指定时间转换成Unix时间戳的时间差，单位秒
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long LocalTimeToUnixTimeSeconds(DateTime time)
        {
            var utcDateTime = time.ToUniversalTime();
            return (long)(utcDateTime - EpochUtc).TotalSeconds;
        }

        /// <summary>
        /// 指定时间转换成Unix时间戳的时间差，单位毫秒
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static long LocalTimeToUnixTimeMilliseconds(DateTime time)
        {
            var utcDateTime = time.ToUniversalTime();
            return (long)(utcDateTime - EpochUtc).TotalMilliseconds;
        }

        /// <summary>
        /// 获取当前 UTC 时间的 Unix 时间戳（秒级精度）。
        /// </summary>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前时间的秒数。
        /// </returns>
        /// <remarks>
        /// 此方法返回的时间戳精度为秒级，适用于不需要高精度时间的场景。
        /// 时间戳基于 UTC 时间计算，避免了时区转换的复杂性。
        /// </remarks>
        /// <example>
        /// <code>
        /// long timestamp = TimerHelper.UnixTimeSeconds();
        /// Console.WriteLine($"当前 Unix 时间戳（秒）: {timestamp}");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeMilliseconds"/>
        public static long UnixTimeSeconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取当前 UTC 时间的 Unix 时间戳（毫秒级精度）。
        /// </summary>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前时间的毫秒数。
        /// </returns>
        /// <remarks>
        /// 此方法返回的时间戳精度为毫秒级，适用于需要高精度时间的场景，如日志记录、性能监控等。
        /// 时间戳基于 UTC 时间计算，确保在不同时区环境下的一致性。
        /// </remarks>
        /// <example>
        /// <code>
        /// long timestamp = TimerHelper.UnixTimeMilliseconds();
        /// Console.WriteLine($"当前 Unix 时间戳（毫秒）: {timestamp}");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeSeconds"/>
        public static long UnixTimeMilliseconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// UTC 时间戳 转换成UTC时间
        /// </summary>
        /// <param name="utcTimestamp">UTC时间戳,单位秒</param>
        /// <returns>转换后的UTC时间。</returns>
        public static DateTime UtcToUtcDateTime(long utcTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(utcTimestamp).UtcDateTime;
        }

        /// <summary>
        /// UTC 时间戳 转换成本地时间
        /// </summary>
        /// <param name="utcTimestamp">UTC时间戳,单位秒</param>
        /// <returns>转换后的本地时间。</returns>
        public static DateTime UtcToLocalDateTime(long utcTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(utcTimestamp).LocalDateTime;
        }

        /// <summary>
        /// 获取当前本地时区时间的 Unix 时间戳（秒级精度）。
        /// </summary>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前本地时间的秒数。
        /// </returns>
        /// <remarks>
        /// 此方法执行以下步骤：
        /// 1. 获取当前本地时区时间（<see cref="DateTime.Now"/>）
        /// 2. 创建 <see cref="DateTimeOffset"/> 对象以保留时区信息
        /// 3. 转换为 Unix 时间戳（秒级精度）
        /// 
        /// 与 <see cref="UnixTimeSeconds"/> 方法的区别：
        /// - 本方法基于本地时区时间计算
        /// - <see cref="UnixTimeSeconds"/> 基于 UTC 时间计算
        /// 
        /// 适用场景：需要考虑本地时区的时间戳生成，如用户界面显示、本地日志记录等。
        /// </remarks>
        /// <example>
        /// <code>
        /// // 获取当前本地时区的时间戳
        /// long localTimestamp = TimerHelper.NowTimeSeconds();
        /// Console.WriteLine($"本地时区时间戳（秒）: {localTimestamp}");
        /// 
        /// // 与UTC时间戳对比
        /// long utcTimestamp = TimerHelper.UnixTimeSeconds();
        /// long timezoneOffset = localTimestamp - utcTimestamp;
        /// Console.WriteLine($"时区偏移（秒）: {timezoneOffset}");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeSeconds"/>
        /// <seealso cref="NowTimeMilliseconds"/>
        /// <seealso cref="EpochLocal"/>
        public static long NowTimeSeconds()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取当前本地时区时间的 Unix 时间戳（毫秒级精度）。
        /// </summary>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前本地时间的毫秒数。
        /// </returns>
        /// <remarks>
        /// 此方法执行以下步骤：
        /// 1. 获取当前本地时区时间（<see cref="DateTime.Now"/>）
        /// 2. 创建 <see cref="DateTimeOffset"/> 对象以保留时区信息
        /// 3. 转换为 Unix 时间戳（毫秒级精度）
        /// 
        /// 与 <see cref="UnixTimeMilliseconds"/> 方法的区别：
        /// - 本方法基于本地时区时间计算
        /// - <see cref="UnixTimeMilliseconds"/> 基于 UTC 时间计算
        /// 
        /// 毫秒级精度适用于：
        /// - 高精度时间计算和比较
        /// - 性能监控和基准测试
        /// - 需要精确时间差计算的场景
        /// </remarks>
        /// <example>
        /// <code>
        /// // 获取当前本地时区的毫秒时间戳
        /// long localTimestamp = TimerHelper.NowTimeMilliseconds();
        /// Console.WriteLine($"本地时区时间戳（毫秒）: {localTimestamp}");
        /// 
        /// // 计算代码执行时间
        /// long startTime = TimerHelper.NowTimeMilliseconds();
        /// // ... 执行某些操作
        /// long endTime = TimerHelper.NowTimeMilliseconds();
        /// Console.WriteLine($"执行时间: {endTime - startTime} 毫秒");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeMilliseconds"/>
        /// <seealso cref="NowTimeSeconds"/>
        /// <seealso cref="EpochLocal"/>
        public static long NowTimeMilliseconds()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 获取指定时间距离纪元时间的毫秒数。
        /// </summary>
        /// <param name="time">要转换的指定时间。</param>
        /// <param name="utc">指定使用的纪元时间类型。如果为 <c>true</c>，使用 UTC 纪元时间；如果为 <c>false</c>，使用本地纪元时间。默认值为 <c>false</c>。</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示指定时间距离相应纪元时间的毫秒数。
        /// </returns>
        /// <remarks>
        /// 此方法根据 <paramref name="utc"/> 参数选择不同的纪元时间进行计算：
        /// - 当 <paramref name="utc"/> 为 <c>true</c> 时，使用 <see cref="EpochUtc"/>（1970-01-01 00:00:00 UTC）作为基准
        /// - 当 <paramref name="utc"/> 为 <c>false</c> 时，使用 <see cref="EpochLocal"/>（1970-01-01 00:00:00 本地时间）作为基准
        /// 
        /// 计算公式：毫秒数 = (指定时间 - 纪元时间).TotalMilliseconds
        /// 
        /// 注意事项：
        /// - 如果指定时间早于纪元时间，返回值将为负数
        /// - 毫秒级精度适用于需要高精度时间差计算的场景
        /// </remarks>
        /// <example>
        /// <code>
        /// DateTime now = DateTime.Now;
        /// DateTime utcNow = DateTime.UtcNow;
        /// 
        /// // 使用本地纪元时间计算
        /// long localMillis = TimerHelper.TimeToMilliseconds(now, false);
        /// Console.WriteLine($"距离本地纪元时间: {localMillis} 毫秒");
        /// 
        /// // 使用UTC纪元时间计算
        /// long utcMillis = TimerHelper.TimeToMilliseconds(utcNow, true);
        /// Console.WriteLine($"距离UTC纪元时间: {utcMillis} 毫秒");
        /// 
        /// // 计算历史时间（负值示例）
        /// DateTime historical = new DateTime(1969, 12, 31, 23, 59, 59);
        /// long historicalMillis = TimerHelper.TimeToMilliseconds(historical, true);
        /// Console.WriteLine($"历史时间毫秒数: {historicalMillis}"); // 负值
        /// </code>
        /// </example>
        /// <seealso cref="TimeToSecond"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="EpochLocal"/>
        public static long TimeToMilliseconds(DateTime time, bool utc = false)
        {
            if (utc)
            {
                return (long)(time - EpochUtc).TotalMilliseconds;
            }

            return (long)(time - EpochLocal).TotalMilliseconds;
        }

        /// <summary>
        /// 获取指定时间距离纪元时间的秒数。
        /// </summary>
        /// <param name="time">要转换的指定时间。</param>
        /// <param name="utc">指定使用的纪元时间类型。如果为 <c>true</c>，使用 UTC 纪元时间；如果为 <c>false</c>，使用本地纪元时间。默认值为 <c>false</c>。</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示指定时间距离相应纪元时间的秒数。
        /// </returns>
        /// <remarks>
        /// 此方法根据 <paramref name="utc"/> 参数选择不同的纪元时间进行计算：
        /// - 当 <paramref name="utc"/> 为 <c>true</c> 时，使用 <see cref="EpochUtc"/>（1970-01-01 00:00:00 UTC）作为基准
        /// - 当 <paramref name="utc"/> 为 <c>false</c> 时，使用 <see cref="EpochLocal"/>（1970-01-01 00:00:00 本地时间）作为基准
        /// 
        /// 计算公式：秒数 = (指定时间 - 纪元时间).TotalSeconds
        /// 
        /// 注意事项：
        /// - 如果指定时间早于纪元时间，返回值将为负数
        /// - 秒级精度适用于一般的时间戳计算和存储场景
        /// - 相比毫秒级精度，占用更少的存储空间
        /// </remarks>
        /// <example>
        /// <code>
        /// DateTime now = DateTime.Now;
        /// DateTime utcNow = DateTime.UtcNow;
        /// 
        /// // 使用本地纪元时间计算
        /// long localSeconds = TimerHelper.TimeToSecond(now, false);
        /// Console.WriteLine($"距离本地纪元时间: {localSeconds} 秒");
        /// 
        /// // 使用UTC纪元时间计算
        /// long utcSeconds = TimerHelper.TimeToSecond(utcNow, true);
        /// Console.WriteLine($"距离UTC纪元时间: {utcSeconds} 秒");
        /// 
        /// // 与毫秒级精度对比
        /// long millis = TimerHelper.TimeToMilliseconds(now, false);
        /// Console.WriteLine($"秒级: {localSeconds}, 毫秒级: {millis}");
        /// Console.WriteLine($"精度差异: {millis - localSeconds * 1000} 毫秒");
        /// </code>
        /// </example>
        /// <seealso cref="TimeToMilliseconds"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="EpochLocal"/>
        public static long TimeToSecond(DateTime time, bool utc = false)
        {
            if (utc)
            {
                return (long)(time - EpochUtc).TotalSeconds;
            }

            return (long)(time - EpochLocal).TotalSeconds;
        }
    }
}