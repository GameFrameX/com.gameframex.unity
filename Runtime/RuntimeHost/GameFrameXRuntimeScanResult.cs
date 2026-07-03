using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时扫描结果。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXRuntimeScanResult
    {
        private readonly List<Type> m_ComponentTypes = new List<Type>();
        private readonly List<GameFrameXRuntimeManagerDescriptor> m_ManagerDescriptors =
            new List<GameFrameXRuntimeManagerDescriptor>();

        private readonly List<string> m_Errors = new List<string>();

        /// <summary>
        /// 获取扫描到的组件类型。
        /// </summary>
        public IList<Type> ComponentTypes
        {
            get { return m_ComponentTypes; }
        }

        /// <summary>
        /// 获取扫描到的 Manager 描述。
        /// </summary>
        public IList<GameFrameXRuntimeManagerDescriptor> ManagerDescriptors
        {
            get { return m_ManagerDescriptors; }
        }

        /// <summary>
        /// 获取扫描错误。
        /// </summary>
        public IList<string> Errors
        {
            get { return m_Errors; }
        }
    }
}
