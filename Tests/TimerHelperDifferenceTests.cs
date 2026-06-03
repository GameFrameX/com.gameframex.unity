using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperDifferenceTests
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

        #region GetTimeDifference

        [Test]
        public void GetTimeDifference_EndAfterStart_ReturnsPositive()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 1, 0, 0);
            var result = TimerHelper.GetTimeDifference(start, end);
            Assert.That(result, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        [Test]
        public void GetTimeDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 2, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 0);
            var result = TimerHelper.GetTimeDifference(start, end);
            Assert.That(result, Is.EqualTo(TimeSpan.FromDays(-1)));
        }

        [Test]
        public void GetTimeDifference_SameTime_ReturnsZero()
        {
            var time = new DateTime(2024, 6, 15, 12, 0, 0);
            var result = TimerHelper.GetTimeDifference(time, time);
            Assert.That(result, Is.EqualTo(TimeSpan.Zero));
        }

        #endregion

        #region GetSecondsDifference (DateTime)

        [Test]
        public void GetSecondsDifference_OneMinute_Returns60()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 1, 0);
            Assert.That(TimerHelper.GetSecondsDifference(start, end), Is.EqualTo(60));
        }

        [Test]
        public void GetSecondsDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 1, 0, 1, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 0);
            Assert.That(TimerHelper.GetSecondsDifference(start, end), Is.EqualTo(-60));
        }

        [Test]
        public void GetSecondsDifference_SameTime_ReturnsZero()
        {
            var time = new DateTime(2024, 5, 10, 12, 0, 0);
            Assert.That(TimerHelper.GetSecondsDifference(time, time), Is.EqualTo(0));
        }

        #endregion

        #region GetMillisecondsDifference (DateTime)

        [Test]
        public void GetMillisecondsDifference_OneSecond_Returns1000()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 1);
            Assert.That(TimerHelper.GetMillisecondsDifference(start, end), Is.EqualTo(1000));
        }

        [Test]
        public void GetMillisecondsDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 1);
            var end = new DateTime(2024, 1, 1, 0, 0, 0);
            Assert.That(TimerHelper.GetMillisecondsDifference(start, end), Is.EqualTo(-1000));
        }

        [Test]
        public void GetMillisecondsDifference_500Ms_Returns500()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 0, 500);
            Assert.That(TimerHelper.GetMillisecondsDifference(start, end), Is.EqualTo(500));
        }

        #endregion

        #region GetMinutesDifference

        [Test]
        public void GetMinutesDifference_OneHour_Returns60()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 1, 0, 0);
            Assert.That(TimerHelper.GetMinutesDifference(start, end), Is.EqualTo(60.0));
        }

        [Test]
        public void GetMinutesDifference_30Seconds_ReturnsHalfMinute()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 30);
            Assert.That(TimerHelper.GetMinutesDifference(start, end), Is.EqualTo(0.5));
        }

        [Test]
        public void GetMinutesDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 1, 1, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 0);
            Assert.That(TimerHelper.GetMinutesDifference(start, end), Is.EqualTo(-60.0));
        }

        #endregion

        #region GetHoursDifference

        [Test]
        public void GetHoursDifference_OneDay_Returns24()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 2, 0, 0, 0);
            Assert.That(TimerHelper.GetHoursDifference(start, end), Is.EqualTo(24.0));
        }

        [Test]
        public void GetHoursDifference_30Minutes_ReturnsHalfHour()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 30, 0);
            Assert.That(TimerHelper.GetHoursDifference(start, end), Is.EqualTo(0.5));
        }

        [Test]
        public void GetHoursDifference_EndBeforeStart_ReturnsNegative()
        {
            var start = new DateTime(2024, 1, 2, 0, 0, 0);
            var end = new DateTime(2024, 1, 1, 0, 0, 0);
            Assert.That(TimerHelper.GetHoursDifference(start, end), Is.EqualTo(-24.0));
        }

        #endregion

        #region GetSecondsDifference (long timestamps)

        [Test]
        public void GetSecondsDifference_Timestamps_OneMinute_Returns60()
        {
            Assert.That(TimerHelper.GetSecondsDifference(100, 160), Is.EqualTo(60));
        }

        [Test]
        public void GetSecondsDifference_Timestamps_EndBeforeStart_ReturnsNegative()
        {
            Assert.That(TimerHelper.GetSecondsDifference(200, 100), Is.EqualTo(-100));
        }

        [Test]
        public void GetSecondsDifference_Timestamps_Same_ReturnsZero()
        {
            Assert.That(TimerHelper.GetSecondsDifference(500, 500), Is.EqualTo(0));
        }

        #endregion

        #region GetMillisecondsDifference (long timestamps)

        [Test]
        public void GetMillisecondsDifference_Timestamps_OneSec_Returns1000()
        {
            Assert.That(TimerHelper.GetMillisecondsDifference(1000, 2000), Is.EqualTo(1000));
        }

        [Test]
        public void GetMillisecondsDifference_Timestamps_EndBeforeStart_ReturnsNegative()
        {
            Assert.That(TimerHelper.GetMillisecondsDifference(5000, 1000), Is.EqualTo(-4000));
        }

        [Test]
        public void GetMillisecondsDifference_Timestamps_Same_ReturnsZero()
        {
            Assert.That(TimerHelper.GetMillisecondsDifference(9999, 9999), Is.EqualTo(0));
        }

        #endregion

        #region GetAbsoluteSecondsDifference / GetAbsoluteMillisecondsDifference

        [Test]
        public void GetAbsoluteSecondsDifference_RegardlessOfOrder()
        {
            var t1 = new DateTime(2024, 1, 1, 0, 0, 5);
            var t2 = new DateTime(2024, 1, 1, 0, 0, 0);
            Assert.That(TimerHelper.GetAbsoluteSecondsDifference(t1, t2), Is.EqualTo(5));
            Assert.That(TimerHelper.GetAbsoluteSecondsDifference(t2, t1), Is.EqualTo(5));
        }

        [Test]
        public void GetAbsoluteSecondsDifference_SameTime_ReturnsZero()
        {
            var time = new DateTime(2024, 5, 10, 12, 0, 0);
            Assert.That(TimerHelper.GetAbsoluteSecondsDifference(time, time), Is.EqualTo(0));
        }

        [Test]
        public void GetAbsoluteMillisecondsDifference_RegardlessOfOrder()
        {
            var t1 = new DateTime(2024, 1, 1, 0, 0, 0, 500);
            var t2 = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            Assert.That(TimerHelper.GetAbsoluteMillisecondsDifference(t1, t2), Is.EqualTo(500));
            Assert.That(TimerHelper.GetAbsoluteMillisecondsDifference(t2, t1), Is.EqualTo(500));
        }

        [Test]
        public void GetAbsoluteMillisecondsDifference_SameTime_ReturnsZero()
        {
            var time = new DateTime(2024, 5, 10, 12, 0, 0, 0);
            Assert.That(TimerHelper.GetAbsoluteMillisecondsDifference(time, time), Is.EqualTo(0));
        }

        #endregion

        #region GetTimeDifference (timestamps)

        [Test]
        public void GetTimeDifference_WithUtcTimestamps_Computed()
        {
            var ts1 = new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
            var ts2 = new DateTimeOffset(new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
            var result = TimerHelper.GetTimeDifference(ts1, ts2);
            Assert.That(result, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        [Test]
        public void GetTimeDifference_WithUtcFalse_UsesTimeZone()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var ts1 = 0L;
            var ts2 = 3600L;
            // Both are epoch-based; with utc=false, they're interpreted in CurrentTimeZone
            var result = TimerHelper.GetTimeDifference(ts1, ts2, false);
            Assert.That(result, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        #endregion

        #region GetTimeDifferenceMillisecond (timestamps)

        [Test]
        public void GetTimeDifferenceMillisecond_ValidTimestamps_Computed()
        {
            var ts1 = 0L;
            var ts2 = 5000L;
            var result = TimerHelper.GetTimeDifferenceMillisecond(ts1, ts2);
            Assert.That(result, Is.EqualTo(TimeSpan.FromMilliseconds(5000)));
        }

        #endregion

        #region GetTimeDifferenceFromNow

        [Test]
        public void GetTimeDifferenceFromNow_PastTime_ReturnsPositive()
        {
            var past = DateTime.UtcNow.AddHours(-1);
            var result = TimerHelper.GetTimeDifferenceFromNow(past, isUseUtc: true);
            Assert.That(result.TotalHours, Is.EqualTo(1.0).Within(0.01));
        }

        [Test]
        public void GetTimeDifferenceFromNow_FutureTime_ReturnsNegative()
        {
            var future = DateTime.UtcNow.AddHours(1);
            var result = TimerHelper.GetTimeDifferenceFromNow(future, isUseUtc: true);
            Assert.That(result.TotalHours, Is.EqualTo(-1.0).Within(0.01));
        }

        [Test]
        public void GetTimeDifferenceFromNow_Timestamp_ReturnsPositiveForPast()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddMinutes(-5)).ToUnixTimeSeconds();
            var result = TimerHelper.GetTimeDifferenceFromNow(pastTs, isUseUtc: true);
            Assert.That(result.TotalMinutes, Is.EqualTo(5.0).Within(0.01));
        }

        [Test]
        public void GetTimeDifferenceFromNowMs_MillisecondTimestamp_Works()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddSeconds(-3)).ToUnixTimeMilliseconds();
            var result = TimerHelper.GetTimeDifferenceFromNowMs(pastTs, isUseUtc: true);
            Assert.That(result.TotalSeconds, Is.EqualTo(3.0).Within(0.01));
        }

        #endregion

        #region GetElapsedSeconds

        [Test]
        public void GetElapsedSeconds_PastTime_ReturnsPositive()
        {
            var past = DateTime.UtcNow.AddSeconds(-30);
            var result = TimerHelper.GetElapsedSeconds(past, isUseUtc: true);
            Assert.That(result, Is.EqualTo(30).Within(1));
        }

        [Test]
        public void GetElapsedSeconds_Now_ReturnsNearZero()
        {
            var result = TimerHelper.GetElapsedSeconds(DateTime.UtcNow, isUseUtc: true);
            Assert.That(result, Is.EqualTo(0).Within(1));
        }

        #endregion

        #region GetElapsedSecondsWithUtc / GetElapsedMillisecondsWithUtc

        [Test]
        public void GetElapsedSecondsWithUtc_PastTimestamp_ReturnsPositive()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddSeconds(-60)).ToUnixTimeSeconds();
            var result = TimerHelper.GetElapsedSecondsWithUtc(pastTs);
            Assert.That(result, Is.EqualTo(60).Within(1));
        }

        [Test]
        public void GetElapsedMillisecondsWithUtc_PastTimestamp_ReturnsPositive()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddMilliseconds(-2000)).ToUnixTimeMilliseconds();
            var result = TimerHelper.GetElapsedMillisecondsWithUtc(pastTs);
            Assert.That(result, Is.EqualTo(2000).Within(1000));
        }

        #endregion

        #region GetElapsedSecondsWithTimeZone

        [Test]
        public void GetElapsedSecondsWithTimeZone_PastTimestamp_ReturnsPositive()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddSeconds(-45)).ToUnixTimeSeconds();
            var result = TimerHelper.GetElapsedSecondsWithTimeZone(pastTs);
            Assert.That(result, Is.EqualTo(45).Within(1));
        }

        [Test]
        public void GetElapsedMillisecondsWithTimeZone_PastTimestamp_ReturnsPositive()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddMilliseconds(-1500)).ToUnixTimeMilliseconds();
            var result = TimerHelper.GetElapsedMillisecondsWithTimeZone(pastTs);
            Assert.That(result, Is.EqualTo(1500).Within(1000));
        }

        #endregion

        #region TimeZone-specific Difference Methods

        [Test]
        public void GetTimeDifferenceWithTimeZone_Timestamps_Computed()
        {
            var ts1 = 0L;
            var ts2 = 3600L;
            var result = TimerHelper.GetTimeDifferenceWithTimeZone(ts1, ts2);
            Assert.That(result, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        [Test]
        public void GetTimeDifferenceMillisecondWithTimeZone_Computed()
        {
            var ts1 = 0L;
            var ts2 = 1000L;
            var result = TimerHelper.GetTimeDifferenceMillisecondWithTimeZone(ts1, ts2);
            Assert.That(result, Is.EqualTo(TimeSpan.FromMilliseconds(1000)));
        }

        [Test]
        public void GetTimeDifferenceFromNowWithTimeZone_Past_ReturnsPositive()
        {
            var past = TimerHelper.GetNowWithTimeZone().AddMinutes(-10);
            var result = TimerHelper.GetTimeDifferenceFromNowWithTimeZone(past);
            Assert.That(result.TotalMinutes, Is.EqualTo(10.0).Within(0.01));
        }

        [Test]
        public void GetTimeDifferenceFromNowWithTimeZone_Timestamp_Works()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddSeconds(-10)).ToUnixTimeSeconds();
            var result = TimerHelper.GetTimeDifferenceFromNowWithTimeZone(pastTs);
            Assert.That(result.TotalSeconds, Is.EqualTo(10.0).Within(1.0));
        }

        [Test]
        public void GetTimeDifferenceFromNowMsWithTimeZone_Works()
        {
            var pastTs = new DateTimeOffset(DateTime.UtcNow.AddMilliseconds(-500)).ToUnixTimeMilliseconds();
            var result = TimerHelper.GetTimeDifferenceFromNowMsWithTimeZone(pastTs);
            Assert.That(result.TotalMilliseconds, Is.EqualTo(500.0).Within(50));
        }

        [Test]
        public void GetElapsedSecondsWithTimeZone_DateTime_Works()
        {
            var past = TimerHelper.GetNowWithTimeZone().AddSeconds(-20);
            var result = TimerHelper.GetElapsedSecondsWithTimeZone(past);
            Assert.That(result, Is.EqualTo(20).Within(1));
        }

        #endregion
    }
}
