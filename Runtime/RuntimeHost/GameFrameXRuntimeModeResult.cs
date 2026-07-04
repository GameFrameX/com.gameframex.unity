using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 运行时模式裁决结果。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXRuntimeModeResult
    {
        /// <summary>
        /// 初始化运行时模式裁决结果。
        /// </summary>
        /// <param name="mode">裁决出的运行模式。</param>
        /// <param name="reason">裁决原因。</param>
        /// <param name="manualEntryPath">手动入口路径。</param>
        public GameFrameXRuntimeModeResult(GameFrameXRuntimeMode mode, string reason, string manualEntryPath)
        {
            Mode = mode;
            Reason = reason ?? string.Empty;
            ManualEntryPath = manualEntryPath ?? string.Empty;
        }

        /// <summary>
        /// 获取裁决出的运行模式。
        /// </summary>
        public GameFrameXRuntimeMode Mode { get; private set; }

        /// <summary>
        /// 获取裁决原因。
        /// </summary>
        public string Reason { get; private set; }

        /// <summary>
        /// 获取手动入口路径。
        /// </summary>
        public string ManualEntryPath { get; private set; }
    }
}
