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
        /// 将 Unix 时间戳（秒级）转换为 .NET 刻度数（Ticks）。
        /// </summary>
        /// <remarks>
        /// Converts a Unix timestamp (seconds) to .NET ticks.
        /// This method performs the following conversion:
        /// 1. Validates if the timestamp is within the valid range of <see cref="DateTime"/>
        /// 2. Converts the Unix timestamp to .NET ticks
        /// 3. Uses <see cref="EpochUtc"/> as the base point for calculation
        /// Conversion formula: ticks = timestampSeconds × 10,000,000 + EpochUtc.Ticks
        /// .NET ticks description:
        /// - 1 tick = 100 nanoseconds
        /// - 1 second = 10,000,000 ticks (<see cref="TimeSpan.TicksPerSecond"/>)
        /// - Ticks are calculated from January 1, 0001 00:00:00
        /// Applicable scenarios:
        /// - Converting Unix timestamps to .NET DateTime objects
        /// - High-precision time calculation and comparison
        /// - Time data serialization and deserialization
        /// </remarks>
        /// <param name="timestampSeconds">Unix 时间戳，表示从 1970年1月1日 00:00:00 UTC 以来经过的秒数 / Unix timestamp, representing the number of seconds elapsed since 1970-01-01 00:00:00 UTC</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从公元1年1月1日 00:00:00 以来的刻度数 / A <see cref="long"/> value representing the number of ticks since January 1, 0001 00:00:00.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 当 <paramref name="timestampSeconds"/> 超出 <see cref="DateTime"/> 有效范围时抛出此异常。有效范围：-62135596800 到 253402300799 秒 / Thrown when <paramref name="timestampSeconds"/> exceeds the valid range of <see cref="DateTime"/>. Valid range: -62135596800 to 253402300799 seconds.
        /// </exception>
        /// <example>
        /// <code>
        /// // 转换当前时间戳
        /// long currentTimestamp = TimerHelper.UnixTimeSeconds();
        /// long ticks = TimerHelper.TimestampToTicks(currentTimestamp);
        /// DateTime dateTime = new DateTime(ticks);
        /// Console.WriteLine($"转换后的时间: {dateTime}");
        ///
        /// // 转换特定时间戳
        /// long timestamp = 1609459200; // 2021-01-01 00:00:00 UTC
        /// long ticksValue = TimerHelper.TimestampToTicks(timestamp);
        /// DateTime specificDate = new DateTime(ticksValue);
        /// Console.WriteLine($"2021年元旦: {specificDate}");
        ///
        /// // 处理边界值
        /// try
        /// {
        ///     long invalidTimestamp = long.MaxValue;
        ///     TimerHelper.TimestampToTicks(invalidTimestamp); // 抛出异常
        /// }
        /// catch (ArgumentOutOfRangeException ex)
        /// {
        ///     Console.WriteLine($"时间戳超出范围: {ex.Message}");
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="TimestampMillisToTicks"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="TimeSpan.TicksPerSecond"/>
        /// <seealso cref="DateTime"/>
        [Preserve]
        public static long TimestampToTicks(long timestampSeconds)
        {
            if (timestampSeconds < -62135596800L || timestampSeconds > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampSeconds), "Timestamp is out of valid range");
            }

            // 将Unix时间戳转换为刻度数，每秒等于10000000刻度
            // 使用TimeHelper.EpochUtc.Ticks确保与项目中其他时间计算保持一致
            return timestampSeconds * TimeSpan.TicksPerSecond + EpochUtc.Ticks;
        }

        /// <summary>
        /// 将 Unix 时间戳（毫秒级）转换为 .NET 刻度数（Ticks）。
        /// </summary>
        /// <remarks>
        /// Converts a Unix timestamp (milliseconds) to .NET ticks.
        /// This method performs the following conversion:
        /// 1. Validates if the millisecond timestamp is within the valid range of <see cref="DateTime"/>
        /// 2. Converts the Unix millisecond timestamp to .NET ticks
        /// 3. Uses <see cref="EpochUtc"/> as the base point for calculation
        /// Conversion formula: ticks = timestampMillisSeconds × 10,000 + EpochUtc.Ticks
        /// .NET ticks description:
        /// - 1 tick = 100 nanoseconds
        /// - 1 millisecond = 10,000 ticks (<see cref="TimeSpan.TicksPerMillisecond"/>)
        /// - Ticks are calculated from January 1, 0001 00:00:00
        /// Difference from <see cref="TimestampToTicks"/>:
        /// - This method handles millisecond-level precision timestamps
        /// - <see cref="TimestampToTicks"/> handles second-level precision timestamps
        /// - Millisecond level provides higher time precision
        /// Applicable scenarios:
        /// - High-precision timestamp conversion
        /// - JavaScript timestamp conversion (JavaScript uses millisecond timestamps)
        /// - Scenarios requiring precise time calculations
        /// </remarks>
        /// <param name="timestampMillisSeconds">Unix 毫秒时间戳，表示从 1970年1月1日 00:00:00 UTC 以来经过的毫秒数 / Unix millisecond timestamp, representing the number of milliseconds elapsed since 1970-01-01 00:00:00 UTC</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从公元1年1月1日 00:00:00 以来的刻度数 / A <see cref="long"/> value representing the number of ticks since January 1, 0001 00:00:00.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 当 <paramref name="timestampMillisSeconds"/> 超出 <see cref="DateTime"/> 有效范围时抛出此异常。有效范围：-62135596800000 到 253402300799999 毫秒 / Thrown when <paramref name="timestampMillisSeconds"/> exceeds the valid range of <see cref="DateTime"/>. Valid range: -62135596800000 to 253402300799999 milliseconds.
        /// </exception>
        /// <example>
        /// <code>
        /// // 转换当前毫秒时间戳
        /// long currentMillisTimestamp = TimerHelper.UnixTimeMilliseconds();
        /// long ticks = TimerHelper.TimestampMillisToTicks(currentMillisTimestamp);
        /// DateTime dateTime = new DateTime(ticks);
        /// Console.WriteLine($"转换后的时间: {dateTime:yyyy-MM-dd HH:mm:ss.fff}");
        ///
        /// // 转换JavaScript时间戳
        /// long jsTimestamp = 1609459200000; // 2021-01-01 00:00:00.000 UTC
        /// long ticksValue = TimerHelper.TimestampMillisToTicks(jsTimestamp);
        /// DateTime jsDate = new DateTime(ticksValue);
        /// Console.WriteLine($"JavaScript时间: {jsDate}");
        ///
        /// // 精度对比
        /// long secondsTimestamp = 1609459200; // 秒级
        /// long millisTimestamp = 1609459200123; // 毫秒级
        ///
        /// DateTime fromSeconds = new DateTime(TimerHelper.TimestampToTicks(secondsTimestamp));
        /// DateTime fromMillis = new DateTime(TimerHelper.TimestampMillisToTicks(millisTimestamp));
        ///
        /// Console.WriteLine($"秒级精度: {fromSeconds:yyyy-MM-dd HH:mm:ss.fff}");
        /// Console.WriteLine($"毫秒级精度: {fromMillis:yyyy-MM-dd HH:mm:ss.fff}");
        /// </code>
        /// </example>
        /// <seealso cref="TimestampToTicks"/>
        /// <seealso cref="EpochUtc"/>
        /// <seealso cref="TimeSpan.TicksPerMillisecond"/>
        /// <seealso cref="DateTime"/>
        [Preserve]
        public static long TimestampMillisToTicks(long timestampMillisSeconds)
        {
            if (timestampMillisSeconds < -62135596800000L || timestampMillisSeconds > 253402300799999L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampMillisSeconds), "Timestamp is out of valid range");
            }

            // 将Unix毫秒时间戳转换为刻度数，每毫秒等于10000刻度
            // 使用TimeHelper.EpochUtc.Ticks确保与项目中其他时间计算保持一致
            return timestampMillisSeconds * TimeSpan.TicksPerMillisecond + EpochUtc.Ticks;
        }

        /// <summary>
        /// 毫秒时间戳转换为 DateTime。
        /// </summary>
        /// <remarks>
        /// Converts millisecond timestamp to DateTime.
        /// </remarks>
        /// <param name="utcTimestampMilliseconds">毫秒时间戳 / Millisecond timestamp</param>
        /// <param name="utc">是否使用UTC时间 / Whether to use UTC time</param>
        /// <returns>转换后的时间。如果utc为false，则返回当前时区 (<see cref="CurrentTimeZone"/>) 的时间 / The converted time. If utc is false, returns the time in the current time zone (<see cref="CurrentTimeZone"/>)</returns>
        [Preserve]
        public static DateTime TimeStampMillisecondToDateTime(long utcTimestampMilliseconds, bool utc = false)
        {
            var dateTime = EpochUtc.AddMilliseconds(utcTimestampMilliseconds);
            if (utc)
            {
                return dateTime;
            }

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, CurrentTimeZone);
        }

        /// <summary>
        /// 秒时间戳转换为 DateTime。
        /// </summary>
        /// <remarks>
        /// Converts second timestamp to DateTime.
        /// </remarks>
        /// <param name="utcTimestampSeconds">秒时间戳 / Second timestamp</param>
        /// <param name="utc">是否使用UTC时间 / Whether to use UTC time</param>
        /// <returns>转换后的时间。如果utc为false，则返回当前时区 (<see cref="CurrentTimeZone"/>) 的时间 / The converted time. If utc is false, returns the time in the current time zone (<see cref="CurrentTimeZone"/>)</returns>
        [Preserve]
        public static DateTime TimestampSecondToDateTime(long utcTimestampSeconds, bool utc = false)
        {
            var dateTime = EpochUtc.AddSeconds(utcTimestampSeconds);
            if (utc)
            {
                return dateTime;
            }

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, CurrentTimeZone);
        }
    }
}