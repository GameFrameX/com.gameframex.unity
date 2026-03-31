using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineVector2Extension
    {
        /// <summary>
        /// 取 <see cref="Vector2" /> 的 (x, y) 转换为 <see cref="Vector3" /> 的 (x, 0, y)。
        /// </summary>
        /// <remarks>
        /// Converts the Vector2's (x, y) to Vector3's (x, 0, y), useful for 2D to 3D conversion where y becomes z.
        /// </remarks>
        /// <param name="vector2">要转换的 Vector2。 / The Vector2 to convert.</param>
        /// <returns>转换后的 Vector3。 / The converted Vector3.</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }

        /// <summary>
        /// 取 <see cref="Vector2" /> 的 (x, y) 和给定参数 y 转换为 <see cref="Vector3" /> 的 (x, 参数 y, y)。
        /// </summary>
        /// <remarks>
        /// Converts the Vector2's (x, y) to Vector3's (x, newY, y), allowing custom y-axis value in 3D space.
        /// </remarks>
        /// <param name="vector2">要转换的 Vector2。 / The Vector2 to convert.</param>
        /// <param name="y">Vector3 的 y 值。 / The y value for the Vector3.</param>
        /// <returns>转换后的 Vector3。 / The converted Vector3.</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 ToVector3(this Vector2 vector2, float y)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }

        /// <summary>
        /// 将 Vector2 转换为 Vector4，在 z 和 w 分量上设置为 0
        /// </summary>
        /// <remarks>
        /// Converts Vector2 to Vector4 with z and w components set to 0.
        /// </remarks>
        /// <param name="self">源 Vector2 对象 / The source Vector2 object.</param>
        /// <returns>包含源 Vector2 的 x、y 分量且 z、w 分量为 0 的新 Vector4 对象 / A new Vector4 containing the source Vector2's x and y components with z and w set to 0.</returns>
        public static Vector4 ToVector4(this Vector2 self)
        {
            return new Vector4(self.x, self.y, 0, 0);
        }

        /// <summary>
        /// 将 Vector2 转换为 Vector3，在 z 分量上设置为 0
        /// </summary>
        /// <remarks>
        /// Converts Vector2 to Vector3 with z component set to 0.
        /// </remarks>
        /// <param name="self">源 Vector2 对象 / The source Vector2 object.</param>
        /// <returns>包含源 Vector2 的 x、y 分量且 z 分量为 0 的新 Vector3 对象 / A new Vector3 containing the source Vector2's x and y components with z set to 0.</returns>
        public static Vector3 AsVector3(this Vector2 self)
        {
            return new Vector3(self.x, self.y, 0);
        }
    }
}
