using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityObjectTests
    {
        #region Swap

        [Test]
        public void Swap_Integers()
        {
            int a = 10;
            int b = 20;
            ObjectUtility.Swap(ref a, ref b);
            Assert.AreEqual(20, a);
            Assert.AreEqual(10, b);
        }

        [Test]
        public void Swap_Strings()
        {
            string a = "hello";
            string b = "world";
            ObjectUtility.Swap(ref a, ref b);
            Assert.AreEqual("world", a);
            Assert.AreEqual("hello", b);
        }

        [Test]
        public void Swap_SameValue()
        {
            int a = 42;
            int b = 42;
            ObjectUtility.Swap(ref a, ref b);
            Assert.AreEqual(42, a);
            Assert.AreEqual(42, b);
        }

        [Test]
        public void Swap_ReferenceTypes()
        {
            object a = new object();
            object b = new object();
            object origA = a;
            object origB = b;
            ObjectUtility.Swap(ref a, ref b);
            Assert.AreSame(origB, a);
            Assert.AreSame(origA, b);
        }

        [Test]
        public void Swap_NullValues()
        {
            string a = "value";
            string b = null;
            ObjectUtility.Swap(ref a, ref b);
            Assert.IsNull(a);
            Assert.AreEqual("value", b);
        }

        [Test]
        public void Swap_BothNull()
        {
            string a = null;
            string b = null;
            ObjectUtility.Swap(ref a, ref b);
            Assert.IsNull(a);
            Assert.IsNull(b);
        }

        [Test]
        public void Swap_Floats()
        {
            float a = 1.5f;
            float b = -3.14f;
            ObjectUtility.Swap(ref a, ref b);
            Assert.AreEqual(-3.14f, a, 0.001f);
            Assert.AreEqual(1.5f, b, 0.001f);
        }

        [Test]
        public void Swap_Bool()
        {
            bool a = true;
            bool b = false;
            ObjectUtility.Swap(ref a, ref b);
            Assert.IsFalse(a);
            Assert.IsTrue(b);
        }

        #endregion
    }
}
