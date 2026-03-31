using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 坐标帮助类。
    /// </summary>
    /// <remarks>
    /// Position helper class for coordinate transformations.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class PositionHelper
    {
        /// <summary>
        /// 将二维坐标转换为三维坐标，Y轴设为0。
        /// </summary>
        /// <remarks>
        /// Converts a 2D coordinate to a 3D coordinate with Y set to 0.
        /// </remarks>
        /// <param name="pos">二维坐标 / 2D coordinate</param>
        /// <returns>三维坐标 / 3D coordinate</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 RayCastV2ToV3(Vector2 pos)
        {
            return new Vector3(pos.x, 0, pos.y);
        }

        /// <summary>
        /// 将X和Y坐标转换为三维坐标，Y轴设为0。
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <returns>三维坐标</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 RayCastXYToV3(float x, float y)
        {
            return new Vector3(x, 0, y);
        }

        /// <summary>
        /// 将三维坐标的Y轴设为0。
        /// </summary>
        /// <remarks>
        /// Sets the Y component of a 3D coordinate to 0.
        /// </remarks>
        /// <param name="pos">三维坐标 / 3D coordinate</param>
        /// <returns>修改后的三维坐标 / Modified 3D coordinate with Y=0</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 RayCastV3ToV3(Vector3 pos)
        {
            return new Vector3(pos.x, 0, pos.z);
        }

        /// <summary>
        /// 将角度转换为四元数。
        /// </summary>
        /// <remarks>
        /// Converts an angle (in degrees) to a quaternion rotation.
        /// </remarks>
        /// <param name="angle">角度值 / Angle value in degrees</param>
        /// <returns>对应的四元数 / Corresponding quaternion</returns>
        [UnityEngine.Scripting.Preserve]
        public static Quaternion AngleToQuaternion(int angle)
        {
            return Quaternion.AngleAxis(-angle, Vector3.up) * Quaternion.AngleAxis(90, Vector3.up);
        }

        /// <summary>
        /// 根据源向量和目标向量计算四元数。
        /// </summary>
        /// <remarks>
        /// Calculates a quaternion that rotates from the source vector to the target direction.
        /// </remarks>
        /// <param name="source">源向量位置 / Source position</param>
        /// <param name="dire">目标向量位置 / Target position</param>
        /// <returns>对应的四元数 / Quaternion representing the rotation</returns>
        [UnityEngine.Scripting.Preserve]
        public static Quaternion GetVector3ToQuaternion(Vector3 source, Vector3 dire)
        {
            Vector3 nowPos = source;
            if (nowPos == dire)
            {
                return new Quaternion();
            }

            Vector3 direction = (dire - nowPos).normalized;
            return Quaternion.LookRotation(direction, Vector3.up);
        }

        /// <summary>
        /// 计算二维距离（忽略Y轴）。
        /// </summary>
        /// <remarks>
        /// Calculates the 2D distance between two 3D points, ignoring the Y axis.
        /// </remarks>
        /// <param name="v1">第一个三维坐标 / First 3D coordinate</param>
        /// <param name="v2">第二个三维坐标 / Second 3D coordinate</param>
        /// <returns>两点之间的二维距离 / 2D distance between the two points</returns>
        [UnityEngine.Scripting.Preserve]
        public static float Distance2D(Vector3 v1, Vector3 v2)
        {
            Vector2 d1 = new Vector2(v1.x, v1.z);
            Vector2 d2 = new Vector2(v2.x, v2.z);
            return Vector2.Distance(d1, d2);
        }

        /// <summary>
        /// 根据角度获取四元数。
        /// </summary>
        /// <remarks>
        /// Converts an angle (in degrees) to a quaternion rotation.
        /// </remarks>
        /// <param name="angle">角度值 / Angle value in degrees</param>
        /// <returns>对应的四元数 / Corresponding quaternion</returns>
        [UnityEngine.Scripting.Preserve]
        public static Quaternion GetAngleToQuaternion(float angle)
        {
            return Quaternion.AngleAxis(-angle, Vector3.up) * Quaternion.AngleAxis(90, Vector3.up);
        }

        /// <summary>
        /// 计算从一个向量到另一个向量的360度角度。
        /// </summary>
        /// <remarks>
        /// Calculates the 360-degree angle from one vector to another.
        /// </remarks>
        /// <param name="from">起始向量 / Starting vector</param>
        /// <param name="to">目标向量 / Target vector</param>
        /// <returns>360度角度值 / 360-degree angle value</returns>
        [UnityEngine.Scripting.Preserve]
        public static float Vector3ToAngle360(Vector3 from, Vector3 to)
        {
            float angle = Vector3.Angle(from, to);
            Vector3 cross = Vector3.Cross(from, to);
            return cross.y > 0 ? angle : 360 - angle;
        }

        /// <summary>
        /// 求点到直线的距离。
        /// </summary>
        /// <remarks>
        /// Calculates the distance from a point to a line using the formula: Ax+By+C = 0; d = A*p.x + B*p.y + C / sqrt(A^2 + B^2)
        /// </remarks>
        /// <param name="startPoint">线的起点 / Line start point</param>
        /// <param name="endPoint">线的终点 / Line end point</param>
        /// <param name="point">要计算的点 / Point to calculate distance for</param>
        /// <returns>点到直线的距离 / Distance from point to line</returns>
        [UnityEngine.Scripting.Preserve]
        public static float DistanceOfPointToVector(Vector3 startPoint, Vector3 endPoint, Vector3 point)
        {
            Vector2 startVe2 = startPoint.IgnoreYAxis();
            Vector2 endVe2 = endPoint.IgnoreYAxis();
            float A = endVe2.y - startVe2.y;
            float B = startVe2.x - endVe2.x;
            float C = endVe2.x * startVe2.y - startVe2.x * endVe2.y;
            float denominator = Mathf.Sqrt(A * A + B * B);
            Vector2 pointVe2 = point.IgnoreYAxis();
            return Mathf.Abs((A * pointVe2.x + B * pointVe2.y + C) / denominator);
        }

        /// <summary>
        /// 判断射线是否碰撞到球体。
        /// </summary>
        /// <remarks>
        /// Determines if a ray intersects with a sphere and returns the distance to the intersection point.
        /// </remarks>
        /// <param name="ray">射线 / Ray to test</param>
        /// <param name="center">球体中心点 / Sphere center point</param>
        /// <param name="redis">球体半径 / Sphere radius</param>
        /// <param name="dist">输出：射线起点到碰撞点的距离 / Output: distance from ray origin to intersection point</param>
        /// <returns>如果碰撞到返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if intersects; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public static bool RayCastSphere(Ray ray, Vector3 center, float redis, out float dist)
        {
            dist = 0;
            Vector3 ma = center - ray.origin;
            float distance = Vector3.Cross(ma, ray.direction).magnitude / ray.direction.magnitude;
            if (distance < redis)
            {
                float op = PythagoreanTheorem(Vector3.Distance(center, ray.origin), distance);
                float rp = PythagoreanTheorem(redis, distance);
                dist = op - rp;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 勾股定理计算。
        /// </summary>
        /// <remarks>
        /// Applies the Pythagorean theorem to calculate the hypotenuse: sqrt(x^2 + y^2).
        /// </remarks>
        /// <param name="x">边长x / Side length x</param>
        /// <param name="y">边长y / Side length y</param>
        /// <returns>斜边长度 / Hypotenuse length</returns>
        [UnityEngine.Scripting.Preserve]
        public static float PythagoreanTheorem(float x, float y)
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// 去掉三维向量的Y轴，把向量投射到XZ平面。
        /// </summary>
        /// <remarks>
        /// Removes the Y component from a 3D vector, projecting it onto the XZ plane.
        /// </remarks>
        /// <param name="vector3">三维向量 / 3D vector</param>
        /// <returns>投影后的二维向量 / Projected 2D vector</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector2 IgnoreYAxis(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        /// <summary>
        /// 判断目标点是否位于向量的左边。
        /// </summary>
        /// <remarks>
        /// Determines if the target point is on the left side of the vector.
        /// </remarks>
        /// <param name="vector3">方向向量 / Direction vector</param>
        /// <param name="originPoint">原点 / Origin point</param>
        /// <param name="point">目标点 / Target point</param>
        /// <returns>如果在左边返回 <c>true</c>；如果在右边返回 <c>false</c> / <c>true</c> if on left; <c>false</c> if on right</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool PointOnLeftSideOfVector(this Vector3 vector3, Vector3 originPoint, Vector3 point)
        {
            Vector2 originVec2 = originPoint.IgnoreYAxis();

            Vector2 pointVec2 = (point.IgnoreYAxis() - originVec2).normalized;

            Vector2 vector2 = vector3.IgnoreYAxis();

            float verticalX = originVec2.x;

            float verticalY = (-verticalX * vector2.x) / vector2.y;

            Vector2 norVertical = (new Vector2(verticalX, verticalY)).normalized;

            float dotValue = Vector2.Dot(norVertical, pointVec2);

            return dotValue < 0f;
        }
    }
}