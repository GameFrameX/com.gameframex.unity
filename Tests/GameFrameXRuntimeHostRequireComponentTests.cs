using System.Linq;
using System.Reflection;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    /// <summary>
    /// 自动宿主 AddComponent 对 Host 子树上已存在同类型组件的复用行为回归：
    /// 自动宿主为每个计划组件建独立子节点；若不检查复用（如 RequireComponent 在先前组件创建时
    /// 已自动补齐同类型组件），Play 启动时会出现两个同类型组件，第二个在 GameEntry.RegisterComponent 处报
    /// "already exist"。以 AssetComponent（声明 [RequireComponent(typeof(GameFrameXAssetCroppingHelper))]）为样本，
    /// 同一 descriptor 重复挂载必须复用既有实例。测试 asmdef 不引用功能包程序集，按 FullName 匹配。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameXRuntimeHostRequireComponentTests
    {
        private const string AssetComponentFullName = "GameFrameX.Asset.Runtime.AssetComponent";
        private const string AssetCroppingHelperFullName = "GameFrameX.Asset.Runtime.GameFrameXAssetCroppingHelper";

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
            var assetDescriptors = plan.Components
                .Where(descriptor => descriptor.ComponentType.FullName == AssetComponentFullName)
                .ToList();
            Assert.AreEqual(1, assetDescriptors.Count,
                "plan should contain the AssetComponent sample. totalPlan=" + plan.Components.Count);

            var host = new GameObject("[GameFrameX]");
            host.SetActive(false);
            hostField.SetValue(null, host);
            try
            {
                // 同一 descriptor 挂两次：第二次必须命中 Host 子树复用分支，不得新建第二个实例。
                addComponentMethod.Invoke(null, new object[] { assetDescriptors[0] });
                addComponentMethod.Invoke(null, new object[] { assetDescriptors[0] });

                var assetComponentCount = 0;
                var croppingHelperCount = 0;
                foreach (var behaviour in host.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (behaviour == null)
                    {
                        continue;
                    }

                    var typeName = behaviour.GetType().FullName;
                    if (typeName == AssetComponentFullName)
                    {
                        assetComponentCount++;
                    }
                    else if (typeName == AssetCroppingHelperFullName)
                    {
                        croppingHelperCount++;
                    }
                }

                Assert.AreEqual(1, assetComponentCount,
                    "AssetComponent should be created once (existing instance on the host subtree must be reused, otherwise GameEntry.RegisterComponent reports 'already exist')");
                Assert.AreEqual(1, croppingHelperCount,
                    "RequireComponent auto-added GameFrameXAssetCroppingHelper should exist exactly once");
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
