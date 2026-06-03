using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class RandomHelperTests
    {
        [SetUp]
        public void SetUp()
        {
            RandomHelper.SetSeed(42);
        }

        #region SetSeed

        [Test]
        public void SetSeed_ProducesReproducibleSequence()
        {
            RandomHelper.SetSeed(42);
            int first = RandomHelper.Next(0, 1000);
            int second = RandomHelper.Next(0, 1000);

            RandomHelper.SetSeed(42);
            int firstAgain = RandomHelper.Next(0, 1000);
            int secondAgain = RandomHelper.Next(0, 1000);

            Assert.AreEqual(first, firstAgain);
            Assert.AreEqual(second, secondAgain);
        }

        [Test]
        public void SetSeed_DifferentSeeds_ProduceDifferentValues()
        {
            RandomHelper.SetSeed(1);
            int fromSeed1 = RandomHelper.Next(0, int.MaxValue);

            RandomHelper.SetSeed(999);
            int fromSeed999 = RandomHelper.Next(0, int.MaxValue);

            Assert.AreNotEqual(fromSeed1, fromSeed999);
        }

        #endregion

        #region Next (range)

        [Test]
        public void Next_ReturnsValueWithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                int result = RandomHelper.Next(10, 20);
                Assert.GreaterOrEqual(result, 10, "Result should be >= lower bound");
                Assert.Less(result, 20, "Result should be < upper bound");
            }
        }

        [Test]
        public void Next_SingleRange_ReturnsLower()
        {
            int result = RandomHelper.Next(5, 6);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void Next_NegativeRange_ReturnsWithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                int result = RandomHelper.Next(-100, -50);
                Assert.GreaterOrEqual(result, -100);
                Assert.Less(result, -50);
            }
        }

        [Test]
        public void Next_LargeRange_ReturnsWithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                int result = RandomHelper.Next(int.MinValue, int.MaxValue);
                Assert.GreaterOrEqual(result, int.MinValue);
                Assert.Less(result, int.MaxValue);
            }
        }

        #endregion

        #region Next (float 0-1)

        [Test]
        public void Next_NoArgs_ReturnsFloatBetweenZeroAndOne()
        {
            for (int i = 0; i < 100; i++)
            {
                float result = RandomHelper.Next();
                Assert.GreaterOrEqual(result, 0.0f, "Result should be >= 0");
                Assert.Less(result, 1.0f, "Result should be < 1");
            }
        }

        [Test]
        public void Next_NoArgs_WithSeed_ReturnsReproducibleValue()
        {
            RandomHelper.SetSeed(123);
            float first = RandomHelper.Next();

            RandomHelper.SetSeed(123);
            float second = RandomHelper.Next();

            Assert.AreEqual(first, second);
        }

        #endregion

        #region NextInt64

        [Test]
        public void NextInt64_ReturnsNonZeroOrAnyLongValue()
        {
            long result = RandomHelper.NextInt64();

            Assert.That(result, Is.TypeOf<long>());
        }

        [Test]
        public void NextInt64_WithSeed_ReturnsReproducibleValue()
        {
            RandomHelper.SetSeed(77);
            long first = RandomHelper.NextInt64();

            RandomHelper.SetSeed(77);
            long second = RandomHelper.NextInt64();

            Assert.AreEqual(first, second);
        }

        [Test]
        public void NextInt64_MultipleCalls_ProduceDifferentValues()
        {
            long a = RandomHelper.NextInt64();
            long b = RandomHelper.NextInt64();
            long c = RandomHelper.NextInt64();

            Assert.AreNotEqual(a, b, "Two consecutive calls should usually differ");
            Assert.AreNotEqual(b, c, "Three consecutive calls should usually differ");
        }

        #endregion

        #region NextUInt64

        [Test]
        public void NextUInt64_ReturnsULongValue()
        {
            ulong result = RandomHelper.NextUInt64();

            Assert.That(result, Is.TypeOf<ulong>());
        }

        [Test]
        public void NextUInt64_WithSeed_ReturnsReproducibleValue()
        {
            RandomHelper.SetSeed(55);
            ulong first = RandomHelper.NextUInt64();

            RandomHelper.SetSeed(55);
            ulong second = RandomHelper.NextUInt64();

            Assert.AreEqual(first, second);
        }

        [Test]
        public void NextUInt64_MultipleCalls_ProduceDifferentValues()
        {
            ulong a = RandomHelper.NextUInt64();
            ulong b = RandomHelper.NextUInt64();

            Assert.AreNotEqual(a, b);
        }

        #endregion
    }
}
