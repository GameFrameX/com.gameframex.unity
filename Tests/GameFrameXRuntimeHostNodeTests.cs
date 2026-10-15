using System.Reflection;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    /// <summary>
    /// GetOrCreateHostNode 分组节点解析回归：
    /// Host 未启动时必须返回 null（卫星包据此在 Update 中重试，而非误建根级节点）；
    /// 启动后按路径段逐级查找、缺段即建、已存在即复用；空路径 / 纯分隔符返回 null。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameXRuntimeHostNodeTests
    {
        [TearDown]
        public void TearDown()
        {
            // EditMode 下 EnsureStarted 建立的 [GameFrameX] Host 树不会随 Play 结束销毁，必须显式清理，否则泄漏进打开的场景。
            var hostField = typeof(GameFrameXRuntimeHost).GetField("s_Host", BindingFlags.Static | BindingFlags.NonPublic);
            var host = hostField.GetValue(null) as GameObject;
            if (host != null)
            {
                Object.DestroyImmediate(host);
            }

            GameEntry.Shutdown(ShutdownType.None);
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        [Test]
        public void GetOrCreateHostNode_ReturnsNullWhenHostNotStarted()
        {
            Assert.IsNull(GameFrameXRuntimeHost.GetOrCreateHostNode("UI/Keyboard"));
        }

        [Test]
        public void GetOrCreateHostNode_ReturnsNullForPathWithoutSegment()
        {
            GameFrameXRuntimeHost.EnsureStarted();

            Assert.IsNull(GameFrameXRuntimeHost.GetOrCreateHostNode(null));
            Assert.IsNull(GameFrameXRuntimeHost.GetOrCreateHostNode(string.Empty));
            Assert.IsNull(GameFrameXRuntimeHost.GetOrCreateHostNode("/"));
        }

        [Test]
        public void GetOrCreateHostNode_CreatesMissingSegments_AndReusesExistingNodes()
        {
            GameFrameXRuntimeHost.EnsureStarted();

            var hostField = typeof(GameFrameXRuntimeHost).GetField("s_Host", BindingFlags.Static | BindingFlags.NonPublic);
            var host = hostField.GetValue(null) as GameObject;
            Assert.IsNotNull(host, "EnsureStarted 后必须存在 [GameFrameX] Host");

            var keyboardNode = GameFrameXRuntimeHost.GetOrCreateHostNode("UI/Keyboard");
            Assert.IsNotNull(keyboardNode);
            Assert.AreEqual("Keyboard", keyboardNode.name);

            var uiNode = host.transform.Find("UI");
            Assert.IsNotNull(uiNode, "路径中间段缺失时必须逐段补齐（UI）");
            Assert.AreSame(keyboardNode, uiNode.Find("Keyboard"), "末段必须落在上一段之下");

            // 二次解析必须复用既有节点，不得重复创建。
            Assert.AreSame(keyboardNode, GameFrameXRuntimeHost.GetOrCreateHostNode("UI/Keyboard"));
            var keyboardChildCount = 0;
            foreach (Transform child in uiNode)
            {
                if (child.name == "Keyboard")
                {
                    keyboardChildCount++;
                }
            }

            Assert.AreEqual(1, keyboardChildCount, "同名分组节点不得重复创建");
        }
    }
}
