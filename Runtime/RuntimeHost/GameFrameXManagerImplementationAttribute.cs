using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 声明 IGameFrameworkModule 对应的 Manager 接口和自动选择优先级。
    /// </summary>
    /// <remarks>
    /// Declares the manager interface and auto-selection priority for an IGameFrameworkModule implementation.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [UnityEngine.Scripting.Preserve]
    public sealed class GameFrameXManagerImplementationAttribute : Attribute
    {
        /// <summary>
        /// 初始化 Manager 实现声明。
        /// </summary>
        /// <remarks>
        /// Initializes the manager implementation declaration.
        /// </remarks>
        /// <param name="interfaceType">Manager 接口类型。 / The manager interface type.</param>
        /// <param name="priority">多实现选择优先级，数值越大优先级越高。 / Selection priority among multiple implementations; larger values win.</param>
        [UnityEngine.Scripting.Preserve]
        public GameFrameXManagerImplementationAttribute(Type interfaceType, int priority = 0)
        {
            InterfaceType = interfaceType;
            Priority = priority;
        }

        /// <summary>
        /// 获取 Manager 接口类型。
        /// </summary>
        /// <remarks>
        /// Gets the manager interface type.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// 获取多实现选择优先级，数值越大优先级越高。
        /// </summary>
        /// <remarks>
        /// Gets the selection priority among multiple implementations; larger values win.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int Priority { get; private set; }
    }
}
