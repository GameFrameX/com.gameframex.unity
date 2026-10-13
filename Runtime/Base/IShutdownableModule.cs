// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
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

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 可关闭游戏框架模块接口。
    /// </summary>
    /// <remarks>
    /// 声明需要清理资源的模块实现本接口；<see cref="GameFrameworkEntry.Shutdown"/> 按优先级反序对实现本接口的模块分发关闭，未实现者被跳过。
    /// 实现必须可重入：重启（Restart）场景下模块被再次创建后可能再次收到关闭调用，清理逻辑不得依赖单次执行假设。
    /// 本方法仅由宿主入口调用，业务代码不得直接调用。
    /// <para>
    /// English: Implement this interface on modules that need resource cleanup; <see cref="GameFrameworkEntry.Shutdown"/>
    /// dispatches shutdown to implementing modules in reverse priority order. Implementations must be reentrant:
    /// after a Restart the module may be created again and receive shutdown again. Called only by the host entry.
    /// </para>
    /// </remarks>
    public interface IShutdownableModule
    {
        /// <summary>
        /// 关闭并清理游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Shuts down and cleans up the game framework module.
        /// </remarks>
        void Shutdown();
    }
}
