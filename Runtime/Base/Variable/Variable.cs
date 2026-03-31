// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
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
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 变量。
    /// </summary>
    /// <remarks>
    /// Abstract base class for variables that supports reference counting.
    /// Provides a type-agnostic interface for getting and setting variable values.
    /// </remarks>
    public abstract class Variable : IReference
    {
        /// <summary>
        /// 初始化变量的新实例。
        /// </summary>
        /// <remarks>
        /// Default constructor for the Variable class.
        /// </remarks>
        public Variable()
        {
        }

        /// <summary>
        /// 获取变量类型。
        /// </summary>
        /// <remarks>
        /// Gets the runtime type of the variable's value.
        /// </remarks>
        public abstract Type Type { get; }

        /// <summary>
        /// 获取变量值。
        /// </summary>
        /// <returns>变量值。 / The variable value.</returns>
        public abstract object GetValue();

        /// <summary>
        /// 设置变量值。
        /// </summary>
        /// <param name="value">变量值。 / The variable value to set.</param>
        public abstract void SetValue(object value);

        /// <summary>
        /// 清理变量值。
        /// </summary>
        /// <remarks>
        /// Resets the variable to its default state, typically used when returning the object to a pool.
        /// </remarks>
        public abstract void Clear();
    }
}