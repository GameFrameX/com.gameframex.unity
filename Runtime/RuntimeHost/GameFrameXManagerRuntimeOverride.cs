using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 描述 Manager 接口到实现类型的运行时覆盖。
    /// </summary>
    /// <remarks>
    /// Describes a runtime override that maps a manager interface to an implementation type.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXManagerRuntimeOverride
    {
        /// <summary>
        /// 初始化 Manager 运行时覆盖。
        /// </summary>
        /// <remarks>
        /// Initializes the manager runtime override.
        /// </remarks>
        /// <param name="interfaceType">Manager 接口类型。 / The manager interface type.</param>
        /// <param name="implementationType">Manager 实现类型。 / The manager implementation type.</param>
        /// <param name="priority">覆盖优先级。 / The override priority.</param>
        public GameFrameXManagerRuntimeOverride(Type interfaceType, Type implementationType, int priority = 0)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
            Priority = priority;
        }

        /// <summary>
        /// 获取 Manager 接口类型。
        /// </summary>
        /// <remarks>
        /// Gets the manager interface type.
        /// </remarks>
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// 获取 Manager 实现类型。
        /// </summary>
        /// <remarks>
        /// Gets the manager implementation type.
        /// </remarks>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// 获取覆盖优先级。
        /// </summary>
        /// <remarks>
        /// Gets the override priority.
        /// </remarks>
        public int Priority { get; private set; }
    }
}
