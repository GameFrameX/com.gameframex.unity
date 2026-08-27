using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityRandomUtilityTests
    {
        #region GetRandom (no args)

        [Test]
        public void GetRandom_ReturnsNonNegative()
        {
            int result = RandomUtility.GetRandom();
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void GetRandom_MultipleCalls_SomeDifferent()
        {
            HashSet<int> results = new HashSet<int>();
            for (int i = 0; i < 100; i++)
            {
                results.Add(RandomUtility.GetRandom());
            }
            Assert.Greater(results.Count, 1, "100 random calls should produce more than 1 unique value");
        }

        #endregion

        #region GetRandom (maxValue)

        [Test]
        public void GetRandom_MaxValue_Zero()
        {
            int result = RandomUtility.GetRandom(0);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetRandom_MaxValue_One()
        {
            int result = RandomUtility.GetRandom(1);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetRandom_MaxValue_WithinRange()
        {
            const int max = 100;
            for (int i = 0; i < 200; i++)
            {
                int result = RandomUtility.GetRandom(max);
                Assert.GreaterOrEqual(result, 0);
                Assert.Less(result, max);
            }
        }

        [Test]
        public void GetRandom_MaxValue_LargeRange()
        {
            const int max = 1000000;
            for (int i = 0; i < 100; i++)
            {
                int result = RandomUtility.GetRandom(max);
                Assert.GreaterOrEqual(result, 0);
                Assert.Less(result, max);
            }
        }

        #endregion

        #region GetRandom (minValue, maxValue)

        [Test]
        public void GetRandom_Range_SameMinAndMax_ReturnsMin()
        {
            int result = RandomUtility.GetRandom(5, 5);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void GetRandom_Range_WithinRange()
        {
            const int min = 10;
            const int max = 20;
            for (int i = 0; i < 200; i++)
            {
                int result = RandomUtility.GetRandom(min, max);
                Assert.GreaterOrEqual(result, min);
                Assert.Less(result, max);
            }
        }

        [Test]
        public void GetRandom_Range_NegativeMin()
        {
            const int min = -50;
            const int max = 50;
            for (int i = 0; i < 200; i++)
            {
                int result = RandomUtility.GetRandom(min, max);
                Assert.GreaterOrEqual(result, min);
                Assert.Less(result, max);
            }
        }

        [Test]
        public void GetRandom_Range_SpanIsOne()
        {
            int result = RandomUtility.GetRandom(42, 43);
            Assert.AreEqual(42, result);
        }

        #endregion

        #region GetRandomDouble

        [Test]
        public void GetRandomDouble_WithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                double result = RandomUtility.GetRandomDouble();
                Assert.GreaterOrEqual(result, 0.0);
                Assert.Less(result, 1.0);
            }
        }

        [Test]
        public void GetRandomDouble_MultipleCalls_SomeDifferent()
        {
            HashSet<double> results = new HashSet<double>();
            for (int i = 0; i < 100; i++)
            {
                results.Add(RandomUtility.GetRandomDouble());
            }
            Assert.Greater(results.Count, 1);
        }

        #endregion

        #region GetRandomBytes

        [Test]
        public void GetRandomBytes_FillsBuffer()
        {
            byte[] buffer = new byte[100];
            RandomUtility.GetRandomBytes(buffer);
            Assert.AreEqual(100, buffer.Length);
        }

        [Test]
        public void GetRandomBytes_NotAllZeros()
        {
            byte[] buffer = new byte[100];
            RandomUtility.GetRandomBytes(buffer);
            bool allZero = true;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] != 0)
                {
                    allZero = false;
                    break;
                }
            }
            Assert.IsFalse(allZero, "Random bytes should not all be zero");
        }

        [Test]
        public void GetRandomBytes_SingleByte()
        {
            byte[] buffer = new byte[1];
            RandomUtility.GetRandomBytes(buffer);
            Assert.AreEqual(1, buffer.Length);
        }

        [Test]
        public void GetRandomBytes_TwoCallsDifferent()
        {
            byte[] buffer1 = new byte[1000];
            byte[] buffer2 = new byte[1000];
            RandomUtility.GetRandomBytes(buffer1);
            RandomUtility.GetRandomBytes(buffer2);
            bool allSame = true;
            for (int i = 0; i < buffer1.Length; i++)
            {
                if (buffer1[i] != buffer2[i])
                {
                    allSame = false;
                    break;
                }
            }
            Assert.IsFalse(allSame, "Two random byte arrays should differ");
        }

        #endregion

        #region SetSeed

        [Test]
        public void SetSeed_DeterministicSequence()
        {
            RandomUtility.SetSeed(42);
            int first1 = RandomUtility.GetRandom();
            int first2 = RandomUtility.GetRandom();

            RandomUtility.SetSeed(42);
            int second1 = RandomUtility.GetRandom();
            int second2 = RandomUtility.GetRandom();

            Assert.AreEqual(first1, second1);
            Assert.AreEqual(first2, second2);
        }

        [Test]
        public void SetSeed_DifferentSeed_DifferentSequence()
        {
            RandomUtility.SetSeed(1);
            int r1 = RandomUtility.GetRandom();

            RandomUtility.SetSeed(2);
            int r2 = RandomUtility.GetRandom();

            Assert.AreNotEqual(r1, r2);
        }

        #endregion

        #region GetRandomFloat

        [Test]
        public void GetRandomFloat_WithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                float result = RandomUtility.GetRandomFloat();
                Assert.GreaterOrEqual(result, 0.0f);
                Assert.Less(result, 1.0f);
            }
        }

        [Test]
        public void GetRandomFloat_WithSeed_ReturnsReproducibleValue()
        {
            RandomUtility.SetSeed(123);
            float first = RandomUtility.GetRandomFloat();

            RandomUtility.SetSeed(123);
            float second = RandomUtility.GetRandomFloat();

            Assert.AreEqual(first, second);
        }

        #endregion

        #region GetRandomInt64

        [Test]
        public void GetRandomInt64_WithSeed_ReturnsReproducibleValue()
        {
            RandomUtility.SetSeed(77);
            long first = RandomUtility.GetRandomInt64();

            RandomUtility.SetSeed(77);
            long second = RandomUtility.GetRandomInt64();

            Assert.AreEqual(first, second);
        }

        [Test]
        public void GetRandomInt64_MultipleCalls_ProduceDifferentValues()
        {
            long a = RandomUtility.GetRandomInt64();
            long b = RandomUtility.GetRandomInt64();
            long c = RandomUtility.GetRandomInt64();

            Assert.AreNotEqual(a, b, "Two consecutive calls should usually differ");
            Assert.AreNotEqual(b, c, "Three consecutive calls should usually differ");
        }

        #endregion

        #region GetRandomUInt64

        [Test]
        public void GetRandomUInt64_WithSeed_ReturnsReproducibleValue()
        {
            RandomUtility.SetSeed(55);
            ulong first = RandomUtility.GetRandomUInt64();

            RandomUtility.SetSeed(55);
            ulong second = RandomUtility.GetRandomUInt64();

            Assert.AreEqual(first, second);
        }

        [Test]
        public void GetRandomUInt64_MultipleCalls_ProduceDifferentValues()
        {
            ulong a = RandomUtility.GetRandomUInt64();
            ulong b = RandomUtility.GetRandomUInt64();

            Assert.AreNotEqual(a, b);
        }

        #endregion
    }
}
