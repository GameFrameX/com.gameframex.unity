using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// DateTime 扩展方法。
    /// </summary>
    /// <remarks>
    /// Extension methods for DateTime.
    /// </remarks>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 计算从指定日期到当前日期的天数差。
        /// </summary>
        /// <remarks>
        /// Calculates the number of days from the specified date to the current date.
        /// </remarks>
        /// <param name="now">当前日期 / The current date.</param>
        /// <param name="dt">比较的目标日期 / The target date to compare.</param>
        /// <returns>天数差 / The number of days difference.</returns>
        [UnityEngine.Scripting.Preserve]
        public static int GetDaysFrom(this DateTime now, DateTime dt)
        {
            return (int)(now.Date - dt).TotalDays;
        }

        /// <summary>
        /// 计算从默认日期（1970年1月1日）到当前日期的天数差。
        /// </summary>
        /// <remarks>
        /// Calculates the number of days from the default date (January 1, 1970) to the current date.
        /// </remarks>
        /// <param name="now">当前日期 / The current date.</param>
        /// <returns>天数差 / The number of days difference.</returns>
        [UnityEngine.Scripting.Preserve]
        public static int GetDaysFromDefault(this DateTime now)
        {
            return now.GetDaysFrom(new DateTime(1970, 1, 1).Date);
        }
    }
}
