using System;
using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 自动运行时覆盖上下文。
    /// </summary>
    /// <remarks>
    /// Override context for the auto runtime.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class GameFrameXRuntimeOverrideContext
    {
        private readonly Dictionary<Type, GameFrameXComponentRuntimeConfig> m_ComponentConfigs = new Dictionary<Type, GameFrameXComponentRuntimeConfig>(32);

        private readonly List<GameFrameXManagerRuntimeOverride> m_ManagerOverrides = new List<GameFrameXManagerRuntimeOverride>(32);

        /// <summary>
        /// 设置组件是否启用。
        /// </summary>
        /// <remarks>
        /// Sets whether a component is enabled.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <param name="enabled">是否启用。 / Whether the component is enabled.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetComponentEnabled(Type componentType, bool enabled)
        {
            GetOrCreateComponentConfig(componentType).Enabled = enabled;
        }

        /// <summary>
        /// 设置组件启动顺序。
        /// </summary>
        /// <remarks>
        /// Sets the startup order of a component.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <param name="order">启动顺序。 / The startup order.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetComponentOrder(Type componentType, int order)
        {
            GetOrCreateComponentConfig(componentType).Order = order;
        }

        /// <summary>
        /// 设置组件 Manager 实现。
        /// </summary>
        /// <remarks>
        /// Sets the manager implementation of a component.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <param name="managerImplementationType">Manager 实现类型。 / The manager implementation type.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetComponentManager(Type componentType, Type managerImplementationType)
        {
            GetOrCreateComponentConfig(componentType).ManagerImplementationType = managerImplementationType;
        }


        /// <summary>
        /// 设置 Manager 接口到实现类型的运行时覆盖。
        /// </summary>
        /// <remarks>
        /// Sets a runtime override that maps a manager interface to an implementation type.
        /// </remarks>
        /// <param name="interfaceType">Manager 接口类型。 / The manager interface type.</param>
        /// <param name="implementationType">Manager 实现类型。 / The manager implementation type.</param>
        /// <param name="priority">覆盖优先级。 / The override priority.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetManagerImplementation(Type interfaceType, Type implementationType, int priority = 0)
        {
            m_ManagerOverrides.Add(new GameFrameXManagerRuntimeOverride(interfaceType, implementationType, priority));
        }

        /// <summary>
        /// 获取或创建组件运行时配置。
        /// </summary>
        /// <remarks>
        /// Gets or creates the runtime configuration of a component.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <returns>组件运行时配置。 / The component runtime configuration.</returns>
        [UnityEngine.Scripting.Preserve]
        public GameFrameXComponentRuntimeConfig GetOrCreateComponentConfig(Type componentType)
        {
            if (componentType == null)
            {
                throw new GameFrameworkException("Component type is invalid.");
            }

            if (!m_ComponentConfigs.TryGetValue(componentType, out var config))
            {
                config = new GameFrameXComponentRuntimeConfig(componentType);
                m_ComponentConfigs.Add(componentType, config);
            }

            return config;
        }

        /// <summary>
        /// 获取所有组件配置。
        /// </summary>
        /// <remarks>
        /// Gets all component configurations.
        /// </remarks>
        /// <returns>组件配置映射。 / The mapping of component configurations.</returns>
        [UnityEngine.Scripting.Preserve]
        public IReadOnlyDictionary<Type, GameFrameXComponentRuntimeConfig> GetComponentConfigs()
        {
            return m_ComponentConfigs;
        }

        /// <summary>
        /// 获取所有 Manager 覆盖。
        /// </summary>
        /// <remarks>
        /// Gets all manager overrides.
        /// </remarks>
        /// <returns>Manager 覆盖列表。 / The list of manager overrides.</returns>
        [UnityEngine.Scripting.Preserve]
        public IReadOnlyList<GameFrameXManagerRuntimeOverride> GetManagerOverrides()
        {
            return m_ManagerOverrides;
        }
    }
}
