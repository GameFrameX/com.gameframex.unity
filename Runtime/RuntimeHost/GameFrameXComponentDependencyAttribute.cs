using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 声明自动组件的运行时依赖。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXComponentDependencyAttribute : Attribute
    {
        /// <summary>
        /// 初始化依赖声明。
        /// </summary>
        /// <param name="dependencyType">必须先挂载的组件类型。</param>
        public GameFrameXComponentDependencyAttribute(Type dependencyType)
        {
            DependencyType = dependencyType;
        }

        /// <summary>
        /// 获取必须先挂载的组件类型。
        /// </summary>
        public Type DependencyType { get; private set; }
    }
}
