using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时启动计划。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXRuntimePlan
    {
        private readonly List<GameFrameXRuntimeComponentDescriptor> m_Components = new List<GameFrameXRuntimeComponentDescriptor>(32);

        private readonly List<string> m_Diagnostics = new List<string>(32);

        /// <summary>
        /// 获取计划挂载的组件。
        /// </summary>
        public IList<GameFrameXRuntimeComponentDescriptor> Components
        {
            get { return m_Components; }
        }

        /// <summary>
        /// 获取启动诊断。
        /// </summary>
        public IList<string> Diagnostics
        {
            get { return m_Diagnostics; }
        }
    }
}
