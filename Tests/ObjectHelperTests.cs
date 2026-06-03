using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ObjectHelperTests
    {
        #region Swap

        [Test]
        public void Swap_IntValues_ExchangesCorrectly()
        {
            int a = 10;
            int b = 20;

            ObjectHelper.Swap(ref a, ref b);

            Assert.AreEqual(20, a);
            Assert.AreEqual(10, b);
        }

        [Test]
        public void Swap_StringValues_ExchangesCorrectly()
        {
            string a = "hello";
            string b = "world";

            ObjectHelper.Swap(ref a, ref b);

            Assert.AreEqual("world", a);
            Assert.AreEqual("hello", b);
        }

        [Test]
        public void Swap_SameVariable_NoChange()
        {
            int x = 42;

            ObjectHelper.Swap(ref x, ref x);

            Assert.AreEqual(42, x);
        }

        [Test]
        public void Swap_NullReference_ExchangesCorrectly()
        {
            string a = "not null";
            string b = null;

            ObjectHelper.Swap(ref a, ref b);

            Assert.IsNull(a);
            Assert.AreEqual("not null", b);
        }

        [Test]
        public void Swap_BothNull_NoChange()
        {
            string a = null;
            string b = null;

            ObjectHelper.Swap(ref a, ref b);

            Assert.IsNull(a);
            Assert.IsNull(b);
        }

        [Test]
        public void Swap_ReferenceType_ExchangesCorrectly()
        {
            var obj1 = new SwapTestClass { Value = 1 };
            var obj2 = new SwapTestClass { Value = 2 };

            ObjectHelper.Swap(ref obj1, ref obj2);

            Assert.AreEqual(2, obj1.Value);
            Assert.AreEqual(1, obj2.Value);
        }

        [Test]
        public void Swap_FloatValues_ExchangesCorrectly()
        {
            float a = 1.5f;
            float b = 2.5f;

            ObjectHelper.Swap(ref a, ref b);

            Assert.AreEqual(2.5f, a);
            Assert.AreEqual(1.5f, b);
        }

        [Test]
        public void Swap_BoolValues_ExchangesCorrectly()
        {
            bool a = true;
            bool b = false;

            ObjectHelper.Swap(ref a, ref b);

            Assert.IsFalse(a);
            Assert.IsTrue(b);
        }

        [Test]
        public void Swap_NegativeValues_ExchangesCorrectly()
        {
            int a = -100;
            int b = 100;

            ObjectHelper.Swap(ref a, ref b);

            Assert.AreEqual(100, a);
            Assert.AreEqual(-100, b);
        }

        [Test]
        public void Swap_DefaultValues_ExchangesCorrectly()
        {
            int a = 0;
            int b = 0;

            ObjectHelper.Swap(ref a, ref b);

            Assert.AreEqual(0, a);
            Assert.AreEqual(0, b);
        }

        #endregion

        private class SwapTestClass
        {
            public int Value;
        }
    }
}
