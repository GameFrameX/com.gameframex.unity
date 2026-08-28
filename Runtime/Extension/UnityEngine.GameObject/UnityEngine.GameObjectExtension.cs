using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    [Preserve]
    public static class UnityEngineGameObjectExtension
    {
        private static readonly List<Transform> CachedTransforms = new List<Transform>(64);

        /// <summary>
        /// 销毁组件。
        /// </summary>
        /// <remarks>
        /// Destroys the component. In editor non-playing mode, the component is destroyed immediately.
        /// </remarks>
        /// <param name="component">目标组件。 / The target component.</param>
        [Preserve]
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
        [Preserve]
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
        [Preserve]
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
        [Preserve]
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
        [Preserve]
        public static bool InScene(this GameObject gameObject)
        {
            return gameObject.scene.name != null;
        }

        /// <summary>
        /// 设置对象的层
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="layer">层</param>
        /// <param name="recursively">是否设置子物体</param>
        [Preserve]
        public static void SetLayer(this GameObject gameObject, int layer, bool recursively = true)
        {
            if (!recursively)
            {
                gameObject.layer = layer;
                return;
            }

            // GetComponentsInChildren 的结果包含自身, 无需单独设置 gameObject.layer
            CachedTransforms.Clear();
            gameObject.GetComponentsInChildren(true, CachedTransforms);
            foreach (var sg in CachedTransforms)
            {
                sg.gameObject.layer = layer;
            }
        }

        /// <summary>
        /// 销毁子物体。
        /// </summary>
        /// <remarks>
        /// Destroys all child objects of the specified game object.
        /// </remarks>
        /// <param name="gameObject">父物体 / Parent game object</param>
        [Preserve]
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
        /// <param name="gameObject"></param>
        [Preserve]
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
        /// <param name="gameObject"></param>
        [Preserve]
        public static void Destroy(this GameObject gameObject)
        {
            gameObject.DestroyObject();
        }

        /// <summary>
        /// 在指定场景中查找特定名称的节点。
        /// </summary>
        /// <param name="nodeName">节点名称。</param>
        /// <param name="sceneName">场景名称。</param>
        /// <returns>找到的节点的GameObject实例，如果没有找到返回null。</returns>
        [Preserve]
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
        [Preserve]
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
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Preserve]
        public static GameObject CreateChild(this GameObject gameObject, string name)
        {
            Debug.Assert(!ReferenceEquals(gameObject, null), nameof(gameObject) + " == null");
            return gameObject.transform.CreateChild(name);
        }

        /// <summary>
        /// 重置游戏对象的变换数据
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        [Preserve]
        public static void ResetTransform(this GameObject gameObject)
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
        [Preserve]
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