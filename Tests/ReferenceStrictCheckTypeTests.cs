using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ReferenceStrictCheckTypeTests
    {
        #region Enum Values

        [Test]
        public void AlwaysEnable_IsZero()
        {
            Assert.AreEqual((byte)0, (byte)ReferenceStrictCheckType.AlwaysEnable);
        }

        [Test]
        public void OnlyEnableWhenDevelopment_IsOne()
        {
            Assert.AreEqual((byte)1, (byte)ReferenceStrictCheckType.OnlyEnableWhenDevelopment);
        }

        [Test]
        public void OnlyEnableInEditor_IsTwo()
        {
            Assert.AreEqual((byte)2, (byte)ReferenceStrictCheckType.OnlyEnableInEditor);
        }

        [Test]
        public void AlwaysDisable_IsThree()
        {
            Assert.AreEqual((byte)3, (byte)ReferenceStrictCheckType.AlwaysDisable);
        }

        #endregion

        #region Enum Type

        [Test]
        public void UnderlyingType_IsByte()
        {
            Assert.AreEqual(typeof(byte), Enum.GetUnderlyingType(typeof(ReferenceStrictCheckType)));
        }

        [Test]
        public void AllValues_AreDistinct()
        {
            var values = Enum.GetValues(typeof(ReferenceStrictCheckType));
            Assert.AreEqual(4, values.Length);

            Assert.AreNotEqual(ReferenceStrictCheckType.AlwaysEnable, ReferenceStrictCheckType.OnlyEnableWhenDevelopment);
            Assert.AreNotEqual(ReferenceStrictCheckType.OnlyEnableWhenDevelopment, ReferenceStrictCheckType.OnlyEnableInEditor);
            Assert.AreNotEqual(ReferenceStrictCheckType.OnlyEnableInEditor, ReferenceStrictCheckType.AlwaysDisable);
        }

        #endregion

        #region Casting

        [Test]
        public void CanCastFromByte()
        {
            var value = (ReferenceStrictCheckType)(byte)1;
            Assert.AreEqual(ReferenceStrictCheckType.OnlyEnableWhenDevelopment, value);
        }

        [Test]
        public void CanCastToByte()
        {
            byte b = (byte)ReferenceStrictCheckType.AlwaysDisable;
            Assert.AreEqual((byte)3, b);
        }

        #endregion
    }
}
