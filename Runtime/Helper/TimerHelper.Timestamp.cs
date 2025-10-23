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
    public partial class TimerHelper
    {
        /// <summary>
        /// 毫秒转时间
        /// </summary>
        /// <param name="timestamp">毫秒时间戳。</param>
        /// <param name="utc">是否使用UTC时间。</param>
        /// <returns>转换后的时间。</returns>
        public static DateTime MillisecondsTimeStampToDateTime(long timestamp, bool utc = false)
        {
            if (utc)
            {
                return EpochUtc.AddMilliseconds(timestamp);
            }

            return EpochLocal.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 秒时间戳转时间
        /// </summary>
        /// <param name="timestamp">秒时间戳。</param>
        /// <param name="utc">是否使用UTC时间。</param>
        /// <returns>转换后的时间。</returns>
        public static DateTime TimestampToDateTime(long timestamp, bool utc = false)
        {
            if (utc)
            {
                return EpochUtc.AddSeconds(timestamp);
            }

            return EpochLocal.AddSeconds(timestamp);
        }

        /// <summary>
        /// 将 Unix 时间戳（秒级）转换为 .NET 刻度数（Ticks）。
        /// </summary>
        /// <param name="timestampSeconds">Unix 时间戳，表示从 1970年1月1日 00:00:00 UTC 以来经过的秒数。</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从公元1年1月1日 00:00:00 以来的刻度数。
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 当 <paramref name="timestampSeconds"/> 超出 <see cref="DateTime"/> 有效范围时抛出此异常。
        /// 有效范围：-62135596800 到 253402300799 秒。
        /// </exception>
        /// <remarks>
        /// 此方法执行以下转换：
        /// 1. 验证时间戳是否在 <see cref="DateTime"/> 的有效范围内
        /// 2. 将 Unix 时间戳转换为 .NET 刻度数
        /// 3. 使用 <see cref="EpochUtc"/> 作为基准点进行计算
        /// 
        /// 转换公式：刻度数 = timestampSeconds × 10,000,000 + EpochUtc.Ticks
        /// 
        /// .NET 刻度数说明：
        /// - 1 刻度 = 100 纳秒
        /// - 1 秒 = 10,000,000 刻度（<see cref="TimeSpan.TicksPerSecond"/>）
        /// - 刻度数从公元1年1月1日 00:00:00 开始计算
        /// 
        /// 适用场景：
        /// - 将 Unix 时间戳转换为 .NET DateTime 对象
        /// - 高精度时间计算和比较
        /// - 时间数据的序列化和反序列化
        /// </remarks>
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
        public static long TimestampToTicks(long timestampSeconds)
        {
            if (timestampSeconds < -62135596800L || timestampSeconds > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampSeconds), "Timestamp is out of valid range for DateTime conversion.");
            }

            // 将Unix时间戳转换为刻度数，每秒等于10000000刻度
            // 使用TimeHelper.EpochUtc.Ticks确保与项目中其他时间计算保持一致
            return timestampSeconds * TimeSpan.TicksPerSecond + EpochUtc.Ticks;
        }

        /// <summary>
        /// 将 Unix 时间戳（毫秒级）转换为 .NET 刻度数（Ticks）。
        /// </summary>
        /// <param name="timestampMillisSeconds">Unix 毫秒时间戳，表示从 1970年1月1日 00:00:00 UTC 以来经过的毫秒数。</param>
        /// <returns>
        /// 返回一个 <see cref="long"/> 值，表示从公元1年1月1日 00:00:00 以来的刻度数。
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 当 <paramref name="timestampMillisSeconds"/> 超出 <see cref="DateTime"/> 有效范围时抛出此异常。
        /// 有效范围：-62135596800000 到 253402300799999 毫秒。
        /// </exception>
        /// <remarks>
        /// 此方法执行以下转换：
        /// 1. 验证毫秒时间戳是否在 <see cref="DateTime"/> 的有效范围内
        /// 2. 将 Unix 毫秒时间戳转换为 .NET 刻度数
        /// 3. 使用 <see cref="EpochUtc"/> 作为基准点进行计算
        /// 
        /// 转换公式：刻度数 = timestampMillisSeconds × 10,000 + EpochUtc.Ticks
        /// 
        /// .NET 刻度数说明：
        /// - 1 刻度 = 100 纳秒
        /// - 1 毫秒 = 10,000 刻度（<see cref="TimeSpan.TicksPerMillisecond"/>）
        /// - 刻度数从公元1年1月1日 00:00:00 开始计算
        /// 
        /// 与 <see cref="TimestampToTicks"/> 的区别：
        /// - 本方法处理毫秒级精度的时间戳
        /// - <see cref="TimestampToTicks"/> 处理秒级精度的时间戳
        /// - 毫秒级提供更高的时间精度
        /// 
        /// 适用场景：
        /// - 高精度时间戳转换
        /// - JavaScript 时间戳转换（JavaScript 使用毫秒时间戳）
        /// - 需要精确时间计算的场景
        /// </remarks>
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
        public static long TimestampMillisToTicks(long timestampMillisSeconds)
        {
            if (timestampMillisSeconds < -62135596800000L || timestampMillisSeconds > 253402300799999L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampMillisSeconds), "Timestamp is out of valid range for DateTime conversion.");
            }

            // 将Unix毫秒时间戳转换为刻度数，每毫秒等于10000刻度
            // 使用TimeHelper.EpochUtc.Ticks确保与项目中其他时间计算保持一致
            return timestampMillisSeconds * TimeSpan.TicksPerMillisecond + EpochUtc.Ticks;
        }

        /// <summary>
        /// UTC 时间戳 转换成UTC时间
        /// </summary>
        /// <param name="utcTimestampSeconds">UTC时间戳,单位秒</param>
        /// <returns>转换后的UTC时间。</returns>
        /// <remarks>
        /// 此方法将Unix时间戳(从1970-01-01 00:00:00 UTC开始的秒数)转换为UTC DateTime
        /// 使用DateTimeOffset.FromUnixTimeSeconds进行转换
        /// 返回的是UTC时区的时间
        /// </remarks>
        public static DateTime UtcSecondsToUtcDateTime(long utcTimestampSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(utcTimestampSeconds).UtcDateTime;
        }

        /// <summary>
        /// UTC 毫秒时间戳 转换成UTC时间
        /// </summary>
        /// <param name="utcTimestampMilliseconds">UTC时间戳,单位毫秒</param>
        /// <returns>转换后的UTC时间。</returns>
        /// <remarks>
        /// 此方法将Unix毫秒时间戳(从1970-01-01 00:00:00 UTC开始的毫秒数)转换为UTC DateTime
        /// 使用DateTimeOffset.FromUnixTimeMilliseconds进行转换
        /// 返回的是UTC时区的时间
        /// </remarks>
        public static DateTime UtcMillisecondsToUtcDateTime(long utcTimestampMilliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(utcTimestampMilliseconds).UtcDateTime;
        }

        /// <summary>
        /// UTC 时间戳 转换成本地时间
        /// </summary>
        /// <param name="utcTimestamp">UTC时间戳,单位秒</param>
        /// <returns>转换后的本地时间。</returns>
        /// <remarks>
        /// 此方法将Unix时间戳(从1970-01-01 00:00:00 UTC开始的秒数)转换为本地时区的DateTime
        /// 使用DateTimeOffset.FromUnixTimeSeconds进行转换
        /// 返回的时间会根据系统时区自动调整
        /// </remarks>
        public static DateTime UtcSecondsToLocalDateTime(long utcTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(utcTimestamp).LocalDateTime;
        }

        /// <summary>
        /// UTC 毫秒时间戳 转换成本地时间
        /// </summary>
        /// <param name="utcTimestampMilliseconds">UTC时间戳,单位毫秒</param>
        /// <returns>转换后的本地时间。</returns>
        /// <remarks>
        /// 此方法将Unix毫秒时间戳(从1970-01-01 00:00:00 UTC开始的毫秒数)转换为本地时区的DateTime
        /// 使用DateTimeOffset.FromUnixTimeMilliseconds进行转换
        /// 返回的时间会根据系统时区自动调整
        /// </remarks>
        public static DateTime UtcMillisecondsToDateTime(long utcTimestampMilliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(utcTimestampMilliseconds).LocalDateTime;
        }

        /// <summary>
        /// 将给定的时间戳转换为相对于EpochUtc的 TimeSpan 对象。
        /// </summary>
        /// <param name="timestamp">自1970年1月1日午夜以来经过的秒数。</param>
        /// <returns>一个 TimeSpan 对象，表示从EpochUtc到给定时间戳的间隔。</returns>
        /// <exception cref="ArgumentOutOfRangeException">当时间戳超出有效范围时抛出此异常</exception>
        public static TimeSpan TimeSpanWithTimestamp(long timestamp)
        {
            if (timestamp < -62135596800L || timestamp > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp), "Timestamp is out of valid range for DateTime conversion.");
            }

            // 直接将秒数转换为TimeSpan
            return TimeSpan.FromSeconds(timestamp);
        }

        /// <summary>
        /// 将给定的时间戳转换为相对于EpochLocal的 TimeSpan 对象。
        /// </summary>
        /// <param name="timestamp">自1970年1月1日午夜以来经过的秒数。</param>
        /// <returns>一个 TimeSpan 对象，表示从EpochLocal到给定时间戳的间隔。</returns>
        /// <exception cref="ArgumentOutOfRangeException">当时间戳超出有效范围时抛出此异常</exception>
        public static TimeSpan TimeSpanLocalWithTimestamp(long timestamp)
        {
            if (timestamp < -62135596800L || timestamp > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp), "Timestamp is out of valid range for DateTime conversion.");
            }

            // 直接将秒数转换为TimeSpan
            return TimeSpan.FromSeconds(timestamp);
        }
    }
}