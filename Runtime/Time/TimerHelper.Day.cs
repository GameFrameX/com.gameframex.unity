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
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class TimerHelper
    {
        /// <summary>
        /// 判断两个 <see cref="DateTime"/> 是否表示同一日历日期。
        /// </summary>
        /// <remarks>
        /// Determines whether two <see cref="DateTime"/> values represent the same calendar date.
        /// This method only compares the year, month, and day parts, ignoring time components such as hours, minutes, seconds, and milliseconds.
        /// This comparison does not perform time zone conversion and directly uses the date values of the passed <see cref="DateTime"/>.
        /// </remarks>
        /// <param name="timeA">要比较的第一个时间 / The first time to compare</param>
        /// <param name="timeB">要比较的第二个时间 / The second time to compare</param>
        /// <returns>如果两个时间在同一天内，则返回 <c>true</c>；否则返回 <c>false</c> / Returns <c>true</c> if both times are on the same day; otherwise returns <c>false</c></returns>
        /// <example>
        /// <code>
        /// DateTime morning = new DateTime(2024, 1, 10, 8, 30, 0);
        /// DateTime evening = new DateTime(2024, 1, 10, 20, 45, 30);
        /// bool sameDay1 = TimerHelper.IsSameDay(morning, evening);
        /// Console.WriteLine(sameDay1); // True
        ///
        /// DateTime today = new DateTime(2024, 1, 10, 23, 59, 59);
        /// DateTime tomorrow = new DateTime(2024, 1, 11, 0, 0, 1);
        /// bool sameDay2 = TimerHelper.IsSameDay(today, tomorrow);
        /// Console.WriteLine(sameDay2); // False
        /// </code>
        /// </example>
        /// <seealso cref="DateTime.Date"/>
        /// <seealso cref="DateTime.Year"/>
        /// <seealso cref="DateTime.Month"/>
        /// <seealso cref="DateTime.Day"/>
        [Preserve]
        public static bool IsSameDay(DateTime timeA, DateTime timeB)
        {
            return timeA.Date.Year == timeB.Date.Year && timeA.Date.Month == timeB.Date.Month && timeA.Date.Day == timeB.Date.Day;
        }

        /// <summary>
        /// 获取两个时间之间的天数差。
        /// </summary>
        /// <remarks>
        /// Gets the number of days difference between two times.
        /// The return value comes from <see cref="TimeSpan.TotalDays"/> and includes the decimal part.
        /// If <paramref name="endTime"/> is earlier than <paramref name="startTime"/>, the result is negative.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>天数差，使用 <see cref="double"/> 表示，可能为负数 / The number of days difference, represented as <see cref="double"/>, may be negative</returns>
        /// <example>
        /// <code>
        /// DateTime start = new DateTime(2024, 1, 10, 8, 0, 0);
        /// DateTime end = new DateTime(2024, 1, 11, 20, 0, 0);
        /// double days = TimerHelper.GetDaysDifference(start, end);
        /// Console.WriteLine(days); // 1.5
        /// </code>
        /// </example>
        /// <seealso cref="TimeSpan.TotalDays"/>
        [Preserve]
        public static double GetDaysDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalDays;
        }

        /// <summary>
        /// 获取两个日期之间跨越的天数。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed between two dates.
        /// This method first calculates the day difference between two dates (ignoring the specific time part), then adjusts the result based on the hour threshold.
        /// If the hour of <paramref name="startTime"/> is less than the threshold, the result is incremented by one.
        /// If the hour of <paramref name="endTime"/> is less than the threshold, the result is decremented by one.
        /// </remarks>
        /// <param name="startTime">起始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <param name="hour">用于判定跨日的小时阈值，有效范围为 0-23 / The hour threshold for determining day crossing, valid range is 0-23</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        /// <exception cref="ArgumentOutOfRangeException">当 <paramref name="hour"/> 不在 0-23 范围内时抛出 / Thrown when <paramref name="hour"/> is not in the 0-23 range</exception>
        /// <example>
        /// <code>
        /// DateTime start = new DateTime(2024, 1, 10, 3, 0, 0);
        /// DateTime end = new DateTime(2024, 1, 11, 2, 0, 0);
        /// int days = TimerHelper.GetCrossDays(start, end, 5);
        /// Console.WriteLine(days); // 0
        /// </code>
        /// </example>
        [Preserve]
        public static int GetCrossDays(DateTime startTime, DateTime endTime, int hour = 0)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "Hour must be between 0 and 23");
            }

            var days = (int)(endTime.Date - startTime.Date).TotalDays;
            if (startTime.Hour < hour)
            {
                days++;
            }

            if (endTime.Hour < hour)
            {
                days--;
            }

            return days;
        }

        /// <summary>
        /// 获取指定日期的开始时间。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the specified date.
        /// This method returns the midnight time (00:00:00) of the specified date.
        /// For example: input 2024-01-10 14:30:00, returns 2024-01-10 00:00:00.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期零点时间 / The midnight time of the specified date</returns>
        [Preserve]
        public static DateTime GetStartTimeOfDay(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// 获取指定日期的开始时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the specified date.
        /// This method returns the Unix timestamp of the midnight time of the specified date.
        /// For example: input 2024-01-10 14:30:00, returns the timestamp of 2024-01-10 00:00:00.
        /// Uses the current time zone (<see cref="CurrentTimeZone"/>) to calculate the offset and converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期零点时间戳(秒) / The midnight timestamp (seconds) of the specified date</returns>
        [Preserve]
        public static long GetStartTimestampOfDay(DateTime date)
        {
            var targetDate = GetStartTimeOfDay(date);
            return DateTimeToUnixTimeSeconds(targetDate);
        }


        /// <summary>
        /// 获取指定日期的开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the specified date (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期零点时间戳(秒) + 时区偏移 / The midnight timestamp (seconds) of the specified date + time zone offset</returns>
        [Preserve]
        public static long GetStartTimestampOfDayWithTimeZone(DateTime date)
        {
            return DateTimeToSecondsWithTimeZone(GetStartTimeOfDay(date));
        }

        /// <summary>
        /// 获取指定日期的结束时间。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the specified date.
        /// This method returns the last second (23:59:59) of the specified date.
        /// For example: input 2024-01-10 14:30:00, returns 2024-01-10 23:59:59.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期23:59:59的时间 / The time at 23:59:59 of the specified date</returns>
        [Preserve]
        public static DateTime GetEndTimeOfDay(DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期的结束时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the specified date.
        /// This method returns the Unix timestamp of the last second of the specified date.
        /// For example: input 2024-01-10 14:30:00, returns the timestamp of 2024-01-10 23:59:59.
        /// Uses the current time zone (<see cref="CurrentTimeZone"/>) to calculate the offset and converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 of the specified date</returns>
        [Preserve]
        public static long GetEndTimestampOfDay(DateTime date)
        {
            var targetDate = GetEndTimeOfDay(date);
            return DateTimeToUnixTimeSeconds(targetDate);
        }

        /// <summary>
        /// 获取指定日期的结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the specified date (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>指定日期23:59:59的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 23:59:59 of the specified date + time zone offset</returns>
        [Preserve]
        public static long GetEndTimestampOfDayWithTimeZone(DateTime date)
        {
            return DateTimeToSecondsWithTimeZone(GetEndTimeOfDay(date));
        }
    }
}