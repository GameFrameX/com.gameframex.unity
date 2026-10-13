using UnityEngine;
using UnityEngine.Rendering;
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
    public static class GameObjectHelper
    {
        /// <summary>
        /// 销毁子物体。
        /// </summary>
        /// <remarks>
        /// Destroys all child objects of the specified game object.
        /// </remarks>
        /// <param name="go">父物体 / Parent game object</param>
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
        /// <remarks>
        /// Destroys the game object.
        /// </remarks>
        /// <param name="gameObject">要销毁的游戏对象 / The game object to destroy</param>
        [UnityEngine.Scripting.Preserve]
        public static void Destroy(GameObject gameObject)
        {
            gameObject.DestroyObject();
        }

        /// <summary>
        /// 销毁游戏组件
        /// </summary>
        /// <remarks>
        /// Destroys the game component.
        /// </remarks>
        /// <param name="component">要销毁的组件 / The component to destroy</param>
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
        /// <remarks>
        /// Finds a child node with the specified name in the given scene.
        /// </remarks>
        /// <param name="sceneName">场景名称。 / Scene name.</param>
        /// <param name="nodeName">节点名称。 / Node name.</param>
        /// <returns>找到的节点的GameObject实例，如果没有找到返回null。 / The GameObject instance of the found node, or null if not found.</returns>
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
        /// <remarks>
        /// Finds a child object by game object name.
        /// </remarks>
        /// <param name="gameObject">父游戏对象 / The parent game object</param>
        /// <param name="name">子对象名称 / The child object name</param>
        /// <returns>找到的子对象，未找到返回 null / The found child object, or null if not found</returns>
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
        /// <remarks>
        /// Creates a game object under the specified parent transform.
        /// </remarks>
        /// <param name="parent">父变换 / The parent transform</param>
        /// <param name="name">游戏对象名称 / The game object name</param>
        /// <returns>新创建的游戏对象 / The newly created game object</returns>
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
        /// <remarks>
        /// Creates a game object under the specified parent game object.
        /// </remarks>
        /// <param name="parent">父游戏对象 / The parent game object</param>
        /// <param name="name">游戏对象名称 / The game object name</param>
        /// <returns>新创建的游戏对象 / The newly created game object</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject Create(GameObject parent, string name)
        {
            Debug.Assert(!ReferenceEquals(parent, null), nameof(parent) + " == null");
            return Create(parent.transform, name);
        }

        /// <summary>
        /// 重置游戏对象的变换数据
        /// </summary>
        /// <remarks>
        /// Resets the transform data of the game object.
        /// </remarks>
        /// <param name="gameObject">游戏对象 / The game object</param>
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
        /// <remarks>
        /// Sets the sorting layer of the object.
        /// </remarks>
        /// <param name="gameObject">游戏对象 / The game object</param>
        /// <param name="sortingLayer">显示层 / The sorting layer</param>
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
        /// <remarks>
        /// Sets the layer of the object.
        /// </remarks>
        /// <param name="gameObject">游戏对象 / The game object</param>
        /// <param name="layer">层 / The layer</param>
        /// <param name="children">是否设置子物体 / Whether to also set child objects</param>
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