using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理 GameFrameX 自动运行时 Host（唯一启动路径）。
    /// </summary>
    /// <remarks>
    /// 接管语义：包被引用即由本 Host 拥有框架组件所有权。场景里手工摆放的 GFX 组件在 <see cref="IsAutoInjectionClaimed"/>
    /// 为真时自我抑制并告警，不注册、不驱动、不关停会话——框架的组件所有权始终唯一。
    /// <para>
    /// English: The auto runtime host owns framework components as soon as the package is referenced. GFX components placed
    /// by hand in a scene suppress themselves with a warning while <see cref="IsAutoInjectionClaimed"/> is true — they never
    /// register, drive, or shut the session down, so component ownership stays unique.
    /// </para>
    /// </remarks>
    [Preserve]
    public static class GameFrameXRuntimeHost
    {
        private const string HostName = "[GameFrameX]";

        private static readonly Dictionary<Type, GameFrameXComponentRuntimeConfig> PendingConfigs = new Dictionary<Type, GameFrameXComponentRuntimeConfig>(32);

        private static GameObject s_Host;
        private static bool s_Started;
        private static bool s_AutoInjectionClaimed;
        private static GameFrameXRuntimePlan s_LastPlan;

        /// <summary>
        /// 获取是否已宣告自动注入接管（Bootstrap 在场景加载前置位、Host 启动时清除）。
        /// </summary>
        /// <remarks>
        /// 场景内组件的 <c>Awake</c> 据此判定自身是否为残留入口：为真即自我抑制。
        /// Gets whether auto-injection ownership has been claimed (set by the bootstrap before scene load, cleared when the host starts).
        /// </remarks>
        public static bool IsAutoInjectionClaimed
        {
            get { return s_AutoInjectionClaimed; }
        }

        /// <summary>
        /// 获取最近一次启动计划。
        /// </summary>
        /// <remarks>
        /// Gets the most recent startup plan.
        /// </remarks>
        public static GameFrameXRuntimePlan LastPlan
        {
            get { return s_LastPlan; }
        }

        /// <summary>
        /// 宣告框架将接管全部 GFX 组件（由 <see cref="GameFrameXRuntimeBootstrap"/> 在场景加载前调用）。
        /// </summary>
        /// <remarks>
        /// 宣告后、<see cref="EnsureStarted"/> 清标记前 <c>Awake</c> 的 GFX 组件一律视为场景残留并被抑制。
        /// 业务代码不应调用本方法——它是 Bootstrap 与组件抑制之间的内部契约，公开仅为可测试性。
        /// <para>
        /// English: Declares that the framework will own every GFX component; called by <see cref="GameFrameXRuntimeBootstrap"/>
        /// before scene load. Any GFX component awakening between the claim and <see cref="EnsureStarted"/> clearing it is treated
        /// as a scene leftover and suppressed. Business code must not call this; it is public for testability only.
        /// </para>
        /// </remarks>
        [Preserve]
        public static void ClaimAutoInjection()
        {
            s_AutoInjectionClaimed = true;
        }

        /// <summary>
        /// 重置自动运行时 Host 状态。
        /// </summary>
        /// <remarks>
        /// Resets the auto runtime host state.
        /// </remarks>
        public static void Reset()
        {
            s_Host = null;
            s_Started = false;
            s_AutoInjectionClaimed = false;
            s_LastPlan = null;
            PendingConfigs.Clear();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        /// <summary>
        /// 确保自动运行时已经启动：创建 <c>[GameFrameX]</c> Host 并挂载扫描到的全部组件。
        /// </summary>
        /// <remarks>
        /// 幂等：重复调用只追加一条诊断。启动前先清除接管标记，使 Host 自己挂载的组件正常注册。
        /// English: Idempotent — repeated calls only append a diagnostic. The claim is cleared before mounting so the
        /// components the host adds itself register normally.
        /// </remarks>
        public static void EnsureStarted()
        {
            if (s_Started)
            {
                if (s_LastPlan != null)
                {
                    s_LastPlan.Diagnostics.Add("Auto runtime startup was requested again and skipped.");
                    WriteDiagnostics();
                }

                return;
            }

            // 清标记必须早于创建 Host：场景内残留组件已在更早的 Awake 阶段抑制完毕，
            // 此后 Awake 的组件只可能是 Host 自己挂载的，必须正常注册。
            // The claim must be cleared before the host is created: leftover scene components were already suppressed
            // during an earlier Awake, so any component awakening after this point is mounted by the host and must register.
            s_AutoInjectionClaimed = false;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var scanResult = GameFrameXRuntimeScanner.Scan();
            var overrideContext = GameFrameXRuntimeOverrideProvider.CollectOverrides();
            GameFrameXRuntimeManagerResolver.RegisterDescriptors(scanResult.ManagerDescriptors);
            GameFrameXRuntimeManagerResolver.RegisterOverrides(overrideContext.GetManagerOverrides());

            s_LastPlan = GameFrameXRuntimePlanner.CreatePlan(scanResult, overrideContext);
            s_LastPlan.Diagnostics.Add(Utility.Text.Format(
                "Scan result: {0} component(s), {1} manager implementation(s), {2} manager override(s).",
                scanResult.ComponentTypes.Count,
                scanResult.ManagerDescriptors.Count,
                overrideContext.GetManagerOverrides().Count));
            s_Host = new GameObject(HostName);
            s_Host.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(s_Host);

            foreach (var descriptor in s_LastPlan.Components)
            {
                AddComponent(descriptor);
            }

            s_Host.SetActive(true);
            s_Started = true;
            s_LastPlan.Diagnostics.Add(Utility.Text.Format("Startup elapsed: {0} ms.", stopwatch.ElapsedMilliseconds));
            WriteDiagnostics();
        }

        /// <summary>
        /// 获取组件待应用的运行时配置。
        /// </summary>
        /// <remarks>
        /// Gets the runtime configuration pending for the component.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <returns>运行时配置。 / The runtime configuration.</returns>
        internal static GameFrameXComponentRuntimeConfig GetPendingConfig(Type componentType)
        {
            if (componentType == null)
            {
                return null;
            }

            PendingConfigs.TryGetValue(componentType, out var config);
            return config;
        }

        /// <summary>
        /// 禁用场景中残留的 GFX 组件入口并输出告警（由组件在 Awake 中经 <see cref="IsAutoInjectionClaimed"/> 判定后调用）。
        /// </summary>
        /// <param name="component">被抑制的组件。 / The suppressed component.</param>
        /// <remarks>
        /// English: Disables a leftover GFX component entry placed by hand in the scene and emits a warning, telling the
        /// developer to remove the manual node and let the auto host own it.
        /// </remarks>
        internal static void SuppressSceneEntry(Component component)
        {
            if (component == null)
            {
                return;
            }

            component.enabled = false;
            Debug.LogWarning(Utility.Text.Format(
                "[GameFrameX] Scene entry '{0}' on '{1}' was suppressed because the auto runtime owns all GameFrameX components. Remove the hand-placed GameFrameX node from the scene; the framework injects it automatically.",
                component.GetType().FullName,
                GetHierarchyPath(component.transform)));
        }

        private static string GetHierarchyPath(Transform transform)
        {
            if (transform == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder(transform.name);
            var parent = transform.parent;
            while (parent != null)
            {
                builder.Insert(0, "/");
                builder.Insert(0, parent.name);
                parent = parent.parent;
            }

            return builder.ToString();
        }

        private static void AddComponent(GameFrameXRuntimeComponentDescriptor descriptor)
        {
            if (descriptor == null || descriptor.ComponentType == null)
            {
                return;
            }

            if (descriptor.Config != null)
            {
                PendingConfigs[descriptor.ComponentType] = descriptor.Config;
            }

            // RequireComponent 可能在先前组件创建时已在宿主子树上自动补齐同类型组件（如 FairyGUIComponent 自动附带 FairyGUIPackageComponent），
            // 此时直接复用该实例并应用配置，避免再创建一个会在注册阶段被判重复的实例。
            var existingComponent = s_Host != null ? s_Host.GetComponentInChildren(descriptor.ComponentType, true) : null;
            if (existingComponent != null)
            {
                if (existingComponent is IGameFrameXComponentRuntimeConfigurable configurable && descriptor.Config != null)
                {
                    configurable.ApplyRuntimeConfig(descriptor.Config);
                }

                return;
            }

            var componentNodeName = GetComponentNodeName(descriptor.ComponentType);
            var componentNode = new GameObject(componentNodeName);
            componentNode.transform.SetParent(s_Host.transform, false);

            var component = componentNode.AddComponent(descriptor.ComponentType) as GameFrameworkComponent;
            if (component is IGameFrameXComponentRuntimeConfigurable configurable2 && descriptor.Config != null)
            {
                configurable2.ApplyRuntimeConfig(descriptor.Config);
            }
        }

        private static string GetComponentNodeName(Type componentType)
        {
            const string ComponentSuffix = "Component";
            var componentName = componentType.Name;
            if (componentName.Length <= ComponentSuffix.Length || !componentName.EndsWith(ComponentSuffix, StringComparison.Ordinal))
            {
                return componentName;
            }

            return componentName.Substring(0, componentName.Length - ComponentSuffix.Length);
        }

        private static void WriteDiagnostics()
        {
            if (s_LastPlan == null)
            {
                return;
            }

            Debug.Log(Utility.Text.Format("GameFrameX auto runtime started. Components: {0}.", s_LastPlan.Components.Count));
            foreach (var component in s_LastPlan.Components)
            {
                Debug.Log(Utility.Text.Format(
                    "GameFrameX auto runtime enabled component: {0}, node: {1}/{2}, order: {3}.",
                    component.ComponentType.FullName,
                    HostName,
                    GetComponentNodeName(component.ComponentType),
                    component.Order));
            }

            foreach (var diagnostic in s_LastPlan.Diagnostics)
            {
                Debug.LogWarning(Utility.Text.Format("GameFrameX auto runtime: {0}", diagnostic));
            }
        }
    }
}
