// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents and other related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and other international regulations.
//
//  使用本项目须严格遵守相应法律法规与开源许可证之规定。
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
//  Any legal disputes or liabilities arising from secondary development based on this project
//  本组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB 仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标注"跳过启动期全量实例化"的模块（逃生口）。
    /// </summary>
    /// <remarks>
    /// 默认情况下，启动完成点（AutoPackage：AfterSceneLoad 模式裁决后；Headless：<see cref="GameFrameworkStartup.Headless"/> 调用内）
    /// 会按 Priority 全量实例化类型映射中的模块（安装即启用）。
    /// 仅当模块构造存在真实开销（重资源初始化、外部连接）或极少使用时，用本属性标注跳过 eager，
    /// 保留 <see cref="GameFrameworkEntry.GetModule(System.Type)"/> 的惰性创建兜底。
    /// 现有框架模块构造均为平凡初始化，不应使用本属性。
    /// <para>
    /// English: By default all registered modules are instantiated eagerly at the startup completion point. Mark a module with
    /// this attribute only when its constructor has real cost (heavy resource initialization, external connections) or is rarely
    /// used, keeping the lazy creation fallback of <see cref="GameFrameworkEntry.GetModule(System.Type)"/>. Existing framework
    /// modules all have trivial constructors and should not use this attribute.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    [Preserve]
    public sealed class GameFrameXLazyModuleAttribute : Attribute
    {
    }
}
