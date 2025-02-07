//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.SByte 变量类。
    /// </summary>
    [Preserve]
    public sealed class VarSByte : Variable<sbyte>
    {
        /// <summary>
        /// 初始化 System.SByte 变量类的新实例。
        /// </summary>
        [Preserve]
        public VarSByte()
        {
        }

        /// <summary>
        /// 从 System.SByte 到 System.SByte 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator VarSByte(sbyte value)
        {
            VarSByte varValue = ReferencePool.Acquire<VarSByte>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.SByte 变量类到 System.SByte 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [Preserve]
        public static implicit operator sbyte(VarSByte value)
        {
            return value.Value;
        }
    }
}
