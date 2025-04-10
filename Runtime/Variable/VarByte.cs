﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.Byte 变量类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class VarByte : Variable<byte>
    {
        /// <summary>
        /// 初始化 System.Byte 变量类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public VarByte()
        {
        }

        /// <summary>
        /// 从 System.Byte 到 System.Byte 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator VarByte(byte value)
        {
            VarByte varValue = ReferencePool.Acquire<VarByte>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.Byte 变量类到 System.Byte 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator byte(VarByte value)
        {
            return value.Value;
        }
    }
}
