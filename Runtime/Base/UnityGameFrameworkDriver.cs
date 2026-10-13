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
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 默认 Unity MonoBehaviour 驱动组件。
    /// </summary>
    /// <remarks>
    /// 供独立 Unity 项目或不想自写驱动的宿主使用，配合 <see cref="GameFrameworkStartup.Headless"/> 组成默认驱动路径：
    /// <list type="number">
    /// <item>宿主在首个场景加载前调用 <c>GameFrameworkStartup.Headless()</c> 完成启动；本组件不负责启动。</item>
    /// <item>宿主把本组件挂到自己的常驻 GameObject 上（Headless 模式下禁止挂 GameFrameX 业务组件，本驱动组件除外）。</item>
    /// <item><c>Update</c> 驱动 <see cref="GameFrameworkEntry.Update(float,float)"/>；<c>Application.lowMemory</c> 转发 <see cref="GameFrameworkEntry.NotifyLowMemory"/>；销毁时自动调用 <see cref="GameFrameworkEntry.Shutdown"/>（三行契约的第三行由此兜底）。</item>
    /// </list>
    /// 单实例约束：全场景只挂一个；组件模式（BaseComponent 已驱动）下检测到 BaseComponent 会自动禁用并告警，无需也不得使用本组件。
    /// <para>
    /// English: Default Unity MonoBehaviour driver for standalone Unity projects or hosts that do not want to write their own
    /// driver, paired with <see cref="GameFrameworkStartup.Headless"/>: the host calls Headless() before the first scene load,
    /// mounts this component on a persistent GameObject, and Update / lowMemory / OnDestroy forward to
    /// <see cref="GameFrameworkEntry.Update(float,float)"/>, <see cref="GameFrameworkEntry.NotifyLowMemory"/> and
    /// <see cref="GameFrameworkEntry.Shutdown"/> respectively. Only one instance is allowed per scene; in component mode
    /// (BaseComponent already driving) it disables itself with a warning.
    /// </para>
    /// </remarks>
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/Unity Game Framework Driver")]
    [UnityEngine.Scripting.Preserve]
    [DefaultExecutionOrder(-500)]
    public sealed class UnityGameFrameworkDriver : MonoBehaviour
    {
        private bool m_Active;

        private void Awake()
        {
            if (FindObjectOfType<BaseComponent>() != null)
            {
                // 组件模式（BaseComponent 已驱动 Entry）误挂本驱动时退场，避免双订阅 / 双驱动 / 双 Shutdown。
                enabled = false;
                Log.Warning("UnityGameFrameworkDriver was disabled because a BaseComponent is already driving GameFrameX. Do not use the driver in component mode.");
                return;
            }

            m_Active = true;
            Application.lowMemory += OnLowMemory;
        }

        private void Update()
        {
            GameFrameworkEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnDestroy()
        {
            if (!m_Active)
            {
                return;
            }

            Application.lowMemory -= OnLowMemory;
            GameFrameworkEntry.Shutdown();
        }

        private void OnLowMemory()
        {
            GameFrameworkEntry.NotifyLowMemory();
        }
    }
}
