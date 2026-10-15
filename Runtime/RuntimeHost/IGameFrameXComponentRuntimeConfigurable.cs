
namespace GameFrameX.Runtime
{
    /// <summary>
    /// 表示组件支持在 Awake 前应用运行时覆盖配置。
    /// </summary>
    /// <remarks>
    /// Indicates that a component supports applying runtime override configuration before Awake.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public interface IGameFrameXComponentRuntimeConfigurable
    {
        /// <summary>
        /// 应用运行时配置覆盖。
        /// </summary>
        /// <remarks>
        /// Applies the runtime configuration overrides.
        /// </remarks>
        /// <param name="config">组件运行时配置。 / The component runtime configuration.</param>
        void ApplyRuntimeConfig(GameFrameXComponentRuntimeConfig config);
    }
}
