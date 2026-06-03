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
        /// 获取当前UTC时间的秒级时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time as a second-level timestamp.
        /// This method:
        /// 1. Gets the current UTC time
        /// 2. Converts to Unix timestamp (seconds)
        /// 3. Adds the TimeOffsetSeconds offset
        /// Mainly used for scenarios requiring UTC timestamps, such as cross-timezone business.
        /// </remarks>
        /// <returns>返回自1970年1月1日 00:00:00 UTC以来经过的秒数,加上时区偏移量 / Returns the number of seconds elapsed since 1970-01-01 00:00:00 UTC, plus time zone offset</returns>
        [Preserve]
        public static long UnixTimeSecondsWithOffset()
        {
            return new DateTimeOffset(GetNowWithUtc()).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前UTC时间的毫秒级时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time as a millisecond-level timestamp.
        /// This method:
        /// 1. Gets the current UTC time
        /// 2. Converts to Unix timestamp (milliseconds)
        /// 3. Adds the TimeOffsetMilliseconds offset
        /// Provides higher precision than second-level timestamps, suitable for scenarios requiring precise time calculations.
        /// </remarks>
        /// <returns>返回自1970年1月1日 00:00:00 UTC以来经过的毫秒数,加上时区偏移量 / Returns the number of milliseconds elapsed since 1970-01-01 00:00:00 UTC, plus time zone offset</returns>
        [Preserve]
        public static long UnixTimeMillisecondsWithOffset()
        {
            return new DateTimeOffset(GetNowWithUtc()).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }
    }
}