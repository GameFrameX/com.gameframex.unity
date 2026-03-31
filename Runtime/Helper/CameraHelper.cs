using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 相机帮助类。
    /// </summary>
    /// <remarks>
    /// Camera helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class CameraHelper
    {
        /// <summary>
        /// 获取相机快照。
        /// </summary>
        /// <remarks>
        /// Captures a screenshot from the specified camera.
        /// </remarks>
        /// <param name="main">相机对象 / Camera object</param>
        /// <param name="scale">缩放比例，默认为0.5 / Scale factor, default is 0.5</param>
        /// <returns>捕获的纹理 / Captured texture</returns>
        [UnityEngine.Scripting.Preserve]
        public static Texture2D GetCaptureScreenshot(Camera main, float scale = 0.5f)
        {
            Rect rect = new Rect(0, 0, Screen.width * scale, Screen.height * scale);
            string name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            RenderTexture renderTexture = RenderTexture.GetTemporary((int)rect.width, (int)rect.height, 0);
            renderTexture.name = SceneManager.GetActiveScene().name + "_" + renderTexture.width + "_" + renderTexture.height + "_" + name;
            main.targetTexture = renderTexture;
            main.Render();

            RenderTexture.active = renderTexture;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false)
            {
                name = renderTexture.name
            };
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            main.targetTexture = null;
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);
            return screenShot;
        }
    }
}