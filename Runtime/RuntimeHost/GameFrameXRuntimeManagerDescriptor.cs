using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 自动运行时中的 Manager 实现描述。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXRuntimeManagerDescriptor
    {
        /// <summary>
        /// 初始化 Manager 描述。
        /// </summary>
        /// <param name="interfaceType">Manager 接口类型。</param>
        /// <param name="implementationType">Manager 实现类型。</param>
        /// <param name="priority">选择优先级。</param>
        public GameFrameXRuntimeManagerDescriptor(Type interfaceType, Type implementationType, int priority)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
            Priority = priority;
        }

        /// <summary>
        /// 获取 Manager 接口类型。
        /// </summary>
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// 获取 Manager 实现类型。
        /// </summary>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// 获取选择优先级。
        /// </summary>
        public int Priority { get; private set; }
    }
}
