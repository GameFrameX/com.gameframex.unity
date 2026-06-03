using System;
using System.Collections;
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        #region IsImplWithInterface - Basic

        [Test]
        public void IsImplWithInterface_ImplementsInterface_ReturnsTrue()
        {
            Type type = typeof(List<int>);
            Type iface = typeof(IEnumerable<int>);

            bool result = type.IsImplWithInterface(iface);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsImplWithInterface_DoesNotImplementInterface_ReturnsFalse()
        {
            Type type = typeof(string);
            Type iface = typeof(IList<int>);

            bool result = type.IsImplWithInterface(iface);

            Assert.IsFalse(result);
        }

        #endregion

        #region IsImplWithInterface - Null checks

        [Test]
        public void IsImplWithInterface_SelfNull_ReturnsFalse()
        {
            Type type = null;
            Type iface = typeof(IEnumerable);

            bool result = type.IsImplWithInterface(iface);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsImplWithInterface_TargetNull_ReturnsFalse()
        {
            Type type = typeof(List<int>);
            Type iface = null;

            bool result = type.IsImplWithInterface(iface);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsImplWithInterface_BothNull_ReturnsFalse()
        {
            bool result = ((Type)null).IsImplWithInterface(null);

            Assert.IsFalse(result);
        }

        #endregion

        #region IsImplWithInterface - Non-interface target

        [Test]
        public void IsImplWithInterface_TargetIsClass_ReturnsFalse()
        {
            Type type = typeof(List<int>);
            Type target = typeof(object);

            bool result = type.IsImplWithInterface(target);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsImplWithInterface_TargetIsAbstractClass_ReturnsFalse()
        {
            Type type = typeof(List<int>);
            Type target = typeof(AbstractBase);

            bool result = type.IsImplWithInterface(target);

            Assert.IsFalse(result);
        }

        #endregion

        #region IsImplWithInterface - Self is interface or abstract

        [Test]
        public void IsImplWithInterface_SelfIsInterface_ReturnsFalse()
        {
            Type type = typeof(IEnumerable);
            Type target = typeof(IEnumerable);

            bool result = type.IsImplWithInterface(target);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsImplWithInterface_SelfIsAbstract_ReturnsFalse()
        {
            Type type = typeof(AbstractBase);
            Type target = typeof(IDisposable);

            bool result = type.IsImplWithInterface(target);

            Assert.IsFalse(result);
        }

        #endregion

        #region IsImplWithInterface - directOnly

        [Test]
        public void IsImplWithInterface_DirectOnly_TrueWhenDirectlyImplemented()
        {
            Type type = typeof(List<int>);
            Type iface = typeof(IList<int>);

            bool result = type.IsImplWithInterface(iface, directOnly: true);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsImplWithInterface_DirectOnly_FalseWhenIndirectlyImplemented()
        {
            Type type = typeof(MyList);
            Type iface = typeof(IMyBase);

            bool result = type.IsImplWithInterface(iface, directOnly: true);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsImplWithInterface_NotDirectOnly_TrueWhenIndirectlyImplemented()
        {
            Type type = typeof(MyList);
            Type iface = typeof(IMyBase);

            bool result = type.IsImplWithInterface(iface, directOnly: false);

            Assert.IsTrue(result);
        }

        #endregion

        #region Test helpers

        private abstract class AbstractBase
        {
        }

        private interface IMyBase
        {
        }

        private interface IMyDerived : IMyBase
        {
        }

        private class MyList : IMyDerived
        {
        }

        #endregion
    }
}
