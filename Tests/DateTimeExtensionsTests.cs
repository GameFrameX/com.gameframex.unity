using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        #region GetDaysFrom

        [Test]
        public void GetDaysFrom_SameDate_ReturnsZero()
        {
            var date = new DateTime(2024, 6, 15);

            int result = date.GetDaysFrom(date);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetDaysFrom_OneDayLater_ReturnsOne()
        {
            var earlier = new DateTime(2024, 6, 15);
            var later = new DateTime(2024, 6, 16);

            int result = later.GetDaysFrom(earlier);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetDaysFrom_OneDayEarlier_ReturnsMinusOne()
        {
            var later = new DateTime(2024, 6, 16);
            var earlier = new DateTime(2024, 6, 15);

            int result = earlier.GetDaysFrom(later);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetDaysFrom_CrossesMonthBoundary_CalculatesCorrectly()
        {
            var juneEnd = new DateTime(2024, 6, 30);
            var julyStart = new DateTime(2024, 7, 1);

            int result = julyStart.GetDaysFrom(juneEnd);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetDaysFrom_IgnoresTimeComponent_UsesDateOnly()
        {
            var dt1 = new DateTime(2024, 6, 15, 23, 59, 59);
            var dt2 = new DateTime(2024, 6, 16, 0, 0, 0);

            int result = dt2.GetDaysFrom(dt1);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetDaysFrom_SameDayDifferentTime_ReturnsZero()
        {
            var morning = new DateTime(2024, 6, 15, 6, 0, 0);
            var evening = new DateTime(2024, 6, 15, 22, 0, 0);

            int result = evening.GetDaysFrom(morning);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetDaysFrom_LargeSpan_CalculatesCorrectly()
        {
            var start = new DateTime(2000, 1, 1);
            var end = new DateTime(2000, 12, 31);

            int result = end.GetDaysFrom(start);

            Assert.AreEqual(365, result);
        }

        [Test]
        public void GetDaysFrom_LeapYear_February()
        {
            var start = new DateTime(2024, 2, 28);
            var end = new DateTime(2024, 3, 1);

            int result = end.GetDaysFrom(start);

            Assert.AreEqual(2, result);
        }

        #endregion

        #region GetDaysFromDefault

        [Test]
        public void GetDaysFromDefault_UnixEpoch_ReturnsZero()
        {
            var epoch = new DateTime(1970, 1, 1);

            int result = epoch.GetDaysFromDefault();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetDaysFromDefault_DayAfterEpoch_ReturnsOne()
        {
            var dayAfter = new DateTime(1970, 1, 2);

            int result = dayAfter.GetDaysFromDefault();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetDaysFromDefault_BeforeEpoch_ReturnsNegative()
        {
            var before = new DateTime(1969, 12, 31);

            int result = before.GetDaysFromDefault();

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetDaysFromDefault_KnownDate_CalculatesCorrectly()
        {
            var known = new DateTime(2000, 1, 1);
            var epoch = new DateTime(1970, 1, 1);
            int expected = (int)(known.Date - epoch).TotalDays;

            int result = known.GetDaysFromDefault();

            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}
