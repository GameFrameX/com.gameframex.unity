using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UnityEngineGameObjectExtensionTests
    {
        private GameObject _root;

        [SetUp]
        public void SetUp()
        {
            _root = new GameObject("X4Root");
            var child = new GameObject("Child");
            child.transform.SetParent(_root.transform);
            var grandChild = new GameObject("GrandChild");
            grandChild.transform.SetParent(child.transform);
        }

        [TearDown]
        public void TearDown()
        {
            if (Application.isPlaying)
            {
                Object.Destroy(_root);
            }
            else
            {
                Object.DestroyImmediate(_root);
            }
        }

        public void SetLayer_Recursive_SetsAllDescendants()
        {
            _root.SetLayer(3);
            Assert.AreEqual(3, _root.layer);
            Assert.AreEqual(3, _root.transform.GetChild(0).gameObject.layer);
            Assert.AreEqual(3, _root.transform.GetChild(0).GetChild(0).gameObject.layer);
        }
    }
}
