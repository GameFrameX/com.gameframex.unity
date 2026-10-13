using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    /// <summary>
    /// RandomHelper 用例。随机引擎已统一到 ThreadLocalRandom 单一引擎（C27），
    /// 种子断言按 ThreadLocalRandom 语义重写（SetSeed 后同线程序列可复现）。
    /// </summary>
    [TestFixture]
    public class RandomHelperTests
    {
        #region NextInt32

        [Test]
        public void NextInt32_ReturnsNonNegative()
        {
            int result = RandomHelper.NextInt32();
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void NextInt32_MultipleCalls_SomeDifferent()
        {
            HashSet<int> results = new HashSet<int>();
            for (int i = 0; i < 100; i++)
            {
                results.Add(RandomHelper.NextInt32());
            }
            Assert.Greater(results.Count, 1, "100 random calls should produce more than 1 unique value");
        }

        #endregion

        #region NextDouble

        [Test]
        public void NextDouble_WithinRange()
        {
            for (int i = 0; i < 100; i++)
            {
                double result = RandomHelper.NextDouble();
                Assert.GreaterOrEqual(result, 0.0);
                Assert.Less(result, 1.0);
            }
        }

        [Test]
        public void NextDouble_MultipleCalls_SomeDifferent()
        {
            HashSet<double> results = new HashSet<double>();
            for (int i = 0; i < 100; i++)
            {
                results.Add(RandomHelper.NextDouble());
            }
            Assert.Greater(results.Count, 1);
        }

        #endregion

        #region NextBytes

        [Test]
        public void NextBytes_FillsBuffer()
        {
            byte[] buffer = new byte[100];
            RandomHelper.NextBytes(buffer);
            Assert.AreEqual(100, buffer.Length);
        }

        [Test]
        public void NextBytes_NotAllZeros()
        {
            byte[] buffer = new byte[100];
            RandomHelper.NextBytes(buffer);
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
        public void NextBytes_SingleByte()
        {
            byte[] buffer = new byte[1];
            RandomHelper.NextBytes(buffer);
            Assert.AreEqual(1, buffer.Length);
        }

        [Test]
        public void NextBytes_TwoCallsDifferent()
        {
            byte[] buffer1 = new byte[1000];
            byte[] buffer2 = new byte[1000];
            RandomHelper.NextBytes(buffer1);
            RandomHelper.NextBytes(buffer2);
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

        #region SetSeed (ThreadLocalRandom semantics)

        [Test]
        public void SetSeed_DeterministicSequence()
        {
            RandomHelper.SetSeed(42);
            int first1 = RandomHelper.NextInt32();
            int first2 = RandomHelper.NextInt32();

            RandomHelper.SetSeed(42);
            int second1 = RandomHelper.NextInt32();
            int second2 = RandomHelper.NextInt32();

            Assert.AreEqual(first1, second1);
            Assert.AreEqual(first2, second2);
        }

        [Test]
        public void SetSeed_DifferentSeed_DifferentSequence()
        {
            RandomHelper.SetSeed(1);
            int r1 = RandomHelper.NextInt32();

            RandomHelper.SetSeed(2);
            int r2 = RandomHelper.NextInt32();

            Assert.AreNotEqual(r1, r2);
        }

        #endregion

        #region RandomHelper.Next

        [Test]
        public void Next_NoArgs_ReturnsFloatBetweenZeroAndOne()
        {
            for (int i = 0; i < 100; i++)
            {
                float result = RandomHelper.Next();
                Assert.GreaterOrEqual(result, 0.0f);
                Assert.Less(result, 1.0f);
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

        [Test]
        public void Next_Range_WithinRange()
        {
            const int min = 10;
            const int max = 20;
            for (int i = 0; i < 200; i++)
            {
                int result = RandomHelper.Next(min, max);
                Assert.GreaterOrEqual(result, min);
                Assert.Less(result, max);
            }
        }

        [Test]
        public void Next_Range_NegativeMin()
        {
            const int min = -50;
            const int max = 50;
            for (int i = 0; i < 200; i++)
            {
                int result = RandomHelper.Next(min, max);
                Assert.GreaterOrEqual(result, min);
                Assert.Less(result, max);
            }
        }

        #endregion

        #region RandomHelper.NextInt64 / NextUInt64

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
