// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 变量。
    /// </summary>
    /// <typeparam name="T">变量类型。 / The type of the variable.</typeparam>
    /// <remarks>
    /// Generic implementation of Variable that provides type-safe access to variable values.
    /// This abstract class serves as a base for creating strongly-typed variables.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public abstract class Variable<T> : Variable
    {
        private T _value;

        /// <summary>
        /// 初始化变量的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes the variable with the default value of type T.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public Variable()
        {
            _value = default(T);
        }

        /// <summary>
        /// 获取变量类型。
        /// </summary>
        /// <remarks>
        /// Returns the runtime type of the generic type parameter T.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public override Type Type
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 获取或设置变量值。
        /// </summary>
        /// <remarks>
        /// Provides type-safe access to the variable's value.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 获取变量值。
        /// </summary>
        /// <returns>变量值。 / The variable value.</returns>
        [UnityEngine.Scripting.Preserve]
        public override object GetValue()
        {
            return _value;
        }

        /// <summary>
        /// 设置变量值。
        /// </summary>
        /// <param name="value">变量值。 / The variable value to set.</param>
        [UnityEngine.Scripting.Preserve]
        public override void SetValue(object value)
        {
            if (value != null && !(value is T))
            {
                throw new GameFrameworkException(GameFrameworkText.Format("Cannot set value of type '{0}' to variable of type '{1}'.", value.GetType().FullName, typeof(T).FullName));
            }

            _value = (value == null) ? default(T) : (T)value;
        }

        /// <summary>
        /// 清理变量值。
        /// </summary>
        /// <remarks>
        /// Resets the variable to its default value, typically used when returning the object to a pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public override void Clear()
        {
            _value = default(T);
        }

        /// <summary>
        /// 获取变量字符串。
        /// </summary>
        /// <returns>变量字符串。 / The string representation of the variable value.</returns>
        [UnityEngine.Scripting.Preserve]
        public override string ToString()
        {
            return (_value != null) ? _value.ToString() : "<Null>";
        }
    }
}