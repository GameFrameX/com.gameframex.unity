using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 自动运行时覆盖上下文。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXRuntimeOverrideContext
    {
        private readonly Dictionary<Type, GameFrameXComponentRuntimeConfig> m_ComponentConfigs = new Dictionary<Type, GameFrameXComponentRuntimeConfig>(32);

        private readonly List<GameFrameXManagerRuntimeOverride> m_ManagerOverrides = new List<GameFrameXManagerRuntimeOverride>(32);

        /// <summary>
        /// 设置组件是否启用。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <param name="enabled">是否启用。</param>
        public void SetComponentEnabled(Type componentType, bool enabled)
        {
            GetOrCreateComponentConfig(componentType).Enabled = enabled;
        }

        /// <summary>
        /// 设置组件启动顺序。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <param name="order">启动顺序。</param>
        public void SetComponentOrder(Type componentType, int order)
        {
            GetOrCreateComponentConfig(componentType).Order = order;
        }

        /// <summary>
        /// 设置组件 Manager 实现。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <param name="managerImplementationType">Manager 实现类型。</param>
        public void SetComponentManager(Type componentType, Type managerImplementationType)
        {
            GetOrCreateComponentConfig(componentType).ManagerImplementationType = managerImplementationType;
        }

        /// <summary>
        /// 设置组件 Inspector 字段或公开属性的运行时覆盖值。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <param name="name">字段名或属性名。</param>
        /// <param name="value">覆盖值。</param>
        public void SetComponentValue(Type componentType, string name, object value)
        {
            GetOrCreateComponentConfig(componentType).SetValue(name, value);
        }

        /// <summary>
        /// 设置 Manager 接口到实现类型的运行时覆盖。
        /// </summary>
        /// <param name="interfaceType">Manager 接口类型。</param>
        /// <param name="implementationType">Manager 实现类型。</param>
        /// <param name="priority">覆盖优先级。</param>
        public void SetManagerImplementation(Type interfaceType, Type implementationType, int priority = 0)
        {
            m_ManagerOverrides.Add(new GameFrameXManagerRuntimeOverride(interfaceType, implementationType, priority));
        }

        /// <summary>
        /// 获取或创建组件运行时配置。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        /// <returns>组件运行时配置。</returns>
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
        /// <returns>组件配置映射。</returns>
        public IReadOnlyDictionary<Type, GameFrameXComponentRuntimeConfig> GetComponentConfigs()
        {
            return m_ComponentConfigs;
        }

        /// <summary>
        /// 获取所有 Manager 覆盖。
        /// </summary>
        /// <returns>Manager 覆盖列表。</returns>
        public IReadOnlyList<GameFrameXManagerRuntimeOverride> GetManagerOverrides()
        {
            return m_ManagerOverrides;
        }
    }
}
