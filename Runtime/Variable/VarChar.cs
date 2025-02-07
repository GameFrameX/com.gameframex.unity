//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.Char 变量类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class VarChar : Variable<char>
    {
        /// <summary>
        /// 初始化 System.Char 变量类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public VarChar()
        {
        }

        /// <summary>
        /// 从 System.Char 到 System.Char 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator VarChar(char value)
        {
            VarChar varValue = ReferencePool.Acquire<VarChar>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.Char 变量类到 System.Char 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator char(VarChar value)
        {
            return value.Value;
        }
    }
}
