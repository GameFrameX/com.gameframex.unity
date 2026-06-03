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
    public static partial class TimerHelper
    {
        /// <summary>
        /// 获取当前时区 (<see cref="CurrentTimeZone"/>) 的日期，格式为yyyyMMdd的整数。
        /// </summary>
        /// <remarks>
        /// Gets the current time zone (<see cref="CurrentTimeZone"/>) date as an integer in yyyyMMdd format.
        /// This method converts the current time zone time to an 8-digit format:
        /// - First 4 digits represent the year
        /// - Middle 2 digits represent the month
        /// - Last 2 digits represent the day
        /// Uses <see cref="GetNowWithTimeZone"/> to get the current time zone time.
        /// </remarks>
        /// <returns>返回一个8位整数，表示当前时区 (<see cref="CurrentTimeZone"/>) 的日期。例如：20231225表示2023年12月25日 / Returns an 8-digit integer representing the current time zone date. For example: 20231225 represents December 25, 2023</returns>
        public static int CurrentDateWithDayWithTimeZone()
        {
            return Convert.ToInt32(GetNowWithTimeZone().ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 获取两个当前时区 (<see cref="CurrentTimeZone"/>) 时间戳之间的间隔天数。
        /// </summary>
        /// <remarks>
        /// Gets the number of days between two current time zone (<see cref="CurrentTimeZone"/>) timestamps.
        /// This method first converts UTC timestamps to current time zone (<see cref="CurrentTimeZone"/>) time, then calculates the day difference between the two times.
        /// The calculation considers the hours, minutes, and seconds parts of the dates.
        /// </remarks>
        /// <param name="startTimestamp">开始时间戳(秒),UTC时间戳将被转换为当前时区 (<see cref="CurrentTimeZone"/>) 时间 / Start timestamp (seconds), UTC timestamp will be converted to current time zone time</param>
        /// <param name="endTimestamp">结束时间戳(秒),UTC时间戳将被转换为当前时区 (<see cref="CurrentTimeZone"/>) 时间 / End timestamp (seconds), UTC timestamp will be converted to current time zone time</param>
        /// <param name="hour">跨天计算的小时数,默认值为0,表示跨天计算 / The hour for day crossing calculation, defaults to 0, meaning day crossing calculation</param>
        /// <returns>间隔天数,如果开始时间晚于结束时间,返回负数 / The number of days interval, returns negative if start time is later than end time</returns>
        public static int GetCrossDaysWithTimeZone(long startTimestamp, long endTimestamp, int hour = 0)
        {
            var startTime = TimestampSecondToDateTime(startTimestamp);
            var endTime = TimestampSecondToDateTime(endTimestamp);
            return GetCrossDays(startTime, endTime, hour);
        }

        /// <summary>
        /// 获取从指定日期到当前时区 (<see cref="CurrentTimeZone"/>) 日期之间跨越的天数。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed from the specified date to the current time zone (<see cref="CurrentTimeZone"/>) date.
        /// </remarks>
        /// <param name="startTime">起始日期 / The start date</param>
        /// <param name="hour">小时阈值 / Hour threshold</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        public static int GetCrossDaysWithTimeZone(DateTime startTime, int hour = 0)
        {
            return GetCrossDays(startTime, GetNowWithTimeZone(), hour);
        }

        /// <summary>
        /// 获取今天开始时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of today (based on set time zone).
        /// This method returns the midnight time (00:00:00) of the current day.
        /// Uses <see cref="GetNowWithTimeZone"/> to get the midnight time of the current date.
        /// Returns the time in the <see cref="CurrentTimeZone"/> time zone.
        /// </remarks>
        /// <returns>今天零点时间 / The midnight time of today</returns>
        public static DateTime GetTodayStartTimeWithTimeZone()
        {
            var dateTime = GetNowWithTimeZone();
            return dateTime.Date;
        }

        /// <summary>
        /// 获取今天开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of today (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// Suitable for scenarios requiring forged local timestamps.
        /// </remarks>
        /// <returns>今天零点时间戳(秒) + 时区偏移 / The midnight timestamp (seconds) of today + time zone offset</returns>
        public static long GetTodayStartTimestampWithTimeZone()
        {
            var date = GetTodayStartTimeWithTimeZone();
            return DateTimeToSecondsWithTimeZone(date);
        }

        /// <summary>
        /// 获取今天结束时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of today (based on set time zone).
        /// This method returns the last second (23:59:59) of the current day.
        /// Calculated by getting the midnight time of tomorrow and subtracting 1 second.
        /// Returns the time in the <see cref="CurrentTimeZone"/> time zone.
        /// </remarks>
        /// <returns>今天23:59:59的时间 / The time at 23:59:59 today</returns>
        public static DateTime GetTodayEndTimeWithTimeZone()
        {
            return GetTodayStartTimeWithTimeZone().AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取今天结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of today (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <returns>今天23:59:59的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 23:59:59 today + time zone offset</returns>
        public static long GetTodayEndTimestampWithTimeZone()
        {
            var date = GetTodayEndTimeWithTimeZone();
            return DateTimeToSecondsWithTimeZone(date);
        }

        /// <summary>
        /// 获取明天开始时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of tomorrow (based on set time zone).
        /// This method returns the midnight time of tomorrow.
        /// For example: if today is 2024-01-10, returns 2024-01-11 00:00:00.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>明天零点时间 / The midnight time of tomorrow</returns>
        public static DateTime GetTomorrowStartTimeWithTimeZone()
        {
            return GetTodayStartTimeWithTimeZone().AddDays(1);
        }

        /// <summary>
        /// 获取明天开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of tomorrow (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <returns>明天零点时间戳(秒) + 时区偏移 / The midnight timestamp (seconds) of tomorrow + time zone offset</returns>
        public static long GetTomorrowStartTimestampWithTimeZone()
        {
            return DateTimeToSecondsWithTimeZone(GetTomorrowStartTimeWithTimeZone());
        }

        /// <summary>
        /// 获取明天结束时间。
        /// </summary>
        /// <remarks>
        /// Gets the end time of tomorrow.
        /// This method returns the last second of tomorrow.
        /// For example: if today is 2024-01-10, returns 2024-01-11 23:59:59.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>明天23:59:59的时间 / The time at 23:59:59 tomorrow</returns>
        public static DateTime GetTomorrowEndTimeWithTimeZone()
        {
            return GetNowWithTimeZone().Date.AddDays(2).AddSeconds(-1);
        }

        /// <summary>
        /// 获取明天结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of tomorrow (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <returns>明天23:59:59的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 23:59:59 tomorrow + time zone offset</returns>
        public static long GetTomorrowEndTimestampWithTimeZone()
        {
            return DateTimeToSecondsWithTimeZone(GetTomorrowEndTimeWithTimeZone());
        }

        /// <summary>
        /// 获取两个时间戳之间跨越的天数（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed between two timestamps (based on set time zone).
        /// </remarks>
        /// <param name="beginTimestamp">起始时间戳,从1970年1月1日以来经过的秒数 / Start timestamp, number of seconds elapsed since January 1, 1970</param>
        /// <param name="hour">小时阈值 / Hour threshold</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        public static int GetCrossDaysWithTimeZone(long beginTimestamp, int hour = 0)
        {
            var begin = TimestampSecondToDateTime(beginTimestamp);
            return GetCrossDaysWithTimeZone(begin, hour);
        }
    }
}