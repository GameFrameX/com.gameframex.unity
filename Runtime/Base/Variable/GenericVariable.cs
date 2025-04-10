﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 变量。
    /// </summary>
    /// <typeparam name="T">变量类型。</typeparam>
    [UnityEngine.Scripting.Preserve]
    public abstract class Variable<T> : Variable
    {
        private T m_Value;

        /// <summary>
        /// 初始化变量的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public Variable()
        {
            m_Value = default(T);
        }

        /// <summary>
        /// 获取变量类型。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// 获取或设置变量值。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        /// <summary>
        /// 获取变量值。
        /// </summary>
        /// <returns>变量值。</returns>
        [UnityEngine.Scripting.Preserve]
        public override object GetValue()
        {
            return m_Value;
        }

        /// <summary>
        /// 设置变量值。
        /// </summary>
        /// <param name="value">变量值。</param>
        [UnityEngine.Scripting.Preserve]
        public override void SetValue(object value)
        {
            m_Value = (T)value;
        }

        /// <summary>
        /// 清理变量值。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public override void Clear()
        {
            m_Value = default(T);
        }

        /// <summary>
        /// 获取变量字符串。
        /// </summary>
        /// <returns>变量字符串。</returns>
        [UnityEngine.Scripting.Preserve]
        public override string ToString()
        {
            return (m_Value != null) ? m_Value.ToString() : "<Null>";
        }
    }
}
