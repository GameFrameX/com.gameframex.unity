using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameFrameX.Editor
{
    public static class ObjectExtensions
    {
        public static string GetAssetPath(this Object obj)
        {
            return AssetDatabase.GetAssetPath(obj);
        }

        public static string GetAssetGUID(this Object obj)
        {
            return AssetDatabase.AssetPathToGUID(GetAssetPath(obj));
        }

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