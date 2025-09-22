// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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
            get { return typeof(T); }
        }

        /// <summary>
        /// 获取或设置变量值。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public T Value
        {
            get { return m_Value; }
            set { m_Value = value; }
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