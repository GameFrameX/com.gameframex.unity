using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 表示组件支持在 Awake 前应用运行时覆盖配置。
    /// </summary>
    [Preserve]
    public interface IGameFrameXComponentRuntimeConfigurable
    {
        /// <summary>
        /// 应用运行时配置覆盖。
        /// </summary>
        /// <param name="config">组件运行时配置。</param>
        void ApplyRuntimeConfig(GameFrameXComponentRuntimeConfig config);
    }
}
