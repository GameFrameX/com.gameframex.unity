using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineVector2Extension
    {
        /// <summary>
        /// 取 <see cref="Vector2" /> 的 (x, y) 转换为 <see cref="Vector3" /> 的 (x, 0, y)。
        /// </summary>
        /// <param name="vector2">要转换的 Vector2。</param>
        /// <returns>转换后的 Vector3。</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }

        /// <summary>
        /// 取 <see cref="Vector2" /> 的 (x, y) 和给定参数 y 转换为 <see cref="Vector3" /> 的 (x, 参数 y, y)。
        /// </summary>
        /// <param name="vector2">要转换的 Vector2。</param>
        /// <param name="y">Vector3 的 y 值。</param>
        /// <returns>转换后的 Vector3。</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 ToVector3(this Vector2 vector2, float y)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }

        /// <summary>
        /// 将 Vector2 转换为 Vector4，在 z 和 w 分量上设置为 0
        /// </summary>
        /// <param name="self">源 Vector2 对象</param>
        /// <returns>包含源 Vector2 的 x、y 分量且 z、w 分量为 0 的新 Vector4 对象</returns>
        public static Vector4 ToVector4(this Vector2 self)
        {
            return new Vector4(self.x, self.y, 0, 0);
        }

        /// <summary>
        /// 将 Vector2 转换为 Vector3，在 z 分量上设置为 0
        /// </summary>
        /// <param name="self">源 Vector2 对象</param>
        /// <returns>包含源 Vector2 的 x、y 分量且 z 分量为 0 的新 Vector3 对象</returns>
        public static Vector3 AsVector3(this Vector2 self)
        {
            return new Vector3(self.x, self.y, 0);
        }
    }
}