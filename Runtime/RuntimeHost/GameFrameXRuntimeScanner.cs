using System;
using System.Reflection;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 扫描已加载程序集中的 GameFrameX 组件和 Manager。
    /// </summary>
    /// <remarks>
    /// Scans loaded assemblies for GameFrameX components and managers.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameXRuntimeScanner
    {
        /// <summary>
        /// 执行运行时扫描。
        /// </summary>
        /// <remarks>
        /// Performs the runtime scan.
        /// </remarks>
        /// <returns>扫描结果。 / The scan result.</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameFrameXRuntimeScanResult Scan()
        {
            var result = new GameFrameXRuntimeScanResult();
            foreach (var assembly in Utility.Assembly.GetAssemblies())
            {
                ScanAssemblyInto(assembly, result);
            }

            return result;
        }

        /// <summary>
        /// 执行单个程序集的增量扫描（C29）：门与全量扫描一致（Editor / Tests 跳过），
        /// Manager 描述符收集口径与全量扫描共用同一条路径。
        /// </summary>
        /// <remarks>
        /// Performs an incremental scan of a single assembly (C29): same gate as the full scan
        /// (Editor / Tests assemblies are skipped); manager descriptor collection shares the same path as the full scan.
        /// </remarks>
        /// <param name="assembly">要扫描的程序集。 / The assembly to scan.</param>
        /// <returns>扫描结果。 / The scan result.</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameFrameXRuntimeScanResult Scan(Assembly assembly)
        {
            var result = new GameFrameXRuntimeScanResult();
            ScanAssemblyInto(assembly, result);
            return result;
        }

        /// <summary>
        /// 判断程序集是否应被扫描门跳过（Editor / Tests），供增量注册与全量扫描共用同一口径。
        /// </summary>
        /// <remarks>
        /// Determines whether an assembly should be skipped by the scan gate (Editor / Tests);
        /// shared by incremental registration and the full scan.
        /// </remarks>
        /// <param name="assembly">要判断的程序集。 / The assembly to check.</param>
        /// <returns>是否跳过。 / Whether to skip.</returns>
        internal static bool ShouldSkipAssembly(Assembly assembly)
        {
            string name = assembly.GetName().Name;
            return IsEditorAssemblyName(name) || IsTestAssemblyName(name);
        }

        private static void ScanAssemblyInto(Assembly assembly, GameFrameXRuntimeScanResult result)
        {
            if (assembly == null || ShouldSkipAssembly(assembly))
            {
                return;
            }

            var types = GetLoadableTypes(assembly, result);
            foreach (var type in types)
            {
                if (type == null || !type.IsClass || type.IsAbstract || type.IsGenericTypeDefinition)
                {
                    continue;
                }

                if (typeof(GameFrameworkComponent).IsAssignableFrom(type))
                {
                    if (type.GetCustomAttributes(typeof(GameFrameXManualStartupAttribute), false).Length <= 0)
                    {
                        result.ComponentTypes.Add(type);
                    }
                }

                if (typeof(IGameFrameworkModule).IsAssignableFrom(type))
                {
                    AddManagerDescriptors(result, type);
                }

                if (typeof(IGameFrameXOptionsContributor).IsAssignableFrom(type))
                {
                    result.OptionsContributorTypes.Add(type);
                }

                if (typeof(IGameFrameXRuntimeOverrideProvider).IsAssignableFrom(type))
                {
                    result.OverrideProviderTypes.Add(type);
                }
            }
        }


        private static bool IsEditorAssemblyName(string name)
        {
            return name.EndsWith(".Editor", StringComparison.Ordinal) ||
                   name.IndexOf("Editor", StringComparison.OrdinalIgnoreCase) >= 0 && name.StartsWith("UnityEditor", StringComparison.Ordinal);
        }

        private static bool IsTestAssemblyName(string name)
        {
            return name.EndsWith(".Tests", StringComparison.OrdinalIgnoreCase) ||
                   name.IndexOf(".Tests.", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static Type[] GetLoadableTypes(Assembly assembly, GameFrameXRuntimeScanResult result)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                result.Errors.Add(Utility.Text.Format("Assembly '{0}' contains unloaded types: {1}", assembly.FullName, exception.Message));
                return exception.Types;
            }
            catch (Exception exception)
            {
                result.Errors.Add(Utility.Text.Format("Can not scan assembly '{0}': {1}", assembly.FullName, exception.Message));
                return Array.Empty<Type>();
            }
        }

        private static void AddManagerDescriptors(GameFrameXRuntimeScanResult result, Type implementationType)
        {
            var attributes = implementationType.GetCustomAttributes(typeof(GameFrameXManagerImplementationAttribute), false);
            if (attributes.Length > 0)
            {
                foreach (GameFrameXManagerImplementationAttribute attribute in attributes)
                {
                    if (IsValidManagerInterface(attribute.InterfaceType, implementationType))
                    {
                        result.ManagerDescriptors.Add(new GameFrameXRuntimeManagerDescriptor(attribute.InterfaceType, implementationType, attribute.Priority));
                    }
                }

                return;
            }

            var interfaces = implementationType.GetInterfaces();
            foreach (var interfaceType in interfaces)
            {
                if (IsValidManagerInterface(interfaceType, implementationType))
                {
                    result.ManagerDescriptors.Add(new GameFrameXRuntimeManagerDescriptor(interfaceType, implementationType, 0));
                }
            }
        }

        private static bool IsValidManagerInterface(Type interfaceType, Type implementationType)
        {
            return interfaceType != null &&
                   interfaceType.IsInterface &&
                   interfaceType.FullName != null &&
                   interfaceType.FullName.StartsWith("GameFrameX.", StringComparison.Ordinal) &&
                   !IsFrameworkContractInterface(interfaceType) &&
                   interfaceType.IsAssignableFrom(implementationType);
        }

        private static bool IsFrameworkContractInterface(Type interfaceType)
        {
            return interfaceType == typeof(IGameFrameworkModule) ||
                   interfaceType == typeof(IUpdatableModule) ||
                   interfaceType == typeof(IShutdownableModule) ||
                   interfaceType == typeof(IPrioritizedModule) ||
                   interfaceType == typeof(ILowMemoryAwareModule);
        }
    }
}
