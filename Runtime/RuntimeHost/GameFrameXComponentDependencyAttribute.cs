using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 声明自动组件的运行时依赖。
    /// </summary>
    /// <remarks>
    /// Declares a runtime dependency of an auto component.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXComponentDependencyAttribute : Attribute
    {
        /// <summary>
        /// 初始化依赖声明。
        /// </summary>
        /// <remarks>
        /// Initializes the dependency declaration.
        /// </remarks>
        /// <param name="dependencyType">必须先挂载的组件类型。 / The component type that must be mounted first.</param>
        public GameFrameXComponentDependencyAttribute(Type dependencyType)
        {
            DependencyType = dependencyType;
        }

        /// <summary>
        /// 获取必须先挂载的组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the component type that must be mounted first.
        /// </remarks>
        public Type DependencyType { get; private set; }
    }
}
