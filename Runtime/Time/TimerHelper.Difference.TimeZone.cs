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
//  禁止利用本项目实施任何国家安全、破坏社会秩序、
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
        /// 计算两个时间戳之间的时间差（秒级）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference between two timestamps (seconds level).
        /// This method first converts timestamps to DateTime before calculating the difference.
        /// Timestamps are based on January 1, 1970.
        /// When isUseUtc=true, uses UTC time; otherwise uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// The returned TimeSpan object contains complete time difference information.
        /// </remarks>
        /// <param name="startUtcTimestampSeconds">开始时间戳（秒） / Start timestamp (seconds)</param>
        /// <param name="endUtcTimestampSeconds">结束时间戳（秒） / End timestamp (seconds)</param>
        /// <param name="isUseUtc">是否使用UTC时间，默认为true / Whether to use UTC time, defaults to true</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifference(long startUtcTimestampSeconds, long endUtcTimestampSeconds, bool isUseUtc = true)
        {
            var startTime = TimestampSecondToDateTime(startUtcTimestampSeconds, isUseUtc);
            var endTime = TimestampSecondToDateTime(endUtcTimestampSeconds, isUseUtc);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算两个毫秒时间戳之间的时间差。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference between two millisecond timestamps.
        /// This method provides millisecond-level precise time difference calculation.
        /// Timestamps are based on January 1, 1970.
        /// When isUseUtc=true, uses UTC time; otherwise uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// Suitable for scenarios requiring high-precision time difference calculations.
        /// </remarks>
        /// <param name="startUtcTimestampMillisecond">开始时间戳（毫秒） / Start timestamp (milliseconds)</param>
        /// <param name="endUtcTimestampMillisecond">结束时间戳（毫秒） / End timestamp (milliseconds)</param>
        /// <param name="isUseUtc">是否使用UTC时间，默认为true / Whether to use UTC time, defaults to true</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceMillisecond(long startUtcTimestampMillisecond, long endUtcTimestampMillisecond, bool isUseUtc = true)
        {
            var startTime = TimeStampMillisecondToDateTime(startUtcTimestampMillisecond, isUseUtc);
            var endTime = TimeStampMillisecondToDateTime(endUtcTimestampMillisecond, isUseUtc);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算指定时间到当前时间的时间差。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified time to the current time.
        /// This method calculates the difference from the specified time to the current time.
        /// When isUseUtc=true, uses UTC time; otherwise uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// If the specified time is after the current time, returns a negative value.
        /// Commonly used for calculating time intervals and checking expiration times.
        /// </remarks>
        /// <param name="time">指定时间 / The specified time</param>
        /// <param name="isUseUtc">是否使用UTC时间作为当前时间，默认为false（使用当前时区 (<see cref="CurrentTimeZone"/>) 时间） / Whether to use UTC time as current time, defaults to false (uses current time zone time)</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNow(DateTime time, bool isUseUtc = false)
        {
            var now = isUseUtc ? GetNowWithUtc() : GetNowWithTimeZone();
            return now - time;
        }

        /// <summary>
        /// 计算指定时间戳到当前时间的时间差。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified timestamp to the current time.
        /// This method first converts the timestamp to DateTime, then calculates the difference with the current time.
        /// Timestamps are based on January 1, 1970.
        /// When isUseUtc=true, uses UTC time; otherwise uses local time.
        /// Suitable for time difference calculations in Unix timestamp format.
        /// </remarks>
        /// <param name="timestamp">时间戳（秒） / Timestamp (seconds)</param>
        /// <param name="isUseUtc">是否使用UTC时间，默认为true / Whether to use UTC time, defaults to true</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNow(long timestamp, bool isUseUtc = true)
        {
            var time = TimestampSecondToDateTime(timestamp, isUseUtc);
            return GetTimeDifferenceFromNow(time, isUseUtc);
        }

        /// <summary>
        /// 计算指定毫秒时间戳到当前时间的时间差。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified millisecond timestamp to the current time.
        /// This method provides millisecond-level precision time difference calculation.
        /// Timestamps are based on January 1, 1970.
        /// When isUseUtc=true, uses UTC time; otherwise uses local time.
        /// Suitable for scenarios requiring high-precision time difference calculations.
        /// </remarks>
        /// <param name="timestampMs">时间戳（毫秒） / Timestamp (milliseconds)</param>
        /// <param name="isUseUtc">是否使用UTC时间，默认为true / Whether to use UTC time, defaults to true</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNowMs(long timestampMs, bool isUseUtc = true)
        {
            var time = TimeStampMillisecondToDateTime(timestampMs, isUseUtc);
            return GetTimeDifferenceFromNow(time, isUseUtc);
        }

        /// <summary>
        /// 计算指定时间到当前时间经过了多少秒。
        /// </summary>
        /// <remarks>
        /// Calculates how many seconds have passed from the specified time to the current time.
        /// This method calculates the total seconds from the specified time to now.
        /// When isUseUtc=true, uses UTC time; otherwise uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// Commonly used for checking if time has expired or calculating remaining time.
        /// </remarks>
        /// <param name="time">指定时间 / The specified time</param>
        /// <param name="isUseUtc">是否使用UTC时间作为当前时间，默认为false（使用当前时区 (<see cref="CurrentTimeZone"/>) 时间） / Whether to use UTC time as current time, defaults to false (uses current time zone time)</param>
        /// <returns>经过的秒数（如果指定时间在未来，返回负数） / The number of seconds elapsed (returns negative if the specified time is in the future)</returns>
        [Preserve]
        public static long GetElapsedSeconds(DateTime time, bool isUseUtc = false)
        {
            var now = isUseUtc ? GetNowWithUtc() : GetNowWithTimeZone();
            return (long)(now - time).TotalSeconds;
        }


        /// <summary>
        /// 计算两个时间戳之间的时间差（秒级，基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference between two timestamps (seconds level, based on TimeZone).
        /// This method first converts timestamps to DateTime before calculating the difference.
        /// Timestamps are based on January 1, 1970.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// The returned TimeSpan object contains complete time difference information.
        /// </remarks>
        /// <param name="startTimestampSeconds">开始时间戳（秒） / Start timestamp (seconds)</param>
        /// <param name="endTimestampSeconds">结束时间戳（秒） / End timestamp (seconds)</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceWithTimeZone(long startTimestampSeconds, long endTimestampSeconds)
        {
            var startTime = TimestampSecondToDateTime(startTimestampSeconds, false);
            var endTime = TimestampSecondToDateTime(endTimestampSeconds, false);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算两个毫秒时间戳之间的时间差（基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference between two millisecond timestamps (based on TimeZone).
        /// This method provides millisecond-level precise time difference calculation.
        /// Timestamps are based on January 1, 1970.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// Suitable for scenarios requiring high-precision time difference calculations.
        /// </remarks>
        /// <param name="startTimestampMillisecond">开始时间戳（毫秒） / Start timestamp (milliseconds)</param>
        /// <param name="endTimestampMillisecond">结束时间戳（毫秒） / End timestamp (milliseconds)</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceMillisecondWithTimeZone(long startTimestampMillisecond, long endTimestampMillisecond)
        {
            var startTime = TimeStampMillisecondToDateTime(startTimestampMillisecond, false);
            var endTime = TimeStampMillisecondToDateTime(endTimestampMillisecond, false);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算指定时间到当前时间的时间差（基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified time to the current time (based on TimeZone).
        /// This method calculates the difference from the specified time to the current time.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// If the specified time is after the current time, returns a negative value.
        /// Commonly used for calculating time intervals and checking expiration times.
        /// </remarks>
        /// <param name="time">指定时间 / The specified time</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNowWithTimeZone(DateTime time)
        {
            var now = GetNowWithTimeZone();
            return now - time;
        }

        /// <summary>
        /// 计算指定时间戳到当前时间的时间差（基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified timestamp to the current time (based on TimeZone).
        /// This method first converts the timestamp to DateTime, then calculates the difference with the current time.
        /// Timestamps are based on January 1, 1970.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// Suitable for time difference calculations in Unix timestamp format.
        /// </remarks>
        /// <param name="timestamp">时间戳（秒） / Timestamp (seconds)</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNowWithTimeZone(long timestamp)
        {
            var time = TimestampSecondToDateTime(timestamp, false);
            return GetTimeDifferenceFromNowWithTimeZone(time);
        }

        /// <summary>
        /// 计算指定毫秒时间戳到当前时间的时间差（基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference from the specified millisecond timestamp to the current time (based on TimeZone).
        /// This method provides millisecond-level precision time difference calculation.
        /// Timestamps are based on January 1, 1970.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// Suitable for scenarios requiring high-precision time difference calculations.
        /// </remarks>
        /// <param name="timestampMs">时间戳（毫秒） / Timestamp (milliseconds)</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifferenceFromNowMsWithTimeZone(long timestampMs)
        {
            var time = TimeStampMillisecondToDateTime(timestampMs, false);
            return GetTimeDifferenceFromNowWithTimeZone(time);
        }

        /// <summary>
        /// 计算指定时间到当前时间经过了多少秒（基于TimeZone）。
        /// </summary>
        /// <remarks>
        /// Calculates how many seconds have passed from the specified time to the current time (based on TimeZone).
        /// This method calculates the total seconds from the specified time to now.
        /// Uses current time zone (<see cref="CurrentTimeZone"/>) time.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// Commonly used for checking if time has expired or calculating remaining time.
        /// </remarks>
        /// <param name="time">指定时间 / The specified time</param>
        /// <returns>经过的秒数（如果指定时间在未来，返回负数） / The number of seconds elapsed (returns negative if the specified time is in the future)</returns>
        [Preserve]
        public static long GetElapsedSecondsWithTimeZone(DateTime time)
        {
            var now = GetNowWithTimeZone();
            return (long)(now - time).TotalSeconds;
        }
    }
}