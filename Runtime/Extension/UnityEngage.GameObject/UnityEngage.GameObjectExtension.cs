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
        /// <param name="self">目标组件。</param>
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
        /// <typeparam name="T">要销毁的组件类型。</typeparam>
        /// <param name="gameObject">目标游戏对象。</param>
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
        /// <typeparam name="T">要获取或增加的组件。</typeparam>
        /// <param name="gameObject">目标对象。</param>
        /// <returns>获取或增加的组件。</returns>
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
        /// <param name="gameObject">目标对象。</param>
        /// <param name="type">要获取或增加的组件类型。</param>
        /// <returns>获取或增加的组件。</returns>
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
        /// <param name="gameObject">目标对象。</param>
        /// <returns>GameObject 是否在场景中。</returns>
        /// <remarks>若返回 true，表明此 GameObject 是一个场景中的实例对象；若返回 false，表明此 GameObject 是一个 Prefab。</remarks>
        [Preserve]
        public static bool InScene(this GameObject gameObject)
        {
            return gameObject.scene.name != null;
        }

        /// <summary>
        /// 递归设置游戏对象的层次。
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject" /> 对象。</param>
        /// <param name="layer">目标层次的编号。</param>
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