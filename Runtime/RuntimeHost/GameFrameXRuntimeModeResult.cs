using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 运行时模式裁决结果。
    /// </summary>
    /// <remarks>
    /// Result of the GameFrameX runtime mode resolution.
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXRuntimeModeResult
    {
        /// <summary>
        /// 初始化运行时模式裁决结果。
        /// </summary>
        /// <remarks>
        /// Initializes the runtime mode resolution result.
        /// </remarks>
        /// <param name="mode">裁决出的运行模式。 / The resolved runtime mode.</param>
        /// <param name="reason">裁决原因。 / The reason for the resolution.</param>
        /// <param name="manualEntryPath">手动入口路径。 / The path of the manual entry.</param>
        public GameFrameXRuntimeModeResult(GameFrameXRuntimeMode mode, string reason, string manualEntryPath)
        {
            Mode = mode;
            Reason = reason ?? string.Empty;
            ManualEntryPath = manualEntryPath ?? string.Empty;
        }

        /// <summary>
        /// 获取裁决出的运行模式。
        /// </summary>
        /// <remarks>
        /// Gets the resolved runtime mode.
        /// </remarks>
        public GameFrameXRuntimeMode Mode { get; private set; }

        /// <summary>
        /// 获取裁决原因。
        /// </summary>
        /// <remarks>
        /// Gets the reason for the resolution.
        /// </remarks>
        public string Reason { get; private set; }

        /// <summary>
        /// 获取手动入口路径。
        /// </summary>
        /// <remarks>
        /// Gets the path of the manual entry.
        /// </remarks>
        public string ManualEntryPath { get; private set; }
    }
}
