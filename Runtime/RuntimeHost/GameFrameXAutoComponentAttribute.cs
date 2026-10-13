using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标记组件可被 GameFrameX 自动运行时发现并挂载。
    /// </summary>
    /// <remarks>
    /// Marks a component so the GameFrameX auto runtime can discover and mount it.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXAutoComponentAttribute : Attribute
    {
        /// <summary>
        /// 初始化自动组件标记。
        /// </summary>
        /// <remarks>
        /// Initializes the auto component attribute.
        /// </remarks>
        /// <param name="order">启动顺序，数值越小越早挂载。 / Startup order; smaller values are mounted earlier.</param>
        /// <param name="dependencies">必须先挂载的组件类型。 / Component types that must be mounted first.</param>
        public GameFrameXAutoComponentAttribute(int order = 0, params Type[] dependencies)
        {
            Order = order;
            Dependencies = dependencies ?? Array.Empty<Type>();
            EnabledByDefault = true;
        }

        /// <summary>
        /// 获取启动顺序，数值越小越早挂载。
        /// </summary>
        /// <remarks>
        /// Gets the startup order; smaller values are mounted earlier.
        /// </remarks>
        public int Order { get; private set; }

        /// <summary>
        /// 获取依赖的组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the dependent component types.
        /// </remarks>
        public Type[] Dependencies { get; private set; }

        /// <summary>
        /// 获取或设置组件是否默认启用。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the component is enabled by default.
        /// </remarks>
        public bool EnabledByDefault { get; set; }
    }
}
