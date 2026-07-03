using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 声明 GameFrameworkModule 对应的 Manager 接口和自动选择优先级。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXManagerImplementationAttribute : Attribute
    {
        /// <summary>
        /// 初始化 Manager 实现声明。
        /// </summary>
        /// <param name="interfaceType">Manager 接口类型。</param>
        /// <param name="priority">多实现选择优先级，数值越大优先级越高。</param>
        public GameFrameXManagerImplementationAttribute(Type interfaceType, int priority = 0)
        {
            InterfaceType = interfaceType;
            Priority = priority;
        }

        /// <summary>
        /// 获取 Manager 接口类型。
        /// </summary>
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// 获取多实现选择优先级，数值越大优先级越高。
        /// </summary>
        public int Priority { get; private set; }
    }
}
