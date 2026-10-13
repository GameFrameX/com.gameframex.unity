using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 自动运行时中的组件描述。
    /// </summary>
    /// <remarks>
    /// Describes a component in the auto runtime.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXRuntimeComponentDescriptor
    {
        private readonly List<Type> m_Dependencies = new List<Type>(32);

        /// <summary>
        /// 初始化组件描述。
        /// </summary>
        /// <remarks>
        /// Initializes the component descriptor.
        /// </remarks>
        /// <param name="componentType">组件类型。 / The component type.</param>
        /// <param name="order">启动顺序。 / The startup order.</param>
        public GameFrameXRuntimeComponentDescriptor(Type componentType, int order)
        {
            ComponentType = componentType;
            Order = order;
            Enabled = true;
        }

        /// <summary>
        /// 获取组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the component type.
        /// </remarks>
        public Type ComponentType { get; private set; }

        /// <summary>
        /// 获取或设置启动顺序。
        /// </summary>
        /// <remarks>
        /// Gets or sets the startup order.
        /// </remarks>
        public int Order { get; set; }

        /// <summary>
        /// 获取或设置是否启用。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the component is enabled.
        /// </remarks>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置组件运行时配置。
        /// </summary>
        /// <remarks>
        /// Gets or sets the component runtime configuration.
        /// </remarks>
        public GameFrameXComponentRuntimeConfig Config { get; set; }

        /// <summary>
        /// 获取依赖组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the dependent component types.
        /// </remarks>
        public IList<Type> Dependencies
        {
            get { return m_Dependencies; }
        }
    }
}
