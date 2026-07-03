using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时启动入口。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeBootstrap
    {
        /// <summary>
        /// 重置自动运行时静态状态，兼容关闭 Domain Reload 的 PlayMode。
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            GameFrameXRuntimeHost.Reset();
        }

        /// <summary>
        /// 在场景加载前触发自动运行时类加载。
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
        }

        /// <summary>
        /// 在首个场景加载后启动 GameFrameX 自动运行时，保留旧场景节点的 Inspector 配置优先级。
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            GameFrameXRuntimeHost.EnsureStarted();
        }
    }
}
