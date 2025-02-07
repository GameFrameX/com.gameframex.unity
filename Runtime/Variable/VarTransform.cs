//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// UnityEngine.Transform 变量类。
    /// </summary>
    [Preserve]
    public sealed class VarTransform : Variable<Transform>
    {
        /// <summary>
        /// 初始化 UnityEngine.Transform 变量类的新实例。
        /// </summary>
        [Preserve]
        public VarTransform()
        {
        }

        /// <summary>
        /// 从 UnityEngine.Transform 到 UnityEngine.Transform 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator VarTransform(Transform value)
        {
            VarTransform varValue = ReferencePool.Acquire<VarTransform>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 UnityEngine.Transform 变量类到 UnityEngine.Transform 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator Transform(VarTransform value)
        {
            return value.Value;
        }
    }
}
