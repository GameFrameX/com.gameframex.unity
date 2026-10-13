using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 描述组件在自动运行时中的覆盖配置。
    /// </summary>
    /// <remarks>
    /// Describes the override configuration of a component in the auto runtime.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXComponentRuntimeConfig
    {
        private readonly List<Type> m_Dependencies = new List<Type>();

        /// <summary>
        /// 初始化组件运行时配置。
        /// </summary>
        /// <remarks>
        /// Initializes the component runtime configuration.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        public GameFrameXComponentRuntimeConfig(Type componentType)
        {
            ComponentType = componentType;
        }

        /// <summary>
        /// 获取组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the component type.
        /// </remarks>
        public Type ComponentType { get; private set; }

        /// <summary>
        /// 获取或设置是否启用组件。为空时使用自动发现结果。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the component is enabled. When null, the auto discovery result is used.
        /// </remarks>
        public bool? Enabled { get; set; }

        /// <summary>
        /// 获取或设置组件挂载顺序。为空时使用 Attribute 或默认顺序。
        /// </summary>
        /// <remarks>
        /// Gets or sets the component mount order. When null, the attribute or default order is used.
        /// </remarks>
        public int? Order { get; set; }

        /// <summary>
        /// 获取或设置 Manager 实现类型。
        /// </summary>
        /// <remarks>
        /// Gets or sets the manager implementation type.
        /// </remarks>
        public Type ManagerImplementationType { get; set; }

        /// <summary>
        /// 获取或设置 Manager 实现类型名称。
        /// </summary>
        /// <remarks>
        /// Gets or sets the manager implementation type name.
        /// </remarks>
        public string ManagerImplementationTypeName { get; set; }

        /// <summary>
        /// 获取依赖组件类型列表。
        /// </summary>
        /// <remarks>
        /// Gets the list of dependent component types.
        /// </remarks>
        public IList<Type> Dependencies
        {
            get { return m_Dependencies; }
        }



    }
}
