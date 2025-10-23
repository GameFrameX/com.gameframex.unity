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
    public partial class TimerHelper
    {
        /// <summary>
        /// 时区偏移秒数。用于调整时间计算的偏移量。
        /// 正值表示向未来偏移,负值表示向过去偏移。
        /// </summary>
        public static long TimeOffsetSeconds { get; private set; } = 0;

        /// <summary>
        /// 时区偏移毫秒数。用于调整时间计算的偏移量。
        /// 正值表示向未来偏移,负值表示向过去偏移。
        /// </summary>
        public static long TimeOffsetMilliseconds { get; private set; } = 0;

        /// <summary>
        /// 设置时区偏移值
        /// </summary>
        /// <param name="offsetSeconds">秒级偏移量</param>
        /// <param name="offsetMilliseconds">毫秒级偏移量</param>
        /// <remarks>
        /// 此方法用于调整时间计算的基准。
        /// 例如要模拟未来时间,可以传入正数;要模拟过去时间,可以传入负数。
        /// 通常用于调试和测试场景。
        /// </remarks>
        public static void SetTimeOffset(long offsetSeconds, long offsetMilliseconds)
        {
            TimeOffsetSeconds = offsetSeconds;
            TimeOffsetMilliseconds = offsetMilliseconds;
        }

        /// <summary>
        /// 重置时区偏移值为默认值(0)
        /// </summary>
        /// <remarks>
        /// 此方法会将秒级和毫秒级的偏移量都重置为0,
        /// 使时间计算恢复到未经调整的状态。
        /// </remarks>
        public static void ResetTimeOffset()
        {
            TimeOffsetSeconds = default;
            TimeOffsetMilliseconds = default;
        }

        /// <summary>
        /// 获取当前UTC时间的秒级时间戳
        /// </summary>
        /// <returns>返回自1970年1月1日 00:00:00 UTC以来经过的秒数,加上时区偏移量</returns>
        /// <remarks>
        /// 此方法:
        /// 1. 获取当前UTC时间
        /// 2. 转换为Unix时间戳(秒)
        /// 3. 加上TimeOffsetSeconds偏移量
        /// 主要用于需要UTC时间戳的场景,如跨时区业务
        /// </remarks>
        public static long UnixTimeSecondsWithOffset()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前UTC时间的毫秒级时间戳
        /// </summary>
        /// <returns>返回自1970年1月1日 00:00:00 UTC以来经过的毫秒数,加上时区偏移量</returns>
        /// <remarks>
        /// 此方法:
        /// 1. 获取当前UTC时间
        /// 2. 转换为Unix时间戳(毫秒)
        /// 3. 加上TimeOffsetMilliseconds偏移量
        /// 相比秒级时间戳提供更高的精度,适用于需要精确时间计算的场景
        /// </remarks>
        public static long UnixTimeMillisecondsWithOffset()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }

        /// <summary>
        /// 获取当前本地时区时间的秒级时间戳
        /// </summary>
        /// <returns>返回自1970年1月1日 00:00:00以来经过的秒数(本地时区),加上时区偏移量</returns>
        /// <remarks>
        /// 此方法:
        /// 1. 获取当前本地时区时间
        /// 2. 转换为Unix时间戳(秒)
        /// 3. 加上TimeOffsetSeconds偏移量
        /// 主要用于需要本地时区时间戳的场景
        /// </remarks>
        public static long TimeSecondsWithOffset()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() + TimeOffsetSeconds;
        }

        /// <summary>
        /// 获取当前本地时区时间的毫秒级时间戳
        /// </summary>
        /// <returns>返回自1970年1月1日 00:00:00以来经过的毫秒数(本地时区),加上时区偏移量</returns>
        /// <remarks>
        /// 此方法:
        /// 1. 获取当前本地时区时间
        /// 2. 转换为Unix时间戳(毫秒)
        /// 3. 加上TimeOffsetMilliseconds偏移量
        /// 相比秒级时间戳提供更高的精度,适用于需要精确时间计算的场景
        /// </remarks>
        public static long TimeMillisecondsWithOffset()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() + TimeOffsetMilliseconds;
        }
    }
}