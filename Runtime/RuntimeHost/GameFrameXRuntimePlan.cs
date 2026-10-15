using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时启动计划。
    /// </summary>
    /// <remarks>
    /// Startup plan of the GameFrameX auto runtime.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class GameFrameXRuntimePlan
    {
        private readonly List<GameFrameXRuntimeComponentDescriptor> m_Components = new List<GameFrameXRuntimeComponentDescriptor>(32);

        private readonly List<string> m_Diagnostics = new List<string>(32);

        /// <summary>
        /// 获取计划挂载的组件。
        /// </summary>
        /// <remarks>
        /// Gets the components planned for mounting.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public IList<GameFrameXRuntimeComponentDescriptor> Components
        {
            get { return m_Components; }
        }

        /// <summary>
        /// 获取启动诊断。
        /// </summary>
        /// <remarks>
        /// Gets the startup diagnostics.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public IList<string> Diagnostics
        {
            get { return m_Diagnostics; }
        }
    }
}
