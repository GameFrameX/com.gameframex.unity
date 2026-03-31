using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// Unity渲染帮助类。
    /// </summary>
    /// <remarks>
    /// Unity renderer helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class UnityRendererHelper
    {
        /// <summary>
        /// 判断渲染组件是否在相机范围内。
        /// </summary>
        /// <remarks>
        /// Determines if the renderer is visible from the specified camera.
        /// </remarks>
        /// <param name="renderer">渲染组件 / Renderer component</param>
        /// <param name="camera">相机对象 / Camera object</param>
        /// <returns>如果渲染组件在相机范围内返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if visible; otherwise <c>false</c></returns>
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