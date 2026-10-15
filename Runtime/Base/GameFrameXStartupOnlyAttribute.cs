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

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标注 Options 中的"启动期型"参数。
    /// </summary>
    /// <remarks>
    /// 参数分两档：
    /// <list type="bullet">
    /// <item><b>实时型（默认，不标注）</b>：运行期可经模块公开 API 修改，组件活视图可写回。</item>
    /// <item><b>启动期型（本属性标注）</b>：仅在模块实例化时消费（如下载代理数量、Helper 实现类型名）；
    /// 运行期经模块 API 修改时模块侧应 <c>Log.Warning</c> 并拒绝（不静默失效），活视图中呈现为只读。</item>
    /// </list>
    /// 用于 Options 字段 / 属性；模块在消费 Options 时按本属性分流读写路径。
    /// <para>
    /// English: Marks a "startup-only" parameter in Options. Realtime parameters (the default) can be changed at runtime through
    /// the module's public API; startup-only parameters are consumed only at module instantiation (e.g. download agent count,
    /// helper implementation type names) — runtime modification is rejected with a warning instead of silently failing, and live
    /// views render them read-only.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [UnityEngine.Scripting.Preserve]
    public sealed class GameFrameXStartupOnlyAttribute : Attribute
    {
    }
}
