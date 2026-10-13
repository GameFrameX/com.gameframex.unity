using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 裁决 GameFrameX 本次运行应使用的启动模式。
    /// </summary>
    /// <remarks>
    /// Resolves the startup mode the current GameFrameX run should use.
    /// </remarks>
    [Preserve]
    public static class GameFrameXRuntimeModeResolver
    {
        private static GameFrameXRuntimeModeResult s_LastResult;

        /// <summary>
        /// 获取最近一次模式裁决结果。
        /// </summary>
        /// <remarks>
        /// Gets the most recent mode resolution result.
        /// </remarks>
        public static GameFrameXRuntimeModeResult LastResult
        {
            get { return s_LastResult; }
        }

        /// <summary>
        /// 重置模式裁决状态。
        /// </summary>
        /// <remarks>
        /// Resets the mode resolution state.
        /// </remarks>
        public static void Reset()
        {
            s_LastResult = null;
        }

        /// <summary>
        /// 裁决本次运行模式。
        /// </summary>
        /// <remarks>
        /// Resolves the runtime mode for the current run.
        /// Resolution priority: HeadlessMode (explicit host contract) &gt; ManualSceneMode (manual scene entry) &gt; AutoPackageMode (auto host fallback).
        /// When headless is active, both the manual GFX entry in the scene and the auto host are rejected to avoid mixed takeover.
        /// </remarks>
        /// <returns>运行模式裁决结果。 / The runtime mode resolution result.</returns>
        public static GameFrameXRuntimeModeResult Resolve()
        {
            if (GameFrameXRuntimeHost.IsHeadlessRequested)
            {
                var conflictEntry = Object.FindObjectOfType<BaseComponent>();
                if (conflictEntry != null)
                {
                    s_LastResult = new GameFrameXRuntimeModeResult(
                        GameFrameXRuntimeMode.HeadlessMode,
                        "Headless startup is active; manual scene entry and auto host startup are rejected.",
                        GetHierarchyPath(conflictEntry.transform));
                    return s_LastResult;
                }

                s_LastResult = new GameFrameXRuntimeModeResult(
                    GameFrameXRuntimeMode.HeadlessMode,
                    "Headless startup was requested by the host.",
                    string.Empty);
                return s_LastResult;
            }

            var registeredBaseComponent = GameEntry.GetComponent<BaseComponent>();
            if (registeredBaseComponent != null)
            {
                s_LastResult = new GameFrameXRuntimeModeResult(
                    GameFrameXRuntimeMode.ManualSceneMode,
                    "Registered BaseComponent already exists.",
                    GetHierarchyPath(registeredBaseComponent.transform));
                return s_LastResult;
            }

            var sceneBaseComponent = Object.FindObjectOfType<BaseComponent>();
            if (sceneBaseComponent != null)
            {
                s_LastResult = new GameFrameXRuntimeModeResult(
                    GameFrameXRuntimeMode.ManualSceneMode,
                    "Scene BaseComponent exists before auto host startup.",
                    GetHierarchyPath(sceneBaseComponent.transform));
                return s_LastResult;
            }

            s_LastResult = new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.AutoPackageMode,
                "No registered or scene BaseComponent was found.",
                string.Empty);
            return s_LastResult;
        }

        private static string GetHierarchyPath(Transform transform)
        {
            if (transform == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder(transform.name);
            var parent = transform.parent;
            while (parent != null)
            {
                builder.Insert(0, "/");
                builder.Insert(0, parent.name);
                parent = parent.parent;
            }

            return builder.ToString();
        }
    }
}
