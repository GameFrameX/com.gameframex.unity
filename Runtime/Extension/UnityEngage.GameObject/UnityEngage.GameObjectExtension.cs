using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    [Preserve]
    public static class UnityEngageGameObjectExtension
    {
        private static readonly List<Transform> s_CachedTransforms = new List<Transform>();

        /// <summary>
        /// 销毁组件。
        /// </summary>
        /// <remarks>
        /// Destroys the component attached to the same GameObject.
        /// </remarks>
        /// <param name="self">目标组件。 / The target component.</param>
        [Preserve]
        public static void DestroyComponent(this Component self)
        {
            var component = self.GetComponent(self.GetType());
            if (component != null)
            {
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
        /// 递归设置游戏对象的层次。
        /// </summary>
        /// <remarks>
        /// Sets the layer of the GameObject and all its children recursively.
        /// </remarks>
        /// <param name="gameObject"><see cref="GameObject" /> 对象。 / The GameObject instance.</param>
        /// <param name="layer">目标层次的编号。 / The target layer number.</param>
        [Preserve]
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.GetComponentsInChildren(true, s_CachedTransforms);
            foreach (var tf in s_CachedTransforms)
            {
                tf.gameObject.layer = layer;
            }

            s_CachedTransforms.Clear();
        }
    }
}
