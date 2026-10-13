#if ENABLE_GAME_FRAME_X_DO_TWEEN
using System;
using DG.Tweening;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// DoTween帮助类。
    /// </summary>
    /// <remarks>
    /// DOTween animation helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class DoTweenHelper
    {
        /// <summary>
        /// 关闭对象上的全部动画。
        /// </summary>
        /// <remarks>
        /// Kills all tweens on the specified game object.
        /// </remarks>
        /// <param name="gameObject">物体对象 / Game object</param>
        /// <param name="complete">是否直接完成动画 / Whether to complete the tween before killing</param>
        [UnityEngine.Scripting.Preserve]
        public static void Kill(GameObject gameObject, bool complete = false)
        {
            DOTween.Kill(gameObject, complete);
        }

        /// <summary>
        /// 从浮点值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one float value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(float startValue, float endValue, float time, Action<float> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从整形值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one int value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(int startValue, int endValue, float time, Action<int> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从无符号整形值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one uint value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(uint startValue, uint endValue, float time, Action<uint> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从长整形值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one long value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(long startValue, long endValue, float time, Action<long> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从无符号长整形值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one ulong value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(ulong startValue, ulong endValue, float time, Action<ulong> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从浮点值到另一个值，并在完成时触发回调
        /// </summary>
        /// <remarks>
        /// Tweens from one float value to another with a complete callback.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <param name="complete">完成回调 / Complete callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(float startValue, float endValue, float time, Action<float> update, Action complete)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time).OnComplete(() => { complete?.Invoke(); });
        }

        /// <summary>
        /// 从三维向量值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector3 value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector3 startValue, Vector3 endValue, float time, Action<Vector3> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从三维向量值到另一个值，并在完成时触发回调
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector3 value to another with a complete callback.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <param name="complete">完成回调 / Complete callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector3 startValue, Vector3 endValue, float time, Action<Vector3> update, Action complete)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time).OnComplete(() => { complete?.Invoke(); });
        }

        /// <summary>
        /// 从三维整数向量值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector3Int value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector3Int startValue, Vector3Int endValue, float time, Action<Vector3Int> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(new Vector3Int((int)m.x, (int)m.y, (int)m.z)); }, endValue, time);
        }

        /// <summary>
        /// 从二维向量值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector2 value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector2 startValue, Vector2 endValue, float time, Action<Vector2> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time);
        }

        /// <summary>
        /// 从二维向量值到另一个值，并在完成时触发回调
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector2 value to another with a complete callback.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <param name="complete">完成回调 / Complete callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector2 startValue, Vector2 endValue, float time, Action<Vector2> update, Action complete)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(m); }, endValue, time).OnComplete(() => { complete?.Invoke(); });
        }

        /// <summary>
        /// 从二维整数向量值到另一个值
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector2Int value to another.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector2Int startValue, Vector2Int endValue, float time, Action<Vector2Int> update)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(new Vector2Int((int)m.x, (int)m.y)); }, endValue, time);
        }

        /// <summary>
        /// 从二维整数向量值到另一个值，并在完成时触发回调
        /// </summary>
        /// <remarks>
        /// Tweens from one Vector2Int value to another with a complete callback.
        /// </remarks>
        /// <param name="startValue">开始值 / Start value</param>
        /// <param name="endValue">结束值 / End value</param>
        /// <param name="time">持续时长 / Duration</param>
        /// <param name="update">更新回调 / Update callback</param>
        /// <param name="complete">完成回调 / Complete callback</param>
        /// <returns>补间动画对象 / The tweener</returns>
        [UnityEngine.Scripting.Preserve]
        public static Tweener To(Vector2Int startValue, Vector2Int endValue, float time, Action<Vector2Int> update, Action complete)
        {
            return DOTween.To(() => startValue, (m) => { update?.Invoke(new Vector2Int((int)m.x, (int)m.y)); }, endValue, time).OnComplete(() => { complete?.Invoke(); });
        }
    }
}
#endif