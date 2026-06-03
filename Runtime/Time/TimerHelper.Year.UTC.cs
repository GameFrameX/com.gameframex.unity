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
        /// 获取今年开始时间 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the current year (UTC).
        /// This method returns the midnight time of the first day of the current year.
        /// Uses UTC time zone for calculation.
        /// </remarks>
        /// <returns>今年1月1号00:00:00的时间 (UTC) / The time at 00:00:00 on January 1st of the current year (UTC)</returns>
        [Preserve]
        public static DateTime GetYearStartTimeWithUtc()
        {
            var now = GetNowWithUtc();
            return new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// 获取今年开始时间戳 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the current year (UTC).
        /// This method returns the Unix timestamp of the midnight time on the first day of the current year.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <returns>今年1月1号00:00:00的时间戳(秒) (UTC) / The timestamp (seconds) at 00:00:00 on January 1st of the current year (UTC)</returns>
        [Preserve]
        public static long GetYearStartTimestampWithUtc()
        {
            var date = GetYearStartTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取今年结束时间 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the end time of the current year (UTC).
        /// This method returns the last second of the last day of the current year.
        /// Uses UTC time zone for calculation.
        /// </remarks>
        /// <returns>今年12月31号23:59:59的时间 (UTC) / The time at 23:59:59 on December 31st of the current year (UTC)</returns>
        [Preserve]
        public static DateTime GetYearEndTimeWithUtc()
        {
            var now = GetNowWithUtc();
            return GetStartTimeOfYear(now).AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取今年结束时间戳 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the current year (UTC).
        /// This method returns the Unix timestamp of the last second of the last day of the current year.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <returns>今年12月31号23:59:59的时间戳(秒) (UTC) / The timestamp (seconds) at 23:59:59 on December 31st of the current year (UTC)</returns>
        [Preserve]
        public static long GetYearEndTimestampWithUtc()
        {
            var date = GetYearEndTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取明年开始时间 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the start time of the next year (UTC).
        /// This method returns the midnight time of the first day of the next year.
        /// Uses UTC time zone for calculation.
        /// </remarks>
        /// <returns>明年1月1号00:00:00的时间 (UTC) / The time at 00:00:00 on January 1st of the next year (UTC)</returns>
        [Preserve]
        public static DateTime GetNextYearStartTimeWithUtc()
        {
            return GetYearStartTimeWithUtc().AddYears(1);
        }

        /// <summary>
        /// 获取明年开始时间戳 (UTC)。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the next year (UTC).
        /// This method returns the Unix timestamp of the midnight time on the first day of the next year.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <returns>明年1月1号00:00:00的时间戳(秒) (UTC) / The timestamp (seconds) at 00:00:00 on January 1st of the next year (UTC)</returns>
        [Preserve]
        public static long GetNextYearStartTimestampWithUtc()
        {
            var date = GetNextYearStartTimeWithUtc();
            return DateTimeToUnixTimeSeconds(date);
        }

        /// <summary>
        /// 获取指定日期所在年的结束时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the end timestamp of the year for the specified date (UTC).
        /// This method returns the Unix timestamp of the last second of the last day of the year for the specified date.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在年12月31号23:59:59的时间戳(秒)（UTC） / The timestamp (seconds) at 23:59:59 on December 31st of the year (UTC)</returns>
        [Preserve]
        public static long GetEndTimestampOfYearWithUtc(DateTime date)
        {
            var endTime = GetEndTimeOfYear(date);
            return DateTimeToUnixTimeSeconds(new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second, DateTimeKind.Utc));
        }

        /// <summary>
        /// 获取指定日期所在年的开始时间戳（UTC）。
        /// </summary>
        /// <remarks>
        /// Gets the start timestamp of the year for the specified date (UTC).
        /// This method returns the Unix timestamp of the midnight time on the first day of the year for the specified date.
        /// Based on UTC time calculation.
        /// </remarks>
        /// <param name="date">指定日期 / The specified date</param>
        /// <returns>所在年1月1号00:00:00的时间戳(秒)（UTC） / The timestamp (seconds) at 00:00:00 on January 1st of the year (UTC)</returns>
        [Preserve]
        public static long GetStartTimestampOfYearWithUtc(DateTime date)
        {
            var startTime = GetStartTimeOfYear(date);
            return DateTimeToUnixTimeSeconds(new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}