using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 解析 Manager 接口对应的运行时实现。
    /// </summary>
    /// <remarks>
    /// Resolves the runtime implementation for a manager interface.
    /// </remarks>
    [Preserve]
    public static class GameFrameXRuntimeManagerResolver
    {
        private static readonly Dictionary<Type, List<GameFrameXRuntimeManagerDescriptor>> ManagerDescriptors = new Dictionary<Type, List<GameFrameXRuntimeManagerDescriptor>>(32);

        private static readonly Dictionary<Type, GameFrameXManagerRuntimeOverride> ManagerOverrides = new Dictionary<Type, GameFrameXManagerRuntimeOverride>(32);

        private static readonly HashSet<string> RegisteredAssemblyNames = new HashSet<string>(StringComparer.Ordinal);

        private static bool s_DescriptorsReady;

        /// <summary>
        /// 重置 Manager 解析器。
        /// </summary>
        /// <remarks>
        /// Resets the manager resolver.
        /// </remarks>
        public static void Reset()
        {
            ManagerDescriptors.Clear();
            ManagerOverrides.Clear();
            RegisteredAssemblyNames.Clear();
            s_DescriptorsReady = false;
        }


        /// <summary>
        /// 注册 Manager 描述。
        /// </summary>
        /// <remarks>
        /// Registers manager descriptors.
        /// </remarks>
        /// <param name="descriptors">Manager 描述列表。 / The list of manager descriptors.</param>
        public static void RegisterDescriptors(IEnumerable<GameFrameXRuntimeManagerDescriptor> descriptors)
        {
            RegisterDescriptors(descriptors, true);
        }

        // C29 修复：增量注册不得置位 s_DescriptorsReady——首个 Resolve 前经钩子注册的含描述符程序集若提前置位，
        // 将抑制全量兜底，订阅之前已加载的启动程序集描述符永不注册（Manual / Headless 模式下热更程序集
        // 在首个 Resolve 前加载即触发）。ready 只能由全量路径（EnsureDescriptors / Host Auto 全量注册）置位。
        // C29 fix: incremental registration must NOT set s_DescriptorsReady — if a descriptor-bearing assembly
        // registered via the hook sets it before the first Resolve, the full fallback is suppressed and descriptors
        // of startup assemblies loaded before subscription are never registered (triggered when a hot-update assembly
        // loads before the first Resolve in Manual / Headless mode). Only the full paths (EnsureDescriptors /
        // Host Auto full registration) may set ready.
        internal static void RegisterDescriptors(IEnumerable<GameFrameXRuntimeManagerDescriptor> descriptors, bool markReady)
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

            if (markReady)
            {
                s_DescriptorsReady = true;
            }
        }


        /// <summary>
        /// 增量注册单个程序集内的 Manager 描述（C29 起权威）：程序集粒度幂等去重
        /// （同名程序集重复注册 / 热更卸载后同名再加载只生效一次），
        /// Editor / Tests 程序集走与全量扫描相同的门直接跳过。
        /// </summary>
        /// <remarks>
        /// Incrementally registers manager descriptors from a single assembly (authoritative since C29):
        /// idempotent per assembly (re-registering an assembly name — including reload after hot-update unload —
        /// takes effect once); Editor / Tests assemblies are skipped by the same gate as the full scan.
        /// </remarks>
        /// <param name="assembly">迟加载的程序集。 / The late-loaded assembly.</param>
        [Preserve]
        public static void RegisterAssembly(System.Reflection.Assembly assembly)
        {
            RegisterAssembly(assembly, true);
        }

        internal static void RegisterAssembly(System.Reflection.Assembly assembly, bool announce)
        {
            // Reflection.Emit 的 AssemblyBuilder 尚未定义类型即触发 AssemblyLoad，且会污染程序集粒度去重集；
            // Reflection.Emit AssemblyBuilder fires AssemblyLoad before any type is defined and would poison the per-assembly dedup set;
            // 直接忽略（不标记已注册），待真实 LoadFrom/Load 完成时再注册。
            // ignore it entirely (without marking registered); registration happens on the real LoadFrom/Load.
            if (assembly is System.Reflection.Emit.AssemblyBuilder)
            {
                return;
            }

            if (assembly == null)
            {
                return;
            }

            string assemblyName = assembly.GetName().Name;
            if (RegisteredAssemblyNames.Contains(assemblyName))
            {
                return;
            }

            RegisteredAssemblyNames.Add(assemblyName);

            if (GameFrameXRuntimeScanner.ShouldSkipAssembly(assembly))
            {
                return;
            }

            var scanResult = GameFrameXRuntimeScanner.Scan(assembly);
            int descriptorCount = scanResult.ManagerDescriptors.Count;
            if (descriptorCount > 0)
            {
                RegisterDescriptors(scanResult.ManagerDescriptors, false);
            }

            if (announce)
            {
                UnityEngine.Debug.Log(Utility.Text.Format(
                    "[GameFrameX] Late-loaded assembly '{0}' registered {1} manager descriptor(s).",
                    assembly.FullName,
                    descriptorCount.ToString()));
            }
        }

        /// <summary>
        /// 注册 Manager 运行时覆盖。
        /// </summary>
        /// <remarks>
        /// Registers manager runtime overrides.
        /// </remarks>
        /// <param name="overrides">Manager 运行时覆盖列表。 / The list of manager runtime overrides.</param>
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
                    UnityEngine.Debug.LogWarning(Utility.Text.Format(
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
        /// <remarks>
        /// Resolves the manager implementation type.
        /// </remarks>
        /// <param name="interfaceType">Manager 接口类型。 / The manager interface type.</param>
        /// <param name="configuredTypeName">显式配置的实现类型名。 / The explicitly configured implementation type name.</param>
        /// <returns>Manager 实现类型。 / The manager implementation type.</returns>
        public static Type Resolve(Type interfaceType, string configuredTypeName)
        {
            if (interfaceType == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(configuredTypeName))
            {
                Type configuredType = Utility.Assembly.GetType(configuredTypeName);
                if (configuredType == null)
                {
                    UnityEngine.Debug.LogWarning(Utility.Text.Format(
                        "GameFrameX auto runtime can not resolve configured manager '{0}' for interface '{1}'.",
                        configuredTypeName,
                        interfaceType.FullName));
                    return null;
                }

                if (!interfaceType.IsAssignableFrom(configuredType))
                {
                    UnityEngine.Debug.LogWarning(Utility.Text.Format(
                        "GameFrameX auto runtime configured manager '{0}' is not assignable to interface '{1}'.",
                        configuredType.FullName,
                        interfaceType.FullName));
                    return null;
                }

                UnityEngine.Debug.Log(Utility.Text.Format(
                    "GameFrameX auto runtime manager selected by component config: {0} -> {1}.",
                    interfaceType.FullName,
                    configuredType.FullName));
                return configuredType;
            }

            if (ManagerOverrides.TryGetValue(interfaceType, out var managerOverride))
            {
                UnityEngine.Debug.Log(Utility.Text.Format(
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
                    UnityEngine.Debug.LogWarning(Utility.Text.Format(
                        "GameFrameX auto runtime manager conflict for interface '{0}'. Selected '{1}' by priority.",
                        interfaceType.FullName,
                        descriptors[0].ImplementationType.FullName));
                }
                else
                {
                    UnityEngine.Debug.Log(Utility.Text.Format(
                        "GameFrameX auto runtime manager selected by scan: {0} -> {1}.",
                        interfaceType.FullName,
                        descriptors[0].ImplementationType.FullName));
                }

                return descriptors[0].ImplementationType;
            }

            if (interfaceType.Name.Length > 1 && interfaceType.Name[0] == 'I')
            {
                string conventionName = Utility.Text.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
                Type conventionType = Utility.Assembly.GetType(conventionName);
                if (conventionType != null && interfaceType.IsAssignableFrom(conventionType))
                {
                    UnityEngine.Debug.Log(Utility.Text.Format(
                        "GameFrameX auto runtime manager selected by naming convention: {0} -> {1}.",
                        interfaceType.FullName,
                        conventionType.FullName));
                    return conventionType;
                }
            }

            UnityEngine.Debug.LogWarning(Utility.Text.Format(
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

            // C29：全量兜底与增量注册共用同一条按程序集注册路径（静默，避免启动期程序集级日志噪音）。
            // C29: the full fallback and incremental registration share the same per-assembly path (silent, to avoid per-assembly log noise during startup).
            foreach (var assembly in Utility.Assembly.GetAssemblies())
            {
                RegisterAssembly(assembly, false);
            }

            s_DescriptorsReady = true;
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
