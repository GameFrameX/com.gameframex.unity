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
//  禁止利用本项目实施任何国家安全、破坏社会秩序、
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
        /// 获取当前时区 (<see cref="CurrentTimeZone"/>) 时间的秒级时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the current time zone (<see cref="CurrentTimeZone"/>) time as a second-level timestamp.
        /// This method:
        /// 1. Gets the current time zone (<see cref="CurrentTimeZone"/>) time
        /// 2. Converts to Unix timestamp (seconds)
        /// 3. Adds the TimeOffsetSeconds offset
        /// Mainly used for scenarios requiring current time zone (<see cref="CurrentTimeZone"/>) timestamps.
        /// </remarks>
        /// <returns>返回自1970年1月1日 00:00:00以来经过的秒数(当前时区),加上时区偏移量 / Returns the number of seconds elapsed since 1970-01-01 00:00:00 (current time zone), plus time zone offset</returns>
        [Preserve]
        public static long UnixTimeSecondsWithOffsetWithTimeZone()
        {
            return new DateTimeOffset(GetNowWithTimeZone()).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前时区 (<see cref="CurrentTimeZone"/>) 时间的毫秒级时间戳。
        /// </summary>
        /// <remarks>
        /// Gets the current time zone (<see cref="CurrentTimeZone"/>) time as a millisecond-level timestamp.
        /// This method:
        /// 1. Gets the current time zone (<see cref="CurrentTimeZone"/>) time
        /// 2. Converts to Unix timestamp (milliseconds)
        /// 3. Adds the TimeOffsetMilliseconds offset
        /// Provides higher precision than second-level timestamps, suitable for scenarios requiring precise time calculations.
        /// </remarks>
        /// <returns>返回自1970年1月1日 00:00:00以来经过的毫秒数(当前时区),加上时区偏移量 / Returns the number of milliseconds elapsed since 1970-01-01 00:00:00 (current time zone), plus time zone offset</returns>
        [Preserve]
        public static long UnixTimeMillisecondsWithOffsetWithTimeZone()
        {
            return new DateTimeOffset(GetNowWithTimeZone()).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }
    }
}