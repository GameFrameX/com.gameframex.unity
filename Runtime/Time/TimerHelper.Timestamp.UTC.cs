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
        /// 将给定的时间戳转换为相对于 UTC 纪元的 TimeSpan 对象。
        /// </summary>
        /// <remarks>
        /// Converts the given timestamp to a TimeSpan object relative to the UTC epoch.
        /// </remarks>
        /// <param name="timestamp">自1970年1月1日午夜以来经过的秒数 / Number of seconds elapsed since midnight January 1, 1970</param>
        /// <returns>一个 TimeSpan 对象，表示从 UTC 纪元到给定时间戳的间隔 / A TimeSpan object representing the interval from the UTC epoch to the given timestamp</returns>
        /// <exception cref="ArgumentOutOfRangeException">当时间戳超出有效范围时抛出 / Thrown when the timestamp exceeds the valid range</exception>
        public static TimeSpan TimeSpanWithTimestampUtc(long timestamp)
        {
            if (timestamp < -62135596800L || timestamp > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp), "Timestamp is out of valid range for DateTime conversion.");
            }

            return TimeSpan.FromSeconds(timestamp);
        }

        /// <summary>
        /// 将给定的毫秒时间戳转换为相对于 UTC 纪元的 TimeSpan 对象。
        /// </summary>
        /// <remarks>
        /// Converts the given millisecond timestamp to a TimeSpan object relative to the UTC epoch.
        /// </remarks>
        /// <param name="timestampMilliseconds">自1970年1月1日午夜以来经过的毫秒数 / Number of milliseconds elapsed since midnight January 1, 1970</param>
        /// <returns>一个 TimeSpan 对象，表示从 UTC 纪元到给定毫秒时间戳的间隔 / A TimeSpan object representing the interval from the UTC epoch to the given millisecond timestamp</returns>
        /// <exception cref="ArgumentOutOfRangeException">当毫秒时间戳超出有效范围时抛出 / Thrown when the millisecond timestamp exceeds the valid range</exception>
        public static TimeSpan TimeSpanWithTimestampUtcMs(long timestampMilliseconds)
        {
            if (timestampMilliseconds < -62135596800000L || timestampMilliseconds > 253402300799999L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampMilliseconds), "Timestamp is out of valid range for DateTime conversion.");
            }

            return TimeSpan.FromMilliseconds(timestampMilliseconds);
        }
    }
}