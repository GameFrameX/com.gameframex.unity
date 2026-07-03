using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 描述 Manager 接口到实现类型的运行时覆盖。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXManagerRuntimeOverride
    {
        /// <summary>
        /// 初始化 Manager 运行时覆盖。
        /// </summary>
        /// <param name="interfaceType">Manager 接口类型。</param>
        /// <param name="implementationType">Manager 实现类型。</param>
        /// <param name="priority">覆盖优先级。</param>
        public GameFrameXManagerRuntimeOverride(Type interfaceType, Type implementationType, int priority = 0)
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
        /// 获取覆盖优先级。
        /// </summary>
        public int Priority { get; private set; }
    }
}
