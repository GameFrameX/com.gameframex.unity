using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供编辑器下 Object 的扩展方法。
    /// </summary>
    /// <remarks>
    /// Provides extension methods for Object in the editor.
    /// </remarks>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取资源的资产路径。
        /// </summary>
        /// <remarks>
        /// Gets the asset path of the object.
        /// </remarks>
        /// <param name="obj">目标对象。 / The target object.</param>
        /// <returns>资产路径。 / The asset path.</returns>
        public static string GetAssetPath(this Object obj)
        {
            return AssetDatabase.GetAssetPath(obj);
        }

        /// <summary>
        /// 获取资源的 GUID。
        /// </summary>
        /// <remarks>
        /// Gets the GUID of the object's asset.
        /// </remarks>
        /// <param name="obj">目标对象。 / The target object.</param>
        /// <returns>资产 GUID。 / The asset GUID.</returns>
        public static string GetAssetGUID(this Object obj)
        {
            return AssetDatabase.AssetPathToGUID(GetAssetPath(obj));
        }

        /// <summary>
        /// 获取材质引用的所有纹理。
        /// </summary>
        /// <remarks>
        /// Gets all textures referenced by the material.
        /// </remarks>
        /// <param name="material">目标材质。 / The target material.</param>
        /// <returns>材质引用的纹理数组。 / The array of textures referenced by the material.</returns>
        public static Texture2D[] GetMaterialTextures(this Material material)
        {
            var result = new List<Texture2D>();

            var dependencies = EditorUtility.CollectDependencies(new Object[] {material});
            foreach (var item in dependencies)
            {
                if (!(item is Texture2D texture))
                {
                    continue;
                }

                result.Add(texture);
            }

            return result.ToArray();
        }
    }
}