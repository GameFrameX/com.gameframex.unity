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
        /// 获取本年开始时间
        /// </summary>
        /// <returns>本年1月1日零点时间</returns>
        /// <remarks>
        /// 此方法基于UTC时间计算年份:
        /// 1. 获取当前UTC时间的年份
        /// 2. 返回该年份1月1日零点时间
        /// 
        /// 示例:
        /// - 当前UTC时间为2024-03-15 14:30:00
        /// - 返回2024-01-01 00:00:00
        /// 
        /// 注意:
        /// - 返回的是UTC时间,不考虑本地时区
        /// - 返回时间的时分秒毫秒都为0
        /// - 使用DateTime.UtcNow避免时区转换带来的问题
        /// </remarks>
        public static DateTime GetYearStartTime()
        {
            return new DateTime(DateTime.UtcNow.Year, 1, 1);
        }

        /// <summary>
        /// 获取本年开始时间戳
        /// </summary>
        /// <returns>本年1月1日零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回当前年份1月1日零点的Unix时间戳
        /// 使用本地时区计算时间
        /// 例如:2024年返回2024-01-01 00:00:00的时间戳
        /// </remarks>
        public static long GetYearStartTimestamp()
        {
            return new DateTimeOffset(GetYearStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取本年结束时间
        /// </summary>
        /// <returns>本年12月31日23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回当前年份最后一天的最后一秒
        /// 使用本地时区计算时间
        /// 例如:2024年返回2024-12-31 23:59:59
        /// </remarks>
        public static DateTime GetYearEndTime()
        {
            return GetYearStartTime().AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本年结束时间戳
        /// </summary>
        /// <returns>本年12月31日23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回当前年份最后一天的最后一秒的Unix时间戳
        /// 使用本地时区计算时间
        /// 例如:2024年返回2024-12-31 23:59:59的时间戳
        /// </remarks>
        public static long GetYearEndTimestamp()
        {
            return new DateTimeOffset(GetYearEndTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取指定日期所在年的开始时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在年1月1日零点时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在年份的1月1日零点时间
        /// 例如:输入2024-01-10,返回2024-01-01 00:00:00
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetStartTimeOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定日期所在年的开始时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在年1月1日零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在年份的1月1日零点时间的Unix时间戳
        /// 例如:输入2024-01-10,返回2024-01-01 00:00:00的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetStartTimestampOfYear(DateTime date)
        {
            return new DateTimeOffset(GetStartTimeOfYear(date)).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取指定日期所在年的结束时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在年12月31日23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在年份的12月31日最后一秒
        /// 例如:输入2024-01-10,返回2024-12-31 23:59:59
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetEndTimeOfYear(DateTime date)
        {
            return GetStartTimeOfYear(date).AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期所在年的结束时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在年12月31日23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在年份的12月31日最后一秒的Unix时间戳
        /// 例如:输入2024-01-10,返回2024-12-31 23:59:59的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetEndTimestampOfYear(DateTime date)
        {
            return new DateTimeOffset(GetEndTimeOfYear(date)).ToUnixTimeSeconds();
        }
    }
}