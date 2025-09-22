// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public abstract class GameFrameworkComponent : MonoBehaviour
    {
        /// <summary>
        /// 是否自动注册
        /// </summary>
        protected bool IsAutoRegister { get; set; } = true;

        /// <summary>
        /// 实现类的类型
        /// </summary>
        protected Type ImplementationComponentType = null;

        /// <summary>
        /// 接口类的类型
        /// </summary>
        protected Type InterfaceComponentType = null;

        /// <summary>
        /// 游戏框架组件类型。
        /// </summary>
        [SerializeField] protected string componentType = string.Empty;

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected virtual void Awake()
        {
            GameEntry.RegisterComponent(this);
            if (IsAutoRegister)
            {
                GameFrameworkGuard.NotNull(ImplementationComponentType, nameof(ImplementationComponentType));
                GameFrameworkGuard.NotNull(InterfaceComponentType, nameof(InterfaceComponentType));
                GameFrameworkEntry.RegisterModule(InterfaceComponentType, ImplementationComponentType);
            }
        }
    }
}