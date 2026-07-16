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

using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class TimerHelper
    {
        /// <summary>
        /// 计算指定Unix时间戳到当前时间经过了多少秒（基于UTC）。
        /// </summary>
        /// <remarks>
        /// Calculates how many seconds have passed from the specified Unix timestamp to the current time (based on UTC).
        /// This method directly uses Unix timestamps to calculate elapsed seconds.
        /// Uses <see cref="UnixTimeSeconds"/> to get the current UTC timestamp for calculation.
        /// More efficient than DateTime conversion methods.
        /// Suitable for remaining time calculations for Unix timestamps.
        /// </remarks>
        /// <param name="timestamp">Unix时间戳（秒）。应为UTC时间戳 / Unix timestamp (seconds). Should be a UTC timestamp</param>
        /// <returns>经过的秒数。如果timestamp在未来，返回负数 / The number of seconds elapsed. Returns a negative number if timestamp is in the future</returns>
        [Preserve]
        public static long GetElapsedSecondsWithUtc(long timestamp)
        {
            var currentTimestamp = UnixTimeSeconds();
            return currentTimestamp - timestamp;
        }

        /// <summary>
        /// 计算指定Unix时间戳到当前时间经过了多少毫秒（基于UTC）。
        /// </summary>
        /// <remarks>
        /// Calculates how many milliseconds have passed from the specified Unix timestamp to the current time (based on UTC).
        /// This method directly uses Unix millisecond timestamps to calculate elapsed milliseconds.
        /// Uses <see cref="UnixTimeMilliseconds"/> to get the current UTC timestamp for calculation.
        /// More efficient than DateTime conversion methods.
        /// Suitable for remaining time calculations requiring millisecond-level precision.
        /// </remarks>
        /// <param name="timestampMs">Unix时间戳（毫秒）。应为UTC时间戳 / Unix timestamp (milliseconds). Should be a UTC timestamp</param>
        /// <returns>经过的毫秒数。如果timestampMs在未来，返回负数 / The number of milliseconds elapsed. Returns a negative number if timestampMs is in the future</returns>
        [Preserve]
        public static long GetElapsedMillisecondsWithUtc(long timestampMs)
        {
            var currentTimestamp = UnixTimeMilliseconds();
            return currentTimestamp - timestampMs;
        }
    }
}