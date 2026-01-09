using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineTransformExtension
    {
        /// <summary>
        /// 查找子节点的名称符合的 <see cref="Transform" />。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="name">子节点的名称</param>
        [UnityEngine.Scripting.Preserve]
        public static Transform FindChildName(this Transform transform, string name)
        {
            var child = transform.Find(name);
            if (child.IsNotNull())
            {
                return child;
            }

            var childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var t = transform.GetChild(i);
                if (t.name.EqualsFast(name))
                {
                    return t;
                }

                t = t.FindChildName(name);

                if (t.IsNotNull())
                {
                    return t;
                }
            }

            return null;
        }

        /// <summary>
        /// 设置绝对位置的 x 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">x 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetPositionX(this Transform transform, float newValue)
        {
            Vector3 v = transform.position;
            v.x = newValue;
            transform.position = v;
        }

        /// <summary>
        /// 设置绝对位置的 y 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">y 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetPositionY(this Transform transform, float newValue)
        {
            Vector3 v = transform.position;
            v.y = newValue;
            transform.position = v;
        }

        /// <summary>
        /// 设置绝对位置的 z 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">z 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetPositionZ(this Transform transform, float newValue)
        {
            Vector3 v = transform.position;
            v.z = newValue;
            transform.position = v;
        }

        /// <summary>
        /// 增加绝对位置的 x 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">x 坐标值增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddPositionX(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.position;
            v.x += deltaValue;
            transform.position = v;
        }

        /// <summary>
        /// 增加绝对位置的 y 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">y 坐标值增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddPositionY(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.position;
            v.y += deltaValue;
            transform.position = v;
        }

        /// <summary>
        /// 增加绝对位置的 z 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">z 坐标值增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddPositionZ(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.position;
            v.z += deltaValue;
            transform.position = v;
        }

        /// <summary>
        /// 设置相对位置的 x 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">x 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalPositionX(this Transform transform, float newValue)
        {
            Vector3 v = transform.localPosition;
            v.x = newValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 设置相对位置的 y 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">y 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalPositionY(this Transform transform, float newValue)
        {
            Vector3 v = transform.localPosition;
            v.y = newValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 设置相对位置的 z 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">z 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalPositionZ(this Transform transform, float newValue)
        {
            Vector3 v = transform.localPosition;
            v.z = newValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 增加相对位置的 x 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">x 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalPositionX(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localPosition;
            v.x += deltaValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 增加相对位置的 y 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">y 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalPositionY(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localPosition;
            v.y += deltaValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 增加相对位置的 z 坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">z 坐标值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalPositionZ(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localPosition;
            v.z += deltaValue;
            transform.localPosition = v;
        }

        /// <summary>
        /// 设置相对尺寸的 x 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">x 分量值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalScaleX(this Transform transform, float newValue)
        {
            Vector3 v = transform.localScale;
            v.x = newValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 设置相对尺寸的 y 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">y 分量值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalScaleY(this Transform transform, float newValue)
        {
            Vector3 v = transform.localScale;
            v.y = newValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 设置相对尺寸的 z 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="newValue">z 分量值。</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLocalScaleZ(this Transform transform, float newValue)
        {
            Vector3 v = transform.localScale;
            v.z = newValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 增加相对尺寸的 x 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">x 分量增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalScaleX(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localScale;
            v.x += deltaValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 增加相对尺寸的 y 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">y 分量增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalScaleY(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localScale;
            v.y += deltaValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 增加相对尺寸的 z 分量。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="deltaValue">z 分量增量。</param>
        [UnityEngine.Scripting.Preserve]
        public static void AddLocalScaleZ(this Transform transform, float deltaValue)
        {
            Vector3 v = transform.localScale;
            v.z += deltaValue;
            transform.localScale = v;
        }

        /// <summary>
        /// 二维空间下使 <see cref="Transform" /> 指向指向目标点的算法，使用世界坐标。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="lookAtPoint2D">要朝向的二维坐标点。</param>
        /// <remarks>假定其 forward 向量为 <see cref="Vector3.up" />。</remarks>
        [UnityEngine.Scripting.Preserve]
        public static void LookAt2D(this Transform transform, Vector2 lookAtPoint2D)
        {
            Vector3 vector = lookAtPoint2D.ToVector3() - transform.position;
            vector.y = 0f;

            if (vector.magnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
            }
        }

        /// <summary>
        /// 将 <see cref="Transform" /> 重置为初始状态：本地坐标归零、旋转归零、缩放归一。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <param name="parent">要设置的新父级 Transform。</param>
        /// <remarks>
        /// 等价于同时设置 localPosition = Vector3.zero、localRotation = Quaternion.identity、localScale = Vector3.one。
        /// 注意：此方法会先设置父级，再执行重置，因此重置后的本地坐标、旋转、缩放均相对于新父级。
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void SetParentAndReset(this Transform transform, Transform parent)
        {
            transform.SetParent(parent);
            transform.Reset();
        }

        /// <summary>
        /// 将 <see cref="Transform" /> 重置为初始状态：本地坐标归零、旋转归零、缩放归一。
        /// </summary>
        /// <param name="transform"><see cref="Transform" /> 对象。</param>
        /// <remarks>
        /// 等价于同时设置 localPosition = Vector3.zero、localRotation = Quaternion.identity、localScale = Vector3.one。
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}