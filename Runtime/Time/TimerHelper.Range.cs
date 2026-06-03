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
        /// 获取指定时间是否在指定的时间范围内。
        /// </summary>
        /// <remarks>
        /// Gets whether the specified time is within the specified time range.
        /// This method uses closed interval comparison, i.e., returns true when time equals startTime or endTime.
        /// Does not check the order of startTime and endTime, caller must ensure startTime is not later than endTime.
        /// </remarks>
        /// <param name="time">指定时间。例如：2024-01-10 14:30:00 / The specified time. For example: 2024-01-10 14:30:00</param>
        /// <param name="startTime">开始时间。例如：2024-01-10 00:00:00 / The start time. For example: 2024-01-10 00:00:00</param>
        /// <param name="endTime">结束时间。例如：2024-01-10 23:59:59 / The end time. For example: 2024-01-10 23:59:59</param>
        /// <returns>如果指定时间在开始时间和结束时间之间（包含边界），则返回true；否则返回false / Returns true if the specified time is between start time and end time (inclusive); otherwise returns false</returns>
        public static bool IsTimeInRange(DateTime time, DateTime startTime, DateTime endTime)
        {
            return time >= startTime && time <= endTime;
        }

        /// <summary>
        /// 获取指定时间戳是否在指定的时间戳范围内。
        /// </summary>
        /// <remarks>
        /// Gets whether the specified timestamp is within the specified timestamp range.
        /// This method uses closed interval comparison, i.e., returns true when timestamp equals startTimestamp or endTimestamp.
        /// Does not check the order of startTimestamp and endTimestamp, caller must ensure startTimestamp is not greater than endTimestamp.
        /// Timestamps should be Unix second-level timestamps (seconds since January 1, 1970 UTC midnight).
        /// </remarks>
        /// <param name="timestamp">指定时间戳（Unix秒级时间戳）。例如：1704857400 / The specified timestamp (Unix seconds). For example: 1704857400</param>
        /// <param name="startTimestamp">开始时间戳（Unix秒级时间戳）。例如：1704816000 / The start timestamp (Unix seconds). For example: 1704816000</param>
        /// <param name="endTimestamp">结束时间戳（Unix秒级时间戳）。例如：1704902399 / The end timestamp (Unix seconds). For example: 1704902399</param>
        /// <returns>如果指定时间戳在开始时间戳和结束时间戳之间（包含边界），则返回true；否则返回false / Returns true if the specified timestamp is between start timestamp and end timestamp (inclusive); otherwise returns false</returns>
        public static bool IsTimestampInRange(long timestamp, long startTimestamp, long endTimestamp)
        {
            return timestamp >= startTimestamp && timestamp <= endTimestamp;
        }
    }
}