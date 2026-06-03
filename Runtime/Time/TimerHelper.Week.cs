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
        /// 判断指定时间戳是否与当前UTC时间是同一周。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified timestamp is in the same week as the current UTC time.
        /// This method uses UTC time for comparison.
        /// The input ticks will be converted to DateTime and compared with the current UTC time.
        /// </remarks>
        /// <param name="ticks">时间刻度(Ticks) / Time ticks</param>
        /// <param name="isUtc">是否使用UTC时间进行比较，默认值为true / Whether to use UTC time for comparison, defaults to true</param>
        /// <returns>如果是同一周返回true,否则返回false / Returns true if it same week, otherwise returns false</returns>
        [Preserve]
        public static bool IsSameWeek(long ticks, bool isUtc = true)
        {
            return IsSameWeek(new DateTime(ticks), isUtc);
        }

        /// <summary>
        /// 判断指定日期是否与当前UTC时间是同一周。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified date is in the same week as the current UTC time.
        /// This method uses UTC time for comparison.
        /// Uses <see cref="GetNowWithUtc"/> to get the current UTC time.
        /// The comparison logic is based on the <see cref="IsSameWeek(DateTime, DateTime)"/> method.
        /// </remarks>
        /// <param name="start">要比较的日期 / The date to compare</param>
        /// <param name="isUtc">是否使用UTC时间进行比较,默认值为true / Whether to use UTC time for comparison, defaults to true</param>
        /// <returns>如果是同一周返回true,否则返回false / Returns true if the same week, otherwise returns false</returns>
        [Preserve]
        public static bool IsSameWeek(DateTime start, bool isUtc = true)
        {
            return IsSameWeek(start, isUtc ? GetNowWithUtc() : GetNowWithTimeZone());
        }

        /// <summary>
        /// 判断两个日期是否在同一周。
        /// </summary>
        /// <remarks>
        /// Determines whether two dates are in the same week.
        /// This method determines whether two dates are in the same week by calculating the day difference from Monday for each date.
        /// Assumes Monday is the first day of the week.
        /// Algorithm logic:
        /// 1. Calculate the day of week for startTime (1-7)
        /// 2. Calculate the Monday date of the week containing startTime
        /// 3. Calculate the day of week for endTime (1-7)
        /// 4. Calculate the Monday date of the week containing endTime
        /// 5. Compare whether the two Monday dates are the same
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>如果是同一周返回true，否则返回false / Returns true if in the same week, otherwise returns false</returns>
        [Preserve]
        public static bool IsSameWeek(DateTime startTime, DateTime endTime)
        {
            var startDayOfWeek = (int)startTime.DayOfWeek;
            startDayOfWeek = startDayOfWeek == 0 ? 7 : startDayOfWeek;
            var startWeekMonday = startTime.AddDays(1 - startDayOfWeek).Date;

            var endDayOfWeek = (int)endTime.DayOfWeek;
            endDayOfWeek = endDayOfWeek == 0 ? 7 : endDayOfWeek;
            var endWeekMonday = endTime.AddDays(1 - endDayOfWeek).Date;

            return startWeekMonday == endWeekMonday;
        }

        /// <summary>
        /// 获取指定日期所在周的指定星期几的日期时间。
        /// </summary>
        /// <remarks>
        /// Gets the date time of the specified day of the week for the week containing the specified date.
        /// Calculation logic:
        /// 1. Get the day of week for the input date (DayOfWeek)
        /// 2. Calculate the difference between the target day and the current day
        /// 3. Add the difference to the input date to get the result
        /// For example:
        /// If input is Wednesday and requesting Monday, the difference is -2, returns date minus 2 days
        /// If input is Wednesday and requesting Friday, the difference is +2, returns date plus 2 days
        /// Note:
        /// - The week starts on Sunday (standard DayOfWeek definition)
        /// - Does not change the time part, only changes the date part
        /// </remarks>
        /// <param name="dateTime">指定日期 / The specified date</param>
        /// <param name="day">目标星期几 (DayOfWeek.Sunday 到 DayOfWeek.Saturday) / The target day of week (DayOfWeek.Sunday to DayOfWeek.Saturday)</param>
        /// <returns>计算结果日期时间 / The calculated date time</returns>
        [Preserve]
        public static DateTime GetDayOfWeekTime(DateTime dateTime, DayOfWeek day)
        {
            return dateTime.AddDays(day - dateTime.DayOfWeek);
        }

        /// <summary>
        /// 获取指定日期所在周的开始时间。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the week containing the specified date.
        /// This method returns the Monday midnight time of the week containing the specified date.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在周周一00:00:00的时间 / The time at 00:00:00 on Monday of the week</returns>
        [Preserve]
        public static DateTime GetStartTimeOfWeek(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return date.AddDays(1 - dayOfWeek).Date;
        }

        /// <summary>
        /// 获取指定日期所在周的结束时间。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the week containing the specified date.
        /// This method returns the last second of Sunday of the week containing the specified date.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在周周日23:59:59的时间 / The time at 23:59:59 on Sunday of the week</returns>
        [Preserve]
        public static DateTime GetEndTimeOfWeek(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return date.AddDays(7 - dayOfWeek).Date.AddDays(1).AddSeconds(-1);
        }
    }
}