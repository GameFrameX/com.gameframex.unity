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
        /// 获取本周指定星期几的UTC时间。
        /// </summary>
        /// <remarks>
        /// Gets the UTC time of the specified day of the current week.
        /// This method calculates based on the current UTC time.
        /// The time part returned is consistent with the current UTC time, only the date is changed to the corresponding day of the current week.
        /// Note: "Current week" is defined based on UTC time.
        /// </remarks>
        /// <param name="day">星期几 (DayOfWeek.Sunday 到 DayOfWeek.Saturday) / Day of week (DayOfWeek.Sunday to DayOfWeek.Saturday)</param>
        /// <returns>本周指定星期几的UTC日期时间 / The UTC date time of the specified day of the current week</returns>
        [Preserve]
        public static DateTime GetDayOfWeekTime(DayOfWeek day)
        {
            return GetDayOfWeekTime(GetNowWithUtc(), day);
        }

        /// <summary>
        /// 获取下周开始时间。
        /// </summary>
        /// <remarks>
        /// Gets the start time of next week.
        /// This method returns the midnight time of the first day (Monday) of next week.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>下周周一00:00:00的时间 / The time at 00:00:00 on Monday of next week</returns>
        [Preserve]
        public static DateTime GetNextWeekStartTimeWithUtc()
        {
            var now = GetNowWithUtc();
            var dayOfWeek = (int)now.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return now.AddDays(1 - dayOfWeek + 7).Date;
        }

        /// <summary>
        /// 获取下周开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of next week (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <returns>下周周一00:00:00的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 00:00:00 on Monday of next week + time zone offset</returns>
        [Preserve]
        public static long GetNextWeekStartTimestampWithUtc()
        {
            return DateTimeToUnixTimeSeconds(GetNextWeekStartTimeWithUtc());
        }

        /// <summary>
        /// 获取下周结束时间。
        /// </summary>
        /// <remarks>
        /// Gets the end time of next week.
        /// This method returns the last second of the last day (Sunday) of next week.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>下周周日23:59:59的时间 / The time at 23:59:59 on Sunday of next week</returns>
        [Preserve]
        public static DateTime GetNextWeekEndTimeWithUtc()
        {
            return GetNextWeekStartTimeWithUtc().AddDays(7).AddSeconds(-1);
        }

        /// <summary>
        /// 获取下周结束时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of next week.
        /// This method returns the Unix timestamp of the last second of Sunday of next week.
        /// Converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <returns>下周周日23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 on Sunday of next week</returns>
        [Preserve]
        public static long GetNextWeekEndTimestampWithUtc()
        {
            var date = GetNextWeekEndTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取本周开始时间（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the current week (UTC).
        /// This method returns the midnight time of the first day (Monday) of the current week.
        /// Uses UTC time zone for calculation.
        /// </remarks>
        /// <returns>本周周一00:00:00的时间（UTC） / The time at 00:00:00 on Monday of the current week (UTC)</returns>
        [Preserve]
        public static DateTime GetWeekStartTimeWithUtc()
        {
            var now = GetNowWithUtc();
            var dayOfWeek = (int)now.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return now.AddDays(1 - dayOfWeek).Date;
        }

        /// <summary>
        /// 获取本周开始时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the current week (UTC).
        /// This method returns the Unix timestamp of the Monday midnight time of the current week.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <returns>本周周一00:00:00的时间戳(秒)（UTC） / The timestamp (seconds) at 00:00:00 on Monday of the current week (UTC)</returns>
        [Preserve]
        public static long GetWeekStartTimestampWithUtc()
        {
            var date = GetWeekStartTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取本周结束时间（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the current week (UTC).
        /// This method returns the last second of the last day (Sunday) of the current week.
        /// Uses UTC time zone for calculation.
        /// </remarks>
        /// <returns>本周周日23:59:59的时间（UTC） / The time at 23:59:59 on Sunday of the current week (UTC)</returns>
        [Preserve]
        public static DateTime GetWeekEndTimeWithUtc()
        {
            var now = GetNowWithUtc();
            var dayOfWeek = (int)now.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return now.AddDays(7 - dayOfWeek).Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本周结束时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the current week (UTC).
        /// This method returns the Unix timestamp of the last second of Sunday of the current week.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <returns>本周周日23:59:59的时间戳(秒)（UTC） / The timestamp (seconds) at 23:59:59 on Sunday of the current week (UTC)</returns>
        [Preserve]
        public static long GetWeekEndTimestampWithUtc()
        {
            var date = GetWeekEndTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取指定日期所在周的开始时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the week containing the specified date (UTC).
        /// This method returns the Unix timestamp of the Monday midnight time of the week containing the specified date.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在周周一00:00:00的时间戳(秒)（UTC） / The timestamp (seconds) at 00:00:00 on Monday of the week (UTC)</returns>
        [Preserve]
        public static long GetStartTimestampOfWeekWithUtc(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            var monday = date.AddDays(1 - dayOfWeek).Date;
            return DateTimeToUnixTimeSeconds(monday);
        }

        /// <summary>
        /// 获取指定日期所在周的结束时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the week containing the specified date (UTC).
        /// This method returns the Unix timestamp of the last second of Sunday of the week containing the specified date.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在周周日23:59:59的时间戳(秒)（UTC） / The timestamp (seconds) at 23:59:59 on Sunday of the week (UTC)</returns>
        [Preserve]
        public static long GetEndTimestampOfWeekWithUtc(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            var sunday = date.AddDays(7 - dayOfWeek).Date.AddDays(1).AddSeconds(-1);
            return DateTimeToUnixTimeSeconds(sunday);
        }
    }
}