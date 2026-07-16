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
        /// 获取当前UTC时区的日期，格式为yyyyMMdd的整数。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC date as an integer in yyyyMMdd format.
        /// This method converts the current UTC time to an 8-digit format:
        /// - First 4 digits represent the year
        /// - Middle 2 digits represent the month
        /// - Last 2 digits represent the day
        /// Uses DateTime.UtcNow to get UTC time.
        /// </remarks>
        /// <returns>返回一个8位整数，表示当前UTC时区的日期。例如：20231225表示2023年12月25日 / Returns an 8-digit integer representing the current UTC date. For example: 20231225 represents December 25, 2023</returns>
        [Preserve]
        public static int CurrentDateWithUtcDay()
        {
            return Convert.ToInt32(GetNowWithUtc().ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 获取两个UTC时间戳之间跨越的天数（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed between two UTC timestamps (based on UTC time).
        /// </remarks>
        /// <param name="beginUnixTimestamp">开始时间戳(秒)，从1970年1月1日以来经过的秒数 / Start timestamp (seconds), number of seconds elapsed since January 1, 1970</param>
        /// <param name="afterUnixTimestamp">结束时间戳(秒)，从1970年1月1日以来经过的秒数 / End timestamp (seconds), number of seconds elapsed since January 1, 1970</param>
        /// <param name="hour">小时阈值 / Hour threshold</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        [Preserve]
        public static int GetCrossDaysUtc(long beginUnixTimestamp, long afterUnixTimestamp, int hour = 0)
        {
            var begin = TimestampSecondToDateTime(beginUnixTimestamp, true);
            var after = TimestampSecondToDateTime(afterUnixTimestamp, true);
            return GetCrossDays(begin, after, hour);
        }

        /// <summary>
        /// 获取从指定日期到当前UTC日期之间跨越的天数（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed from the specified date to the current UTC date (based on UTC time).
        /// </remarks>
        /// <param name="startTime">起始日期 / The start date</param>
        /// <param name="hour">小时阈值 / Hour threshold</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        [Preserve]
        public static int GetCrossDaysWithUtc(DateTime startTime, int hour = 0)
        {
            return GetCrossDays(startTime, GetNowWithUtc(), hour);
        }

        /// <summary>
        /// 获取今天开始时间（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of today (based on UTC time).
        /// This method returns the midnight time (00:00:00) of the current day.
        /// Uses <see cref="GetNowWithUtc"/> to get the midnight time of the current date.
        /// Returns UTC time.
        /// </remarks>
        /// <returns>今天零点时间 / The midnight time of today</returns>
        [Preserve]
        public static DateTime GetTodayStartTimeWithUtc()
        {
            var dateTime = GetNowWithUtc();
            return dateTime.Date;
        }

        /// <summary>
        /// 获取今天开始时间戳（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of today (based on UTC time).
        /// This method returns the Unix timestamp of the midnight time of the current day.
        /// First gets the UTC midnight time of today, then converts to timestamp.
        /// Returns the number of seconds from 1970-01-01 00:00:00 UTC.
        /// </remarks>
        /// <returns>今天零点时间戳(秒) / The midnight timestamp (seconds) of today</returns>
        [Preserve]
        public static long GetTodayStartTimestampWithUtc()
        {
            var date = GetTodayStartTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取今天结束时间（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of today (based on UTC time).
        /// This method returns the last second (23:59:59) of the current day.
        /// Calculated by getting the midnight time of tomorrow and subtracting 1 second.
        /// Returns UTC time.
        /// </remarks>
        /// <returns>今天23:59:59的时间 / The time at 23:59:59 today</returns>
        [Preserve]
        public static DateTime GetTodayEndTimeWithUtc()
        {
            return GetTodayStartTimeWithUtc().AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取今天结束时间戳（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of today (based on UTC time).
        /// This method returns the Unix timestamp of the last second of the current day.
        /// First gets the UTC time at 23:59:59 today, then converts to timestamp.
        /// Returns the number of seconds from 1970-01-01 00:00:00 UTC.
        /// </remarks>
        /// <returns>今天23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 today</returns>
        [Preserve]
        public static long GetTodayEndTimestampWithUtc()
        {
            var date = GetTodayEndTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取明天开始时间（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of tomorrow (based on UTC time).
        /// This method returns the midnight time of tomorrow.
        /// For example: if today is 2024-01-10, returns 2024-01-11 00:00:00.
        /// Uses UTC time for calculation.
        /// </remarks>
        /// <returns>明天零点时间 / The midnight time of tomorrow</returns>
        [Preserve]
        public static DateTime GetTomorrowStartTimeWithUtc()
        {
            return GetTodayStartTimeWithUtc().AddDays(1);
        }

        /// <summary>
        /// 获取明天开始时间戳（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of tomorrow (based on UTC time).
        /// This method returns the Unix timestamp of the midnight time of tomorrow.
        /// For example: if today is 2024-01-10, returns the timestamp of 2024-01-11 00:00:00.
        /// Uses UTC time for calculation and converts to timestamp.
        /// </remarks>
        /// <returns>明天零点时间戳(秒) / The midnight timestamp (seconds) of tomorrow</returns>
        [Preserve]
        public static long GetTomorrowStartTimestampWithUtc()
        {
            var date = GetTomorrowStartTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取明天结束时间（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of tomorrow (based on UTC time).
        /// This method returns the last second of tomorrow.
        /// For example: if today is 2024-01-10, returns 2024-01-11 23:59:59.
        /// Uses UTC time for calculation.
        /// </remarks>
        /// <returns>明天23:59:59的时间 / The time at 23:59:59 tomorrow</returns>
        [Preserve]
        public static DateTime GetTomorrowEndTimeWithUtc()
        {
            return GetNowWithUtc().Date.AddDays(2).AddSeconds(-1);
        }

        /// <summary>
        /// 获取明天结束时间戳（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of tomorrow (based on UTC time).
        /// This method returns the Unix timestamp of the last second of tomorrow.
        /// For example: if today is 2024-01-10, returns the timestamp of 2024-01-11 23:59:59.
        /// Converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <returns>明天23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 tomorrow</returns>
        [Preserve]
        public static long GetTomorrowEndTimestampWithUtc()
        {
            return DateTimeToUnixTimeSeconds(GetTomorrowEndTimeWithUtc());
        }

        /// <summary>
        /// 获取两个时间戳之间跨越的天数（基于UTC时间）。
        /// </summary>
        /// <remarks>
        /// Gets the number of days crossed between two timestamps (based on UTC time).
        /// </remarks>
        /// <param name="beginTimestamp">起始时间戳,从1970年1月1日以来经过的秒数 / Start timestamp, number of seconds elapsed since January 1, 1970</param>
        /// <param name="hour">小时阈值 / Hour threshold</param>
        /// <returns>跨越的天数 / The number of days crossed</returns>
        [Preserve]
        public static int GetCrossDaysWithUtc(long beginTimestamp, int hour = 0)
        {
            var begin = TimestampSecondToDateTime(beginTimestamp);
            return GetCrossDaysWithUtc(begin, hour);
        }
    }
}