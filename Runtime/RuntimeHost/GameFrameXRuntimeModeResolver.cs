using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 裁决 GameFrameX 本次运行应使用的启动模式。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeModeResolver
    {
        private static GameFrameXRuntimeModeResult s_LastResult;

        /// <summary>
        /// 获取最近一次模式裁决结果。
        /// </summary>
        public static GameFrameXRuntimeModeResult LastResult
        {
            get { return s_LastResult; }
        }

        /// <summary>
        /// 重置模式裁决状态。
        /// </summary>
        public static void Reset()
        {
            s_LastResult = null;
        }

        /// <summary>
        /// 裁决本次运行模式。
        /// </summary>
        /// <returns>运行模式裁决结果。</returns>
        public static GameFrameXRuntimeModeResult Resolve()
        {
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
