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
        /// 计算两个DateTime之间的时间差。
        /// </summary>
        /// <remarks>
        /// Calculates the time difference between two DateTimes.
        /// Returns the time difference of endTime - startTime.
        /// If endTime is earlier than startTime, returns a negative TimeSpan.
        /// This method can be used to calculate the precise time interval between any two DateTimes.
        /// The returned TimeSpan object contains detailed information such as days, hours, minutes, seconds, and milliseconds.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>时间差TimeSpan对象 / The time difference TimeSpan object</returns>
        [Preserve]
        public static TimeSpan GetTimeDifference(DateTime startTime, DateTime endTime)
        {
            return endTime - startTime;
        }

        /// <summary>
        /// 获取两个时间之间的秒数差。
        /// </summary>
        /// <remarks>
        /// Gets the seconds difference between two times.
        /// This method returns the total seconds between two times.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// If endTime is earlier than startTime, returns a negative number.
        /// Suitable for scenarios requiring second-level time differences.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>秒数差（可能为负数） / The seconds difference (may be negative)</returns>
        [Preserve]
        public static long GetSecondsDifference(DateTime startTime, DateTime endTime)
        {
            return (long)(endTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// 获取两个时间之间的毫秒数差。
        /// </summary>
        /// <remarks>
        /// Gets the milliseconds difference between two times.
        /// This method returns the total milliseconds between two times.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// If endTime is earlier than startTime, returns a negative number.
        /// Suitable for scenarios requiring millisecond-level precise time differences.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>毫秒数差（可能为负数） / The milliseconds difference (may be negative)</returns>
        [Preserve]
        public static long GetMillisecondsDifference(DateTime startTime, DateTime endTime)
        {
            return (long)(endTime - startTime).TotalMilliseconds;
        }

        /// <summary>
        /// 获取两个时间之间的分钟数差。
        /// </summary>
        /// <remarks>
        /// Gets the minutes difference between two times.
        /// This method returns the total minutes between two times, preserving the decimal part.
        /// If endTime is earlier than startTime, returns a negative number.
        /// Returns a double type to maintain precision.
        /// Suitable for scenarios requiring minute-level time differences.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>分钟数差（可能为负数） / The minutes difference (may be negative)</returns>
        [Preserve]
        public static double GetMinutesDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalMinutes;
        }

        /// <summary>
        /// 获取两个时间之间的小时数差。
        /// </summary>
        /// <remarks>
        /// Gets the hours difference between two times.
        /// This method returns the total hours between two times, preserving the decimal part.
        /// If endTime is earlier than startTime, returns a negative number.
        /// Returns a double type to maintain precision.
        /// Suitable for scenarios requiring hour-level time differences.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>小时数差（可能为负数） / The hours difference (may be negative)</returns>
        [Preserve]
        public static double GetHoursDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalHours;
        }

        /// <summary>
        /// 获取两个时间戳之间的秒数差。
        /// </summary>
        /// <remarks>
        /// Gets the seconds difference between two timestamps.
        /// This method directly calculates the difference between two timestamps.
        /// No need to convert to DateTime, faster calculation.
        /// If endTimestamp is less than startTimestamp, returns a negative number.
        /// Suitable for second-level difference calculations for Unix timestamps.
        /// </remarks>
        /// <param name="startTimestamp">开始时间戳（秒） / The start timestamp (seconds)</param>
        /// <param name="endTimestamp">结束时间戳（秒） / The end timestamp (seconds)</param>
        /// <returns>秒数差 / The seconds difference</returns>
        [Preserve]
        public static long GetSecondsDifference(long startTimestamp, long endTimestamp)
        {
            return endTimestamp - startTimestamp;
        }

        /// <summary>
        /// 获取两个毫秒时间戳之间的毫秒数差。
        /// </summary>
        /// <remarks>
        /// Gets the milliseconds difference between two millisecond timestamps.
        /// This method directly calculates the difference between two millisecond timestamps.
        /// No need to convert to DateTime, faster calculation.
        /// If endTimestampMs is less than startTimestampMs, returns a negative number.
        /// Suitable for millisecond-level difference calculations for Unix timestamps.
        /// </remarks>
        /// <param name="startTimestampMs">开始时间戳（毫秒） / The start timestamp (milliseconds)</param>
        /// <param name="endTimestampMs">结束时间戳（毫秒） / The end timestamp (milliseconds)</param>
        /// <returns>毫秒数差 / The milliseconds difference</returns>
        [Preserve]
        public static long GetMillisecondsDifference(long startTimestampMs, long endTimestampMs)
        {
            return endTimestampMs - startTimestampMs;
        }

        /// <summary>
        /// 获取时间差的绝对值（秒）。
        /// </summary>
        /// <remarks>
        /// Gets the absolute value of time difference (seconds).
        /// This method returns the absolute seconds difference between two times.
        /// Regardless of whether endTime is earlier than startTime, returns a positive number.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// Suitable for scenarios that only need the time interval without caring about the order.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>时间差的绝对秒数 / The absolute seconds difference</returns>
        [Preserve]
        public static long GetAbsoluteSecondsDifference(DateTime startTime, DateTime endTime)
        {
            return System.Math.Abs(GetSecondsDifference(startTime, endTime));
        }

        /// <summary>
        /// 获取时间差的绝对值（毫秒）。
        /// </summary>
        /// <remarks>
        /// Gets the absolute value of time difference (milliseconds).
        /// This method returns the absolute milliseconds difference between two times.
        /// Regardless of whether endTime is earlier than startTime, returns a positive number.
        /// The result is converted to a long integer, which may lose decimal precision.
        /// Suitable for scenarios requiring millisecond-level precision without caring about the order.
        /// </remarks>
        /// <param name="startTime">开始时间 / The start time</param>
        /// <param name="endTime">结束时间 / The end time</param>
        /// <returns>时间差的绝对毫秒数 / The absolute milliseconds difference</returns>
        [Preserve]
        public static long GetAbsoluteMillisecondsDifference(DateTime startTime, DateTime endTime)
        {
            return System.Math.Abs(GetMillisecondsDifference(startTime, endTime));
        }
    }
}