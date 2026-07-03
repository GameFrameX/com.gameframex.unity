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
        /// 获取组件本次 Awake 是否已完成运行时注册。
        /// </summary>
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
                    Log.Warning("Game Framework component '{0}' can not resolve manager. Component has been disabled.", GetType().FullName);
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
        /// <param name="config">组件运行时配置。</param>
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

            foreach (var pair in config.GetValues())
            {
                ApplyRuntimeConfigValue(pair.Key, pair.Value);
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

        private void ApplyRuntimeConfigValue(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var currentType = GetType();
            while (currentType != null && currentType != typeof(MonoBehaviour))
            {
                FieldInfo fieldInfo = currentType.GetField(name, flags);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(this, ConvertRuntimeConfigValue(value, fieldInfo.FieldType));
                    return;
                }

                PropertyInfo propertyInfo = currentType.GetProperty(name, flags);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, ConvertRuntimeConfigValue(value, propertyInfo.PropertyType), null);
                    return;
                }

                currentType = currentType.BaseType;
            }
        }

        private static object ConvertRuntimeConfigValue(object value, Type targetType)
        {
            if (value == null || targetType == null)
            {
                return value;
            }

            var valueType = value.GetType();
            if (targetType.IsAssignableFrom(valueType))
            {
                return value;
            }

            if (targetType.IsEnum)
            {
                if (value is string enumName)
                {
                    return Enum.Parse(targetType, enumName);
                }

                return Enum.ToObject(targetType, value);
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}
