using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperWeekTests
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

        #region IsSameWeek (two DateTimes)

        [Test]
        public void IsSameWeek_TwoDateTimes_SameWeek_ReturnsTrue()
        {
            var monday = new DateTime(2024, 1, 1, 0, 0, 0); // Monday
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0); // Wednesday
            Assert.That(TimerHelper.IsSameWeek(monday, wednesday), Is.True);
        }

        [Test]
        public void IsSameWeek_TwoDateTimes_MondayAndSunday_ReturnsTrue()
        {
            var monday = new DateTime(2024, 1, 1, 0, 0, 0); // Monday
            var sunday = new DateTime(2024, 1, 7, 23, 59, 59); // Sunday
            Assert.That(TimerHelper.IsSameWeek(monday, sunday), Is.True);
        }

        [Test]
        public void IsSameWeek_TwoDateTimes_DifferentWeek_ReturnsFalse()
        {
            var thisMonday = new DateTime(2024, 1, 1, 0, 0, 0); // Monday
            var nextMonday = new DateTime(2024, 1, 8, 0, 0, 0); // Next Monday
            Assert.That(TimerHelper.IsSameWeek(thisMonday, nextMonday), Is.False);
        }

        [Test]
        public void IsSameWeek_TwoDateTimes_CrossYear_ReturnsFalse()
        {
            var oldWeek = new DateTime(2023, 12, 25, 0, 0, 0); // Monday Dec 25, 2023
            var newWeek = new DateTime(2024, 1, 1, 0, 0, 0);  // Monday Jan 1, 2024
            Assert.That(TimerHelper.IsSameWeek(oldWeek, newWeek), Is.False);
        }

        [Test]
        public void IsSameWeek_TwoDateTimes_Identical_ReturnsTrue()
        {
            var time = new DateTime(2024, 6, 15, 12, 0, 0);
            Assert.That(TimerHelper.IsSameWeek(time, time), Is.True);
        }

        #endregion

        #region IsSameWeek (DateTime, bool)

        [Test]
        public void IsSameWeek_WithUtcTrue_ComparesAgainstUtcNow()
        {
            var now = DateTime.UtcNow;
            Assert.That(TimerHelper.IsSameWeek(now, isUtc: true), Is.True);
        }

        [Test]
        public void IsSameWeek_OneWeekAgoUtc_ReturnsFalse()
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
            Assert.That(TimerHelper.IsSameWeek(oneWeekAgo, isUtc: true), Is.False);
        }

        [Test]
        public void IsSameWeek_WithUtcFalse_UsesTimeZone()
        {
            var now = TimerHelper.GetNowWithTimeZone();
            Assert.That(TimerHelper.IsSameWeek(now, isUtc: false), Is.True);
        }

        #endregion

        #region IsSameWeek (long ticks, bool)

        [Test]
        public void IsSameWeek_ByTicks_CurrentWeek_ReturnsTrue()
        {
            var nowTicks = DateTime.UtcNow.Ticks;
            Assert.That(TimerHelper.IsSameWeek(nowTicks, isUtc: true), Is.True);
        }

        [Test]
        public void IsSameWeek_ByTicks_LastWeek_ReturnsFalse()
        {
            var lastWeekTicks = DateTime.UtcNow.AddDays(-7).Ticks;
            Assert.That(TimerHelper.IsSameWeek(lastWeekTicks, isUtc: true), Is.False);
        }

        #endregion

        #region GetStartTimeOfWeek / GetEndTimeOfWeek

        [Test]
        public void GetStartTimeOfWeek_Monday_ReturnsSameMidnight()
        {
            var monday = new DateTime(2024, 1, 1, 14, 30, 0); // Monday
            var result = TimerHelper.GetStartTimeOfWeek(monday);
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(result.Hour, Is.EqualTo(0));
            Assert.That(result.Minute, Is.EqualTo(0));
            Assert.That(result.Second, Is.EqualTo(0));
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 1, 0, 0, 0)));
        }

        [Test]
        public void GetStartTimeOfWeek_Sunday_ReturnsPreviousMonday()
        {
            var sunday = new DateTime(2024, 1, 7, 14, 30, 0); // Sunday
            var result = TimerHelper.GetStartTimeOfWeek(sunday);
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 1, 0, 0, 0)));
        }

        [Test]
        public void GetStartTimeOfWeek_Wednesday_ReturnsMonday()
        {
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0);
            var result = TimerHelper.GetStartTimeOfWeek(wednesday);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 1, 0, 0, 0)));
        }

        [Test]
        public void GetEndTimeOfWeek_Monday_ReturnsSunday235959()
        {
            var monday = new DateTime(2024, 1, 1, 14, 30, 0);
            var result = TimerHelper.GetEndTimeOfWeek(monday);
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 7, 23, 59, 59)));
        }

        [Test]
        public void GetEndTimeOfWeek_WeekStartToEnd_IsExactlyOneWeek()
        {
            var date = new DateTime(2024, 3, 15, 12, 0, 0);
            var start = TimerHelper.GetStartTimeOfWeek(date);
            var end = TimerHelper.GetEndTimeOfWeek(date);
            Assert.That((end - start).TotalDays, Is.EqualTo(7.0).Within(0.01));
            Assert.That(end.Hour, Is.EqualTo(23));
            Assert.That(end.Minute, Is.EqualTo(59));
            Assert.That(end.Second, Is.EqualTo(59));
        }

        #endregion

        #region GetDayOfWeekTime

        [Test]
        public void GetDayOfWeekTime_FromWednesday_ToMonday()
        {
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0);
            var result = TimerHelper.GetDayOfWeekTime(wednesday, DayOfWeek.Monday);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 1, 12, 0, 0)));
        }

        [Test]
        public void GetDayOfWeekTime_FromWednesday_ToFriday()
        {
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0);
            var result = TimerHelper.GetDayOfWeekTime(wednesday, DayOfWeek.Friday);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 5, 12, 0, 0)));
        }

        [Test]
        public void GetDayOfWeekTime_SameDay_ReturnsSameDay()
        {
            var wednesday = new DateTime(2024, 1, 3, 15, 30, 0);
            var result = TimerHelper.GetDayOfWeekTime(wednesday, DayOfWeek.Wednesday);
            Assert.That(result, Is.EqualTo(wednesday));
        }

        [Test]
        public void GetDayOfWeekTime_FromSunday_ToSunday()
        {
            var sunday = new DateTime(2024, 1, 7, 12, 0, 0);
            var result = TimerHelper.GetDayOfWeekTime(sunday, DayOfWeek.Sunday);
            Assert.That(result, Is.EqualTo(sunday));
        }

        [Test]
        public void GetDayOfWeekTime_PreservesTimePart()
        {
            var input = new DateTime(2024, 6, 12, 14, 30, 45); // Wednesday
            var result = TimerHelper.GetDayOfWeekTime(input, DayOfWeek.Monday);
            Assert.That(result.Hour, Is.EqualTo(14));
            Assert.That(result.Minute, Is.EqualTo(30));
            Assert.That(result.Second, Is.EqualTo(45));
        }

        #endregion

        #region UTC Week Helpers

        [Test]
        public void GetWeekStartTimeWithUtc_IsMonday()
        {
            var result = TimerHelper.GetWeekStartTimeWithUtc();
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(result.Hour, Is.EqualTo(0));
        }

        [Test]
        public void GetWeekEndTimeWithUtc_IsSunday235959()
        {
            var result = TimerHelper.GetWeekEndTimeWithUtc();
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void GetWeekStartTimestampWithUtc_MatchesTime()
        {
            var time = TimerHelper.GetWeekStartTimeWithUtc();
            var timestamp = TimerHelper.GetWeekStartTimestampWithUtc();
            var expected = new DateTimeOffset(time).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetWeekEndTimestampWithUtc_MatchesTime()
        {
            var time = TimerHelper.GetWeekEndTimeWithUtc();
            var timestamp = TimerHelper.GetWeekEndTimestampWithUtc();
            Assert.That(timestamp, Is.GreaterThan(TimerHelper.GetWeekStartTimestampWithUtc()));
        }

        [Test]
        public void GetNextWeekStartTimeWithUtc_IsNextMonday()
        {
            var thisWeekStart = TimerHelper.GetWeekStartTimeWithUtc();
            var nextWeekStart = TimerHelper.GetNextWeekStartTimeWithUtc();
            Assert.That(nextWeekStart, Is.EqualTo(thisWeekStart.AddDays(7)));
        }

        [Test]
        public void GetNextWeekEndTimeWithUtc_IsNextSunday()
        {
            var nextWeekStart = TimerHelper.GetNextWeekStartTimeWithUtc();
            var nextWeekEnd = TimerHelper.GetNextWeekEndTimeWithUtc();
            Assert.That((nextWeekEnd - nextWeekStart).TotalDays, Is.EqualTo(7.0).Within(0.01));
        }

        [Test]
        public void GetNextWeekStartTimestampWithUtc_IsWeekStartPlus7Days()
        {
            var thisStart = TimerHelper.GetWeekStartTimestampWithUtc();
            var nextStart = TimerHelper.GetNextWeekStartTimestampWithUtc();
            Assert.That(nextStart, Is.EqualTo(thisStart + 7 * 86400));
        }

        [Test]
        public void GetNextWeekEndTimestampWithUtc_IsNextWeekEnd()
        {
            var nextStart = TimerHelper.GetNextWeekStartTimestampWithUtc();
            var nextEnd = TimerHelper.GetNextWeekEndTimestampWithUtc();
            Assert.That(nextEnd, Is.EqualTo(nextStart + 7 * 86400 - 1));
        }

        [Test]
        public void GetDayOfWeekTime_Utc_ReturnsCorrectDay()
        {
            var result = TimerHelper.GetDayOfWeekTime(DayOfWeek.Monday);
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
        }

        [Test]
        public void GetStartTimestampOfWeekWithUtc_Matches()
        {
            var date = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc); // Wednesday
            var timestamp = TimerHelper.GetStartTimestampOfWeekWithUtc(date);
            var expectedMonday = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedMonday).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfWeekWithUtc_Matches()
        {
            var date = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc); // Wednesday
            var timestamp = TimerHelper.GetEndTimestampOfWeekWithUtc(date);
            var expectedSunday = new DateTime(2024, 1, 7, 23, 59, 59, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedSunday).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        #endregion

        #region TimeZone Week Helpers

        [Test]
        public void GetChinaDayOfWeekWithTimeZone_NoArgs_ReturnsZeroToSix()
        {
            var result = TimerHelper.GetChinaDayOfWeekWithTimeZone();
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
            Assert.That(result, Is.LessThanOrEqualTo(6));
        }

        [Test]
        public void GetChinaDayOfWeekWithTimeZone_WithDate_Monday_ReturnsOne()
        {
            var monday = new DateTime(2024, 1, 1, 0, 0, 0); // Monday
            var result = TimerHelper.GetChinaDayOfWeekWithTimeZone(monday);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetChinaDayOfWeekWithTimeZone_WithDate_Sunday_ReturnsZero()
        {
            var sunday = new DateTime(2024, 1, 7, 0, 0, 0); // Sunday
            var result = TimerHelper.GetChinaDayOfWeekWithTimeZone(sunday);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetWeekStartTimeWithTimeZone_IsMondayMidnight()
        {
            var result = TimerHelper.GetWeekStartTimeWithTimeZone();
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(result.Hour, Is.EqualTo(0));
        }

        [Test]
        public void GetWeekEndTimeWithTimeZone_IsSunday235959()
        {
            var result = TimerHelper.GetWeekEndTimeWithTimeZone();
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(result.Hour, Is.EqualTo(23));
        }

        [Test]
        public void GetWeekStartTimestampWithTimeZone_Matches()
        {
            var time = TimerHelper.GetWeekStartTimeWithTimeZone();
            var timestamp = TimerHelper.GetWeekStartTimestampWithTimeZone();
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(time);
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetWeekEndTimestampWithTimeZone_Matches()
        {
            var start = TimerHelper.GetWeekStartTimestampWithTimeZone();
            var end = TimerHelper.GetWeekEndTimestampWithTimeZone();
            Assert.That(end, Is.GreaterThan(start));
        }

        [Test]
        public void GetNextWeekStartTimeWithTimeZone_IsNextMonday()
        {
            var thisWeekStart = TimerHelper.GetWeekStartTimeWithTimeZone();
            var nextWeekStart = TimerHelper.GetNextWeekStartTimeWithTimeZone();
            Assert.That(nextWeekStart, Is.EqualTo(thisWeekStart.AddDays(7)));
        }

        [Test]
        public void GetNextWeekEndTimeWithTimeZone_IsNextSunday()
        {
            var nextWeekStart = TimerHelper.GetNextWeekStartTimeWithTimeZone();
            var nextWeekEnd = TimerHelper.GetNextWeekEndTimeWithTimeZone();
            Assert.That((nextWeekEnd - nextWeekStart).TotalDays, Is.EqualTo(7.0).Within(0.01));
        }

        [Test]
        public void GetNextWeekStartTimestampWithTimeZone_IsWeekStartPlus7Days()
        {
            var thisWeekStart = TimerHelper.GetWeekStartTimestampWithTimeZone();
            var nextWeekStart = TimerHelper.GetNextWeekStartTimestampWithTimeZone();
            Assert.That(nextWeekStart, Is.EqualTo(thisWeekStart + 7 * 86400));
        }

        [Test]
        public void GetNextWeekEndTimestampWithTimeZone_IsNextWeekEnd()
        {
            var nextStart = TimerHelper.GetNextWeekStartTimestampWithTimeZone();
            var nextEnd = TimerHelper.GetNextWeekEndTimestampWithTimeZone();
            Assert.That(nextEnd, Is.EqualTo(nextStart + 7 * 86400 - 1));
        }

        [Test]
        public void GetStartTimestampOfWeek_Matches()
        {
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfWeek(wednesday);
            var expectedMonday = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedMonday).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfWeek_Matches()
        {
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfWeek(wednesday);
            var expectedSunday = new DateTime(2024, 1, 7, 23, 59, 59, DateTimeKind.Utc);
            var expected = new DateTimeOffset(expectedSunday).ToUnixTimeSeconds();
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetStartTimestampOfWeekWithTimeZone_IncludesOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetStartTimestampOfWeekWithTimeZone(wednesday);
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(TimerHelper.GetStartTimeOfWeek(wednesday));
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetEndTimestampOfWeekWithTimeZone_IncludesOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var wednesday = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
            var timestamp = TimerHelper.GetEndTimestampOfWeekWithTimeZone(wednesday);
            var expected = TimerHelper.DateTimeToSecondsWithTimeZone(TimerHelper.GetEndTimeOfWeek(wednesday));
            Assert.That(timestamp, Is.EqualTo(expected));
        }

        [Test]
        public void GetDayOfWeekTimeWithTimeZone_ReturnsCorrectDay()
        {
            var result = TimerHelper.GetDayOfWeekTimeWithTimeZone(DayOfWeek.Monday);
            Assert.That(result.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
        }

        #endregion
    }
}
