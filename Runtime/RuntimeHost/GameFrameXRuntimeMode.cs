using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 运行时启动模式。
    /// </summary>
    /// <remarks>
    /// Startup modes of the GameFrameX runtime.
    /// </remarks>
    [Preserve]
    public enum GameFrameXRuntimeMode
    {
        /// <summary>
        /// 尚未裁决运行模式。
        /// </summary>
        /// <remarks>
        /// The runtime mode has not been resolved yet.
        /// </remarks>
        Unknown = 0,

        /// <summary>
        /// 使用用户场景中手动放置的 GFX 入口。
        /// </summary>
        /// <remarks>
        /// Uses the GFX entry placed manually in the user scene.
        /// </remarks>
        ManualSceneMode = 1,

        /// <summary>
        /// 使用安装包自动发现和创建的运行时 Host。
        /// </summary>
        /// <remarks>
        /// Uses the runtime host auto-discovered and created by the package.
        /// </remarks>
        AutoPackageMode = 2,

        /// <summary>
        /// 使用宿主通过 <see cref="GameFrameworkStartup.Headless"/> 接入的 Headless 启动路径。
        /// </summary>
        /// <remarks>
        /// Does not create the [GameFrameX] auto host and mounts no GameFrameX components; only module registration is performed.
        /// Update and shutdown are the host's responsibility. When the headless contract is active,
        /// both the ManualSceneMode and AutoPackageMode entries are rejected by mutual exclusion.
        /// </remarks>
        HeadlessMode = 3,
    }
}
