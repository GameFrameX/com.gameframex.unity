using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineTransformExtension
    {
        /// <summary>
        /// 查找子节点的名称符合的 <see cref="Transform" />。
        /// </summary>
        /// <remarks>
        /// Finds a child Transform by name using depth-first search.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="name">子节点的名称 / The name of the child node to find.</param>
        /// <returns>找到的子节点 Transform，如果未找到则返回 null。 / The found child Transform, or null if not found.</returns>
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
        /// <remarks>
        /// Sets the x component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">x 坐标值。 / The new x coordinate value.</param>
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
        /// <remarks>
        /// Sets the y component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">y 坐标值。 / The new y coordinate value.</param>
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
        /// <remarks>
        /// Sets the z component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">z 坐标值。 / The new z coordinate value.</param>
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
        /// <remarks>
        /// Adds a delta value to the x component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">x 坐标值增量。 / The delta value to add to the x coordinate.</param>
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
        /// <remarks>
        /// Adds a delta value to the y component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">y 坐标值增量。 / The delta value to add to the y coordinate.</param>
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
        /// <remarks>
        /// Adds a delta value to the z component of the world position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">z 坐标值增量。 / The delta value to add to the z coordinate.</param>
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
        /// <remarks>
        /// Sets the x component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">x 坐标值。 / The new x coordinate value.</param>
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
        /// <remarks>
        /// Sets the y component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">y 坐标值。 / The new y coordinate value.</param>
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
        /// <remarks>
        /// Sets the z component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">z 坐标值。 / The new z coordinate value.</param>
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
        /// <remarks>
        /// Adds a delta value to the x component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">x 坐标值。 / The delta value to add to the x coordinate.</param>
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
        /// <remarks>
        /// Adds a delta value to the y component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">y 坐标值。 / The delta value to add to the y coordinate.</param>
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
        /// <remarks>
        /// Adds a delta value to the z component of the local position.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">z 坐标值。 / The delta value to add to the z coordinate.</param>
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
        /// <remarks>
        /// Sets the x component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">x 分量值。 / The new x scale value.</param>
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
        /// <remarks>
        /// Sets the y component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">y 分量值。 / The new y scale value.</param>
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
        /// <remarks>
        /// Sets the z component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="newValue">z 分量值。 / The new z scale value.</param>
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
        /// <remarks>
        /// Adds a delta value to the x component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">x 分量增量。 / The delta value to add to the x scale.</param>
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
        /// <remarks>
        /// Adds a delta value to the y component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">y 分量增量。 / The delta value to add to the y scale.</param>
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
        /// <remarks>
        /// Adds a delta value to the z component of the local scale.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="deltaValue">z 分量增量。 / The delta value to add to the z scale.</param>
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
        /// <remarks>
        /// Makes the Transform face a target point in 2D space using world coordinates.
        /// Assumes the forward vector is <see cref="Vector3.up" />.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="lookAtPoint2D">要朝向的二维坐标点。 / The 2D point to look at.</param>
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
        /// <remarks>
        /// Resets the Transform to its initial state: local position to zero, rotation to identity, and scale to one.
        /// This method first sets the parent, then resets, so the resulting local position, rotation, and scale are relative to the new parent.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        /// <param name="parent">要设置的新父级 Transform。 / The new parent Transform to set.</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetParentAndReset(this Transform transform, Transform parent)
        {
            transform.SetParent(parent);
            transform.Reset();
        }

        /// <summary>
        /// 将 <see cref="Transform" /> 重置为初始状态：本地坐标归零、旋转归零、缩放归一。
        /// </summary>
        /// <remarks>
        /// Resets the Transform to its initial state.
        /// Equivalent to setting localPosition = Vector3.zero, localRotation = Quaternion.identity, and localScale = Vector3.one.
        /// </remarks>
        /// <param name="transform"><see cref="Transform" /> 对象。 / The Transform instance.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}
