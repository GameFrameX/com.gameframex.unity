using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时扫描结果。
    /// </summary>
    /// <remarks>
    /// Scan result of the GameFrameX auto runtime.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXRuntimeScanResult
    {
        private readonly List<Type> m_ComponentTypes = new List<Type>();
        private readonly List<GameFrameXRuntimeManagerDescriptor> m_ManagerDescriptors =
            new List<GameFrameXRuntimeManagerDescriptor>();
        private readonly List<Type> m_OptionsContributorTypes = new List<Type>();
        private readonly List<Type> m_OverrideProviderTypes = new List<Type>();

        private readonly List<string> m_Errors = new List<string>();

        /// <summary>
        /// 获取扫描到的组件类型。
        /// </summary>
        /// <remarks>
        /// Gets the discovered component types.
        /// </remarks>
        public IList<Type> ComponentTypes
        {
            get { return m_ComponentTypes; }
        }

        /// <summary>
        /// 获取扫描到的 Manager 描述。
        /// </summary>
        /// <remarks>
        /// Gets the discovered manager descriptors.
        /// </remarks>
        public IList<GameFrameXRuntimeManagerDescriptor> ManagerDescriptors
        {
            get { return m_ManagerDescriptors; }
        }
        /// <summary>
        /// 获取扫描到的 Options 贡献者类型。
        /// </summary>
        /// <remarks>
        /// Gets the discovered options contributor types.
        /// </remarks>
        public IList<Type> OptionsContributorTypes
        {
            get { return m_OptionsContributorTypes; }
        }

        /// <summary>
        /// 获取扫描到的运行时覆盖 Provider 类型。
        /// </summary>
        /// <remarks>
        /// Gets the discovered runtime override provider types.
        /// </remarks>
        public IList<Type> OverrideProviderTypes
        {
            get { return m_OverrideProviderTypes; }
        }

        /// <summary>
        /// 获取扫描错误。
        /// </summary>
        /// <remarks>
        /// Gets the scan errors.
        /// </remarks>
        public IList<string> Errors
        {
            get { return m_Errors; }
        }
    }
}
