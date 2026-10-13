using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 提供 GameFrameX 自动运行时的组件、Manager 和配置覆盖。
    /// </summary>
    /// <remarks>
    /// Provides component, manager and configuration overrides for the GameFrameX auto runtime.
    /// </remarks>
    [Preserve]
    public interface IGameFrameXRuntimeOverrideProvider
    {
        /// <summary>
        /// 收集运行时覆盖。
        /// </summary>
        /// <remarks>
        /// Collects runtime overrides.
        /// </remarks>
        /// <param name="context">运行时覆盖上下文。 / The runtime override context.</param>
        void CollectOverrides(GameFrameXRuntimeOverrideContext context);
    }
}
