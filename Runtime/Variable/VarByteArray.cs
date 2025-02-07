﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.Byte 数组变量类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class VarByteArray : Variable<byte[]>
    {
        /// <summary>
        /// 初始化 System.Byte 数组变量类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public VarByteArray()
        {
        }

        /// <summary>
        /// 从 System.Byte 数组到 System.Byte 数组变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator VarByteArray(byte[] value)
        {
            VarByteArray varValue = ReferencePool.Acquire<VarByteArray>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.Byte 数组变量类到 System.Byte 数组的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator byte[](VarByteArray value)
        {
            return value.Value;
        }
    }
}
