using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏对象帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class GameObjectHelper
    {
        /// <summary>
        /// 销毁子物体
        /// </summary>
        /// <param name="go"></param>
        [UnityEngine.Scripting.Preserve]
        public static void RemoveChildren(GameObject go)
        {
            for (var i = go.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(go.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 销毁游戏物体
        /// </summary>
        /// <param name="gameObject"></param>
        [UnityEngine.Scripting.Preserve]
        public static void DestroyObject(this GameObject gameObject)
        {
            if (!ReferenceEquals(gameObject, null))
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    Object.DestroyImmediate(gameObject);
                    return;
                }

                Object.Destroy(gameObject);
            }
        }

        /// <summary>
        /// 销毁游戏物体
        /// </summary>
        /// <param name="gameObject"></param>
        [UnityEngine.Scripting.Preserve]
        public static void Destroy(GameObject gameObject)
        {
            gameObject.DestroyObject();
        }

        /// <summary>
        /// 销毁游戏组件
        /// </summary>
        /// <param name="component"></param>
        [UnityEngine.Scripting.Preserve]
        public static void DestroyComponent(Component component)
        {
            if (!ReferenceEquals(component, null))
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    Object.DestroyImmediate(component);
                    return;
                }

                Object.Destroy(component);
            }
        }

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
                var result = FindChildGamObjectByName(rootObject, nodeName);
                if (result.IsNotNull())
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据游戏对象名称查询子对象
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject FindChildGamObjectByName(GameObject gameObject, string name)
        {
            var transform = gameObject.transform.FindChildName(name);
            if (transform.IsNotNull())
            {
                return transform.gameObject;
            }

            return null;
        }

        /// <summary>
        /// 创建游戏对象
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject Create(Transform parent, string name)
        {
            Debug.Assert(!ReferenceEquals(parent, null), nameof(parent) + " == null");
            var gameObject = new GameObject(name);
            gameObject.transform.SetParent(parent);
            return gameObject;
        }

        /// <summary>
        /// 创建游戏对象
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject Create(GameObject parent, string name)
        {
            Debug.Assert(!ReferenceEquals(parent, null), nameof(parent) + " == null");
            return Create(parent.transform, name);
        }

        /// <summary>
        /// 重置游戏对象的变换数据
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static void ResetTransform(GameObject gameObject)
        {
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 设置对象的显示排序层
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="sortingLayer">显示层</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSortingGroupLayer(GameObject gameObject, string sortingLayer)
        {
            SortingGroup[] sortingGroups = gameObject.GetComponentsInChildren<SortingGroup>();
            foreach (SortingGroup sg in sortingGroups)
            {
                sg.sortingLayerName = sortingLayer;
            }
        }

        /// <summary>
        /// 设置对象的层
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="layer">层</param>
        /// <param name="children">是否设置子物体</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLayer(GameObject gameObject, int layer, bool children = true)
        {
            if (gameObject.layer != layer)
            {
                gameObject.layer = layer;
            }

            if (children)
            {
                Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
                foreach (var sg in transforms)
                {
                    sg.gameObject.layer = layer;
                }
            }
        }
    }
}