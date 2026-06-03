using System;
using GameFrameX.ObjectPool;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    internal class TestObjectBase : ObjectBase
    {
        public bool SpawnCalled { get; private set; }
        public bool UnspawnCalled { get; private set; }
        public bool ReleaseCalled { get; private set; }
        public bool ReleaseShutdownFlag { get; private set; }

        public static TestObjectBase Create(string name, object target, bool locked = false, int priority = 0)
        {
            var obj = new TestObjectBase();
            obj.Initialize(name, target, locked, priority);
            return obj;
        }

        public void InvokeInitialize(object target)
        {
            Initialize(target);
        }

        public void InvokeInitialize(string name, object target)
        {
            Initialize(name, target);
        }

        public void InvokeInitialize(string name, object target, bool locked)
        {
            Initialize(name, target, locked);
        }

        public void InvokeInitialize(string name, object target, int priority)
        {
            Initialize(name, target, priority);
        }

        public void InvokeOnSpawn()
        {
            OnSpawn();
        }

        public void InvokeOnUnspawn()
        {
            OnUnspawn();
        }

        public void InvokeRelease(bool isShutdown)
        {
            Release(isShutdown);
        }

        protected override void OnSpawn()
        {
            SpawnCalled = true;
        }

        protected override void OnUnspawn()
        {
            UnspawnCalled = true;
        }

        protected override void Release(bool isShutdown)
        {
            ReleaseCalled = true;
            ReleaseShutdownFlag = isShutdown;
        }
    }

    internal class CustomCanReleaseObject : ObjectBase
    {
        private readonly bool _canRelease;

        private CustomCanReleaseObject(bool canRelease)
        {
            _canRelease = canRelease;
        }

        public static CustomCanReleaseObject Create(object target, bool canRelease)
        {
            var obj = new CustomCanReleaseObject(canRelease);
            obj.Initialize(target);
            return obj;
        }

        public override bool CustomCanReleaseFlag
        {
            get { return _canRelease; }
        }

        protected override void Release(bool isShutdown)
        {
        }
    }

    internal class OverrideNameObject : ObjectBase
    {
        public static OverrideNameObject Create(object target)
        {
            var obj = new OverrideNameObject();
            obj.Initialize("original", target);
            return obj;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        protected override void Release(bool isShutdown)
        {
        }
    }

    [TestFixture]
    public class ObjectBaseTests
    {
        #region Initialization

        [Test]
        public void Initialize_WithTarget_SetsTarget()
        {
            var target = new object();
            var obj = TestObjectBase.Create("test", target);
            Assert.AreSame(target, obj.Target);
        }

        [Test]
        public void Initialize_WithNullName_SetsEmptyString()
        {
            var obj = TestObjectBase.Create(null, new object());
            Assert.AreEqual(string.Empty, obj.Name);
        }

        [Test]
        public void Initialize_WithName_SetsName()
        {
            var obj = TestObjectBase.Create("MyObject", new object());
            Assert.AreEqual("MyObject", obj.Name);
        }

        [Test]
        public void Initialize_WithLockedTrue_SetsLocked()
        {
            var obj = TestObjectBase.Create("test", new object(), locked: true);
            Assert.IsTrue(obj.Locked);
        }

        [Test]
        public void Initialize_WithLockedFalse_SetsUnlocked()
        {
            var obj = TestObjectBase.Create("test", new object(), locked: false);
            Assert.IsFalse(obj.Locked);
        }

        [Test]
        public void Initialize_WithPriority_SetsPriority()
        {
            var obj = TestObjectBase.Create("test", new object(), priority: 5);
            Assert.AreEqual(5, obj.Priority);
        }

        [Test]
        public void Initialize_WithNullTarget_Throws()
        {
            var obj = new TestObjectBase();
            Assert.Throws<GameFrameworkException>(() => obj.InvokeInitialize("test", null));
        }

        [Test]
        public void Initialize_Overload_TargetOnly()
        {
            var target = new object();
            var obj = new TestObjectBase();
            obj.InvokeInitialize(target);
            Assert.AreSame(target, obj.Target);
            Assert.AreEqual(string.Empty, obj.Name);
            Assert.IsFalse(obj.Locked);
            Assert.AreEqual(0, obj.Priority);
        }

        [Test]
        public void Initialize_Overload_NameAndTarget()
        {
            var target = new object();
            var obj = new TestObjectBase();
            obj.InvokeInitialize("name", target);
            Assert.AreEqual("name", obj.Name);
            Assert.AreSame(target, obj.Target);
        }

        [Test]
        public void Initialize_Overload_NameTargetLocked()
        {
            var target = new object();
            var obj = new TestObjectBase();
            obj.InvokeInitialize("name", target, true);
            Assert.IsTrue(obj.Locked);
        }

        [Test]
        public void Initialize_Overload_NameTargetPriority()
        {
            var target = new object();
            var obj = new TestObjectBase();
            obj.InvokeInitialize("name", target, 10);
            Assert.AreEqual(10, obj.Priority);
        }

        [Test]
        public void Initialize_SetsLastUseTime()
        {
            var before = DateTime.UtcNow;
            var obj = TestObjectBase.Create("test", new object());
            var after = DateTime.UtcNow;
            Assert.GreaterOrEqual(obj.LastUseTime, before);
            Assert.LessOrEqual(obj.LastUseTime, after);
        }

        #endregion

        #region Default Constructor State

        [Test]
        public void DefaultConstructor_NameIsNull()
        {
            var obj = new TestObjectBase();
            Assert.IsNull(obj.Name);
        }

        [Test]
        public void DefaultConstructor_TargetIsNull()
        {
            var obj = new TestObjectBase();
            Assert.IsNull(obj.Target);
        }

        [Test]
        public void DefaultConstructor_LockedIsFalse()
        {
            var obj = new TestObjectBase();
            Assert.IsFalse(obj.Locked);
        }

        [Test]
        public void DefaultConstructor_PriorityIsZero()
        {
            var obj = new TestObjectBase();
            Assert.AreEqual(0, obj.Priority);
        }

        [Test]
        public void DefaultConstructor_LastUseTimeIsDefault()
        {
            var obj = new TestObjectBase();
            Assert.AreEqual(default(DateTime), obj.LastUseTime);
        }

        #endregion

        #region Locked Property

        [Test]
        public void Locked_SetToTrue()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.Locked = true;
            Assert.IsTrue(obj.Locked);
        }

        [Test]
        public void Locked_SetToFalse()
        {
            var obj = TestObjectBase.Create("test", new object(), locked: true);
            obj.Locked = false;
            Assert.IsFalse(obj.Locked);
        }

        #endregion

        #region Priority Property

        [Test]
        public void Priority_SetValue()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.Priority = 100;
            Assert.AreEqual(100, obj.Priority);
        }

        [Test]
        public void Priority_CanBeNegative()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.Priority = -5;
            Assert.AreEqual(-5, obj.Priority);
        }

        #endregion

        #region CustomCanReleaseFlag

        [Test]
        public void CustomCanReleaseFlag_DefaultIsTrue()
        {
            var obj = TestObjectBase.Create("test", new object());
            Assert.IsTrue(obj.CustomCanReleaseFlag);
        }

        [Test]
        public void CustomCanReleaseFlag_CanBeOverridden()
        {
            var obj = CustomCanReleaseObject.Create(new object(), false);
            Assert.IsFalse(obj.CustomCanReleaseFlag);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_ResetsAllFields()
        {
            var obj = TestObjectBase.Create("test", new object(), true, 10);
            obj.Clear();

            Assert.IsNull(obj.Name);
            Assert.IsNull(obj.Target);
            Assert.IsFalse(obj.Locked);
            Assert.AreEqual(0, obj.Priority);
            Assert.AreEqual(default(DateTime), obj.LastUseTime);
        }

        #endregion

        #region OnSpawn / OnUnspawn

        [Test]
        public void OnSpawn_IsCalled()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.InvokeOnSpawn();
            Assert.IsTrue(obj.SpawnCalled);
        }

        [Test]
        public void OnUnspawn_IsCalled()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.InvokeOnUnspawn();
            Assert.IsTrue(obj.UnspawnCalled);
        }

        #endregion

        #region Release

        [Test]
        public void Release_IsCalledWithShutdownFlag()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.InvokeRelease(true);
            Assert.IsTrue(obj.ReleaseCalled);
            Assert.IsTrue(obj.ReleaseShutdownFlag);
        }

        [Test]
        public void Release_IsCalledWithNonShutdownFlag()
        {
            var obj = TestObjectBase.Create("test", new object());
            obj.InvokeRelease(false);
            Assert.IsTrue(obj.ReleaseCalled);
            Assert.IsFalse(obj.ReleaseShutdownFlag);
        }

        #endregion

        #region Name Override

        [Test]
        public void Name_CanBeSetBySubclass()
        {
            var obj = OverrideNameObject.Create(new object());
            Assert.AreEqual("original", obj.Name);

            obj.SetName("changed");
            Assert.AreEqual("changed", obj.Name);
        }

        #endregion

        #region IReference

        [Test]
        public void ObjectBase_ImplementsIReference()
        {
            Assert.IsTrue(typeof(IReference).IsAssignableFrom(typeof(ObjectBase)));
        }

        [Test]
        public void Clear_SatisfiesIReferenceContract()
        {
            IReference obj = TestObjectBase.Create("test", new object());
            obj.Clear();

            var concrete = (TestObjectBase)obj;
            Assert.IsNull(concrete.Target);
        }

        #endregion
    }
}
