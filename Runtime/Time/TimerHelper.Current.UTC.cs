using System;

namespace GameFrameX.Runtime
{
    public static partial class TimerHelper
    {
        /// <summary>
        /// 获取当前UTC时间，格式为HHmmss的字符串。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time as a string in HHmmss format.
        /// This method converts the current UTC time to a 6-character time string:
        /// - First 2 digits represent hours (24-hour format)
        /// - Middle 2 digits represent minutes
        /// - Last 2 digits represent seconds
        /// Uses DateTime.UtcNow to get UTC time.
        /// </remarks>
        /// <returns>返回一个6位字符串，表示当前UTC时间。例如：143045表示14:30:45 / Returns a 6-character string representing the current UTC time. For example: 143045 represents 14:30:45</returns>
        public static string CurrentTimeWithUtcFullString()
        {
            return GetNowWithUtc().ToString("HHmmss");
        }

        /// <summary>
        /// 获取当前UTC时间，格式为HHmmss的整数。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time as an integer in HHmmss format.
        /// This method converts the current UTC time to a 6-digit integer:
        /// - First 2 digits represent hours (24-hour format)
        /// - Middle 2 digits represent minutes
        /// - Last 2 digits represent seconds
        /// Internally calls CurrentTimeWithUtcFullString() to get the string and then converts to integer.
        /// </remarks>
        /// <returns>返回一个6位整数，表示当前UTC时间。例如：143045表示14:30:45 / Returns a 6-digit integer representing the current UTC time. For example: 143045 represents 14:30:45</returns>
        public static int CurrentTimeWithUtc()
        {
            return Convert.ToInt32(CurrentTimeWithUtcFullString());
        }

        /// <summary>
        /// 获取当前UTC时区时间的自定义格式字符串。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time as a custom formatted string.
        /// This method allows custom time format strings:
        /// - Default format includes year, month, day, hour, minute, second, millisecond, and time zone information
        /// - Other formats can be specified through the format parameter
        /// - Uses DateTime.UtcNow to get UTC time
        /// Supports standard .NET date and time format specifiers.
        /// </remarks>
        /// <param name="format">时间格式字符串，默认为"yyyy-MM-dd HH:mm:ss.fff K" / Time format string, defaults to "yyyy-MM-dd HH:mm:ss.fff K"</param>
        /// <returns>返回指定格式的UTC时间字符串。例如默认格式返回："2023-12-25 06:30:45.123 +00:00" / Returns the UTC time string in the specified format. For example, the default format returns: "2023-12-25 06:30:45.123 +00:00"</returns>
        public static string CurrentDateTimeWithUtcFormat(string format = "yyyy-MM-dd HH:mm:ss.fff K")
        {
            return GetNowWithUtc().ToString(format);
        }

        /// <summary>
        /// 获取当前UTC时间。
        /// </summary>
        /// <remarks>
        /// Gets the current UTC time.
        /// This method returns the current UTC time (Coordinated Universal Time).
        /// Compared to local time, there will be a time zone offset.
        /// Mainly used for scenarios that require a unified time standard.
        /// </remarks>
        /// <returns>当前UTC时间 / The current UTC time</returns>
        public static DateTime GetNowWithUtc()
        {
            return DateTime.UtcNow;
        }
    }
}