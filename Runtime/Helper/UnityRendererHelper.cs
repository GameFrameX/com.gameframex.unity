using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// Unity 渲染帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class UnityRendererHelper
    {
        /// <summary>
        /// 判断渲染组件是否在相机范围内
        /// </summary>
        /// <param name="renderer">渲染组件</param>
        /// <param name="camera">相机对象</param>
        /// <returns>如果渲染组件在相机范围内返回true，否则返回false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsVisibleFrom(Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        /// <summary>
        /// 判断渲染组件是否在相机范围内
        /// </summary>
        /// <param name="renderer">渲染对象</param>
        /// <param name="camera">相机对象</param>
        /// <returns>如果渲染组件在相机范围内返回true，否则返回false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsVisibleFrom(MeshRenderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}