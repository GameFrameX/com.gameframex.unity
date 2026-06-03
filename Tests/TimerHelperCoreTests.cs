using System;
using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperCoreTests
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

        #region Epoch Constants

        [Test]
        public void EpochLocal_Is19700101Local()
        {
            Assert.That(TimerHelper.EpochLocal.Year, Is.EqualTo(1970));
            Assert.That(TimerHelper.EpochLocal.Month, Is.EqualTo(1));
            Assert.That(TimerHelper.EpochLocal.Day, Is.EqualTo(1));
            Assert.That(TimerHelper.EpochLocal.Kind, Is.EqualTo(DateTimeKind.Local));
        }

        [Test]
        public void EpochUtc_Is19700101Utc()
        {
            Assert.That(TimerHelper.EpochUtc.Year, Is.EqualTo(1970));
            Assert.That(TimerHelper.EpochUtc.Month, Is.EqualTo(1));
            Assert.That(TimerHelper.EpochUtc.Day, Is.EqualTo(1));
            Assert.That(TimerHelper.EpochUtc.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        #endregion

        #region SetTimeZone

        [Test]
        public void SetTimeZone_DefaultIsUtc()
        {
            Assert.That(TimerHelper.CurrentTimeZone, Is.EqualTo(TimeZoneInfo.Utc));
        }

        [Test]
        public void SetTimeZone_WithValidTimeZoneInfo_ChangesTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            Assert.That(TimerHelper.CurrentTimeZone, Is.EqualTo(_utcPlus8));
        }

        [Test]
        public void SetTimeZone_WithNull_FallsBackToUtc()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            TimerHelper.SetTimeZone((TimeZoneInfo)null);
            Assert.That(TimerHelper.CurrentTimeZone, Is.EqualTo(TimeZoneInfo.Utc));
        }

        [Test]
        public void SetTimeZone_WithValidId_ReturnsTrue()
        {
            var result = TimerHelper.SetTimeZone(_utcPlus8.Id);
            Assert.That(result, Is.True);
            Assert.That(TimerHelper.CurrentTimeZone, Is.EqualTo(_utcPlus8));
        }

        [Test]
        public void SetTimeZone_WithInvalidId_ReturnsFalseAndFallsBackToUtc()
        {
            var result = TimerHelper.SetTimeZone("Invalid/TimeZone");
            Assert.That(result, Is.False);
            Assert.That(TimerHelper.CurrentTimeZone, Is.EqualTo(TimeZoneInfo.Utc));
        }

        #endregion

        #region DateTimeToSecond / DateTimeToMilliseconds

        [Test]
        public void DateTimeToSecond_UtcEpoch_ReturnsZero()
        {
            var result = TimerHelper.DateTimeToSecond(TimerHelper.EpochUtc, utc: true);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void DateTimeToSecond_OneSecondAfterEpoch_ReturnsOne()
        {
            var time = TimerHelper.EpochUtc.AddSeconds(1);
            var result = TimerHelper.DateTimeToSecond(time, utc: true);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void DateTimeToSecond_BeforeEpoch_ReturnsNegative()
        {
            var time = TimerHelper.EpochUtc.AddSeconds(-100);
            var result = TimerHelper.DateTimeToSecond(time, utc: true);
            Assert.That(result, Is.EqualTo(-100));
        }

        [Test]
        public void DateTimeToMilliseconds_UtcEpoch_ReturnsZero()
        {
            var result = TimerHelper.DateTimeToMilliseconds(TimerHelper.EpochUtc, utc: true);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void DateTimeToMilliseconds_OneSecondAfterEpoch_Returns1000()
        {
            var time = TimerHelper.EpochUtc.AddMilliseconds(1500);
            var result = TimerHelper.DateTimeToMilliseconds(time, utc: true);
            Assert.That(result, Is.EqualTo(1500));
        }

        [Test]
        public void DateTimeToMilliseconds_BeforeEpoch_ReturnsNegative()
        {
            var time = TimerHelper.EpochUtc.AddMilliseconds(-500);
            var result = TimerHelper.DateTimeToMilliseconds(time, utc: true);
            Assert.That(result, Is.EqualTo(-500));
        }

        [Test]
        public void DateTimeToSecond_WithTimeZone_ReflectsCurrentTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var epochInChina = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.DateTimeToSecond(epochInChina, utc: false);
            Assert.That(result, Is.EqualTo(0));
        }

        #endregion

        #region UnixTimeSeconds / UnixTimeMilliseconds

        [Test]
        public void UnixTimeSeconds_ReturnsCurrentTimestamp()
        {
            var localSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var result = TimerHelper.UnixTimeSeconds();
            Assert.That(result, Is.GreaterThanOrEqualTo(localSeconds));
            Assert.That(result, Is.LessThanOrEqualTo(localSeconds + 2));
        }

        [Test]
        public void UnixTimeMilliseconds_ReturnsCurrentTimestamp()
        {
            var localMillis = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var result = TimerHelper.UnixTimeMilliseconds();
            Assert.That(result, Is.GreaterThanOrEqualTo(localMillis));
            Assert.That(result, Is.LessThanOrEqualTo(localMillis + 2000));
        }

        [Test]
        public void UnixTimeSeconds_IncludesTimeOffset()
        {
            var before = TimerHelper.UnixTimeSeconds();
            TimerHelper.SyncServerTimeSeconds(before + 100);
            var after = TimerHelper.UnixTimeSeconds();
            Assert.That(after, Is.GreaterThanOrEqualTo(before + 99));
            Assert.That(after, Is.LessThanOrEqualTo(before + 102));
        }

        [Test]
        public void UnixTimeMilliseconds_IncludesTimeOffset()
        {
            var before = TimerHelper.UnixTimeMilliseconds();
            TimerHelper.SyncServerTimeMilliseconds(before + 5000);
            var after = TimerHelper.UnixTimeMilliseconds();
            Assert.That(after, Is.GreaterThanOrEqualTo(before + 4000));
            Assert.That(after, Is.LessThanOrEqualTo(before + 7000));
        }

        #endregion

        #region UnixTimeSecondsWithTimeZoneOffset / UnixTimeMillisecondsWithTimeZoneOffset

        [Test]
        public void UnixTimeSecondsWithTimeZoneOffset_IncludesTimeZoneAndServerOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var localSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TimerHelper.SyncServerTimeSeconds(localSeconds + 100);

            var result = TimerHelper.UnixTimeSecondsWithTimeZoneOffset();
            var expected = localSeconds + 28800 + 100;
            Assert.That(result, Is.GreaterThanOrEqualTo(expected));
            Assert.That(result, Is.LessThanOrEqualTo(expected + 2));
        }

        [Test]
        public void UnixTimeMillisecondsWithTimeZoneOffset_IncludesTimeZoneAndServerOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var localMillis = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            TimerHelper.SyncServerTimeMilliseconds(localMillis + 5000);

            var result = TimerHelper.UnixTimeMillisecondsWithTimeZoneOffset();
            var expected = localMillis + 28800000 + 5000;
            Assert.That(result, Is.GreaterThanOrEqualTo(expected));
            Assert.That(result, Is.LessThanOrEqualTo(expected + 2000));
        }

        #endregion

        #region DateTimeToSecondsWithTimeZone / TimeToMillisecondsWithTimeZone

        [Test]
        public void DateTimeToSecondsWithTimeZone_UtcInput_AddsTimeZoneOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var utcTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.DateTimeToSecondsWithTimeZone(utcTime);
            var expected = new DateTimeOffset(utcTime).ToUnixTimeSeconds() + 28800;
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TimeToMillisecondsWithTimeZone_UtcInput_AddsTimeZoneOffset()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var utcTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.TimeToMillisecondsWithTimeZone(utcTime);
            var expected = new DateTimeOffset(utcTime).ToUnixTimeMilliseconds() + 28800000;
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DateTimeToSecondsWithTimeZone_IncludesTimeOffset()
        {
            TimerHelper.SyncServerTimeSeconds(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 60);
            var utcTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = TimerHelper.DateTimeToSecondsWithTimeZone(utcTime);
            var expected = new DateTimeOffset(utcTime).ToUnixTimeSeconds() + 60;
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion
    }
}
