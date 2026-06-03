using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class BindablePropertyTests
    {
        #region Value Get/Set

        [Test]
        public void Value_DefaultConstructor_ReturnsDefault()
        {
            var prop = new BindableProperty<int>();
            Assert.AreEqual(0, prop.Value);
        }

        [Test]
        public void Value_ConstructorWithDefaultValue_ReturnsDefaultValue()
        {
            var prop = new BindableProperty<int>(42);
            Assert.AreEqual(42, prop.Value);
        }

        [Test]
        public void Value_Set_UpdatesValue()
        {
            var prop = new BindableProperty<string>("old");
            prop.Value = "new";
            Assert.AreEqual("new", prop.Value);
        }

        [Test]
        public void Value_SetSameValue_DoesNotFireEvent()
        {
            var prop = new BindableProperty<int>(10);
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = 10;

            Assert.AreEqual(0, fireCount);
        }

        [Test]
        public void Value_SetDifferentValue_FiresEvent()
        {
            var prop = new BindableProperty<int>(10);
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = 20;

            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void Value_SetDifferentValue_PassesNewValueToCallback()
        {
            var prop = new BindableProperty<string>("a");
            string received = null;
            prop.Add(v => received = v);

            prop.Value = "b";

            Assert.AreEqual("b", received);
        }

        #endregion

        #region OnValueChanged Event Firing

        [Test]
        public void OnValueChanged_FiresOnEachDistinctChange()
        {
            var prop = new BindableProperty<int>();
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = 1;
            prop.Value = 2;
            prop.Value = 3;

            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void OnValueChanged_DoesNotFireOnDuplicateSet()
        {
            var prop = new BindableProperty<int>();
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = 1;
            prop.Value = 1;
            prop.Value = 2;
            prop.Value = 2;

            Assert.AreEqual(2, fireCount);
        }

        #endregion

        #region Add / RegisterWithInitValue

        [Test]
        public void Add_NullCallback_Throws()
        {
            var prop = new BindableProperty<int>();
            Assert.Throws<ArgumentNullException>(() => prop.Add(null));
        }

        [Test]
        public void Add_ReturnsSelf_ForChaining()
        {
            var prop = new BindableProperty<int>();
            int fireCount = 0;

            var result = prop.Add(v => fireCount++);

            Assert.AreSame(prop, result);
        }

        [Test]
        public void RegisterWithInitValue_InvokesCallbackImmediately()
        {
            var prop = new BindableProperty<int>(99);
            int received = -1;
            prop.RegisterWithInitValue(v => received = v);

            Assert.AreEqual(99, received);
        }

        [Test]
        public void RegisterWithInitValue_RegistersForFutureChanges()
        {
            var prop = new BindableProperty<int>(5);
            int fireCount = 0;
            prop.RegisterWithInitValue(v => fireCount++);

            Assert.AreEqual(1, fireCount);

            prop.Value = 10;

            Assert.AreEqual(2, fireCount);
        }

        [Test]
        public void RegisterWithInitValue_NullCallback_Throws()
        {
            var prop = new BindableProperty<int>();
            Assert.Throws<ArgumentNullException>(() => prop.RegisterWithInitValue(null));
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_StopsReceivingEvents()
        {
            var prop = new BindableProperty<int>();
            int fireCount = 0;
            Action<int> handler = v => fireCount++;
            prop.Add(handler);

            prop.Value = 1;
            Assert.AreEqual(1, fireCount);

            prop.Remove(handler);
            prop.Value = 2;

            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void Remove_NullCallback_Throws()
        {
            var prop = new BindableProperty<int>();
            Assert.Throws<ArgumentNullException>(() => prop.Remove(null));
        }

        [Test]
        public void Remove_NonSubscribedHandler_DoesNotThrow()
        {
            var prop = new BindableProperty<int>();
            Action<int> handler = v => { };
            Assert.DoesNotThrow(() => prop.Remove(handler));
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_RemovesAllSubscribers()
        {
            var prop = new BindableProperty<int>();
            int fireCount1 = 0;
            int fireCount2 = 0;
            prop.Add(v => fireCount1++);
            prop.Add(v => fireCount2++);

            prop.Clear();
            prop.Value = 100;

            Assert.AreEqual(0, fireCount1);
            Assert.AreEqual(0, fireCount2);
        }

        #endregion

        #region Multiple Subscribers

        [Test]
        public void Add_MultipleSubscribers_AllReceiveEvent()
        {
            var prop = new BindableProperty<int>();
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;

            prop.Add(v => count1++);
            prop.Add(v => count2++);
            prop.Add(v => count3++);

            prop.Value = 1;

            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, count2);
            Assert.AreEqual(1, count3);
        }

        [Test]
        public void Remove_OneSubscriber_OthersStillReceive()
        {
            var prop = new BindableProperty<int>();
            int count1 = 0;
            int count2 = 0;
            Action<int> handler1 = v => count1++;

            prop.Add(handler1);
            prop.Add(v => count2++);

            prop.Remove(handler1);
            prop.Value = 1;

            Assert.AreEqual(0, count1);
            Assert.AreEqual(1, count2);
        }

        #endregion

        #region Reference Type Values

        [Test]
        public void Value_NullReferenceType_Works()
        {
            var prop = new BindableProperty<string>(null);
            Assert.IsNull(prop.Value);
        }

        [Test]
        public void Value_SetFromNullToNonNull_FiresEvent()
        {
            var prop = new BindableProperty<string>(null);
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = "hello";

            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void Value_SetFromNonNullToNull_FiresEvent()
        {
            var prop = new BindableProperty<string>("hello");
            int fireCount = 0;
            prop.Add(v => fireCount++);

            prop.Value = null;

            Assert.AreEqual(1, fireCount);
        }

        #endregion
    }
}
