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
        /// 获取指定日期所在月的开始时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在月1号零点时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在月份的1号零点时间
        /// 例如:输入2024-01-10,返回2024-01-01 00:00:00
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetStartTimeOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获取指定日期所在月的开始时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在月1号零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在月份的1号零点时间的Unix时间戳
        /// 例如:输入2024-01-10,返回2024-01-01 00:00:00的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetStartTimestampOfMonth(DateTime date)
        {
            return new DateTimeOffset(GetStartTimeOfMonth(date)).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取指定日期所在月的结束时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在月最后一天23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在月份的最后一天的最后一秒
        /// 例如:输入2024-01-10,返回2024-01-31 23:59:59
        /// 保持原有时区不变
        /// 自动处理大小月份和闰年
        /// </remarks>
        public static DateTime GetEndTimeOfMonth(DateTime date)
        {
            return GetStartTimeOfMonth(date).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期所在月的结束时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在月最后一天23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在月份的最后一天最后一秒的Unix时间戳
        /// 例如:输入2024-01-10,返回2024-01-31 23:59:59的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetEndTimestampOfMonth(DateTime date)
        {
            return new DateTimeOffset(GetEndTimeOfMonth(date)).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取下月开始时间戳
        /// </summary>
        /// <returns>下月1号零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回下个月1号零点时间的Unix时间戳
        /// 例如:当前是2024-01-10,返回2024-02-01 00:00:00的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetNextMonthStartTimestamp()
        {
            return new DateTimeOffset(GetNextMonthStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取下月结束时间
        /// </summary>
        /// <returns>下月最后一天23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回下个月最后一天的最后一秒
        /// 例如:当前是2024-01-10,返回2024-02-29 23:59:59
        /// 使用本地时区计算时间
        /// 自动处理大小月份和闰年
        /// </remarks>
        public static DateTime GetNextMonthEndTime()
        {
            return GetNextMonthStartTime().AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取下月结束时间戳
        /// </summary>
        /// <returns>下月最后一天23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回下个月最后一天最后一秒的Unix时间戳
        /// 例如:当前是2024-01-10,返回2024-02-29 23:59:59的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetNextMonthEndTimestamp()
        {
            return new DateTimeOffset(GetNextMonthEndTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取下月开始时间
        /// </summary>
        /// <returns>下月1号零点时间</returns>
        /// <remarks>
        /// 此方法返回下个月1号的零点时间
        /// 例如:当前是2024-01-10,返回2024-02-01 00:00:00
        /// 使用本地时区计算时间
        /// </remarks>
        public static DateTime GetNextMonthStartTime()
        {
            return GetMonthStartTime().AddMonths(1);
        }

        /// <summary>
        /// 获取本月开始时间
        /// </summary>
        /// <returns>本月1号零点时间</returns>
        /// <remarks>
        /// 此方法基于UTC时间计算本月开始时间:
        /// 1. 获取当前UTC时间的年份和月份
        /// 2. 创建一个新的DateTime对象,设置为本月1号零点
        /// 3. 返回的时间为UTC时区的时间
        /// 
        /// 示例:
        /// - 当前UTC时间为2024-01-15 14:30:00
        /// - 返回时间为2024-01-01 00:00:00 (UTC)
        /// 
        /// 注意:
        /// - 返回的是UTC时区的时间,如需本地时间请使用TimeZoneInfo.ConvertTimeFromUtc转换
        /// - 返回时间的Hour/Minute/Second/Millisecond均为0
        /// </remarks>
        public static DateTime GetMonthStartTime()
        {
            return new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        }

        /// <summary>
        /// 获取本月开始时间戳
        /// </summary>
        /// <returns>本月1号零点时间戳(秒)</returns>
        public static long GetMonthStartTimestamp()
        {
            return new DateTimeOffset(GetMonthStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取本月结束时间
        /// </summary>
        /// <returns>本月最后一天23:59:59的时间</returns>
        public static DateTime GetMonthEndTime()
        {
            return GetMonthStartTime().AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本月结束时间戳
        /// </summary>
        /// <returns>本月最后一天23:59:59的时间戳(秒)</returns>
        public static long GetMonthEndTimestamp()
        {
            return new DateTimeOffset(GetMonthEndTime()).ToUnixTimeSeconds();
        }
    }
}