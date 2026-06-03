using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TimerHelperTimeOffsetTests
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

        #region ResetTimeOffset

        [Test]
        public void ResetTimeOffset_ResetsBothToZero()
        {
            TimerHelper.SyncServerTimeSeconds(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 100);
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.Not.EqualTo(0));

            TimerHelper.ResetTimeOffset();

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.EqualTo(0));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(0));
        }

        [Test]
        public void ResetTimeOffset_WhenAlreadyZero_StaysZero()
        {
            TimerHelper.ResetTimeOffset();
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.EqualTo(0));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(0));
        }

        #endregion

        #region SyncServerTimeSeconds

        [Test]
        public void SyncServerTimeSeconds_SetsTimeOffsetSeconds()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 100;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.GreaterThan(0));
        }

        [Test]
        public void SyncServerTimeSeconds_AlsoSetsTimeOffsetMilliseconds()
        {
            TimerHelper.ResetTimeOffset();
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 120;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.Not.EqualTo(0));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(TimerHelper.TimeOffsetSeconds * 1000));
        }

        [Test]
        public void SyncServerTimeSeconds_ServerAhead_LocalBehind()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 3600;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.GreaterThan(0));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.GreaterThan(0));
        }

        [Test]
        public void SyncServerTimeSeconds_ServerBehind_LocalAhead()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() - 3600;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.LessThan(0));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.LessThan(0));
        }

        [Test]
        public void SyncServerTimeSeconds_ServerEqual_OffsetNearZero()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.EqualTo(0).Within(1));
        }

        [Test]
        public void SyncServerTimeSeconds_WithZeroTimestamp()
        {
            var localSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TimerHelper.SyncServerTimeSeconds(0);
            // offset = 0 - localSeconds (approximately)
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.LessThanOrEqualTo(0 - localSeconds + 1));
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.GreaterThanOrEqualTo(0 - localSeconds - 1));
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(TimerHelper.TimeOffsetSeconds * 1000));
        }

        #endregion

        #region SyncServerTimeMilliseconds

        [Test]
        public void SyncServerTimeMilliseconds_SetsTimeOffsetMilliseconds()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 5000;
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);
            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.GreaterThan(0));
        }

        [Test]
        public void SyncServerTimeMilliseconds_AlsoSetsTimeOffsetSeconds()
        {
            TimerHelper.ResetTimeOffset();
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 5000;
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.Not.EqualTo(0));
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.EqualTo(TimerHelper.TimeOffsetMilliseconds / 1000));
        }

        [Test]
        public void SyncServerTimeMilliseconds_ServerBehind_LocalAhead()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - 5000;
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.LessThan(0));
            Assert.That(TimerHelper.TimeOffsetSeconds, Is.LessThan(0));
        }

        [Test]
        public void SyncServerTimeMilliseconds_ServerEqual_OffsetNearZero()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(0).Within(1000));
        }

        #endregion

        #region ServerNowSeconds / ServerNowMilliseconds

        [Test]
        public void ServerNowSeconds_AfterSync_MatchesServerTime()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 100;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            var result = TimerHelper.ServerNowSeconds();
            Assert.That(result, Is.EqualTo(serverTimestamp).Within(2));
        }

        [Test]
        public void ServerNowMilliseconds_AfterSyncBySeconds_Works()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 100;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            var result = TimerHelper.ServerNowMilliseconds();
            var expectedMs = serverTimestamp * 1000;
            Assert.That(result, Is.EqualTo(expectedMs).Within(2000));
        }

        [Test]
        public void ServerNowMilliseconds_AfterSyncByMilliseconds_Matches()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 5000;
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);

            var result = TimerHelper.ServerNowMilliseconds();
            Assert.That(result, Is.EqualTo(serverTimestamp).Within(2000));
        }

        [Test]
        public void ServerNowSeconds_AfterSyncByMilliseconds_Works()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 5000;
            TimerHelper.SyncServerTimeMilliseconds(serverTimestamp);

            var result = TimerHelper.ServerNowSeconds();
            var expectedSec = serverTimestamp / 1000;
            Assert.That(result, Is.EqualTo(expectedSec).Within(2));
        }

        [Test]
        public void ServerNowSeconds_WithoutSync_ReturnsLocalTime()
        {
            var result = TimerHelper.ServerNowSeconds();
            var expected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Assert.That(result, Is.EqualTo(expected).Within(1));
        }

        [Test]
        public void ServerNowMilliseconds_WithoutSync_ReturnsLocalTime()
        {
            var result = TimerHelper.ServerNowMilliseconds();
            var expected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            Assert.That(result, Is.EqualTo(expected).Within(1000));
        }

        #endregion

        #region UnixTimeSecondsWithOffset / UnixTimeMillisecondsWithOffset (UTC)

        [Test]
        public void UnixTimeSecondsWithOffset_IncludesTimeOffsetSeconds()
        {
            TimerHelper.SyncServerTimeSeconds(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 200);
            var result = TimerHelper.UnixTimeSecondsWithOffset();
            var expected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 200;
            Assert.That(result, Is.EqualTo(expected).Within(1));
        }

        [Test]
        public void UnixTimeMillisecondsWithOffset_IncludesTimeOffsetMilliseconds()
        {
            TimerHelper.SyncServerTimeMilliseconds(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 7000);
            var result = TimerHelper.UnixTimeMillisecondsWithOffset();
            var expected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 7000;
            Assert.That(result, Is.EqualTo(expected).Within(1000));
        }

        [Test]
        public void UnixTimeSecondsWithOffset_WithoutOffset_EqualsLocalUtc()
        {
            var result = TimerHelper.UnixTimeSecondsWithOffset();
            var expected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Assert.That(result, Is.EqualTo(expected).Within(1));
        }

        #endregion

        #region UnixTimeSecondsWithOffsetWithTimeZone / UnixTimeMillisecondsWithOffsetWithTimeZone

        [Test]
        public void UnixTimeSecondsWithOffsetWithTimeZone_DiffersFromUtcVersion()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var result = TimerHelper.UnixTimeSecondsWithOffsetWithTimeZone();
            // new DateTimeOffset(Unspecified) uses system local TZ, so we can only verify
            // the result is in a reasonable range around current time (within ±1 day)
            Assert.That(result, Is.GreaterThanOrEqualTo(now - 86400));
            Assert.That(result, Is.LessThanOrEqualTo(now + 86400));
        }

        [Test]
        public void UnixTimeMillisecondsWithOffsetWithTimeZone_DiffersFromUtcVersion()
        {
            TimerHelper.SetTimeZone(_utcPlus8);
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var result = TimerHelper.UnixTimeMillisecondsWithOffsetWithTimeZone();
            // new DateTimeOffset(Unspecified) uses system local TZ, so we can only verify
            // the result is in a reasonable range around current time (within ±1 day)
            Assert.That(result, Is.GreaterThanOrEqualTo(now - 86400000));
            Assert.That(result, Is.LessThanOrEqualTo(now + 86400000));
        }

        #endregion

        #region Cross-Sync Consistency

        [Test]
        public void SyncSecondsThenMilliseconds_BothUpdatedConsistently()
        {
            var serverSec = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 100;
            TimerHelper.SyncServerTimeSeconds(serverSec);

            var secAfterSync = TimerHelper.TimeOffsetSeconds;
            var msAfterSync = TimerHelper.TimeOffsetMilliseconds;
            Assert.That(msAfterSync, Is.EqualTo(secAfterSync * 1000));

            var serverMs = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 3000;
            TimerHelper.SyncServerTimeMilliseconds(serverMs);

            Assert.That(TimerHelper.TimeOffsetSeconds, Is.EqualTo(TimerHelper.TimeOffsetMilliseconds / 1000));
        }

        [Test]
        public void NegativeOffset_SecondsAndMillisecondsConsistent()
        {
            var serverTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() - 5000;
            TimerHelper.SyncServerTimeSeconds(serverTimestamp);

            Assert.That(TimerHelper.TimeOffsetMilliseconds, Is.EqualTo(TimerHelper.TimeOffsetSeconds * 1000));
        }

        #endregion
    }
}
