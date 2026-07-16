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
        /// 获取本月开始时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the current month (based on set time zone).
        /// This method returns the midnight time of the first day of the current month.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>本月1号00:00:00的时间 / The time at 00:00:00 on the 1st of the current month</returns>
        [Preserve]
        public static DateTime GetMonthStartTimeWithTimeZone()
        {
            var now = GetNowWithTimeZone();
            return new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// 获取本月开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the current month (based on set time zone).
        /// This method returns the Unix timestamp of the midnight time on the first day of the current month.
        /// Converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <returns>本月1号00:00:00的时间戳(秒) / The timestamp (seconds) at 00:00:00 on the 1st of the current month</returns>
        [Preserve]
        public static long GetMonthStartTimestampWithTimeZone()
        {
            var date = GetMonthStartTimeWithTimeZone();
            return DateTimeToSecondsWithTimeZone(date);
        }

        /// <summary>
        /// 获取本月结束时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the current month (based on set time zone).
        /// This method returns the last second of the last day of the current month.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>本月最后一天23:59:59的时间 / The time at 23:59:59 on the last day of the current month</returns>
        [Preserve]
        public static DateTime GetMonthEndTimeWithTimeZone()
        {
            var now = GetNowWithTimeZone();
            return GetStartTimeOfMonth(now).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本月结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the current month (based on set time zone).
        /// This method returns the Unix timestamp of the last second of the last day of the current month.
        /// Converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <returns>本月最后一天23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 on the last day of the current month</returns>
        [Preserve]
        public static long GetMonthEndTimestampWithTimeZone()
        {
            var date = GetMonthEndTimeWithTimeZone();
            return DateTimeToSecondsWithTimeZone(date);
        }

        /// <summary>
        /// 获取下个月开始时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the next month (based on set time zone).
        /// This method returns the midnight time of the first day of the next month.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>下个月1号00:00:00的时间 / The time at 00:00:00 on the 1st of the next month</returns>
        [Preserve]
        public static DateTime GetNextMonthStartTimeWithTimeZone()
        {
            return GetMonthStartTimeWithTimeZone().AddMonths(1);
        }

        /// <summary>
        /// 获取下个月开始时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the next month (based on set time zone).
        /// Return value = Standard Unix timestamp + time zone offset seconds.
        /// </remarks>
        /// <returns>下个月1号00:00:00的时间戳(秒) + 时区偏移 / The timestamp (seconds) at 00:00:00 on the 1st of the next month + time zone offset</returns>
        [Preserve]
        public static long GetNextMonthStartTimestampWithTimeZone()
        {
            return DateTimeToSecondsWithTimeZone(GetNextMonthStartTimeWithTimeZone());
        }

        /// <summary>
        /// 获取下个月结束时间（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the next month (based on set time zone).
        /// This method returns the last second of the last day of the next month.
        /// Uses the <see cref="CurrentTimeZone"/> time zone for calculation.
        /// </remarks>
        /// <returns>下个月最后一天23:59:59的时间 / The time at 23:59:59 on the last day of the next month</returns>
        [Preserve]
        public static DateTime GetNextMonthEndTimeWithTimeZone()
        {
            return GetNextMonthStartTimeWithTimeZone().AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取下个月结束时间戳（基于设置时区）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the next month (based on set time zone).
        /// This method returns the Unix timestamp of the last second of the last day of the next month.
        /// Converts the time to UTC before calculating the timestamp.
        /// </remarks>
        /// <returns>下个月最后一天23:59:59的时间戳(秒) / The timestamp (seconds) at 23:59:59 on the last day of the next month</returns>
        [Preserve]
        public static long GetNextMonthEndTimestampWithTimeZone()
        {
            var date = GetNextMonthEndTimeWithTimeZone();
            return DateTimeToSecondsWithTimeZone(date);
        }
    }
}