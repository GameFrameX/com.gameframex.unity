using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标记组件不参与 GameFrameX 自动运行时挂载。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    [Preserve]
    public sealed class GameFrameXManualStartupAttribute : Attribute
    {
    }
}
