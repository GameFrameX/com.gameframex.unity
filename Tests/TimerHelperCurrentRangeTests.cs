using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperCurrentRangeTests
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

        #region IsTimeInRange

        [Test]
        public void IsTimeInRange_InsideRange_ReturnsTrue()
        {
            var time = new DateTime(2024, 1, 10, 12, 0, 0);
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 23, 59, 59);
            Assert.That(TimerHelper.IsTimeInRange(time, start, end), Is.True);
        }

        [Test]
        public void IsTimeInRange_EqualToStart_ReturnsTrue()
        {
            var time = new DateTime(2024, 1, 10, 0, 0, 0);
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 23, 59, 59);
            Assert.That(TimerHelper.IsTimeInRange(time, start, end), Is.True);
        }

        [Test]
        public void IsTimeInRange_EqualToEnd_ReturnsTrue()
        {
            var time = new DateTime(2024, 1, 10, 23, 59, 59);
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 23, 59, 59);
            Assert.That(TimerHelper.IsTimeInRange(time, start, end), Is.True);
        }

        [Test]
        public void IsTimeInRange_BeforeStart_ReturnsFalse()
        {
            var time = new DateTime(2024, 1, 9, 23, 59, 59);
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 23, 59, 59);
            Assert.That(TimerHelper.IsTimeInRange(time, start, end), Is.False);
        }

        [Test]
        public void IsTimeInRange_AfterEnd_ReturnsFalse()
        {
            var time = new DateTime(2024, 1, 11, 0, 0, 0);
            var start = new DateTime(2024, 1, 10, 0, 0, 0);
            var end = new DateTime(2024, 1, 10, 23, 59, 59);
            Assert.That(TimerHelper.IsTimeInRange(time, start, end), Is.False);
        }

        [Test]
        public void IsTimeInRange_SameTimeForAll_ReturnsTrue()
        {
            var time = new DateTime(2024, 5, 10, 12, 0, 0);
            Assert.That(TimerHelper.IsTimeInRange(time, time, time), Is.True);
        }

        #endregion

        #region IsTimestampInRange

        [Test]
        public void IsTimestampInRange_Inside_ReturnsTrue()
        {
            Assert.That(TimerHelper.IsTimestampInRange(500, 100, 1000), Is.True);
        }

        [Test]
        public void IsTimestampInRange_EqualToStart_ReturnsTrue()
        {
            Assert.That(TimerHelper.IsTimestampInRange(100, 100, 1000), Is.True);
        }

        [Test]
        public void IsTimestampInRange_EqualToEnd_ReturnsTrue()
        {
            Assert.That(TimerHelper.IsTimestampInRange(1000, 100, 1000), Is.True);
        }

        [Test]
        public void IsTimestampInRange_BeforeStart_ReturnsFalse()
        {
            Assert.That(TimerHelper.IsTimestampInRange(99, 100, 1000), Is.False);
        }

        [Test]
        public void IsTimestampInRange_AfterEnd_ReturnsFalse()
        {
            Assert.That(TimerHelper.IsTimestampInRange(1001, 100, 1000), Is.False);
        }

        [Test]
        public void IsTimestampInRange_SameValueAll_ReturnsTrue()
        {
            Assert.That(TimerHelper.IsTimestampInRange(500, 500, 500), Is.True);
        }

        [Test]
        public void IsTimestampInRange_NegativeRange_Works()
        {
            Assert.That(TimerHelper.IsTimestampInRange(-500, -1000, 0), Is.True);
        }

        [Test]
        public void IsTimestampInRange_ZeroInRange_Works()
        {
            Assert.That(TimerHelper.IsTimestampInRange(0, -1, 1), Is.True);
        }

        #endregion

        #region GetNowWithUtc

        [Test]
        public void GetNowWithUtc_KindIsUtc()
        {
            var result = TimerHelper.GetNowWithUtc();
            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public void GetNowWithUtc_IsCloseToUtcNow()
        {
            var result = TimerHelper.GetNowWithUtc();
            Assert.That(result, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        #endregion

        #region GetNowWithTimeZone

        [Test]
        public void GetNowWithTimeZone_DefaultIsUtc()
        {
            var result = TimerHelper.GetNowWithTimeZone();
            Assert.That(result, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void GetNowWithTimeZone_ChinaTimeZone_IsUtcPlus8()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var result = TimerHelper.GetNowWithTimeZone();
            var expected = DateTime.UtcNow.AddHours(8);
            Assert.That(result, Is.EqualTo(expected).Within(TimeSpan.FromSeconds(1)));
        }

        #endregion

        #region CurrentTimeWithUtcFullString / CurrentTimeWithUtc

        [Test]
        public void CurrentTimeWithUtcFullString_Is6Characters()
        {
            var result = TimerHelper.CurrentTimeWithUtcFullString();
            Assert.That(result.Length, Is.EqualTo(6));
        }

        [Test]
        public void CurrentTimeWithUtcFullString_IsValidFormat()
        {
            var result = TimerHelper.CurrentTimeWithUtcFullString();
            Assert.DoesNotThrow(() => int.Parse(result));
            var hours = int.Parse(result.Substring(0, 2));
            var minutes = int.Parse(result.Substring(2, 2));
            var seconds = int.Parse(result.Substring(4, 2));
            Assert.That(hours, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(23));
            Assert.That(minutes, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(59));
            Assert.That(seconds, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(59));
        }

        [Test]
        public void CurrentTimeWithUtc_ReturnsInteger()
        {
            var result = TimerHelper.CurrentTimeWithUtc();
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
            Assert.That(result, Is.LessThanOrEqualTo(235959));
        }

        [Test]
        public void CurrentTimeWithUtc_MatchesFullString()
        {
            var str = TimerHelper.CurrentTimeWithUtcFullString();
            var integer = TimerHelper.CurrentTimeWithUtc();
            Assert.That(integer, Is.EqualTo(int.Parse(str)));
        }

        #endregion

        #region CurrentTimeWithTimeZoneFullString / CurrentTimeWithTimeZone

        [Test]
        public void CurrentTimeWithTimeZoneFullString_Is6Characters()
        {
            var result = TimerHelper.CurrentTimeWithTimeZoneFullString();
            Assert.That(result.Length, Is.EqualTo(6));
        }

        [Test]
        public void CurrentTimeWithTimeZone_ReturnsInteger()
        {
            var result = TimerHelper.CurrentTimeWithTimeZone();
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
            Assert.That(result, Is.LessThanOrEqualTo(235959));
        }

        [Test]
        public void CurrentTimeWithTimeZone_MatchesFullString()
        {
            var str = TimerHelper.CurrentTimeWithTimeZoneFullString();
            var integer = TimerHelper.CurrentTimeWithTimeZone();
            Assert.That(integer, Is.EqualTo(int.Parse(str)));
        }

        #endregion

        #region CurrentDateTimeWithUtcFormat / CurrentDateTimeWithTimeZoneFormat

        [Test]
        public void CurrentDateTimeWithUtcFormat_DefaultFormat_ContainsT()
        {
            var result = TimerHelper.CurrentDateTimeWithUtcFormat();
            Assert.That(result, Does.Contain("-"));
        }

        [Test]
        public void CurrentDateTimeWithUtcFormat_CustomFormat_MatchesPattern()
        {
            var result = TimerHelper.CurrentDateTimeWithUtcFormat("yyyyMMdd");
            Assert.That(result.Length, Is.EqualTo(8));
            Assert.DoesNotThrow(() => int.Parse(result));
        }

        [Test]
        public void CurrentDateTimeWithTimeZoneFormat_ReturnsString()
        {
            var result = TimerHelper.CurrentDateTimeWithTimeZoneFormat();
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void CurrentDateTimeWithTimeZoneFormat_CustomFormat_Works()
        {
            var result = TimerHelper.CurrentDateTimeWithTimeZoneFormat("HH:mm:ss");
            Assert.That(result, Does.Contain(":"));
        }

        #endregion
    }
}
