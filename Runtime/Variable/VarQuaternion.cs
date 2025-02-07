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
    /// UnityEngine.Quaternion 变量类。
    /// </summary>
    [Preserve]
    public sealed class VarQuaternion : Variable<Quaternion>
    {
        /// <summary>
        /// 初始化 UnityEngine.Quaternion 变量类的新实例。
        /// </summary>
        [Preserve]
        public VarQuaternion()
        {
        }

        /// <summary>
        /// 从 UnityEngine.Quaternion 到 UnityEngine.Quaternion 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator VarQuaternion(Quaternion value)
        {
            VarQuaternion varValue = ReferencePool.Acquire<VarQuaternion>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 UnityEngine.Quaternion 变量类到 UnityEngine.Quaternion 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator Quaternion(VarQuaternion value)
        {
            return value.Value;
        }
    }
}
