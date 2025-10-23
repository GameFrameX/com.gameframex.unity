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
        /// 按照UTC时间判断两个时间戳是否是同一天
        /// </summary>
        /// <param name="timestamp1">时间戳1</param>
        /// <param name="timestamp2">时间戳2</param>
        /// <returns>是否是同一天</returns>
        /// <remarks>
        /// 此方法将两个Unix时间戳转换为UTC时间后比较是否为同一天
        /// 比较时只考虑日期部分(年月日),忽略时间部分
        /// 使用UTC时间避免时区转换带来的问题
        /// </remarks>
        public static bool IsUnixSameDay(long timestamp1, long timestamp2)
        {
            var time1 = UtcSecondsToUtcDateTime(timestamp1);
            var time2 = UtcSecondsToUtcDateTime(timestamp2);
            return IsSameDay(time1, time2);
        }

        /// <summary>
        /// 获取今天开始时间
        /// </summary>
        /// <returns>今天零点时间</returns>
        /// <remarks>
        /// 此方法返回当天的零点时间(00:00:00)
        /// 使用DateTime.Today获取当前日期的零点时间
        /// 返回的是本地时区的时间
        /// </remarks>
        public static DateTime GetTodayStartTime()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// 获取今天开始时间戳
        /// </summary>
        /// <returns>今天零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回当天零点时间的Unix时间戳
        /// 先获取本地时区的今天零点时间,然后转换为时间戳
        /// 返回从1970-01-01 00:00:00 UTC开始的秒数
        /// </remarks>
        public static long GetTodayStartTimestamp()
        {
            return new DateTimeOffset(GetTodayStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取今天结束时间
        /// </summary>
        /// <returns>今天23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回当天的最后一秒(23:59:59)
        /// 通过获取明天零点时间然后减去1秒来计算
        /// 返回的是本地时区的时间
        /// </remarks>
        public static DateTime GetTodayEndTime()
        {
            return DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取今天结束时间戳
        /// </summary>
        /// <returns>今天23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回当天最后一秒的Unix时间戳
        /// 先获取本地时区的今天23:59:59,然后转换为时间戳
        /// 返回从1970-01-01 00:00:00 UTC开始的秒数
        /// </remarks>
        public static long GetTodayEndTimestamp()
        {
            return new DateTimeOffset(GetTodayEndTime()).ToUnixTimeSeconds();
        }


        /// <summary>
        /// 获取指定日期的开始时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>指定日期零点时间</returns>
        /// <remarks>
        /// 此方法返回指定日期的零点时间(00:00:00)
        /// 例如:输入2024-01-10 14:30:00,返回2024-01-10 00:00:00
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetStartTimeOfDay(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// 获取指定日期的开始时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>指定日期零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期零点时间的Unix时间戳
        /// 例如:输入2024-01-10 14:30:00,返回2024-01-10 00:00:00的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetStartTimestampOfDay(DateTime date)
        {
            return new DateTimeOffset(GetStartTimeOfDay(date)).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取指定日期的结束时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>指定日期23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回指定日期的最后一秒(23:59:59)
        /// 例如:输入2024-01-10 14:30:00,返回2024-01-10 23:59:59
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetEndTimeOfDay(DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期的结束时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>指定日期23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期最后一秒的Unix时间戳
        /// 例如:输入2024-01-10 14:30:00,返回2024-01-10 23:59:59的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetEndTimestampOfDay(DateTime date)
        {
            return new DateTimeOffset(GetEndTimeOfDay(date)).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取明天开始时间
        /// </summary>
        /// <returns>明天零点时间</returns>
        /// <remarks>
        /// 此方法返回明天的零点时间
        /// 例如:当前是2024-01-10,返回2024-01-11 00:00:00
        /// 使用本地时区计算时间
        /// </remarks>
        public static DateTime GetTomorrowStartTime()
        {
            return DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// 获取明天开始时间戳
        /// </summary>
        /// <returns>明天零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回明天零点时间的Unix时间戳
        /// 例如:当前是2024-01-10,返回2024-01-11 00:00:00的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetTomorrowStartTimestamp()
        {
            return new DateTimeOffset(GetTomorrowStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取明天结束时间
        /// </summary>
        /// <returns>明天23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回明天的最后一秒
        /// 例如:当前是2024-01-10,返回2024-01-11 23:59:59
        /// 使用本地时区计算时间
        /// </remarks>
        public static DateTime GetTomorrowEndTime()
        {
            return DateTime.Today.AddDays(2).AddSeconds(-1);
        }

        /// <summary>
        /// 获取明天结束时间戳
        /// </summary>
        /// <returns>明天23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回明天最后一秒的Unix时间戳
        /// 例如:当前是2024-01-10,返回2024-01-11 23:59:59的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetTomorrowEndTimestamp()
        {
            return new DateTimeOffset(GetTomorrowEndTime()).ToUnixTimeSeconds();
        }


        /// <summary>
        /// 按照本地时间判断两个时间戳是否是同一天
        /// </summary>
        /// <param name="timestamp1">时间戳1（Unix秒级时间戳）。例如：1704857400</param>
        /// <param name="timestamp2">时间戳2（Unix秒级时间戳）。例如：1704859200</param>
        /// <returns>如果两个时间戳转换为本地时间后是同一天，则返回true；否则返回false</returns>
        /// <remarks>
        /// 此方法会先将UTC时间戳转换为本地时间，然后比较是否为同一天
        /// 比较时只考虑年月日，不考虑具体时间
        /// 使用系统默认时区进行UTC到本地时间的转换
        /// </remarks>
        public static bool IsLocalSameDay(long timestamp1, long timestamp2)
        {
            var time1 = UtcSecondsToLocalDateTime(timestamp1);
            var time2 = UtcSecondsToLocalDateTime(timestamp2);
            return IsSameDay(time1, time2);
        }

        /// <summary>
        /// 判断两个 <see cref="DateTime"/> 对象是否表示同一天。
        /// </summary>
        /// <param name="time1">要比较的第一个时间。例如：2024-01-10 14:30:00</param>
        /// <param name="time2">要比较的第二个时间。例如：2024-01-10 18:45:00</param>
        /// <returns>
        /// 如果两个时间是同一天，则返回 <c>true</c>；否则返回 <c>false</c>。
        /// </returns>
        /// <remarks>
        /// 此方法执行以下比较逻辑：
        /// 1. 使用 <see cref="DateTime.Date"/> 属性获取日期部分（忽略时间部分）
        /// 2. 分别比较年、月、日三个组成部分
        /// 3. 只有当年、月、日都相同时才返回 <c>true</c>
        /// 
        /// 重要特性：
        /// - 忽略具体的时、分、秒、毫秒等时间部分
        /// - 忽略时区差异，直接使用 <see cref="DateTime"/> 中存储的日期值
        /// - 不进行时区转换，基于原始 <see cref="DateTime"/> 值进行比较
        /// 
        /// 性能优化：
        /// - 使用直接的整数比较（Year、Month、Day）
        /// - 避免创建新的 <see cref="DateTime"/> 对象
        /// - 比使用 <c>time1.Date == time2.Date</c> 更高效
        /// 
        /// 适用场景：
        /// - 日程安排和事件管理
        /// - 日志按日期分组
        /// - 统计同一天的数据
        /// - 用户界面中的日期筛选
        /// </remarks>
        /// <example>
        /// <code>
        /// // 同一天的不同时间
        /// DateTime morning = new DateTime(2024, 1, 10, 8, 30, 0);
        /// DateTime evening = new DateTime(2024, 1, 10, 20, 45, 30);
        /// bool sameDay1 = TimerHelper.IsSameDay(morning, evening);
        /// Console.WriteLine($"早晨和晚上是同一天: {sameDay1}"); // True
        /// 
        /// // 不同天的时间
        /// DateTime today = new DateTime(2024, 1, 10, 23, 59, 59);
        /// DateTime tomorrow = new DateTime(2024, 1, 11, 0, 0, 1);
        /// bool sameDay2 = TimerHelper.IsSameDay(today, tomorrow);
        /// Console.WriteLine($"今天和明天是同一天: {sameDay2}"); // False
        /// 
        /// // 跨年比较
        /// DateTime lastYear = new DateTime(2023, 12, 31, 12, 0, 0);
        /// DateTime thisYear = new DateTime(2024, 1, 1, 12, 0, 0);
        /// bool sameDay3 = TimerHelper.IsSameDay(lastYear, thisYear);
        /// Console.WriteLine($"跨年日期是同一天: {sameDay3}"); // False
        /// 
        /// // 实际应用：按日期分组日志
        /// List&lt;DateTime&gt; logTimes = new List&lt;DateTime&gt;
        /// {
        ///     new DateTime(2024, 1, 10, 9, 0, 0),
        ///     new DateTime(2024, 1, 10, 15, 30, 0),
        ///     new DateTime(2024, 1, 11, 10, 0, 0)
        /// };
        /// 
        /// DateTime targetDate = new DateTime(2024, 1, 10);
        /// var sameDayLogs = logTimes.Where(log =&gt; TimerHelper.IsSameDay(log, targetDate)).ToList();
        /// Console.WriteLine($"2024-01-10 的日志数量: {sameDayLogs.Count}"); // 2
        /// </code>
        /// </example>
        /// <seealso cref="DateTime.Date"/>
        /// <seealso cref="DateTime.Year"/>
        /// <seealso cref="DateTime.Month"/>
        /// <seealso cref="DateTime.Day"/>
        public static bool IsSameDay(DateTime time1, DateTime time2)
        {
            return time1.Date.Year == time2.Date.Year && time1.Date.Month == time2.Date.Month && time1.Date.Day == time2.Date.Day;
        }

        /// <summary>
        /// 获取当前UTC时区的日期，格式为yyyyMMdd的整数
        /// </summary>
        /// <returns>返回一个8位整数，表示当前UTC时区的日期。例如：20231225表示2023年12月25日</returns>
        /// <remarks>
        /// 此方法将当前UTC时间转换为8位数字格式:
        /// - 前4位表示年份
        /// - 中间2位表示月份
        /// - 最后2位表示日期
        /// 使用DateTime.UtcNow获取UTC时间
        /// </remarks>
        public static int CurrentDateWithUtcDay()
        {
            return Convert.ToInt32(DateTime.UtcNow.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 获取两个时间之间的天数差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>天数差（可能为负数）</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的总天数，保留小数部分
        /// 如果endTime早于startTime，返回负数
        /// 返回double类型以保持精度
        /// 适用于需要天级时间差的场景
        /// </remarks>
        public static double GetDaysDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalDays;
        }


        /// <summary>
        /// 获取从指定日期到当前UTC日期之间跨越的天数。
        /// </summary>
        /// <param name="startTime">起始日期。</param>
        /// <param name="hour">小时。</param>
        /// <returns>跨越的天数。</returns>
        public static int GetCrossDays(DateTime startTime, int hour = 0)
        {
            return GetCrossDays(startTime, DateTime.UtcNow, hour);
        }

        /// <summary>
        /// 获取从指定日期到当前本地日期之间跨越的天数。
        /// </summary>
        /// <param name="startTime">起始日期。</param>
        /// <param name="hour">小时。</param>
        /// <returns>跨越的天数。</returns>
        public static int GetCrossLocalDays(DateTime startTime, int hour = 0)
        {
            return GetCrossDays(startTime, DateTime.Now, hour);
        }

        /// <summary>
        /// 获取两个时间戳之间跨越的天数。
        /// </summary>
        /// <param name="beginTimestamp">起始时间戳,从1970年1月1日以来经过的秒数。</param>
        /// <param name="hour">小时。</param>
        /// <returns>跨越的天数。</returns>
        public static int GetCrossDays(long beginTimestamp, int hour = 0)
        {
            var begin = TimestampToDateTime(beginTimestamp);
            return GetCrossDays(begin, hour);
        }

        /// <summary>
        /// 获取两个UTC时间戳之间跨越的天数。
        /// </summary>
        /// <param name="beginTimestamp">开始时间戳(秒)，从1970年1月1日以来经过的秒数。</param>
        /// <param name="afterTimestamp">结束时间戳(秒)，从1970年1月1日以来经过的秒数。</param>
        /// <param name="hour">小时。</param>
        /// <returns>跨越的天数。</returns>
        public static int GetCrossDays(long beginTimestamp, long afterTimestamp, int hour = 0)
        {
            var begin = UtcSecondsToUtcDateTime(beginTimestamp);
            var after = UtcSecondsToUtcDateTime(afterTimestamp);
            return GetCrossDays(begin, after, hour);
        }

        /// <summary>
        /// 获取两个日期之间跨越的天数。
        /// </summary>
        /// <param name="startTime">起始日期。</param>
        /// <param name="endTime">结束日期。</param>
        /// <param name="hour">小时。</param>
        /// <returns>跨越的天数。</returns>
        public static int GetCrossDays(DateTime startTime, DateTime endTime, int hour = 0)
        {
            var days = (int)(endTime.Date - startTime.Date).TotalDays;
            if (startTime.Hour < hour)
            {
                days++;
            }

            if (endTime.Hour < hour)
            {
                days--;
            }

            return days;
        }

        /// <summary>
        /// 获取两个本地时间戳之间的间隔天数
        /// </summary>
        /// <param name="startTimestamp">开始时间戳(秒),UTC时间戳将被转换为本地时间</param>
        /// <param name="endTimestamp">结束时间戳(秒),UTC时间戳将被转换为本地时间</param>
        /// <returns>间隔天数,如果开始时间晚于结束时间,返回负数</returns>
        /// <remarks>
        /// 此方法会先将UTC时间戳转换为本地时间,然后计算两个本地时间之间的天数差
        /// 计算时会考虑日期的时分秒部分
        /// </remarks>
        public static int GetCrossLocalDays(long startTimestamp, long endTimestamp)
        {
            var startTime = UtcSecondsToLocalDateTime(startTimestamp);
            var endTime = UtcSecondsToLocalDateTime(endTimestamp);
            return GetCrossDays(startTime, endTime);
        }


        /// <summary>
        /// 获取当前本地时区的日期，格式为yyyyMMdd的整数
        /// </summary>
        /// <returns>返回一个8位整数，表示当前本地时区的日期。例如：20231225表示2023年12月25日</returns>
        /// <remarks>
        /// 此方法将当前本地时间转换为8位数字格式:
        /// - 前4位表示年份
        /// - 中间2位表示月份
        /// - 最后2位表示日期
        /// 使用DateTime.Now获取本地时间
        /// </remarks>
        public static int CurrentDateWithDay()
        {
            return Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}