using System;
using System.Threading;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ThreadLocalRandomTests
    {
        [SetUp]
        public void SetUp()
        {
            ThreadLocalRandom.SetSeed(Environment.TickCount);
        }

        #region Current

        [Test]
        public void Current_IsNotNull()
        {
            Assert.IsNotNull(ThreadLocalRandom.Current);
        }

        [Test]
        public void Current_ReturnsSameInstanceOnSameThread()
        {
            var first = ThreadLocalRandom.Current;
            var second = ThreadLocalRandom.Current;

            Assert.AreSame(first, second);
        }

        #endregion

        #region SetSeed

        [Test]
        public void SetSeed_ProducesReproducibleSequence()
        {
            ThreadLocalRandom.SetSeed(42);
            int val1 = ThreadLocalRandom.Current.Next();
            int val2 = ThreadLocalRandom.Current.Next();

            ThreadLocalRandom.SetSeed(42);
            int val3 = ThreadLocalRandom.Current.Next();
            int val4 = ThreadLocalRandom.Current.Next();

            Assert.AreEqual(val1, val3);
            Assert.AreEqual(val2, val4);
        }

        #endregion

        #region NextInt64

        [Test]
        public void NextInt64_ReturnsLongValue()
        {
            long result = ThreadLocalRandom.NextInt64();

            Assert.IsTrue(result >= long.MinValue && result <= long.MaxValue);
        }

        [Test]
        public void NextInt64_WithSeed_ReturnsReproducibleValue()
        {
            ThreadLocalRandom.SetSeed(123);
            long first = ThreadLocalRandom.NextInt64();

            ThreadLocalRandom.SetSeed(123);
            long second = ThreadLocalRandom.NextInt64();

            Assert.AreEqual(first, second);
        }

        [Test]
        public void NextInt64_MultipleCalls_ReturnDifferentValues()
        {
            ThreadLocalRandom.SetSeed(Environment.TickCount);
            long a = ThreadLocalRandom.NextInt64();
            long b = ThreadLocalRandom.NextInt64();

            Assert.AreNotEqual(a, b);
        }

        #endregion

        #region NextUInt64

        [Test]
        public void NextUInt64_ReturnsUlongValue()
        {
            ulong result = ThreadLocalRandom.NextUInt64();

            Assert.IsTrue(result >= ulong.MinValue && result <= ulong.MaxValue);
        }

        [Test]
        public void NextUInt64_WithSeed_ReturnsReproducibleValue()
        {
            ThreadLocalRandom.SetSeed(456);
            ulong first = ThreadLocalRandom.NextUInt64();

            ThreadLocalRandom.SetSeed(456);
            ulong second = ThreadLocalRandom.NextUInt64();

            Assert.AreEqual(first, second);
        }

        #endregion

        #region Integration

        [Test]
        public void NextInt64_AfterResetSeed_ProducesNewSequence()
        {
            ThreadLocalRandom.SetSeed(1);
            long a = ThreadLocalRandom.NextInt64();

            ThreadLocalRandom.SetSeed(2);
            long b = ThreadLocalRandom.NextInt64();

            Assert.AreNotEqual(a, b);
        }

        #endregion
    }
}
