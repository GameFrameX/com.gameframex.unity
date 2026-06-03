using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperTimestampTests
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

        #region TimestampToTicks

        [Test]
        public void TimestampToTicks_Epoch_ReturnsEpochTicks()
        {
            var result = TimerHelper.TimestampToTicks(0);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks));
        }

        [Test]
        public void TimestampToTicks_OneSecond_AddsTicksPerSecond()
        {
            var result = TimerHelper.TimestampToTicks(1);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks + TimeSpan.TicksPerSecond));
        }

        [Test]
        public void TimestampToTicks_NegativeValue_Works()
        {
            var result = TimerHelper.TimestampToTicks(-1);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks - TimeSpan.TicksPerSecond));
        }

        [Test]
        public void TimestampToTicks_MinBoundary_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => TimerHelper.TimestampToTicks(-62135596800L));
        }

        [Test]
        public void TimestampToTicks_MaxBoundary_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => TimerHelper.TimestampToTicks(253402300799L));
        }

        [Test]
        public void TimestampToTicks_BelowMin_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimestampToTicks(-62135596801L));
            Assert.That(ex.ParamName, Is.EqualTo("timestampSeconds"));
        }

        [Test]
        public void TimestampToTicks_AboveMax_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimestampToTicks(253402300800L));
            Assert.That(ex.ParamName, Is.EqualTo("timestampSeconds"));
        }

        [Test]
        public void TimestampToTicks_CurrentTimestamp_MatchesDateTime()
        {
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var ticks = TimerHelper.TimestampToTicks(now);
            var dateFromTicks = new DateTime(ticks, DateTimeKind.Utc);
            Assert.That(dateFromTicks, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        #endregion

        #region TimestampMillisToTicks

        [Test]
        public void TimestampMillisToTicks_Epoch_ReturnsEpochTicks()
        {
            var result = TimerHelper.TimestampMillisToTicks(0);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks));
        }

        [Test]
        public void TimestampMillisToTicks_OneMillisecond_AddsTicksPerMillisecond()
        {
            var result = TimerHelper.TimestampMillisToTicks(1);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks + TimeSpan.TicksPerMillisecond));
        }

        [Test]
        public void TimestampMillisToTicks_NegativeValue_Works()
        {
            var result = TimerHelper.TimestampMillisToTicks(-1);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.Ticks - TimeSpan.TicksPerMillisecond));
        }

        [Test]
        public void TimestampMillisToTicks_MinBoundary_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => TimerHelper.TimestampMillisToTicks(-62135596800000L));
        }

        [Test]
        public void TimestampMillisToTicks_MaxBoundary_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => TimerHelper.TimestampMillisToTicks(253402300799999L));
        }

        [Test]
        public void TimestampMillisToTicks_BelowMin_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimestampMillisToTicks(-62135596800001L));
            Assert.That(ex.ParamName, Is.EqualTo("timestampMillisSeconds"));
        }

        [Test]
        public void TimestampMillisToTicks_AboveMax_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimestampMillisToTicks(253402300800000L));
            Assert.That(ex.ParamName, Is.EqualTo("timestampMillisSeconds"));
        }

        [Test]
        public void TimestampMillisToTicks_CurrentTimestamp_MatchesDateTime()
        {
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var ticks = TimerHelper.TimestampMillisToTicks(now);
            var dateFromTicks = new DateTime(ticks, DateTimeKind.Utc);
            Assert.That(dateFromTicks, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        #endregion

        #region TimestampSecondToDateTime

        [Test]
        public void TimestampSecondToDateTime_Zero_ReturnsEpochUtc()
        {
            var result = TimerHelper.TimestampSecondToDateTime(0, utc: true);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc));
            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public void TimestampSecondToDateTime_WithUtcTrue_ReturnsUtcTime()
        {
            var timestamp = 1609459200; // 2021-01-01 00:00:00 UTC
            var result = TimerHelper.TimestampSecondToDateTime(timestamp, utc: true);
            Assert.That(result.Year, Is.EqualTo(2021));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public void TimestampSecondToDateTime_WithUtcFalse_UsesCurrentTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8); // UTC+8
            var timestamp = 0; // epoch UTC
            var result = TimerHelper.TimestampSecondToDateTime(timestamp, utc: false);
            Assert.That(result.Year, Is.EqualTo(1970));
            Assert.That(result.Hour, Is.EqualTo(8)); // UTC+8
        }

        [Test]
        public void TimestampSecondToDateTime_DefaultUtcFalse_UsesCurrentTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var timestamp = 0;
            var result = TimerHelper.TimestampSecondToDateTime(timestamp); // default utc: false
            Assert.That(result.Hour, Is.EqualTo(8));
        }

        [Test]
        public void TimestampSecondToDateTime_NegativeTimestamp_BeforeEpoch()
        {
            var result = TimerHelper.TimestampSecondToDateTime(-3600, utc: true);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.AddHours(-1)));
        }

        #endregion

        #region TimeStampMillisecondToDateTime

        [Test]
        public void TimeStampMillisecondToDateTime_Zero_ReturnsEpochUtc()
        {
            var result = TimerHelper.TimeStampMillisecondToDateTime(0, utc: true);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc));
        }

        [Test]
        public void TimeStampMillisecondToDateTime_WithUtcTrue_ReturnsUtcTime()
        {
            var timestamp = 1609459200000; // 2021-01-01 00:00:00.000 UTC
            var result = TimerHelper.TimeStampMillisecondToDateTime(timestamp, utc: true);
            Assert.That(result.Year, Is.EqualTo(2021));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
        }

        [Test]
        public void TimeStampMillisecondToDateTime_WithUtcFalse_UsesCurrentTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var timestamp = 0;
            var result = TimerHelper.TimeStampMillisecondToDateTime(timestamp, utc: false);
            Assert.That(result.Hour, Is.EqualTo(8)); // UTC+8
        }

        [Test]
        public void TimeStampMillisecondToDateTime_SubMillisecondPrecision()
        {
            var timestamp = 1500; // 1.5 seconds after epoch
            var result = TimerHelper.TimeStampMillisecondToDateTime(timestamp, utc: true);
            Assert.That(result, Is.EqualTo(TimerHelper.EpochUtc.AddMilliseconds(1500)));
        }

        #endregion

        #region TimeSpanWithTimestampUtc / TimeSpanWithTimestampUtcMs

        [Test]
        public void TimeSpanWithTimestampUtc_ValidInput_ReturnsTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampUtc(3600);
            Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(3600)));
        }

        [Test]
        public void TimeSpanWithTimestampUtc_Zero_ReturnsZero()
        {
            var result = TimerHelper.TimeSpanWithTimestampUtc(0);
            Assert.That(result, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void TimeSpanWithTimestampUtc_Negative_ReturnsNegativeTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampUtc(-3600);
            Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(-3600)));
        }

        [Test]
        public void TimeSpanWithTimestampUtc_BelowMin_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimeSpanWithTimestampUtc(-62135596801L));
        }

        [Test]
        public void TimeSpanWithTimestampUtcMs_ValidInput_ReturnsTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampUtcMs(5000);
            Assert.That(result, Is.EqualTo(TimeSpan.FromMilliseconds(5000)));
        }

        [Test]
        public void TimeSpanWithTimestampUtcMs_BelowMin_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimeSpanWithTimestampUtcMs(-62135596800001L));
        }

        #endregion

        #region TimeSpanWithTimestampWithTimeZone / TimeSpanWithTimestampWithTimeZoneMs

        [Test]
        public void TimeSpanWithTimestampWithTimeZone_ValidInput_ReturnsTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampWithTimeZone(7200);
            Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(7200)));
        }

        [Test]
        public void TimeSpanWithTimestampWithTimeZone_Negative_ReturnsNegativeTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampWithTimeZone(-7200);
            Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(-7200)));
        }

        [Test]
        public void TimeSpanWithTimestampWithTimeZone_AboveMax_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimeSpanWithTimestampWithTimeZone(253402300800L));
        }

        [Test]
        public void TimeSpanWithTimestampWithTimeZoneMs_ValidInput_ReturnsTimeSpan()
        {
            var result = TimerHelper.TimeSpanWithTimestampWithTimeZoneMs(10000);
            Assert.That(result, Is.EqualTo(TimeSpan.FromMilliseconds(10000)));
        }

        [Test]
        public void TimeSpanWithTimestampWithTimeZoneMs_AboveMax_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TimerHelper.TimeSpanWithTimestampWithTimeZoneMs(253402300800000L));
        }

        #endregion

        #region Tick Consistency

        [Test]
        public void TimestampToTicks_And_TimestampMillisToTicks_AgreeForSameTime()
        {
            var secTicks = TimerHelper.TimestampToTicks(1000);
            var msTicks = TimerHelper.TimestampMillisToTicks(1000000);
            Assert.That(secTicks, Is.EqualTo(msTicks));
        }

        #endregion
    }
}
