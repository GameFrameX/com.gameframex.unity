using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperDayTests
    {
        private TimeZoneInfo _utcPlus8;

        [SetUp]
        public void SetUp()
        {
            _utcPlus8 = GetUtcPlus8TimeZone();
            TimerHelper.SetTimeZone(TimeZoneInfo.Utc);
            TimerHelper.ResetTimeOffset();
        }

        [TearDown]
        public void TearDown()
        {
            TimerHelper.SetTimeZone(TimeZoneInfo.Utc);
            TimerHelper.ResetTimeOffset();
        }

        private static TimeZoneInfo GetUtcPlus8TimeZone()
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai"); }
            catch (Exception) { }
            try { return TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"); }
            catch (Exception) { }
            return TimeZoneInfo.CreateCustomTimeZone("UTC+8", TimeSpan.FromHours(8), "UTC+8", "UTC+8");
        }

        #region IsSameDay

        [Test]
        public void IsSameDay_SameDayDifferentTime_ReturnsTrue()
        {
            var morning = new DateTime(2024, 1, 10, 8, 30, 0);
            var evening = new DateTime(2024, 1, 10, 20, 45, 30);
            Assert.That(TimerHelper.IsSameDay(morning, evening), Is.True);
        }

        [Test]
        public void IsSameDay_ConsecutiveDays_ReturnsFalse()
        {
            var today = new DateTime(2024, 1, 10, 23, 59, 59);
            var tomorrow = new DateTime(2024, 1, 11, 0, 0, 1);
            Assert.That(TimerHelper.IsSameDay(today, tomorrow), Is.False);
        }

        [Test]
        public void IsSameDay_DifferentMonth_ReturnsFalse()
        {
            var jan = new DateTime(2024, 1, 31, 12, 0, 0);
            var feb = new DateTime(2024, 2, 1, 12, 0, 0);
            Assert.That(TimerHelper.IsSameDay(jan, feb), Is.False);
        }

        [Test]
        public void IsSameDay_DifferentYear_ReturnsFalse()
        {
            var dec31 = new DateTime(2023, 12, 31, 12, 0, 0);
            var jan1 = new DateTime(2024, 1, 1, 12, 0, 0);
            Assert.That(TimerHelper.IsSameDay(dec31, jan1), Is.False);
        }

        [Test]
        public void IsSameDay_IdenticalTimes_ReturnsTrue()
        {
            var time = new DateTime(2024, 6, 15, 12, 0, 0);
            Assert.That(TimerHelper.IsSameDay(time, time), Is.True);
        }

        [Test]
        public void IsSameDay_MidnightBoundary_ReturnsFalse()
        {
            var before = new DateTime(2024, 5, 10, 23, 59, 59, 999);
            var after = new DateTime(2024, 5, 11, 0, 0, 0);
            Assert.That(TimerHelper.IsSameDay(before, after), Is.False);
        }

        #endregion

        #region GetDaysDifference

        [Test]
        public void GetDaysDifference_OneDayApart_ReturnsOne()
        {
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 11, 0, 0, 0);
            Assert.That(TimerHelper.GetDaysDifference(start, end), Is.EqualTo(1.0));
        }

        [Test]
        public void GetDaysDifference_TwelveHoursApart_ReturnsHalfDay()
        {
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 12, 0, 0);
            Assert.That(TimerHelper.GetDaysDifference(start, end), Is.EqualTo(0.5));
        }

        [Test]
        public void GetDaysDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 11, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 0, 0, 0);
            Assert.That(TimerHelper.GetDaysDifference(start, end), Is.EqualTo(-1.0));
        }

        [Test]
        public void GetDaysDifference_SameTime_ReturnsZero()
        {
            var time = new DateTime(2024, 5, 20, 12, 0, 0);
            Assert.That(TimerHelper.GetDaysDifference(time, time), Is.EqualTo(0.0));
        }

        #endregion

        #region GetCrossDays

        [Test]
        public void GetCrossDays_DefaultHour_OneDayCrossed()
        {
            var start = new DateTime(2024, 1, 10, 3, 0, 0);
            var end = new DateTime(2024, 1, 11, 2, 0, 0);
            Assert.That(TimerHelper.GetCrossDays(start, end), Is.EqualTo(1));
        }

        [Test]
        public void GetCrossDays_WithHourThreshold_Filtered()
        {
            var start = new DateTime(2024, 1, 10, 3, 0, 0);
            var end = new DateTime(2024, 1, 11, 2, 0, 0);
            // hour=5: start.Hour(3) < 5 → +1, end.Hour(2) < 5 → -1
            // days = 1 (date diff), result = 1+1-1 = 1
            Assert.That(TimerHelper.GetCrossDays(start, end, 5), Is.EqualTo(1));
        }

        [Test]
        public void GetCrossDays_Hour5_WithinSameDayWindow_ReturnsZero()
        {
            var start = new DateTime(2024, 1, 10, 6, 0, 0);
            var end = new DateTime(2024, 1, 11, 4, 0, 0);
            // days = 1, start.Hour(6) >= 5 no change, end.Hour(4) < 5 → -1, result = 0
            Assert.That(TimerHelper.GetCrossDays(start, end, 5), Is.EqualTo(0));
        }

        [Test]
        public void GetCrossDays_SameDay_ReturnsZero()
        {
            var start = new DateTime(2024, 1, 10, 10, 0, 0);
            var end = new DateTime(2024, 1, 10, 20, 0, 0);
            Assert.That(TimerHelper.GetCrossDays(start, end), Is.EqualTo(0));
        }

        [Test]
        public void GetCrossDays_HourBelowZero_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                TimerHelper.GetCrossDays(DateTime.Now, DateTime.Now, -1));
            Assert.That(ex.ParamName, Is.EqualTo("hour"));
        }

        [Test]
        public void GetCrossDays_HourAbove23_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                TimerHelper.GetCrossDays(DateTime.Now, DateTime.Now, 24));
            Assert.That(ex.ParamName, Is.EqualTo("hour"));
        }

        [Test]
        public void GetCrossDays_HourBoundary_0_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
                TimerHelper.GetCrossDays(DateTime.Now, DateTime.Now, 0));
        }

        [Test]
        public void GetCrossDays_HourBoundary_23_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
                TimerHelper.GetCrossDays(DateTime.Now, DateTime.Now, 23));
        }

        #endregion

        #region GetStartTimeOfDay / GetEndTimeOfDay

        [Test]
        public void GetStartTimeOfDay_ReturnsMidnight()
        {
            var input = new DateTime(2024, 6, 15, 14, 30, 45);
            var result = TimerHelper.GetStartTimeOfDay(input);
            Assert.That(result.Year, Is.EqualTo(2024));
            Assert.That(result.Month, Is.EqualTo(6));
            Assert.That(result.Day, Is.EqualTo(15));
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
        }

        [Test]
        public void GetEndTimeOfDay_Returns235959()
        {
            var input = new DateTime(2024, 6, 15, 14, 30, 45);
            var result = TimerHelper.GetEndTimeOfDay(input);
            Assert.That(result.Year, Is.EqualTo(2024));
            Assert.That(result.Month, Is.EqualTo(6));
            Assert.That(result.Day, Is.EqualTo(15));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetStartTimeOfDay_AlreadyMidnight_ReturnsSame()
        {
            var midnight = new DateTime(2024, 3, 1, 0, 0, 0);
            var result = TimerHelper.GetStartTimeOfDay(midnight);
            Assert.That(result, Is.EqualTo(midnight));
        }

        [Test]
        public void GetEndTimeOfDay_NextDayStart_ReturnsPreviousDayEnd()
        {
            var input = new DateTime(2024, 1, 1, 0, 0, 0);
            var result = TimerHelper.GetEndTimeOfDay(input);
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Hour, Is.EqualTo(23));
        }

        #endregion

        #region GetStartTimestampOfDay / GetEndTimestampOfDay

        [Test]
        public void GetStartTimestampOfDay_EpochDay_ReturnsZero()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetStartTimestampOfDay(epoch);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetEndTimestampOfDay_EpochDay_ReturnsEndOfDayTimestamp()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetEndTimestampOfDay(epoch);
            Assert.That(result, Is.EqualTo(86399)); // 86400 - 1
        }

        #endregion

        #region CurrentDateWithUtcDay

        [Test]
        public void CurrentDateWithUtcDay_Returns8DigitInteger()
        {
            var result = TimerHelper.CurrentDateWithUtcDay();
            Assert.That(result, Is.GreaterThanOrEqualTo(20200101));
            Assert.That(result, Is.LessThanOrEqualTo(20991231));
        }

        [Test]
        public void CurrentDateWithUtcDay_MatchesUtcNow()
        {
            var result = TimerHelper.CurrentDateWithUtcDay();
            var expected = int.Parse(DateTime.UtcNow.ToString("yyyyMMdd"));
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region CurrentDateWithDayWithTimeZone

        [Test]
        public void CurrentDateWithDayWithTimeZone_Returns8DigitInteger()
        {
            var result = TimerHelper.CurrentDateWithDayWithTimeZone();
            Assert.That(result, Is.GreaterThanOrEqualTo(20200101));
            Assert.That(result, Is.LessThanOrEqualTo(20991231));
        }

        [Test]
        public void CurrentDateWithDayWithTimeZone_UsesCurrentTimeZone()
        {
            // Both UTC and timezone should give same date (since CurrentTimeZone was reset to UTC)
            var utcResult = TimerHelper.CurrentDateWithUtcDay();
            var tzResult = TimerHelper.CurrentDateWithDayWithTimeZone();
            Assert.That(tzResult, Is.EqualTo(utcResult));
        }

        #endregion

        #region UTC Day Helpers

        [Test]
        public void GetTodayStartTimeWithUtc_IsMidnight()
        {
            var result = TimerHelper.GetTodayStartTimeWithUtc();
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
        }

        [Test]
        public void GetTodayEndTimeWithUtc_Is235959()
        {
            var result = TimerHelper.GetTodayEndTimeWithUtc();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetTomorrowStartTimeWithUtc_IsNextDayMidnight()
        {
            var todayStart = TimerHelper.GetTodayStartTimeWithUtc();
            var tomorrowStart = TimerHelper.GetTomorrowStartTimeWithUtc();
            Assert.That(tomorrowStart, Is.EqualTo(todayStart.AddDays(1)));
        }

        [Test]
        public void GetTomorrowEndTimeWithUtc_IsTomorrow235959()
        {
            var result = TimerHelper.GetTomorrowEndTimeWithUtc();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetTodayStartTimestampWithUtc_IsConsistent()
        {
            var time = TimerHelper.GetTodayStartTimeWithUtc();
            var timestamp = TimerHelper.GetTodayStartTimestampWithUtc();
            var expected = new DateTimeOffset(time).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetTodayEndTimestampWithUtc_IsConsistent()
        {
            var time = TimerHelper.GetTodayEndTimeWithUtc();
            var timestamp = TimerHelper.GetTodayEndTimestampWithUtc();
            var expected = new DateTimeOffset(time).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetTomorrowStartTimestampWithUtc_IsEndOfTodayPlusOne()
        {
            var todayEndTs = TimerHelper.GetTodayEndTimestampWithUtc();
            var tomorrowStartTs = TimerHelper.GetTomorrowStartTimestampWithUtc();
            Assert.That(tomorrowStartTs, Is.EqualTo(todayEndTs + 1));
        }

        [Test]
        public void GetTomorrowEndTimestampWithUtc_IsTomorrowEnd()
        {
            var time = TimerHelper.GetTomorrowEndTimeWithUtc();
            var timestamp = TimerHelper.GetTomorrowEndTimestampWithUtc();
            Assert.That(timestamp, Is.GreaterThan(TimerHelper.GetTomorrowStartTimestampWithUtc()));
        }

        #endregion

        #region TimeZone Day Helpers

        [Test]
        public void GetTodayStartTimeWithTimeZone_IsMidnight()
        {
            var result = TimerHelper.GetTodayStartTimeWithTimeZone();
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
        }

        [Test]
        public void GetTodayEndTimeWithTimeZone_Is235959()
        {
            var result = TimerHelper.GetTodayEndTimeWithTimeZone();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetTomorrowStartTimeWithTimeZone_IsTomorrow()
        {
            var todayStart = TimerHelper.GetTodayStartTimeWithTimeZone();
            var tomorrowStart = TimerHelper.GetTomorrowStartTimeWithTimeZone();
            Assert.That(tomorrowStart, Is.EqualTo(todayStart.AddDays(1)));
        }

        [Test]
        public void GetTomorrowEndTimeWithTimeZone_IsTomorrow235959()
        {
            var result = TimerHelper.GetTomorrowEndTimeWithTimeZone();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetTodayStartTimestampWithTimeZone_IsConsistent()
        {
            var time = TimerHelper.GetTodayStartTimeWithTimeZone();
            var timestamp = TimerHelper.GetTodayStartTimestampWithTimeZone();
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(time);
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetTodayEndTimestampWithTimeZone_IsConsistent()
        {
            var timespan = TimerHelper.GetTodayEndTimeWithTimeZone() - TimerHelper.GetTodayStartTimeWithTimeZone();
            Assert.That(timespan.TotalSeconds, Is.EqualTo(86399));
        }

        [Test]
        public void GetTomorrowStartTimestampWithTimeZone_IsEndPlusOne()
        {
            var todayEnd = TimerHelper.GetTodayEndTimestampWithTimeZone();
            var tomorrowStart = TimerHelper.GetTomorrowStartTimestampWithTimeZone();
            Assert.That(tomorrowStart, Is.EqualTo(todayEnd + 1));
        }

        [Test]
        public void GetTomorrowEndTimestampWithTimeZone_IsTomorrowEnd()
        {
            var tomorrowStart = TimerHelper.GetTomorrowStartTimestampWithTimeZone();
            var tomorrowEnd = TimerHelper.GetTomorrowEndTimestampWithTimeZone();
            Assert.That(tomorrowEnd, Is.EqualTo(tomorrowStart + 86399));
        }

        #endregion

        #region UTC CrossDays

        [Test]
        public void GetCrossDaysUtc_ConsecutiveDays_ReturnsOne()
        {
            var day12024 = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var day22024 = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            var beginTs = new DateTimeOffset(day12024).ToUnixTimeSeconds();
            var endTs = new DateTimeOffset(day22024).ToUnixTimeSeconds();
            Assert.That(TimerHelper.GetCrossDaysUtc(beginTs, endTs), Is.EqualTo(1));
        }

        [Test]
        public void GetCrossDaysWithUtc_DateTime_ToNow_ReturnsNonNegative()
        {
            var pastDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetCrossDaysWithUtc(pastDate);
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void GetCrossDaysWithUtc_Timestamp_ToNow_ReturnsNonNegative()
        {
            var pastTs = new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
            var result = TimerHelper.GetCrossDaysWithUtc(pastTs);
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        }

        #endregion

        #region TimeZone CrossDays

        [Test]
        public void GetCrossDaysWithTimeZone_TwoTimestamps_Computed()
        {
            var time1 = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time2 = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            var ts1 = new DateTimeOffset(time1).ToUnixTimeSeconds();
            var ts2 = new DateTimeOffset(time2).ToUnixTimeSeconds();
            Assert.That(TimerHelper.GetCrossDaysWithTimeZone(ts1, ts2), Is.EqualTo(1));
        }

        [Test]
        public void GetCrossDaysWithTimeZone_DateTime_ToNow_ReturnsNonNegative()
        {
            var pastDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetCrossDaysWithTimeZone(pastDate);
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void GetCrossDaysWithTimeZone_Timestamp_ToNow_ReturnsNonNegative()
        {
            var pastTs = new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
            var result = TimerHelper.GetCrossDaysWithTimeZone(pastTs);
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        }

        #endregion

        #region GetStartTimestampOfDayWithTimeZone / GetEndTimestampOfDayWithTimeZone

        [Test]
        public void GetStartTimestampOfDayWithTimeZone_IncludesTimeZoneOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetStartTimestampOfDayWithTimeZone(epoch);
            Assert.That(result, Is.EqualTo(0 + 28800)); // epoch seconds + UTC+8 offset
        }

        [Test]
        public void GetEndTimestampOfDayWithTimeZone_IncludesTimeZoneOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.GetEndTimestampOfDayWithTimeZone(epoch);
            Assert.That(result, Is.EqualTo(86399 + 28800)); // end-of-day seconds + UTC+8 offset
        }

        #endregion
    }
}
