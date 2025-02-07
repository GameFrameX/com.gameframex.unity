//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.Int16 变量类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class VarInt16 : Variable<short>
    {
        /// <summary>
        /// 初始化 System.Int16 变量类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public VarInt16()
        {
        }

        /// <summary>
        /// 从 System.Int16 到 System.Int16 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator VarInt16(short value)
        {
            VarInt16 varValue = ReferencePool.Acquire<VarInt16>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.Int16 变量类到 System.Int16 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator short(VarInt16 value)
        {
            return value.Value;
        }
    }
}
