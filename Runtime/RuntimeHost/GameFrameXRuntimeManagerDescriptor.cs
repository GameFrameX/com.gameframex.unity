using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 自动运行时中的 Manager 实现描述。
    /// </summary>
    /// <remarks>
    /// Describes a manager implementation in the auto runtime.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXRuntimeManagerDescriptor
    {
        /// <summary>
        /// 初始化 Manager 描述。
        /// </summary>
        /// <remarks>
        /// Initializes the manager descriptor.
        /// </remarks>
        /// <param name="interfaceType">Manager 接口类型。 / The manager interface type.</param>
        /// <param name="implementationType">Manager 实现类型。 / The manager implementation type.</param>
        /// <param name="priority">选择优先级。 / The selection priority.</param>
        public GameFrameXRuntimeManagerDescriptor(Type interfaceType, Type implementationType, int priority)
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
        /// 获取选择优先级。
        /// </summary>
        /// <remarks>
        /// Gets the selection priority.
        /// </remarks>
        public int Priority { get; private set; }
    }
}
