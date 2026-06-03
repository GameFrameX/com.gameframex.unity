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
        /// 获取指定日期所在月的开始时间。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the month for the specified date.
        /// This method returns the midnight time of the first day of the month for the specified date.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月1号00:00:00的时间 / The time at 00:00:00 on the 1st of the month</returns>
        public static DateTime GetStartTimeOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获取指定日期所在月的结束时间。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the month for the specified date.
        /// This method returns the last second of the last day of the month for the specified date.
        /// Preserves the original time zone.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月最后一天23:59:59的时间 / The time at 23:59:59 on the last day of the month</returns>
        public static DateTime GetEndTimeOfMonth(DateTime date)
        {
            return GetStartTimeOfMonth(date).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期所在月的开始时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the month for the specified date.
        /// This method returns the Unix timestamp of the midnight time on the first day of the month for the specified date.
        /// Uses the current time zone (<see cref="CurrentTimeZone"/>) to calculate the offset and converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月1号00:00:00的时间戳(秒) / The timestamp (seconds) at 00:00:00 on the 1st of the month</returns>
        public static long GetStartTimestampOfMonth(DateTime date)
        {
            var time = GetStartTimeOfMonth(date);
            return DateTimeToUnixTimeSeconds(time);
        }

        /// <summary>
        /// 获取指定日期所在月的开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the month for the specified date (based on set time zone).
        /// This method returns the Unix timestamp of the midnight time on the first day of the month for the specified date.
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月1号00:00:00的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 00:00:00 on the 1st of the month + time zone offset</returns>
        public static long GetStartTimestampOfMonthWithTimeZone(DateTime date)
        {
            return DateTimeToSecondsWithTimeZone(GetStartTimeOfMonth(date));
        }

        /// <summary>
        /// 获取指定日期所在月的结束时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the month for the specified date.
        /// This method returns the Unix timestamp of the last second of the last day of the month for the specified date.
        /// Uses the current time zone (<see cref="CurrentTimeZone"/>) to calculate the offset and converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月最后一天23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 on the last day of the month</returns>
        public static long GetEndTimestampOfMonth(DateTime date)
        {
            var time = GetEndTimeOfMonth(date);
            return DateTimeToUnixTimeSeconds(time);
        }

        /// <summary>
        /// 获取指定日期所在月的结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the month for the specified date (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在月最后一天23:59:59的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 23:59:59 on the last day of the month + time zone offset</returns>
        public static long GetEndTimestampOfMonthWithTimeZone(DateTime date)
        {
            return DateTimeToSecondsWithTimeZone(GetEndTimeOfMonth(date));
        }
    }
}