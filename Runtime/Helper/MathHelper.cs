using System;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 数学帮助类。
    /// </summary>
    /// <remarks>
    /// Math helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class MathHelper
    {
        /// <summary>
        /// 检查两个矩形是否相交。
        /// </summary>
        /// <remarks>
        /// Checks if two rectangles intersect.
        /// </remarks>
        /// <param name="src">源矩形 / Source rectangle</param>
        /// <param name="target">目标矩形 / Target rectangle</param>
        /// <returns>如果相交则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if they intersect; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public static bool CheckIntersect(RectInt src, RectInt target)
        {
            int minX = Math.Max(src.x, target.x);
            int minY = Math.Max(src.y, target.y);
            int maxX = Math.Min(src.x + src.width, target.x + target.width);
            int maxY = Math.Min(src.y + src.height, target.y + target.height);
            if (minX >= maxX || minY >= maxY)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检查两个矩形是否相交。
        /// </summary>
        /// <remarks>
        /// Checks if two rectangles intersect based on their coordinates and dimensions.
        /// </remarks>
        /// <param name="x1">第一个矩形的X坐标 / X coordinate of first rectangle</param>
        /// <param name="y1">第一个矩形的Y坐标 / Y coordinate of first rectangle</param>
        /// <param name="w1">第一个矩形的宽度 / Width of first rectangle</param>
        /// <param name="h1">第一个矩形的高度 / Height of first rectangle</param>
        /// <param name="x2">第二个矩形的X坐标 / X coordinate of second rectangle</param>
        /// <param name="y2">第二个矩形的Y坐标 / Y coordinate of second rectangle</param>
        /// <param name="w2">第二个矩形的宽度 / Width of second rectangle</param>
        /// <param name="h2">第二个矩形的高度 / Height of second rectangle</param>
        /// <returns>如果相交则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if they intersect; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public static bool CheckIntersect(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            int minX = Math.Max(x1, x2);
            int minY = Math.Max(y1, y2);
            int maxX = Math.Min(x1 + w1, x2 + w2);
            int maxY = Math.Min(y1 + h1, y2 + h2);
            if (minX >= maxX || minY >= maxY)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检查两个矩形是否相交，并返回相交的区域。
        /// </summary>
        /// <remarks>
        /// Checks if two rectangles intersect and returns the intersection area.
        /// </remarks>
        /// <param name="x1">第一个矩形的X坐标 / X coordinate of first rectangle</param>
        /// <param name="y1">第一个矩形的Y坐标 / Y coordinate of first rectangle</param>
        /// <param name="w1">第一个矩形的宽度 / Width of first rectangle</param>
        /// <param name="h1">第一个矩形的高度 / Height of first rectangle</param>
        /// <param name="x2">第二个矩形的X坐标 / X coordinate of second rectangle</param>
        /// <param name="y2">第二个矩形的Y坐标 / Y coordinate of second rectangle</param>
        /// <param name="w2">第二个矩形的宽度 / Width of second rectangle</param>
        /// <param name="h2">第二个矩形的高度 / Height of second rectangle</param>
        /// <param name="rect">输出的相交区域 / Output intersection rectangle</param>
        /// <returns>如果相交则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if they intersect; otherwise <c>false</c></returns>
        private static bool CheckIntersect(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2, out RectInt rect)
        {
            rect = default;
            int minX = Math.Max(x1, x2);
            int minY = Math.Max(y1, y2);
            int maxX = Math.Min(x1 + w1, x2 + w2);
            int maxY = Math.Min(y1 + h1, y2 + h2);
            if (minX >= maxX || minY >= maxY)
            {
                return false;
            }

            rect.x = minX;
            rect.y = minY;
            rect.width = Math.Abs(maxX - minX);
            rect.height = Math.Abs(maxY - minY);
            return true;
        }

        /// <summary>
        /// 检查两个矩形相交的点。
        /// </summary>
        /// <remarks>
        /// Checks intersection points between two rectangles and marks intersected points.
        /// </remarks>
        /// <param name="x1">矩形A的X坐标 / X coordinate of rectangle A</param>
        /// <param name="y1">矩形A的Y坐标 / Y coordinate of rectangle A</param>
        /// <param name="w1">矩形A的宽度 / Width of rectangle A</param>
        /// <param name="h1">矩形A的高度 / Height of rectangle A</param>
        /// <param name="x2">矩形B的X坐标 / X coordinate of rectangle B</param>
        /// <param name="y2">矩形B的Y坐标 / Y coordinate of rectangle B</param>
        /// <param name="w2">矩形B的宽度 / Width of rectangle B</param>
        /// <param name="h2">矩形B的高度 / Height of rectangle B</param>
        /// <param name="intersectPoints">交叉点列表（会被修改）/ Intersection point list (will be modified)</param>
        /// <returns>返回是否相交 / Returns whether the rectangles intersect</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool CheckIntersectPoints(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2, int[] intersectPoints)
        {
            Vector2Int dPt = new Vector2Int();

            if (false == CheckIntersect(x1, y1, w1, h1, x2, y2, w2, h2, out var rectInt))
            {
                return false;
            }

            for (var i = 0; i < w1; i++)
            {
                for (var n = 0; n < h1; n++)
                {
                    if (intersectPoints[i * h1 + n] == 1)
                    {
                        dPt.x = x1 + i;
                        dPt.y = y1 + n;
                        if (rectInt.Contains(dPt))
                        {
                            intersectPoints[i * h1 + n] = 0;
                        }
                    }
                }
            }

            return true;
        }
    }
}