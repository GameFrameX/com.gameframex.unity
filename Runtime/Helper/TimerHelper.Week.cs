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
        /// 判断当前时间是否与指定时间处于同一周。
        /// </summary>
        /// <param name="ticks">指定时间的起始时间(Ticks)。表示自 0001 年 1 月 1 日午夜 00:00:00 以来所经过的时钟周期数</param>
        /// <returns>如果当前时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将传入的ticks转换为DateTime后与当前时间比较是否在同一周
        /// 使用系统默认的周计算规则(周日为每周第一天)
        /// </remarks>
        public static bool IsNowSameWeek(long ticks)
        {
            return IsNowSameWeek(new DateTime(ticks));
        }

        /// <summary>
        /// 判断当前时间是否与指定时间处于同一周。
        /// 以周一为每周的第一天,周日为每周的最后一天。
        /// 使用本地时间(DateTime.Now)进行比较。
        /// </summary>
        /// <param name="start">指定时间的起始时间。可以是任意DateTime值。</param>
        /// <returns>如果当前时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将调用IsSameWeek方法进行实际比较。
        /// 使用本地时区时间作为当前时间参考点。
        /// </remarks>
        public static bool IsNowSameWeek(DateTime start)
        {
            return IsSameWeek(start, DateTime.Now);
        }

        /// <summary>
        /// 判断两个时间是否处于同一周。
        /// 以周一为每周的第一天,周日为每周的最后一天。
        /// </summary>
        /// <param name="start">起始时间。可以是任意DateTime值。</param>
        /// <param name="end">结束时间。可以是任意DateTime值。</param>
        /// <returns>如果两个时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法会自动调整参数顺序,确保start是较早的时间。
        /// 通过计算较早时间所在周的周日时间点,判断另一个时间是否在同一周内。
        /// </remarks>
        public static bool IsSameWeek(DateTime start, DateTime end)
        {
            // 让start是较早的时间
            if (start > end)
            {
                (start, end) = (end, start);
            }

            var dayOfWeek = (int)start.DayOfWeek;
            if (dayOfWeek == (int)DayOfWeek.Sunday)
            {
                dayOfWeek = 7;
            }

            // 获取较早时间所在星期的星期天的0点
            var startsWeekLastDate = start.AddDays(7 - dayOfWeek).Date;
            // 判断end是否在start所在周
            return startsWeekLastDate >= end.Date;
        }

        /// <summary>
        /// 判断当前UTC时间是否与指定时间处于同一周。
        /// 以周一为每周的第一天,周日为每周的最后一天。
        /// 使用UTC时间(DateTime.UtcNow)进行比较。
        /// </summary>
        /// <param name="start">指定时间的起始时间。可以是任意DateTime值。</param>
        /// <returns>如果当前UTC时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将调用IsSameWeek方法进行实际比较。
        /// 使用UTC时区时间作为当前时间参考点，避免时区差异影响。
        /// </remarks>
        public static bool IsNowSameWeekUtc(DateTime start)
        {
            return IsSameWeek(start, DateTime.UtcNow);
        }

        /// <summary>
        /// 判断当前UTC时间是否与指定时间戳处于同一周。
        /// </summary>
        /// <param name="ticks">指定时间的起始时间(Ticks)。表示自 0001 年 1 月 1 日午夜 00:00:00 以来所经过的时钟周期数</param>
        /// <returns>如果当前UTC时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将传入的ticks转换为DateTime后与当前UTC时间比较是否在同一周
        /// 使用UTC时区时间作为当前时间参考点，避免时区差异影响
        /// </remarks>
        public static bool IsUnixSameWeek(long ticks)
        {
            return IsNowSameWeekUtc(new DateTime(ticks));
        }

        /// <summary>
        /// 判断当前UTC时间是否与指定Unix时间戳处于同一周。
        /// </summary>
        /// <param name="timestampSeconds">指定时间的Unix时间戳(秒)。表示自1970年1月1日00:00:00 UTC以来的秒数</param>
        /// <returns>如果当前UTC时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将传入的Unix时间戳(秒)转换为UTC DateTime后与当前UTC时间比较是否在同一周
        /// 全程使用UTC时间，避免时区差异影响
        /// </remarks>
        public static bool IsUnixSameWeekFromTimestamp(long timestampSeconds)
        {
            var dateTime = UtcSecondsToUtcDateTime(timestampSeconds);
            return IsNowSameWeekUtc(dateTime);
        }

        /// <summary>
        /// 判断当前UTC时间是否与指定Unix时间戳处于同一周。
        /// </summary>
        /// <param name="timestampMilliseconds">指定时间的Unix时间戳(毫秒)。表示自1970年1月1日00:00:00 UTC以来的毫秒数</param>
        /// <returns>如果当前UTC时间与指定时间处于同一周，则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法将传入的Unix时间戳(毫秒)转换为UTC DateTime后与当前UTC时间比较是否在同一周
        /// 全程使用UTC时间，避免时区差异影响
        /// </remarks>
        public static bool IsUnixSameWeekFromTimestampMilliseconds(long timestampMilliseconds)
        {
            var dateTime = UtcMillisecondsToUtcDateTime(timestampMilliseconds);
            return IsNowSameWeekUtc(dateTime);
        }

        /// <summary>
        /// 获取指定日期所在星期的时间。
        /// </summary>
        /// <param name="dateTime">指定日期。例如：2024-01-10</param>
        /// <param name="day">星期几。例如：DayOfWeek.Monday 表示星期一，DayOfWeek.Sunday 表示星期日</param>
        /// <returns>返回指定日期所在星期的指定星期几的零点时间。例如：dateTime为2024-01-10(星期三)，day为DayOfWeek.Monday，则返回2024-01-08 00:00:00</returns>
        /// <remarks>
        /// 此方法将星期日(DayOfWeek.Sunday)视为每周的第7天，而不是第0天
        /// 返回的时间总是该日期的零点时间（00:00:00）
        /// </remarks>
        public static DateTime GetDayOfWeekTime(DateTime dateTime, DayOfWeek day)
        {
            // 将星期几转换为数字(1-7)，将星期日从0转换为7
            var dd = (int)day;
            if (dd == 0)
            {
                dd = 7;
            }

            // 获取指定日期是星期几(1-7)，将星期日从0转换为7
            var dayOfWeek = (int)dateTime.DayOfWeek;
            if (dayOfWeek == 0)
            {
                dayOfWeek = 7;
            }

            // 计算目标日期与当前日期的天数差，并返回目标日期的零点时间
            return dateTime.AddDays(dd - dayOfWeek).Date;
        }

        /// <summary>
        /// 获取当前日期所在星期的时间。
        /// </summary>
        /// <param name="day">星期几。例如：DayOfWeek.Monday 表示星期一，DayOfWeek.Sunday 表示星期日。</param>
        /// <returns>返回当前UTC日期所在星期的指定星期几的零点时间。例如：当前是2024-01-10(星期三)，传入DayOfWeek.Monday，则返回2024-01-08 00:00:00。</returns>
        /// <remarks>
        /// 此方法使用UTC时间作为基准计算。
        /// 如果需要使用本地时间，请使用 GetDayOfWeekTime(DateTime.Now, day)。
        /// </remarks>
        public static DateTime GetDayOfWeekTime(DayOfWeek day)
        {
            return GetDayOfWeekTime(DateTime.UtcNow, day);
        }

        /// <summary>
        /// 获取指定星期在中国的对应数字。
        /// </summary>
        /// <param name="day">星期几。例如：DayOfWeek.Monday 表示星期一，DayOfWeek.Sunday 表示星期日</param>
        /// <returns>星期在中国的对应数字。返回1-7,其中7表示星期日</returns>
        /// <remarks>
        /// 此方法将C#的DayOfWeek枚举值(0-6)转换为中国习惯的星期表示(1-7)
        /// 主要区别在于将星期日从0转换为7
        /// 例如:
        /// - DayOfWeek.Monday(1) -> 1 (星期一)
        /// - DayOfWeek.Sunday(0) -> 7 (星期日)
        /// </remarks>
        public static int GetChinaDayOfWeek(DayOfWeek day)
        {
            var dayOfWeek = (int)day;
            if (dayOfWeek == 0)
            {
                dayOfWeek = 7;
            }

            return dayOfWeek;
        }

        /// <summary>
        /// 获取当前星期在中国的对应数字。
        /// </summary>
        /// <returns>当前星期在中国的对应数字。返回1-7,其中7表示星期日</returns>
        /// <remarks>
        /// 此方法获取当前本地时间的星期几,并转换为中国习惯的表示方式
        /// 使用本地时区时间(DateTime.Now)作为基准
        /// 内部调用GetChinaDayOfWeek(DayOfWeek)方法进行转换
        /// </remarks>
        public static int GetChinaDayOfWeek()
        {
            return GetChinaDayOfWeek(DateTime.Now.DayOfWeek);
        }

        /// <summary>
        /// 获取本周开始时间
        /// </summary>
        /// <returns>本周一零点时间</returns>
        /// <remarks>
        /// 此方法返回本周一的零点时间(00:00:00)
        /// 使用中国习惯:
        /// - 将周日的DayOfWeek值0转换为7
        /// - 以周一为每周的第一天
        /// 返回的是本地时区的时间
        /// </remarks>
        public static DateTime GetWeekStartTime()
        {
            var now = DateTime.Now;
            var dayOfWeek = (int)now.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return now.AddDays(1 - dayOfWeek).Date;
        }

        /// <summary>
        /// 获取本周开始时间戳
        /// </summary>
        /// <returns>本周一零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回本周一零点时间的Unix时间戳
        /// 先获取本地时区的本周一零点时间,然后转换为时间戳
        /// 返回从1970-01-01 00:00:00 UTC开始的秒数
        /// </remarks>
        public static long GetWeekStartTimestamp()
        {
            return new DateTimeOffset(GetWeekStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取本周结束时间
        /// </summary>
        /// <returns>本周日23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回本周日的最后一秒(23:59:59)
        /// 通过获取下周一零点时间然后减去1秒来计算
        /// 返回的是本地时区的时间
        /// </remarks>
        public static DateTime GetWeekEndTime()
        {
            return GetWeekStartTime().AddDays(7).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本周结束时间戳
        /// </summary>
        /// <returns>本周日23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回本周日最后一秒的Unix时间戳
        /// 先获取本地时区的本周日23:59:59,然后转换为时间戳
        /// 返回从1970-01-01 00:00:00 UTC开始的秒数
        /// </remarks>
        public static long GetWeekEndTimestamp()
        {
            return new DateTimeOffset(GetWeekEndTime()).ToUnixTimeSeconds();
        }


        /// <summary>
        /// 获取指定日期所在周的开始时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在周周一零点时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在周的周一零点时间
        /// 例如:输入2024-01-10(周三),返回2024-01-08 00:00:00(周一)
        /// 使用周一作为每周的第一天,周日为每周的最后一天
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetStartTimeOfWeek(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            return date.AddDays(1 - dayOfWeek).Date;
        }

        /// <summary>
        /// 获取指定日期所在周的开始时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在周周一零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在周的周一零点时间的Unix时间戳
        /// 例如:输入2024-01-10(周三),返回2024-01-08 00:00:00(周一)的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetStartTimestampOfWeek(DateTime date)
        {
            return new DateTimeOffset(GetStartTimeOfWeek(date)).ToUnixTimeSeconds();
        }


        /// <summary>
        /// 获取下周开始时间
        /// </summary>
        /// <returns>下周一零点时间</returns>
        /// <remarks>
        /// 此方法返回下周一的零点时间
        /// 例如:当前是2024-01-10(周三),返回2024-01-15 00:00:00(下周一)
        /// 使用本地时区计算时间
        /// </remarks>
        public static DateTime GetNextWeekStartTime()
        {
            return GetWeekStartTime().AddDays(7);
        }

        /// <summary>
        /// 获取下周开始时间戳
        /// </summary>
        /// <returns>下周一零点时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回下周一零点时间的Unix时间戳
        /// 例如:当前是2024-01-10(周三),返回2024-01-15 00:00:00(下周一)的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetNextWeekStartTimestamp()
        {
            return new DateTimeOffset(GetNextWeekStartTime()).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取下周结束时间
        /// </summary>
        /// <returns>下周日23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回下周日的最后一秒
        /// 例如:当前是2024-01-10(周三),返回2024-01-21 23:59:59(下周日)
        /// 使用本地时区计算时间
        /// </remarks>
        public static DateTime GetNextWeekEndTime()
        {
            return GetNextWeekStartTime().AddDays(7).AddSeconds(-1);
        }

        /// <summary>
        /// 获取下周结束时间戳
        /// </summary>
        /// <returns>下周日23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回下周日最后一秒的Unix时间戳
        /// 例如:当前是2024-01-10(周三),返回2024-01-21 23:59:59(下周日)的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetNextWeekEndTimestamp()
        {
            return new DateTimeOffset(GetNextWeekEndTime()).ToUnixTimeSeconds();
        }


        /// <summary>
        /// 获取指定日期所在周的结束时间
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在周周日23:59:59的时间</returns>
        /// <remarks>
        /// 此方法返回指定日期所在周的周日最后一秒
        /// 例如:输入2024-01-10(周三),返回2024-01-14 23:59:59(周日)
        /// 使用周一作为每周的第一天,周日为每周的最后一天
        /// 保持原有时区不变
        /// </remarks>
        public static DateTime GetEndTimeOfWeek(DateTime date)
        {
            return GetStartTimeOfWeek(date).AddDays(7).AddSeconds(-1);
        }

        /// <summary>
        /// 获取指定日期所在周的结束时间戳
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>所在周周日23:59:59的时间戳(秒)</returns>
        /// <remarks>
        /// 此方法返回指定日期所在周的周日最后一秒的Unix时间戳
        /// 例如:输入2024-01-10(周三),返回2024-01-14 23:59:59(周日)的时间戳
        /// 会将时间转换为UTC时间后再计算时间戳
        /// </remarks>
        public static long GetEndTimestampOfWeek(DateTime date)
        {
            return new DateTimeOffset(GetEndTimeOfWeek(date)).ToUnixTimeSeconds();
        }
    }
}