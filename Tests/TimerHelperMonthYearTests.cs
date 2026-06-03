using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperMonthYearTests
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

        #region GetStartTimeOfMonth / GetEndTimeOfMonth

        [Test]
        public void GetStartTimeOfMonth_ReturnsFirstDayMidnight()
        {
            var date = new DateTime(2024, 6, 15, 14, 30, 0);
            var result = TimerHelper.GetStartTimeOfMonth(date);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 6, 1, 0, 0, 0)));
        }

        [Test]
        public void GetStartTimeOfMonth_January_ReturnsJanuaryFirst()
        {
            var date = new DateTime(2024, 1, 15, 0, 0, 0);
            var result = TimerHelper.GetStartTimeOfMonth(date);
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
        }

        [Test]
        public void GetStartTimeOfMonth_December_ReturnsDecemberFirst()
        {
            var date = new DateTime(2024, 12, 25, 0, 0, 0);
            var result = TimerHelper.GetStartTimeOfMonth(date);
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(1));
        }

        [Test]
        public void GetEndTimeOfMonth_January_ReturnsJan31()
        {
            var date = new DateTime(2024, 1, 15, 0, 0, 0);
            var result = TimerHelper.GetEndTimeOfMonth(date);
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(31));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetEndTimeOfMonth_February_LeapYear_ReturnsFeb29()
        {
            var date = new DateTime(2024, 2, 10, 0, 0, 0); // 2024 is leap year
            var result = TimerHelper.GetEndTimeOfMonth(date);
            Assert.That(result.Day, Is.EqualTo(29));
        }

        [Test]
        public void GetEndTimeOfMonth_February_NonLeapYear_ReturnsFeb28()
        {
            var date = new DateTime(2023, 2, 10, 0, 0, 0); // 2023 is not leap year
            var result = TimerHelper.GetEndTimeOfMonth(date);
            Assert.That(result.Day, Is.EqualTo(28));
        }

        [Test]
        public void GetEndTimeOfMonth_April_ReturnsApr30()
        {
            var date = new DateTime(2024, 4, 10, 0, 0, 0);
            var result = TimerHelper.GetEndTimeOfMonth(date);
            Assert.That(result.Day, Is.EqualTo(30));
        }

        [Test]
        public void GetEndTimeOfMonth_December_YearRollover()
        {
            var date = new DateTime(2024, 12, 1, 0, 0, 0);
            var result = TimerHelper.GetEndTimeOfMonth(date);
            Assert.That(result.Year, Is.EqualTo(2024));
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(31));
        }

        #endregion

        #region GetStartTimestampOfMonth / GetEndTimestampOfMonth

        [Test]
        public void GetStartTimestampOfMonth_MatchesTime()
        {
            var date = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfMonth(date);
            var expectedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfMonth_MatchesTime()
        {
            var date = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfMonth(date);
            Assert.That(timestamp, Is.GreaterThan(TimerHelper.GetStartTimestampOfMonth(date)));
        }

        [Test]
        public void GetStartTimestampOfMonthWithTimeZone_IncludesOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var date = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfMonthWithTimeZone(date);
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(TimerHelper.GetStartTimeOfMonth(date));
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfMonthWithTimeZone_IncludesOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var date = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfMonthWithTimeZone(date);
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(TimerHelper.GetEndTimeOfMonth(date));
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        #endregion

        #region UTC Month Helpers

        [Test]
        public void GetMonthStartTimeWithUtc_IsFirstDayMidnight()
        {
            var result = TimerHelper.GetMonthStartTimeWithUtc();
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public void GetMonthStartTimestampWithUtc_MatchesTime()
        {
            var time = TimerHelper.GetMonthStartTimeWithUtc();
            var timestamp = TimerHelper.GetMonthStartTimestampWithUtc();
            var expected = new DateTimeOffset(time).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetMonthEndTimeWithUtc_IsLastDay235959()
        {
            var result = TimerHelper.GetMonthEndTimeWithUtc();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
            Assert.That(result.Day, Is.GreaterThanOrEqualTo(28)); // February minimum
        }

        [Test]
        public void GetMonthEndTimestampWithUtc_MatchesTime()
        {
            var start = TimerHelper.GetMonthStartTimestampWithUtc();
            var end = TimerHelper.GetMonthEndTimestampWithUtc();
            Assert.That(end, Is.GreaterThan(start));
        }

        [Test]
        public void GetNextMonthStartTimeWithUtc_IsNextMonthFirstDay()
        {
            var thisMonth = TimerHelper.GetMonthStartTimeWithUtc();
            var nextMonth = TimerHelper.GetNextMonthStartTimeWithUtc();
            Assert.That(nextMonth, Is.EqualTo(thisMonth.AddMonths(1)));
        }

        [Test]
        public void GetNextMonthStartTimestampWithUtc_IsNextMonthStart()
        {
            var thisMonth = TimerHelper.GetMonthStartTimestampWithUtc();
            var nextMonth = TimerHelper.GetNextMonthStartTimestampWithUtc();
            Assert.That(nextMonth, Is.GreaterThan(thisMonth));
        }

        [Test]
        public void GetNextMonthEndTimeWithUtc_IsNextMonthEnd()
        {
            var nextMonthStart = TimerHelper.GetNextMonthStartTimeWithUtc();
            var nextMonthEnd = TimerHelper.GetNextMonthEndTimeWithUtc();
            Assert.That(nextMonthEnd, Is.EqualTo(nextMonthStart.AddMonths(1).AddSeconds(-1)));
        }

        [Test]
        public void GetNextMonthEndTimestampWithUtc_IsNextMonthEnd()
        {
            var nextEnd = TimerHelper.GetNextMonthEndTimestampWithUtc();
            var nextStart = TimerHelper.GetNextMonthStartTimestampWithUtc();
            Assert.That(nextEnd, Is.GreaterThan(nextStart));
        }

        #endregion

        #region TimeZone Month Helpers

        [Test]
        public void GetMonthStartTimeWithTimeZone_IsFirstDayMidnight()
        {
            var result = TimerHelper.GetMonthStartTimeWithTimeZone();
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
        }

        [Test]
        public void GetMonthStartTimestampWithTimeZone_MatchesTime()
        {
            var time = TimerHelper.GetMonthStartTimeWithTimeZone();
            var timestamp = TimerHelper.GetMonthStartTimestampWithTimeZone();
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(time);
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetMonthEndTimeWithTimeZone_IsLastDay235959()
        {
            var result = TimerHelper.GetMonthEndTimeWithTimeZone();
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetMonthEndTimestampWithTimeZone_MatchesTime()
        {
            var start = TimerHelper.GetMonthStartTimestampWithTimeZone();
            var end = TimerHelper.GetMonthEndTimestampWithTimeZone();
            Assert.That(end, Is.GreaterThan(start));
        }

        [Test]
        public void GetNextMonthStartTimeWithTimeZone_IsNextMonthFirstDay()
        {
            var thisMonth = TimerHelper.GetMonthStartTimeWithTimeZone();
            var nextMonth = TimerHelper.GetNextMonthStartTimeWithTimeZone();
            Assert.That(nextMonth, Is.EqualTo(thisMonth.AddMonths(1)));
        }

        [Test]
        public void GetNextMonthStartTimestampWithTimeZone_IsNextMonthStart()
        {
            var thisMonth = TimerHelper.GetMonthStartTimestampWithTimeZone();
            var nextMonth = TimerHelper.GetNextMonthStartTimestampWithTimeZone();
            Assert.That(nextMonth, Is.GreaterThan(thisMonth));
        }

        [Test]
        public void GetNextMonthEndTimeWithTimeZone_IsNextMonthEnd()
        {
            var nextMonthStart = TimerHelper.GetNextMonthStartTimeWithTimeZone();
            var nextMonthEnd = TimerHelper.GetNextMonthEndTimeWithTimeZone();
            Assert.That(nextMonthEnd, Is.EqualTo(nextMonthStart.AddMonths(1).AddSeconds(-1)));
        }

        [Test]
        public void GetNextMonthEndTimestampWithTimeZone_IsNextMonthEnd()
        {
            var nextMonthStart = TimerHelper.GetNextMonthStartTimestampWithTimeZone();
            var nextMonthEnd = TimerHelper.GetNextMonthEndTimestampWithTimeZone();
            Assert.That(nextMonthEnd, Is.GreaterThan(nextMonthStart));
        }

        #endregion

        #region GetStartTimeOfYear / GetEndTimeOfYear

        [Test]
        public void GetStartTimeOfYear_ReturnsJanuaryFirst()
        {
            var date = new DateTime(2024, 6, 15, 12, 0, 0);
            var result = TimerHelper.GetStartTimeOfYear(date);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 1, 0, 0, 0)));
        }

        [Test]
        public void GetEndTimeOfYear_ReturnsDecember31_235959()
        {
            var date = new DateTime(2024, 6, 15, 12, 0, 0);
            var result = TimerHelper.GetEndTimeOfYear(date);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 12, 31, 23, 59, 59)));
        }

        [Test]
        public void GetEndTimeOfYear_LeapYear_HasCorrectDays()
        {
            // 2024 is leap year — end-of-year is 2024-12-31 regardless
            var date = new DateTime(2024, 2, 1, 0, 0, 0);
            var result = TimerHelper.GetEndTimeOfYear(date);
            Assert.That(result.Year, Is.EqualTo(2024));
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(31));
        }

        #endregion

        #region GetStartTimestampOfYear / GetEndTimestampOfYear

        [Test]
        public void GetStartTimestampOfYear_MatchesTime()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfYear(date);
            var expectedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfYear_MatchesTime()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfYear(date);
            var expectedTime = new DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        #endregion

        #region UTC Year Helpers

        [Test]
        public void GetYearStartTimeWithUtc_IsJan1Midnight()
        {
            var result = TimerHelper.GetYearStartTimeWithUtc();
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public void GetYearStartTimestampWithUtc_MatchesTime()
        {
            var time = TimerHelper.GetYearStartTimeWithUtc();
            var timestamp = TimerHelper.GetYearStartTimestampWithUtc();
            var expected = new DateTimeOffset(time).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetYearEndTimeWithUtc_IsDec31235959()
        {
            var result = TimerHelper.GetYearEndTimeWithUtc();
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(31));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetYearEndTimestampWithUtc_MatchesTime()
        {
            var start = TimerHelper.GetYearStartTimestampWithUtc();
            var end = TimerHelper.GetYearEndTimestampWithUtc();
            Assert.That(end, Is.GreaterThan(start));
        }

        [Test]
        public void GetNextYearStartTimeWithUtc_IsNextYearJan1()
        {
            var thisYear = TimerHelper.GetYearStartTimeWithUtc();
            var nextYear = TimerHelper.GetNextYearStartTimeWithUtc();
            Assert.That(nextYear, Is.EqualTo(thisYear.AddYears(1)));
        }

        [Test]
        public void GetNextYearStartTimestampWithUtc_IsNextYearStart()
        {
            var thisYear = TimerHelper.GetYearStartTimestampWithUtc();
            var nextYear = TimerHelper.GetNextYearStartTimestampWithUtc();
            Assert.That(nextYear, Is.GreaterThan(thisYear));
        }

        [Test]
        public void GetEndTimestampOfYearWithUtc_Matches()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfYearWithUtc(date);
            var expectedTime = new DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetStartTimestampOfYearWithUtc_Matches()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfYearWithUtc(date);
            var expectedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        #endregion

        #region TimeZone Year Helpers

        [Test]
        public void GetYearStartTimeWithTimeZone_IsJan1Midnight()
        {
            var result = TimerHelper.GetYearStartTimeWithTimeZone();
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Hour, Is.EqualTo(0));
        }

        [Test]
        public void GetYearStartTimestampWithTimeZone_MatchesTime()
        {
            var time = TimerHelper.GetYearStartTimeWithTimeZone();
            var timestamp = TimerHelper.GetYearStartTimestampWithTimeZone();
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(time);
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetYearEndTime_IsDec31235959()
        {
            var result = TimerHelper.GetYearEndTime();
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(31));
            Assert.That(result.Hour, Is.EqualTo(23));
        }

        [Test]
        public void GetYearEndTimestampWithTimeZone_MatchesTime()
        {
            var start = TimerHelper.GetYearStartTimestampWithTimeZone();
            var end = TimerHelper.GetYearEndTimestampWithTimeZone();
            Assert.That(end, Is.GreaterThan(start));
        }

        [Test]
        public void GetNextYearStartTimeWithTimeZone_IsNextYearJan1()
        {
            var thisYear = TimerHelper.GetYearStartTimeWithTimeZone();
            var nextYear = TimerHelper.GetNextYearStartTimeWithTimeZone();
            Assert.That(nextYear, Is.EqualTo(thisYear.AddYears(1)));
        }

        [Test]
        public void GetNextYearStartTimestamp_IsNextYearStart()
        {
            var thisYear = TimerHelper.GetYearStartTimestampWithTimeZone();
            var nextYear = TimerHelper.GetNextYearStartTimestamp();
            Assert.That(nextYear, Is.GreaterThan(thisYear));
        }

        [Test]
        public void GetNextYearStartTimestampWithTimeZone_IsNextYearStart()
        {
            var nextYearTs = TimerHelper.GetNextYearStartTimestampWithTimeZone();
            var nextYearTime = TimerHelper.GetNextYearStartTimeWithTimeZone();
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(nextYearTime);
            Assert.That(nextYearTs, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfYearWithTimeZone_Matches()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfYearWithTimeZone(date);
            var expectedTime = new DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetStartTimestampOfYearWithTimeZone_Matches()
        {
            var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfYearWithTimeZone(date);
            var expectedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedTime).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        #endregion
    }
}
