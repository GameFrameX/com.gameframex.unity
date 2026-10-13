using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标记组件不参与 GameFrameX 自动运行时挂载。
    /// </summary>
    /// <remarks>
    /// Marks a component as excluded from GameFrameX auto runtime mounting.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXManualStartupAttribute : Attribute
    {
    }
}
