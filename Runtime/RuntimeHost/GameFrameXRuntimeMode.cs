using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 运行时启动模式。
    /// </summary>
    [Preserve]
    public enum GameFrameXRuntimeMode
    {
        /// <summary>
        /// 尚未裁决运行模式。
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 使用用户场景中手动放置的 GFX 入口。
        /// </summary>
        ManualSceneMode = 1,

        /// <summary>
        /// 使用安装包自动发现和创建的运行时 Host。
        /// </summary>
        AutoPackageMode = 2,
    }
}
