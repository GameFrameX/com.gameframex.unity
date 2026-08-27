using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 解析 Manager 接口对应的运行时实现。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeManagerResolver
    {
        private static readonly Dictionary<Type, List<GameFrameXRuntimeManagerDescriptor>> ManagerDescriptors = new Dictionary<Type, List<GameFrameXRuntimeManagerDescriptor>>(32);

        private static readonly Dictionary<Type, GameFrameXManagerRuntimeOverride> ManagerOverrides = new Dictionary<Type, GameFrameXManagerRuntimeOverride>(32);

        private static bool s_DescriptorsReady;

        /// <summary>
        /// 重置 Manager 解析器。
        /// </summary>
        public static void Reset()
        {
            ManagerDescriptors.Clear();
            ManagerOverrides.Clear();
            s_DescriptorsReady = false;
        }

        /// <summary>
        /// 注册 Manager 描述。
        /// </summary>
        /// <param name="descriptors">Manager 描述列表。</param>
        public static void RegisterDescriptors(IEnumerable<GameFrameXRuntimeManagerDescriptor> descriptors)
        {
            if (descriptors == null)
            {
                return;
            }

            foreach (var descriptor in descriptors)
            {
                if (descriptor == null || descriptor.InterfaceType == null || descriptor.ImplementationType == null)
                {
                    continue;
                }

                if (!ManagerDescriptors.TryGetValue(descriptor.InterfaceType, out var implementations))
                {
                    implementations = new List<GameFrameXRuntimeManagerDescriptor>(32);
                    ManagerDescriptors.Add(descriptor.InterfaceType, implementations);
                }

                implementations.Add(descriptor);
            }

            s_DescriptorsReady = true;
        }

        /// <summary>
        /// 注册 Manager 运行时覆盖。
        /// </summary>
        /// <param name="overrides">Manager 运行时覆盖列表。</param>
        public static void RegisterOverrides(IEnumerable<GameFrameXManagerRuntimeOverride> overrides)
        {
            if (overrides == null)
            {
                return;
            }

            foreach (var managerOverride in overrides)
            {
                if (managerOverride == null || managerOverride.InterfaceType == null || managerOverride.ImplementationType == null)
                {
                    continue;
                }

                if (!managerOverride.InterfaceType.IsAssignableFrom(managerOverride.ImplementationType))
                {
                    UnityEngine.Debug.LogWarning(GameFrameworkText.Format(
                        "GameFrameX auto runtime manager override '{0}' is not assignable to interface '{1}'.",
                        managerOverride.ImplementationType.FullName,
                        managerOverride.InterfaceType.FullName));
                    continue;
                }

                if (!ManagerOverrides.TryGetValue(managerOverride.InterfaceType, out var current) ||
                    managerOverride.Priority >= current.Priority)
                {
                    ManagerOverrides[managerOverride.InterfaceType] = managerOverride;
                }
            }
        }

        /// <summary>
        /// 解析 Manager 实现类型。
        /// </summary>
        /// <param name="interfaceType">Manager 接口类型。</param>
        /// <param name="configuredTypeName">显式配置的实现类型名。</param>
        /// <returns>Manager 实现类型。</returns>
        public static Type Resolve(Type interfaceType, string configuredTypeName)
        {
            if (interfaceType == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(configuredTypeName))
            {
                Type configuredType = AssemblyUtility.GetType(configuredTypeName);
                if (configuredType == null)
                {
                    UnityEngine.Debug.LogWarning(GameFrameworkText.Format(
                        "GameFrameX auto runtime can not resolve configured manager '{0}' for interface '{1}'.",
                        configuredTypeName,
                        interfaceType.FullName));
                    return null;
                }

                if (!interfaceType.IsAssignableFrom(configuredType))
                {
                    UnityEngine.Debug.LogWarning(GameFrameworkText.Format(
                        "GameFrameX auto runtime configured manager '{0}' is not assignable to interface '{1}'.",
                        configuredType.FullName,
                        interfaceType.FullName));
                    return null;
                }

                UnityEngine.Debug.Log(GameFrameworkText.Format(
                    "GameFrameX auto runtime manager selected by component config: {0} -> {1}.",
                    interfaceType.FullName,
                    configuredType.FullName));
                return configuredType;
            }

            if (ManagerOverrides.TryGetValue(interfaceType, out var managerOverride))
            {
                UnityEngine.Debug.Log(GameFrameworkText.Format(
                    "GameFrameX auto runtime manager selected by runtime override: {0} -> {1}.",
                    interfaceType.FullName,
                    managerOverride.ImplementationType.FullName));
                return managerOverride.ImplementationType;
            }

            EnsureDescriptors();

            if (ManagerDescriptors.TryGetValue(interfaceType, out var descriptors) && descriptors.Count > 0)
            {
                descriptors.Sort(CompareManagerDescriptor);
                if (descriptors.Count > 1)
                {
                    UnityEngine.Debug.LogWarning(GameFrameworkText.Format(
                        "GameFrameX auto runtime manager conflict for interface '{0}'. Selected '{1}' by priority.",
                        interfaceType.FullName,
                        descriptors[0].ImplementationType.FullName));
                }
                else
                {
                    UnityEngine.Debug.Log(GameFrameworkText.Format(
                        "GameFrameX auto runtime manager selected by scan: {0} -> {1}.",
                        interfaceType.FullName,
                        descriptors[0].ImplementationType.FullName));
                }

                return descriptors[0].ImplementationType;
            }

            if (interfaceType.Name.Length > 1 && interfaceType.Name[0] == 'I')
            {
                string conventionName = GameFrameworkText.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
                Type conventionType = AssemblyUtility.GetType(conventionName);
                if (conventionType != null && interfaceType.IsAssignableFrom(conventionType))
                {
                    UnityEngine.Debug.Log(GameFrameworkText.Format(
                        "GameFrameX auto runtime manager selected by naming convention: {0} -> {1}.",
                        interfaceType.FullName,
                        conventionType.FullName));
                    return conventionType;
                }
            }

            UnityEngine.Debug.LogWarning(GameFrameworkText.Format(
                "GameFrameX auto runtime can not resolve manager for interface '{0}'.",
                interfaceType.FullName));
            return null;
        }

        private static void EnsureDescriptors()
        {
            if (s_DescriptorsReady)
            {
                return;
            }

            var scanResult = GameFrameXRuntimeScanner.Scan();
            RegisterDescriptors(scanResult.ManagerDescriptors);
        }

        private static int CompareManagerDescriptor(GameFrameXRuntimeManagerDescriptor x, GameFrameXRuntimeManagerDescriptor y)
        {
            int priority = y.Priority.CompareTo(x.Priority);
            if (priority != 0)
            {
                return priority;
            }

            return string.Compare(x.ImplementationType.FullName, y.ImplementationType.FullName, StringComparison.Ordinal);
        }
    }
}