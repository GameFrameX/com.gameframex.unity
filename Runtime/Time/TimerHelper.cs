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
using System.Threading;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 时间辅助工具类
    /// </summary>
    public static partial class TimerHelper
    {
        /// <summary>
        /// 当前时区，默认为 UTC。
        /// </summary>
        /// <remarks>
        /// Current time zone, defaults to UTC.
        /// Uses volatile keyword to ensure visibility in multi-threaded environments.
        /// For more complex thread safety requirements, consider using locks or other synchronization mechanisms.
        /// </remarks>
        private static volatile TimeZoneInfo _currentTimeZone = TimeZoneInfo.Utc;

        /// <summary>
        /// 获取当前时区。
        /// </summary>
        /// <remarks>
        /// Gets the current time zone.
        /// </remarks>
        /// <value>当前时区信息 / Current time zone information</value>
        public static TimeZoneInfo CurrentTimeZone
        {
            get { return _currentTimeZone; }
        }

        /// <summary>
        /// 设置当前时区。
        /// </summary>
        /// <remarks>
        /// Sets the current time zone.
        /// </remarks>
        /// <param name="timeZone">时区信息 / Time zone information</param>
        public static void SetTimeZone(TimeZoneInfo timeZone)
        {
            _currentTimeZone = timeZone ?? TimeZoneInfo.Utc;
        }

        /// <summary>
        /// 设置当前时区。
        /// </summary>
        /// <remarks>
        /// Sets the current time zone.
        /// </remarks>
        /// <param name="timeZoneId">时区ID，如 "China Standard Time" 或 "UTC" / Time zone ID, e.g. "China Standard Time" or "UTC"</param>
        /// <returns>如果成功设置时区返回 <c>true</c>；如果时区ID无效则返回 <c>false</c> 并回退到 UTC / Returns <c>true</c> if time zone is set successfully; returns <c>false</c> and falls back to UTC if the time zone ID is invalid</returns>
        public static bool SetTimeZone(string timeZoneId)
        {
            try
            {
                _currentTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return true;
            }
            catch (Exception)
            {
                _currentTimeZone = TimeZoneInfo.Utc;
                return false;
            }
        }

        /// <summary>
        /// Unix 纪元时间：1970-01-01 00:00:00 本地时间。
        /// </summary>
        /// <remarks>
        /// Unix epoch time: 1970-01-01 00:00:00 local time.
        /// This constant is used for conversion calculations between local time and Unix timestamps.
        /// The Unix epoch is the starting reference point for timestamps in computer systems.
        /// Note: This field is always based on the system local time zone and does not change with <see cref="CurrentTimeZone"/>.
        /// For epoch time based on the currently set time zone, use TimeZoneInfo.ConvertTime(EpochUtc, CurrentTimeZone).
        /// </remarks>
        /// <value>
        /// 表示 Unix 纪元开始时间的 <see cref="DateTime"/> 对象，时区为本地时间 / A <see cref="DateTime"/> object representing the Unix epoch start time in local time zone.
        /// </value>
        /// <seealso cref="EpochUtc"/>
        public static readonly DateTime EpochLocal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        /// <summary>
        /// Unix 纪元时间：1970-01-01 00:00:00 UTC 时间。
        /// </summary>
        /// <remarks>
        /// Unix epoch time: 1970-01-01 00:00:00 UTC time.
        /// This constant is used for conversion calculations between UTC time and Unix timestamps.
        /// UTC time is the international standard time, unaffected by time zones, recommended for cross-timezone applications.
        /// </remarks>
        /// <value>
        /// 表示 Unix 纪元开始时间的 <see cref="DateTime"/> 对象，时区为 UTC / A <see cref="DateTime"/> object representing the Unix epoch start time in UTC.
        /// </value>
        /// <seealso cref="EpochLocal"/>
        public static readonly DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 获取当前 UTC 时间的 Unix 时间戳（秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets the Unix timestamp of the current UTC time with second-level precision.
        /// This method returns a timestamp with second-level precision, suitable for scenarios that do not require high-precision time.
        /// The timestamp is calculated based on UTC time, avoiding the complexity of time zone conversion.
        /// </remarks>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前时间的秒数 / A <see cref="long"/> value representing the number of seconds from the Unix epoch (1970-01-01 00:00:00 UTC) to the current time.
        /// </returns>
        /// <example>
        /// <code>
        /// long timestamp = TimerHelper.UnixTimeSeconds();
        /// Console.WriteLine($"当前 Unix 时间戳（秒）: {timestamp}");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeMilliseconds"/>
        public static long UnixTimeSeconds()
        {
            return new DateTimeOffset(GetNowWithUtc()).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前 UTC 时间的 Unix 时间戳（毫秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets the Unix timestamp of the current UTC time with millisecond-level precision.
        /// This method returns a timestamp with millisecond-level precision, suitable for scenarios that require high-precision time, such as logging and performance monitoring.
        /// The timestamp is calculated based on UTC time, ensuring consistency across different time zone environments.
        /// </remarks>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从 Unix 纪元（1970-01-01 00:00:00 UTC）到当前时间的毫秒数 / A <see cref="long"/> value representing the number of milliseconds from the Unix epoch (1970-01-01 00:00:00 UTC) to the current time.
        /// </returns>
        /// <example>
        /// <code>
        /// long timestamp = TimerHelper.UnixTimeMilliseconds();
        /// Console.WriteLine($"当前 Unix 时间戳（毫秒）: {timestamp}");
        /// </code>
        /// </example>
        /// <seealso cref="UnixTimeSeconds"/>
        public static long UnixTimeMilliseconds()
        {
            return new DateTimeOffset(GetNowWithUtc()).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }

        /// <summary>
        /// 获取基于当前设置时区并包含时区偏移的 Unix 时间戳（秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets a Unix timestamp based on the currently set time zone with time zone offset included (second-level precision).
        /// The timestamp returned by this method includes the time zone offset and <see cref="TimeOffsetSeconds"/> adjustment.
        /// For example: If the current time zone is UTC+8, the returned timestamp will be 8 hours (28800 seconds) larger than the standard UTC timestamp.
        /// </remarks>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示将当前设置时区的时间视为 UTC 时间时的 Unix 时间戳。即：标准 Unix 时间戳 + 时区偏移秒数 + <see cref="TimeOffsetSeconds"/> / A <see cref="long"/> value representing the Unix timestamp when treating the current time zone time as UTC time. That is: standard Unix timestamp + time zone offset seconds + <see cref="TimeOffsetSeconds"/>.
        /// </returns>
        public static long UnixTimeSecondsWithTimeZoneOffset()
        {
            var utcNow = GetNowWithUtc();
            var offset = CurrentTimeZone.GetUtcOffset(utcNow);
            return new DateTimeOffset(utcNow).ToUnixTimeSeconds() + (long)offset.TotalSeconds + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取基于当前设置时区并包含时区偏移的 Unix 时间戳（毫秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets a Unix timestamp based on the currently set time zone with time zone offset included (millisecond-level precision).
        /// The timestamp returned by this method includes the time zone offset and <see cref="TimeOffsetMilliseconds"/> adjustment.
        /// For example: If the current time zone is UTC+8, the returned timestamp will be 8 hours (28800000 milliseconds) larger than the standard UTC timestamp.
        /// </remarks>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示将当前设置时区的时间视为 UTC 时间时的 Unix 时间戳。即：标准 Unix 时间戳 + 时区偏移毫秒数 + <see cref="TimeOffsetMilliseconds"/> / A <see cref="long"/> value representing the Unix timestamp when treating the current time zone time as UTC time. That is: standard Unix timestamp + time zone offset milliseconds + <see cref="TimeOffsetMilliseconds"/>.
        /// </returns>
        public static long UnixTimeMillisecondsWithTimeZoneOffset()
        {
            var utcNow = GetNowWithUtc();
            var offset = CurrentTimeZone.GetUtcOffset(utcNow);
            return new DateTimeOffset(utcNow).ToUnixTimeMilliseconds() + (long)offset.TotalMilliseconds + TimeOffsetMilliseconds;
        }

        /// <summary>
        /// 获取指定时间基于当前设置时区的 Unix 时间戳（秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets the Unix timestamp of the specified time based on the currently set time zone (second-level precision).
        /// This method first converts the input time to UTC (if not already), then adds the offset of the currently set time zone.
        /// If the input time Kind is Unspecified, it defaults to the time of the currently set time zone (<see cref="CurrentTimeZone"/>).
        /// </remarks>
        /// <param name="time">要转换的时间 / The time to convert</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示将指定时间视为 UTC 时间时的 Unix 时间戳 + 时区偏移 / A <see cref="long"/> value representing the Unix timestamp when treating the specified time as UTC time + time zone offset.
        /// </returns>
        public static long DateTimeToSecondsWithTimeZone(DateTime time)
        {
            var utcTime = ConvertToUtc(time);
            var offset = CurrentTimeZone.GetUtcOffset(utcTime);
            return new DateTimeOffset(utcTime).ToUnixTimeSeconds() + (long)offset.TotalSeconds + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取指定时间基于当前设置时区的 Unix 时间戳（毫秒级精度）。
        /// </summary>
        /// <remarks>
        /// Gets the Unix timestamp of the specified time based on the currently set time zone (millisecond-level precision).
        /// This method first converts the input time to UTC (if not already), then adds the offset of the currently set time zone.
        /// If the input time Kind is Unspecified, it defaults to the time of the currently set time zone (<see cref="CurrentTimeZone"/>).
        /// </remarks>
        /// <param name="time">要转换的时间 / The time to convert</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示将指定时间视为 UTC 时间时的 Unix 时间戳 + 时区偏移 / A <see cref="long"/> value representing the Unix timestamp when treating the specified time as UTC time + time zone offset.
        /// </returns>
        public static long TimeToMillisecondsWithTimeZone(DateTime time)
        {
            var utcTime = ConvertToUtc(time);
            var offset = CurrentTimeZone.GetUtcOffset(utcTime);
            return new DateTimeOffset(utcTime).ToUnixTimeMilliseconds() + (long)offset.TotalMilliseconds + TimeOffsetMilliseconds;
        }

        /// <summary>
        /// 将 DateTime 转换为 Unix 时间戳（秒），自动处理 DateTime.Kind。
        /// </summary>
        /// <remarks>
        /// Converts DateTime to Unix timestamp (seconds), automatically handling DateTime.Kind.
        /// Utc -> Uses Zero offset; Local -> Uses Local offset; Unspecified -> Uses CurrentTimeZone offset.
        /// </remarks>
        /// <param name="time">要转换的时间 / The time to convert</param>
        /// <returns>Unix 时间戳（秒） / Unix timestamp (seconds)</returns>
        private static long DateTimeToUnixTimeSeconds(DateTime time)
        {
            TimeSpan offset;
            if (time.Kind == DateTimeKind.Utc)
            {
                offset = TimeSpan.Zero;
            }
            else if (time.Kind == DateTimeKind.Local)
            {
                offset = TimeZoneInfo.Local.GetUtcOffset(time);
            }
            else
            {
                offset = CurrentTimeZone.GetUtcOffset(time);
            }

            return new DateTimeOffset(time, offset).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 将时间转换为 UTC 时间。
        /// </summary>
        /// <remarks>
        /// Converts time to UTC time.
        /// If Kind is Utc, returns directly. If Kind is Local, converts to UTC. If Kind is Unspecified, treats as current time zone (<see cref="CurrentTimeZone"/>) time and converts to UTC.
        /// </remarks>
        /// <param name="time">要转换的时间 / The time to convert</param>
        /// <returns>转换后的 UTC 时间 / The converted UTC time</returns>
        private static DateTime ConvertToUtc(DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
            {
                return time;
            }

            if (time.Kind == DateTimeKind.Local)
            {
                return time.ToUniversalTime();
            }

            // Unspecified, assume CurrentTimeZone
            return TimeZoneInfo.ConvertTimeToUtc(time, CurrentTimeZone);
        }

        /// <summary>
        /// 获取指定时间距离纪元时间的毫秒数。
        /// </summary>
        /// <remarks>
        /// Gets the number of milliseconds from the specified time to the epoch time.
        /// This method selects different epoch times for calculation based on the <paramref name="utc"/> parameter:
        /// - When <paramref name="utc"/> is <c>true</c>, uses <see cref="EpochUtc"/> (1970-01-01 00:00:00 UTC) as the base
        /// - When <paramref name="utc"/> is <c>false</c>, uses the epoch time of the currently set time zone (<see cref="CurrentTimeZone"/>) as the base
        /// Calculation formula: milliseconds = (specified time - epoch time).TotalMilliseconds
        /// Note: If the specified time is earlier than the epoch time, the return value will be negative.
        /// Millisecond-level precision is suitable for scenarios requiring high-precision time difference calculations.
        /// </remarks>
        /// <param name="time">要转换的指定时间 / The specified time to convert</param>
        /// <param name="utc">指定使用的纪元时间类型。如果为 <c>true</c>，使用 UTC 纪元时间；如果为 <c>false</c>，使用当前设置时区的纪元时间。默认值为 <c>false</c> / Specifies the type of epoch time to use. If <c>true</c>, uses UTC epoch time; if <c>false</c>, uses the epoch time of the currently set time zone. Default is <c>false</c></param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示指定时间距离相应纪元时间的毫秒数 / A <see cref="long"/> value representing the number of milliseconds from the specified time to the corresponding epoch time.
        /// </returns>
        /// <example>
        /// <code>
        /// DateTime now = DateTime.Now;
        /// DateTime utcNow = DateTime.UtcNow;
        ///
        /// // 使用当前设置时区的纪元时间计算
        /// long localMillis = TimerHelper.TimeToMilliseconds(now, false);
        /// Console.WriteLine($"距离当前时区纪元时间: {localMillis} 毫秒");
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
        /// <seealso cref="DateTimeToSecond"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="EpochLocal"/>
        public static long DateTimeToMilliseconds(DateTime time, bool utc = false)
        {
            if (utc)
            {
                return (long)(time - EpochUtc).TotalMilliseconds;
            }

            return (long)(time - TimeZoneInfo.ConvertTime(EpochUtc, CurrentTimeZone)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取指定时间距离纪元时间的秒数。
        /// </summary>
        /// <remarks>
        /// Gets the number of seconds from the specified time to the epoch time.
        /// This method selects different epoch times for calculation based on the <paramref name="utc"/> parameter:
        /// - When <paramref name="utc"/> is <c>true</c>, uses <see cref="EpochUtc"/> (1970-01-01 00:00:00 UTC) as the base
        /// - When <paramref name="utc"/> is <c>false</c>, uses the epoch time of the currently set time zone (<see cref="CurrentTimeZone"/>) as the base
        /// Calculation formula: seconds = (specified time - epoch time).TotalSeconds
        /// Note: If the specified time is earlier than the epoch time, the return value will be negative.
        /// Second-level precision is suitable for general timestamp calculation and storage scenarios.
        /// Compared to millisecond-level precision, it takes up less storage space.
        /// </remarks>
        /// <param name="time">要转换的指定时间 / The specified time to convert</param>
        /// <param name="utc">指定使用的纪元时间类型。如果为 <c>true</c>，使用 UTC 纪元时间；如果为 <c>false</c>，使用当前设置时区的纪元时间。默认值为 <c>false</c> / Specifies the type of epoch time to use. If <c>true</c>, uses UTC epoch time; if <c>false</c>, uses the epoch time of the currently set time zone. Default is <c>false</c></param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示指定时间距离相应纪元时间的秒数 / A <see cref="long"/> value representing the number of seconds from the specified time to the corresponding epoch time.
        /// </returns>
        /// <example>
        /// <code>
        /// DateTime now = DateTime.Now;
        /// DateTime utcNow = DateTime.UtcNow;
        ///
        /// // 使用当前设置时区的纪元时间计算
        /// long localSeconds = TimerHelper.TimeToSecond(now, false);
        /// Console.WriteLine($"距离当前时区纪元时间: {localSeconds} 秒");
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
        /// <seealso cref="DateTimeToMilliseconds"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="EpochLocal"/>
        public static long DateTimeToSecond(DateTime time, bool utc = false)
        {
            if (utc)
            {
                return (long)(time - EpochUtc).TotalSeconds;
            }

            return (long)(time - TimeZoneInfo.ConvertTime(EpochUtc, CurrentTimeZone)).TotalSeconds;
        }
    }
}