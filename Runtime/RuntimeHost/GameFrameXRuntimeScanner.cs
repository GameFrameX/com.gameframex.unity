using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 扫描已加载程序集中的 GameFrameX 组件和 Manager。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeScanner
    {
        /// <summary>
        /// 执行运行时扫描。
        /// </summary>
        /// <returns>扫描结果。</returns>
        public static GameFrameXRuntimeScanResult Scan()
        {
            var result = new GameFrameXRuntimeScanResult();
            var assemblies = AssemblyUtility.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly == null || ShouldSkipAssembly(assembly))
                {
                    continue;
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

                    if (typeof(GameFrameworkModule).IsAssignableFrom(type))
                    {
                        AddManagerDescriptors(result, type);
                    }
                }
            }

            return result;
        }

        private static bool ShouldSkipAssembly(Assembly assembly)
        {
            string name = assembly.GetName().Name;
            return IsEditorAssemblyName(name) || IsTestAssemblyName(name);
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
                result.Errors.Add(GameFrameworkText.Format("Assembly '{0}' contains unloaded types: {1}", assembly.FullName, exception.Message));
                return exception.Types;
            }
            catch (Exception exception)
            {
                result.Errors.Add(GameFrameworkText.Format("Can not scan assembly '{0}': {1}", assembly.FullName, exception.Message));
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
                   interfaceType.IsAssignableFrom(implementationType);
        }
    }
}