using System;

namespace GameFrameX.Runtime
{
    public static class DateTimeExtensions
    {
        [UnityEngine.Scripting.Preserve]
        public static int GetDaysFrom(this DateTime now, DateTime dt)
        {
            return (int)(now.Date - dt).TotalDays;
        }

        [UnityEngine.Scripting.Preserve]
        public static int GetDaysFromDefault(this DateTime now)
        {
            return now.GetDaysFrom(new DateTime(1970, 1, 1).Date);
        }
    }
}