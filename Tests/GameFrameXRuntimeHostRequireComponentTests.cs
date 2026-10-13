using System.Linq;
using System.Reflection;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    /// <summary>
    /// 自动宿主 AddComponent 对 RequireComponent 自动补齐组件的复用行为回归：
    /// GameFrameX.FairyGUI.Runtime.FairyGUIComponent 声明 [RequireComponent(typeof(FairyGUIPackageComponent))]，
    /// 自动宿主为每个计划组件建独立子节点；若不复用 RequireComponent 自动补齐的实例，
    /// Play 启动时会出现两个同类型组件，第二个在 GameEntry.RegisterComponent 处报 "already exist"。
    /// 注意：com.gameframex.unity.ui.fairygui 包中存在同名但不同命名空间的 FairyGUIPackageComponent，按 FullName 匹配。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameXRuntimeHostRequireComponentTests
    {
        private const string FairyGuiComponentFullName = "GameFrameX.FairyGUI.Runtime.FairyGUIComponent";
        private const string FairyGuiPackageComponentFullName = "GameFrameX.FairyGUI.Runtime.FairyGUIPackageComponent";

        [Test]
        public void AddComponent_ReusesRequireComponentAutoAddedInstance()
        {
            var hostType = typeof(GameFrameXRuntimeHost);
            var hostField = hostType.GetField("s_Host", BindingFlags.Static | BindingFlags.NonPublic);
            var addComponentMethod = hostType.GetMethod("AddComponent", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.IsNotNull(hostField, "s_Host field should exist");
            Assert.IsNotNull(addComponentMethod, "AddComponent method should exist");

            var scanResult = GameFrameXRuntimeScanner.Scan();
            var plan = GameFrameXRuntimePlanner.CreatePlan(scanResult, null);
            var pairDescriptors = plan.Components
                .Where(descriptor => descriptor.ComponentType.FullName == FairyGuiComponentFullName ||
                                     descriptor.ComponentType.FullName == FairyGuiPackageComponentFullName)
                .ToList();
            Assert.AreEqual(2, pairDescriptors.Count,
                "plan should contain exactly the fairygui RequireComponent pair. totalPlan=" + plan.Components.Count);

            var host = new GameObject("[GameFrameX]");
            host.SetActive(false);
            hostField.SetValue(null, host);
            try
            {
                foreach (var descriptor in pairDescriptors)
                {
                    addComponentMethod.Invoke(null, new object[] { descriptor });
                }

                var packageComponentCount = 0;
                var fairyGuiComponentCount = 0;
                foreach (var behaviour in host.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (behaviour == null)
                    {
                        continue;
                    }

                    var typeName = behaviour.GetType().FullName;
                    if (typeName == FairyGuiPackageComponentFullName)
                    {
                        packageComponentCount++;
                    }
                    else if (typeName == FairyGuiComponentFullName)
                    {
                        fairyGuiComponentCount++;
                    }
                }

                Assert.AreEqual(1, fairyGuiComponentCount, "FairyGUIComponent should be created once");
                Assert.AreEqual(1, packageComponentCount, "FairyGUIPackageComponent should exist exactly once (RequireComponent auto-added instance must be reused)");
            }
            finally
            {
                hostField.SetValue(null, null);
                Object.DestroyImmediate(host);
                GameFrameXRuntimeHost.Reset();
            }
        }
    }
}
