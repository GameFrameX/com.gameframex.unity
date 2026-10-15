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
//  Any legal disputes and liabilities arising from secondary development based on this project
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

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 低内存感知游戏框架模块接口。
    /// </summary>
    /// <remarks>
    /// 声明需要在系统低内存时释放缓存的模块实现本接口；<see cref="GameFrameworkEntry.NotifyLowMemory"/> 仅对实现本接口的模块分发低内存通知，未实现者被跳过。
    /// 触发源唯一：<c>Application.lowMemory</c> → <see cref="BaseComponent"/> 的钩子 → <see cref="GameFrameworkEntry.NotifyLowMemory"/>。
    /// <see cref="OnLowMemory"/> 仅由宿主入口调用，业务代码不得直接调用；业务侧一律通过 <c>GetModule&lt;IXxxManager&gt;()</c> 访问模块。
    /// <para>
    /// English: Implement this interface on modules that should release caches on system low memory;
    /// <see cref="GameFrameworkEntry.NotifyLowMemory"/> dispatches the notification only to implementing modules.
    /// The trigger source is unique: <c>Application.lowMemory</c> → the <see cref="BaseComponent"/> hook → <see cref="GameFrameworkEntry.NotifyLowMemory"/>.
    /// <see cref="OnLowMemory"/> is called only by the host entry; business code must access modules via <c>GetModule&lt;IXxxManager&gt;()</c>.
    /// </para>
    /// </remarks>
    public interface ILowMemoryAwareModule
    {
        /// <summary>
        /// 通知游戏框架模块系统低内存，应释放可回收资源。
        /// </summary>
        /// <remarks>
        /// Notifies the game framework module of system low memory so it can release reclaimable resources.
        /// </remarks>
        void OnLowMemory();
    }
}
