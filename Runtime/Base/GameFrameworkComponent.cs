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
using System.Reflection;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    /// <remarks>
    /// Abstract base class for game framework components.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public abstract class GameFrameworkComponent : MonoBehaviour, IGameFrameXComponentRuntimeConfigurable
    {
        /// <summary>
        /// 是否自动注册。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to automatically register.
        /// </remarks>
        /// <value>如果自动注册则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if auto register; otherwise <c>false</c></value>
        protected bool IsAutoRegister { get; set; } = true;

        /// <summary>
        /// 实现类的类型。
        /// </summary>
        /// <remarks>
        /// The type of the implementation class.
        /// </remarks>
        protected Type ImplementationComponentType = null;

        /// <summary>
        /// 接口类的类型。
        /// </summary>
        /// <remarks>
        /// The type of the interface class.
        /// </remarks>
        protected Type InterfaceComponentType = null;

        /// <summary>
        /// 游戏框架组件类型。
        /// </summary>
        /// <remarks>
        /// The type of the game framework component.
        /// </remarks>
        [SerializeField, HideInInspector] protected string componentType = string.Empty;

        /// <summary>
        /// 是否已应用组件运行时配置。
        /// </summary>
        private bool m_RuntimeConfigApplied;

        /// <summary>
        /// 本组件是否因自动注入接管而被抑制（场景残留入口）。
        /// </summary>
        /// <remarks>
        /// Gets whether this component was suppressed because auto-injection owns all GameFrameX components.
        /// </remarks>
        protected bool m_SuppressedByAutoHost;

        /// <summary>
        /// 获取组件本次 Awake 是否已完成运行时注册。
        /// </summary>
        /// <remarks>
        /// Gets whether the component has completed runtime registration during this Awake.
        /// </remarks>
        protected bool IsRuntimeComponentReady { get; private set; }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        /// <remarks>
        /// Initializes the game framework component.
        /// </remarks>
        protected virtual void Awake()
        {
            IsRuntimeComponentReady = false;

            // 残留抑制：自动注入已宣告接管时，场景里手工摆放的 GFX 组件一律退场。
            // 判据由 Bootstrap 掌握（BeforeSceneLoad 置位、Host 创建前清除），不依赖 Awake 与 Host 创建的执行顺序。
            // Suppression: while auto-injection ownership is claimed, hand-placed scene components stand down.
            // The claim is owned by the bootstrap, so this never depends on Awake/Host-creation ordering.
            if (GameFrameXRuntimeHost.IsAutoInjectionClaimed)
            {
                m_SuppressedByAutoHost = true;
                GameFrameXRuntimeHost.SuppressSceneEntry(this);
                return;
            }

            ApplyPendingRuntimeConfig();
            if (!GameEntry.RegisterComponent(this))
            {
                enabled = false;
                return;
            }

            if (IsAutoRegister)
            {
                if (ImplementationComponentType == null && InterfaceComponentType != null)
                {
                    ImplementationComponentType = GameFrameXRuntimeManagerResolver.Resolve(InterfaceComponentType, componentType);
                    if (ImplementationComponentType != null && string.IsNullOrEmpty(componentType))
                    {
                        componentType = ImplementationComponentType.FullName;
                    }
                }

                if (InterfaceComponentType == null || ImplementationComponentType == null)
                {
                    Log.Warning("GameFrameX component '{0}' can not resolve manager. Component has been disabled.", GetType().FullName);
                    enabled = false;
                    return;
                }

                GameFrameworkEntry.RegisterModule(InterfaceComponentType, ImplementationComponentType);
            }

            IsRuntimeComponentReady = true;
        }

        /// <summary>
        /// 应用组件运行时配置覆盖。
        /// </summary>
        /// <remarks>
        /// Applies the component runtime configuration overrides.
        /// </remarks>
        /// <param name="config">组件运行时配置。 / The component runtime configuration.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void ApplyRuntimeConfig(GameFrameXComponentRuntimeConfig config)
        {
            if (config == null)
            {
                return;
            }

            if (config.ManagerImplementationType != null)
            {
                componentType = config.ManagerImplementationType.FullName;
            }
            else if (!string.IsNullOrEmpty(config.ManagerImplementationTypeName))
            {
                componentType = config.ManagerImplementationTypeName;
            }

            m_RuntimeConfigApplied = true;
        }

        private void ApplyPendingRuntimeConfig()
        {
            if (m_RuntimeConfigApplied)
            {
                return;
            }

            ApplyRuntimeConfig(GameFrameXRuntimeHost.GetPendingConfig(GetType()));
        }


    }
}
