using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏对象帮助类。
    /// </summary>
    /// <remarks>
    /// Game object helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameObjectUtility
    {
        /// <summary>
        /// 在指定场景中查找特定名称的节点。
        /// </summary>
        /// <param name="sceneName">场景名称。</param>
        /// <param name="nodeName">节点名称。</param>
        /// <returns>找到的节点的GameObject实例，如果没有找到返回null。</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject FindChildGamObjectByName(string nodeName, string sceneName = null)
        {
            Scene scene;
            if (sceneName.IsNullOrWhiteSpace())
            {
                scene = SceneManager.GetActiveScene();
            }
            else
            {
                scene = SceneManager.GetSceneByName(sceneName);
                if (!scene.isLoaded)
                {
                    return null;
                }
            }

            var rootObjects = scene.GetRootGameObjects();
            foreach (var rootObject in rootObjects)
            {
                var result = rootObject.FindChildGamObjectByName(nodeName);
                if (result.IsNotNull())
                {
                    return result;
                }
            }

            return null;
        }
    }
}