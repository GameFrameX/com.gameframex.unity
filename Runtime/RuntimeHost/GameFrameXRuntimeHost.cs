using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理 GameFrameX 自动运行时 Host。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeHost
    {
        private const string HostName = "[GameFrameX]";

        private static readonly Dictionary<Type, GameFrameXComponentRuntimeConfig> PendingConfigs = new Dictionary<Type, GameFrameXComponentRuntimeConfig>(32);

        private static GameObject s_Host;
        private static bool s_Started;
        private static GameFrameXRuntimePlan s_LastPlan;
        private static GameFrameXRuntimeModeResult s_LastModeResult;

        /// <summary>
        /// 获取最近一次启动计划。
        /// </summary>
        public static GameFrameXRuntimePlan LastPlan
        {
            get { return s_LastPlan; }
        }

        /// <summary>
        /// 获取最近一次运行模式裁决结果。
        /// </summary>
        public static GameFrameXRuntimeModeResult LastModeResult
        {
            get { return s_LastModeResult; }
        }

        /// <summary>
        /// 重置自动运行时 Host 状态。
        /// </summary>
        public static void Reset()
        {
            s_Host = null;
            s_Started = false;
            s_LastPlan = null;
            s_LastModeResult = null;
            PendingConfigs.Clear();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        /// <summary>
        /// 确保自动运行时已经启动。
        /// </summary>
        public static void EnsureStarted()
        {
            EnsureStarted(new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.AutoPackageMode,
                "Auto runtime host was requested directly.",
                string.Empty));
        }

        /// <summary>
        /// 确保自动运行时已经启动。
        /// </summary>
        /// <param name="modeResult">运行模式裁决结果。</param>
        public static void EnsureStarted(GameFrameXRuntimeModeResult modeResult)
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

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            s_LastModeResult = modeResult ?? new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.AutoPackageMode,
                "Auto runtime host was requested without mode result.",
                string.Empty);

            if (s_LastModeResult.Mode == GameFrameXRuntimeMode.ManualSceneMode)
            {
                RecordManualSceneMode(s_LastModeResult);
                return;
            }

            if (HasExistingRuntime())
            {
                RecordManualSceneMode(new GameFrameXRuntimeModeResult(
                    GameFrameXRuntimeMode.ManualSceneMode,
                    "Existing GameFrameX runtime detected during auto host startup. Auto host startup skipped.",
                    string.Empty));
                return;
            }

            var scanResult = GameFrameXRuntimeScanner.Scan();
            var overrideContext = GameFrameXRuntimeOverrideProvider.CollectOverrides();
            GameFrameXRuntimeManagerResolver.RegisterDescriptors(scanResult.ManagerDescriptors);
            GameFrameXRuntimeManagerResolver.RegisterOverrides(overrideContext.GetManagerOverrides());

            s_LastPlan = GameFrameXRuntimePlanner.CreatePlan(scanResult, overrideContext);
            s_LastPlan.Diagnostics.Add(Utility.Text.Format("Runtime mode: {0}. Reason: {1}.", s_LastModeResult.Mode, s_LastModeResult.Reason));
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
        /// 记录手动场景模式诊断，不创建自动 Host。
        /// </summary>
        /// <param name="modeResult">运行模式裁决结果。</param>
        public static void RecordManualSceneMode(GameFrameXRuntimeModeResult modeResult)
        {
            s_LastModeResult = modeResult ?? new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.ManualSceneMode,
                "Manual scene mode was requested without mode result.",
                string.Empty);
            s_Started = true;
            s_LastPlan = new GameFrameXRuntimePlan();
            s_LastPlan.Diagnostics.Add(Utility.Text.Format("Runtime mode: {0}. Reason: {1}. Manual entry: {2}.",
                s_LastModeResult.Mode,
                s_LastModeResult.Reason,
                string.IsNullOrEmpty(s_LastModeResult.ManualEntryPath) ? "unknown" : s_LastModeResult.ManualEntryPath));
            s_LastPlan.Diagnostics.Add("Auto host startup skipped by manual scene mode.");
            WriteDiagnostics();
        }

        /// <summary>
        /// 获取组件待应用的运行时配置。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <returns>运行时配置。</returns>
        internal static GameFrameXComponentRuntimeConfig GetPendingConfig(Type componentType)
        {
            if (componentType == null)
            {
                return null;
            }

            PendingConfigs.TryGetValue(componentType, out var config);
            return config;
        }

        private static bool HasExistingRuntime()
        {
            if (GameEntry.GetComponent<BaseComponent>() != null)
            {
                return true;
            }

            return UnityEngine.Object.FindObjectOfType<BaseComponent>() != null;
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

            var componentNodeName = GetComponentNodeName(descriptor.ComponentType);
            var componentNode = new GameObject(componentNodeName);
            componentNode.transform.SetParent(s_Host.transform, false);

            var component = componentNode.AddComponent(descriptor.ComponentType) as GameFrameworkComponent;
            if (component is IGameFrameXComponentRuntimeConfigurable configurable && descriptor.Config != null)
            {
                configurable.ApplyRuntimeConfig(descriptor.Config);
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

            if (s_LastModeResult != null && s_LastModeResult.Mode == GameFrameXRuntimeMode.ManualSceneMode)
            {
                Debug.Log("GameFrameX manual scene runtime detected. Auto host startup skipped.");
            }
            else
            {
                Debug.Log(Utility.Text.Format("GameFrameX auto runtime started. Components: {0}.", s_LastPlan.Components.Count));
            }
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
