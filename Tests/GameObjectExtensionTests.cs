using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameObjectExtensionTests
    {
        private GameObject m_Root;

        [SetUp]
        public void SetUp()
        {
            m_Root = new GameObject("GameObjectExtensionTests_Root");
        }

        [TearDown]
        public void TearDown()
        {
            if (m_Root != null)
            {
                Object.DestroyImmediate(m_Root);
            }
        }

        #region SetLayer

        [Test]
        public void SetLayer_NonRecursive_OnlyChangesSelf()
        {
            var child = new GameObject("Child");
            child.transform.SetParent(m_Root.transform);
            const int targetLayer = 4;

            m_Root.SetLayer(targetLayer, false);

            Assert.AreEqual(targetLayer, m_Root.layer);
            Assert.AreEqual(0, child.layer);
        }

        [Test]
        public void SetLayer_Recursive_ChangesAllDescendants()
        {
            var child = new GameObject("Child");
            child.transform.SetParent(m_Root.transform);
            var grandChild = new GameObject("GrandChild");
            grandChild.transform.SetParent(child.transform);
            const int targetLayer = 4;

            m_Root.SetLayer(targetLayer, true);

            Assert.AreEqual(targetLayer, m_Root.layer);
            Assert.AreEqual(targetLayer, child.layer);
            Assert.AreEqual(targetLayer, grandChild.layer);
        }

        [Test]
        public void SetLayer_Recursive_IncludesInactiveDescendants()
        {
            var child = new GameObject("Child");
            child.transform.SetParent(m_Root.transform);
            var inactiveChild = new GameObject("InactiveChild");
            inactiveChild.transform.SetParent(child.transform);
            inactiveChild.SetActive(false);
            const int targetLayer = 4;

            m_Root.SetLayer(targetLayer, true);

            Assert.AreEqual(targetLayer, inactiveChild.layer);
        }

        [Test]
        public void SetLayer_AlreadyOnTargetLayer_RemainsUnchanged()
        {
            const int targetLayer = 4;
            m_Root.layer = targetLayer;

            m_Root.SetLayer(targetLayer, true);

            Assert.AreEqual(targetLayer, m_Root.layer);
        }

        #endregion

        #region CreateChild / ResetTransform / RemoveChildren

        [Test]
        public void CreateChild_CreatesObjectUnderParent()
        {
            var child = m_Root.CreateChild("NewChild");

            Assert.IsNotNull(child);
            Assert.AreEqual("NewChild", child.name);
            Assert.AreSame(m_Root.transform, child.transform.parent);
        }

        [Test]
        public void ResetTransform_ResetsLocalPositionRotationScale()
        {
            m_Root.transform.localPosition = new Vector3(5f, 5f, 5f);
            m_Root.transform.localRotation = Quaternion.Euler(30f, 30f, 30f);
            m_Root.transform.localScale = new Vector3(3f, 3f, 3f);

            m_Root.ResetTransform();

            Assert.AreEqual(Vector3.zero, m_Root.transform.localPosition);
            Assert.AreEqual(Quaternion.identity, m_Root.transform.localRotation);
            Assert.AreEqual(Vector3.one, m_Root.transform.localScale);
        }

        [Test]
        public void RemoveChildren_DestroysAllChildren()
        {
            var child1 = new GameObject("Child1");
            child1.transform.SetParent(m_Root.transform);
            var child2 = new GameObject("Child2");
            child2.transform.SetParent(m_Root.transform);

            m_Root.RemoveChildren();

            Assert.AreEqual(0, m_Root.transform.childCount);
            Assert.IsTrue(child1 == null);
            Assert.IsTrue(child2 == null);
        }

        [Test]
        public void RemoveChildren_NoChildren_KeepsSelf()
        {
            m_Root.RemoveChildren();

            Assert.IsTrue(m_Root != null);
            Assert.AreEqual(0, m_Root.transform.childCount);
        }

        #endregion

        #region FindChildGamObjectByName

        [Test]
        public void FindChildGamObjectByName_Instance_FindsDeepDescendant()
        {
            var child = new GameObject("Level1");
            child.transform.SetParent(m_Root.transform);
            var deep = new GameObject("DeepTarget");
            deep.transform.SetParent(child.transform);

            var result = m_Root.FindChildGamObjectByName("DeepTarget");

            Assert.AreSame(deep, result);
        }

        [Test]
        public void FindChildGamObjectByName_Instance_MissingName_ReturnsNull()
        {
            Assert.IsNull(m_Root.FindChildGamObjectByName("NoSuchNode"));
        }

        [Test]
        public void FindChildGamObjectByName_UnloadedSceneName_ReturnsNull()
        {
            Assert.IsNull(GameObjectUtility.FindChildGamObjectByName("GameObjectExtensionTests_Root", "NoSuchScene"));
        }

        [Test]
        public void FindChildGamObjectByName_ActiveScene_FindsRootDescendant()
        {
            var child = new GameObject("ActiveSceneTarget");
            child.transform.SetParent(m_Root.transform);

            var result = GameObjectUtility.FindChildGamObjectByName("ActiveSceneTarget");

            Assert.IsNotNull(result);
            Assert.AreEqual("ActiveSceneTarget", result.name);
            Object.DestroyImmediate(child);
        }

        [Test]
        public void FindChildGamObjectByName_ActiveScene_MissingNode_ReturnsNull()
        {
            Assert.IsNull(GameObjectUtility.FindChildGamObjectByName("NoSuchNodeAnywhere"));
        }

        [Test]
        public void FindChildGamObjectByName_UtilityMatchesExtensionResult()
        {
            var child = new GameObject("ConsistencyTarget");
            child.transform.SetParent(m_Root.transform);

            var viaUtility = GameObjectUtility.FindChildGamObjectByName("ConsistencyTarget");
            var viaExtension = UnityEngineGameObjectExtension.FindChildGamObjectByName("ConsistencyTarget");

            Assert.AreSame(viaUtility, viaExtension);
            Assert.AreSame(child, viaUtility);
        }

        #endregion
    }
}
