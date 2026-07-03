using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 根据扫描结果和运行时覆盖生成自动启动计划。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimePlanner
    {
        /// <summary>
        /// 创建自动运行时启动计划。
        /// </summary>
        /// <param name="scanResult">扫描结果。</param>
        /// <param name="overrideContext">运行时覆盖上下文。</param>
        /// <returns>自动启动计划。</returns>
        public static GameFrameXRuntimePlan CreatePlan(GameFrameXRuntimeScanResult scanResult, GameFrameXRuntimeOverrideContext overrideContext)
        {
            var plan = new GameFrameXRuntimePlan();
            if (scanResult == null)
            {
                return plan;
            }

            var configs = overrideContext != null ? overrideContext.GetComponentConfigs() : null;
            var descriptors = new Dictionary<Type, GameFrameXRuntimeComponentDescriptor>();

            foreach (var componentType in scanResult.ComponentTypes)
            {
                var descriptor = CreateDescriptor(componentType);
                if (configs != null && configs.TryGetValue(componentType, out var config))
                {
                    ApplyConfig(descriptor, config);
                    plan.Diagnostics.Add(Utility.Text.Format(
                        "Apply runtime override to component '{0}': enabled={1}, order={2}, manager={3}, valueCount={4}.",
                        componentType.FullName,
                        config.Enabled.HasValue ? config.Enabled.Value.ToString() : "default",
                        config.Order.HasValue ? config.Order.Value.ToString() : "default",
                        config.ManagerImplementationType != null ? config.ManagerImplementationType.FullName : config.ManagerImplementationTypeName ?? "default",
                        config.GetValues().Count));
                }

                if (!descriptor.Enabled)
                {
                    plan.Diagnostics.Add(Utility.Text.Format("Skip component '{0}' by runtime override.", componentType.FullName));
                    continue;
                }

                descriptors[componentType] = descriptor;
            }

            foreach (var descriptor in SortByDependency(descriptors.Values, plan.Diagnostics))
            {
                plan.Components.Add(descriptor);
            }

            foreach (var error in scanResult.Errors)
            {
                plan.Diagnostics.Add(error);
            }

            return plan;
        }

        private static GameFrameXRuntimeComponentDescriptor CreateDescriptor(Type componentType)
        {
            var descriptor = new GameFrameXRuntimeComponentDescriptor(componentType, 0);

            var autoAttributes = componentType.GetCustomAttributes(typeof(GameFrameXAutoComponentAttribute), false);
            if (autoAttributes.Length > 0)
            {
                var autoAttribute = (GameFrameXAutoComponentAttribute)autoAttributes[0];
                descriptor.Order = autoAttribute.Order;
                descriptor.Enabled = autoAttribute.EnabledByDefault;
                AddDependencies(descriptor, autoAttribute.Dependencies);
            }

            var dependencyAttributes = componentType.GetCustomAttributes(typeof(GameFrameXComponentDependencyAttribute), false);
            foreach (GameFrameXComponentDependencyAttribute dependencyAttribute in dependencyAttributes)
            {
                if (dependencyAttribute.DependencyType != null)
                {
                    descriptor.Dependencies.Add(dependencyAttribute.DependencyType);
                }
            }

            return descriptor;
        }

        private static void ApplyConfig(GameFrameXRuntimeComponentDescriptor descriptor, GameFrameXComponentRuntimeConfig config)
        {
            descriptor.Config = config;
            if (config.Enabled.HasValue)
            {
                descriptor.Enabled = config.Enabled.Value;
            }

            if (config.Order.HasValue)
            {
                descriptor.Order = config.Order.Value;
            }

            AddDependencies(descriptor, config.Dependencies);
        }

        private static void AddDependencies(GameFrameXRuntimeComponentDescriptor descriptor, IEnumerable<Type> dependencies)
        {
            if (dependencies == null)
            {
                return;
            }

            foreach (var dependency in dependencies)
            {
                if (dependency != null && !descriptor.Dependencies.Contains(dependency))
                {
                    descriptor.Dependencies.Add(dependency);
                }
            }
        }

        private static List<GameFrameXRuntimeComponentDescriptor> SortByDependency(IEnumerable<GameFrameXRuntimeComponentDescriptor> descriptors, IList<string> diagnostics)
        {
            var pending = new List<GameFrameXRuntimeComponentDescriptor>(descriptors);
            pending.Sort(CompareComponentDescriptor);
            var result = new List<GameFrameXRuntimeComponentDescriptor>(pending.Count);

            while (pending.Count > 0)
            {
                bool progressed = false;
                for (int i = 0; i < pending.Count; i++)
                {
                    if (DependenciesSatisfied(pending[i], result, pending))
                    {
                        result.Add(pending[i]);
                        pending.RemoveAt(i);
                        progressed = true;
                        break;
                    }
                }

                if (!progressed)
                {
                    if (diagnostics != null)
                    {
                        diagnostics.Add("Component dependency cycle detected. Falling back to order-only startup for remaining components.");
                    }

                    pending.Sort(CompareComponentDescriptor);
                    result.AddRange(pending);
                    pending.Clear();
                }
            }

            return result;
        }

        private static bool DependenciesSatisfied(GameFrameXRuntimeComponentDescriptor descriptor, List<GameFrameXRuntimeComponentDescriptor> resolved, List<GameFrameXRuntimeComponentDescriptor> pending)
        {
            foreach (var dependency in descriptor.Dependencies)
            {
                if (!ContainsAssignable(pending, dependency))
                {
                    continue;
                }

                if (!ContainsAssignable(resolved, dependency))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ContainsAssignable(IEnumerable<GameFrameXRuntimeComponentDescriptor> descriptors, Type type)
        {
            foreach (var descriptor in descriptors)
            {
                if (type.IsAssignableFrom(descriptor.ComponentType))
                {
                    return true;
                }
            }

            return false;
        }

        private static int CompareComponentDescriptor(GameFrameXRuntimeComponentDescriptor x, GameFrameXRuntimeComponentDescriptor y)
        {
            int order = x.Order.CompareTo(y.Order);
            if (order != 0)
            {
                return order;
            }

            return string.Compare(x.ComponentType.FullName, y.ComponentType.FullName, StringComparison.Ordinal);
        }
    }
}
