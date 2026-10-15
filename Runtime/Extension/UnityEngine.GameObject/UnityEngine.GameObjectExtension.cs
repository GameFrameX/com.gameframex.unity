using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameObject 相关扩展方法类
    /// </summary>
    /// <remarks>
    /// Provides extension methods for GameObject, such as destroying components, finding children, and setting layers.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineGameObjectExtension
    {
        /// <summary>
        /// SetLayer 系列收集变换时使用的局部列表初始容量。
        /// </summary>
        private const int TransformListCapacity = 256;

        /// <summary>
        /// 销毁组件。
        /// </summary>
        /// <remarks>
        /// Destroys the component. In editor non-playing mode, the component is destroyed immediately.
        /// </remarks>
        /// <param name="component">目标组件。 / The target component.</param>
        [UnityEngine.Scripting.Preserve]
        public static void DestroyComponent(this Component component)
        {
            if (!ReferenceEquals(component, null))
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    UnityEngine.Object.DestroyImmediate(component);
                    return;
                }

                UnityEngine.Object.Destroy(component);
            }
        }

        /// <summary>
        /// 销毁指定类型的组件。
        /// </summary>
        /// <remarks>
        /// Destroys the component of the specified type from the GameObject.
        /// </remarks>
        /// <typeparam name="T">要销毁的组件类型。 / The type of the component to destroy.</typeparam>
        /// <param name="gameObject">目标游戏对象。 / The target game object.</param>
        [UnityEngine.Scripting.Preserve]
        public static void DestroyComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }

            // return component;
        }

        /// <summary>
        /// 获取或增加组件。
        /// </summary>
        /// <remarks>
        /// Gets the component if it exists, otherwise adds and returns a new one.
        /// </remarks>
        /// <typeparam name="T">要获取或增加的组件。 / The type of the component to get or add.</typeparam>
        /// <param name="gameObject">目标对象。 / The target game object.</param>
        /// <returns>获取或增加的组件。 / The existing or newly added component.</returns>
        [UnityEngine.Scripting.Preserve]
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// 获取或增加组件。
        /// </summary>
        /// <remarks>
        /// Gets the component of the specified type if it exists, otherwise adds and returns a new one.
        /// </remarks>
        /// <param name="gameObject">目标对象。 / The target game object.</param>
        /// <param name="type">要获取或增加的组件类型。 / The type of the component to get or add.</param>
        /// <returns>获取或增加的组件。 / The existing or newly added component.</returns>
        [UnityEngine.Scripting.Preserve]
        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            Component component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }

            return component;
        }

        /// <summary>
        /// 获取 GameObject 是否在场景中。
        /// </summary>
        /// <remarks>
        /// Checks whether the GameObject is an instance in a scene rather than a prefab asset.
        /// </remarks>
        /// <param name="gameObject">目标对象。 / The target game object.</param>
        /// <returns>GameObject 是否在场景中。 / Whether the GameObject is in a scene.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool InScene(this GameObject gameObject)
        {
            return gameObject.scene.name != null;
        }

        /// <summary>
        /// 设置对象的层
        /// </summary>
        /// <remarks>
        /// Sets the layer of the GameObject, optionally including its children.
        /// </remarks>
        /// <param name="gameObject">游戏对象 / The game object</param>
        /// <param name="layer">层 / The layer to set</param>
        /// <param name="recursively">是否设置子物体 / Whether to set the layer on child objects as well</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLayer(this GameObject gameObject, int layer, bool recursively = true)
        {
            if (!recursively)
            {
                gameObject.layer = layer;
                return;
            }

            // GetComponentsInChildren 的结果包含自身, 无需单独设置 gameObject.layer。
            // 使用局部列表而非共享静态缓存，避免递归期间重入导致缓存被脏化。
            var transforms = new List<Transform>(TransformListCapacity);
            gameObject.GetComponentsInChildren(true, transforms);
            foreach (var tf in transforms)
            {
                tf.gameObject.layer = layer;
            }
        }


        /// <summary>
        /// 销毁子物体。
        /// </summary>
        /// <remarks>
        /// Destroys all child objects of the GameObject.
        /// </remarks>
        /// <param name="gameObject">父物体 / Parent game object</param>
        [UnityEngine.Scripting.Preserve]
        public static void RemoveChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                gameObject.transform.GetChild(i).gameObject.DestroyObject();
            }
        }

        /// <summary>
        /// 销毁游戏物体
        /// </summary>
        /// <remarks>
        /// Destroys the game object. In editor non-playing mode, the object is destroyed immediately.
        /// </remarks>
        /// <param name="gameObject">要销毁的游戏对象 / The game object to destroy</param>
        [UnityEngine.Scripting.Preserve]
        public static void DestroyObject(this GameObject gameObject)
        {
            if (!ReferenceEquals(gameObject, null))
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    UnityEngine.Object.DestroyImmediate(gameObject);
                    return;
                }

                UnityEngine.Object.Destroy(gameObject);
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
        public static void Destroy(this GameObject gameObject)
        {
            gameObject.DestroyObject();
        }

        /// <summary>
        /// 在指定场景中查找特定名称的节点。
        /// </summary>
        /// <remarks>
        /// Finds a child node with the specified name in the given scene.
        /// </remarks>
        /// <param name="nodeName">节点名称。 / The name of the node.</param>
        /// <param name="sceneName">场景名称。 / The name of the scene.</param>
        /// <returns>找到的节点的GameObject实例，如果没有找到返回null。 / The found GameObject instance, or null if not found.</returns>
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
        /// Finds a child object by the game object name.
        /// </remarks>
        /// <param name="gameObject">目标游戏对象 / The target game object</param>
        /// <param name="name">子对象名称 / The name of the child object</param>
        /// <returns>找到的子对象，未找到返回null / The found child object, or null if not found</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject FindChildGamObjectByName(this GameObject gameObject, string name)
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
        /// Creates a child game object with the specified name under the game object.
        /// </remarks>
        /// <param name="gameObject">父游戏对象 / The parent game object</param>
        /// <param name="name">子对象名称 / The name of the child object</param>
        /// <returns>新创建的子游戏对象 / The newly created child game object</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameObject CreateChild(this GameObject gameObject, string name)
        {
            Debug.Assert(!ReferenceEquals(gameObject, null), nameof(gameObject) + " == null");
            return gameObject.transform.CreateChild(name);
        }

        /// <summary>
        /// 重置游戏对象的变换数据
        /// </summary>
        /// <remarks>
        /// Resets the transform data of the game object.
        /// </remarks>
        /// <param name="gameObject">目标游戏对象 / The target game object</param>
        [UnityEngine.Scripting.Preserve]
        public static void ResetTransform(this GameObject gameObject)
        {
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 设置对象的显示排序层
        /// </summary>
        /// <remarks>
        /// Sets the sorting layer of the rendering sort groups on the object.
        /// </remarks>
        /// <param name="gameObject">游戏对象 / The game object</param>
        /// <param name="sortingLayer">显示层 / The sorting layer name</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSortingGroupLayer(this GameObject gameObject, string sortingLayer)
        {
            SortingGroup[] sortingGroups = gameObject.GetComponentsInChildren<SortingGroup>();
            foreach (SortingGroup sg in sortingGroups)
            {
                sg.sortingLayerName = sortingLayer;
            }
        }
    }
}