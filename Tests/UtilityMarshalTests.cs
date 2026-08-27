using System;
using System.Runtime.InteropServices;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityMarshalTests
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct TestStruct
        {
            public int Id;
            public float Value;
        }

        #region EnsureCachedHGlobalSize

        [Test]
        public void EnsureCachedHGlobalSize_PositiveSize_SetsSize()
        {
            MarshalUtility.EnsureCachedHGlobalSize(100);
            Assert.Greater(MarshalUtility.CachedHGlobalSize, 0);
        }

        [Test]
        public void EnsureCachedHGlobalSize_NegativeSize_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                MarshalUtility.EnsureCachedHGlobalSize(-1);
            });
        }

        [Test]
        public void EnsureCachedHGlobalSize_Zero_IsAllowed()
        {
            MarshalUtility.EnsureCachedHGlobalSize(0);
        }

        [Test]
        public void EnsureCachedHGlobalSize_RoundsUpToBlockSize()
        {
            MarshalUtility.EnsureCachedHGlobalSize(1);
            Assert.GreaterOrEqual(MarshalUtility.CachedHGlobalSize, 1);
            Assert.AreEqual(0, MarshalUtility.CachedHGlobalSize % 4096);
        }

        [Test]
        public void EnsureCachedHGlobalSize_DoesNotShrink()
        {
            MarshalUtility.EnsureCachedHGlobalSize(4096);
            int size1 = MarshalUtility.CachedHGlobalSize;
            MarshalUtility.EnsureCachedHGlobalSize(100);
            Assert.AreEqual(size1, MarshalUtility.CachedHGlobalSize);
        }

        #endregion

        #region FreeCachedHGlobal

        [Test]
        public void FreeCachedHGlobal_SetsSizeToZero()
        {
            MarshalUtility.EnsureCachedHGlobalSize(100);
            MarshalUtility.FreeCachedHGlobal();
            Assert.AreEqual(0, MarshalUtility.CachedHGlobalSize);
        }

        [Test]
        public void FreeCachedHGlobal_CalledTwice_DoesNotThrow()
        {
            MarshalUtility.EnsureCachedHGlobalSize(100);
            MarshalUtility.FreeCachedHGlobal();
            MarshalUtility.FreeCachedHGlobal();
            Assert.AreEqual(0, MarshalUtility.CachedHGlobalSize);
        }

        #endregion

        #region StructureToBytes / BytesToStructure round-trip

        [Test]
        public void StructureToBytes_BytesToStructure_RoundTrip()
        {
            TestStruct original = new TestStruct { Id = 42, Value = 3.14f };
            byte[] bytes = MarshalUtility.StructureToBytes(original);
            Assert.IsNotNull(bytes);
            Assert.Greater(bytes.Length, 0);

            TestStruct restored = MarshalUtility.BytesToStructure<TestStruct>(bytes);
            Assert.AreEqual(original.Id, restored.Id);
            Assert.AreEqual(original.Value, restored.Value, 0.0001f);
        }

        [Test]
        public void StructureToBytes_WithBuffer()
        {
            TestStruct original = new TestStruct { Id = 99, Value = -1.5f };
            int structSize = Marshal.SizeOf(typeof(TestStruct));
            byte[] buffer = new byte[structSize];
            MarshalUtility.StructureToBytes(original, buffer);
            TestStruct restored = MarshalUtility.BytesToStructure<TestStruct>(buffer);
            Assert.AreEqual(original.Id, restored.Id);
            Assert.AreEqual(original.Value, restored.Value, 0.0001f);
        }

        [Test]
        public void StructureToBytes_WithBufferAndOffset()
        {
            TestStruct original = new TestStruct { Id = 7, Value = 2.71f };
            int structSize = Marshal.SizeOf(typeof(TestStruct));
            byte[] buffer = new byte[structSize + 10];
            MarshalUtility.StructureToBytes(original, buffer, 5);
            TestStruct restored = MarshalUtility.BytesToStructure<TestStruct>(buffer, 5);
            Assert.AreEqual(original.Id, restored.Id);
            Assert.AreEqual(original.Value, restored.Value, 0.0001f);
        }

        [Test]
        public void BytesToStructure_WithOffset()
        {
            TestStruct original = new TestStruct { Id = 123, Value = 0.5f };
            byte[] bytes = MarshalUtility.StructureToBytes(original);
            byte[] padded = new byte[bytes.Length + 20];
            Buffer.BlockCopy(bytes, 0, padded, 10, bytes.Length);
            TestStruct restored = MarshalUtility.BytesToStructure<TestStruct>(padded, 10);
            Assert.AreEqual(original.Id, restored.Id);
            Assert.AreEqual(original.Value, restored.Value, 0.0001f);
        }

        #endregion

        #region Error cases

        [Test]
        public void StructureToBytes_NullBuffer_Throws()
        {
            TestStruct s = new TestStruct();
            Assert.Throws<GameFrameworkException>(() =>
            {
                MarshalUtility.StructureToBytes(s, null, 0);
            });
        }

        [Test]
        public void StructureToBytes_NegativeStartIndex_Throws()
        {
            TestStruct s = new TestStruct();
            byte[] buffer = new byte[100];
            Assert.Throws<GameFrameworkException>(() =>
            {
                MarshalUtility.StructureToBytes(s, buffer, -1);
            });
        }

        [Test]
        public void StructureToBytes_BufferTooSmall_Throws()
        {
            TestStruct s = new TestStruct();
            byte[] buffer = new byte[1];
            Assert.Throws<GameFrameworkException>(() =>
            {
                MarshalUtility.StructureToBytes(s, buffer, 0);
            });
        }

        #endregion

        #region Multiple structs

        [Test]
        public void MultipleStructs_SameRoundTrip()
        {
            for (int i = 0; i < 50; i++)
            {
                TestStruct original = new TestStruct { Id = i, Value = i * 0.1f };
                byte[] bytes = MarshalUtility.StructureToBytes(original);
                TestStruct restored = MarshalUtility.BytesToStructure<TestStruct>(bytes);
                Assert.AreEqual(original.Id, restored.Id, $"Failed at iteration {i}");
            }
        }

        #endregion

        #region Cleanup

        [TearDown]
        public void TearDown()
        {
            MarshalUtility.FreeCachedHGlobal();
        }

        #endregion
    }
}
