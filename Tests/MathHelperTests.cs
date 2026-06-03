using System;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class MathHelperTests
    {
        #region CheckIntersect (RectInt overload)

        [Test]
        public void CheckIntersect_RectInt_OverlappingRectangles_ReturnsTrue()
        {
            var src = new RectInt(0, 0, 10, 10);
            var target = new RectInt(5, 5, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_RectInt_IdenticalRectangles_ReturnsTrue()
        {
            var rect = new RectInt(0, 0, 10, 10);

            bool result = MathHelper.CheckIntersect(rect, rect);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_RectInt_OneInsideOther_ReturnsTrue()
        {
            var outer = new RectInt(0, 0, 20, 20);
            var inner = new RectInt(5, 5, 5, 5);

            bool result = MathHelper.CheckIntersect(outer, inner);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_RectInt_CompletelySeparate_ReturnsFalse()
        {
            var src = new RectInt(0, 0, 10, 10);
            var target = new RectInt(20, 20, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_RectInt_AdjacentHorizontally_ReturnsFalse()
        {
            var src = new RectInt(0, 0, 10, 10);
            var target = new RectInt(10, 0, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_RectInt_AdjacentVertically_ReturnsFalse()
        {
            var src = new RectInt(0, 0, 10, 10);
            var target = new RectInt(0, 10, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_RectInt_ZeroSizeRect_ReturnsFalse()
        {
            var src = new RectInt(0, 0, 0, 0);
            var target = new RectInt(0, 0, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_RectInt_NegativeCoordinates_ReturnsTrue()
        {
            var src = new RectInt(-5, -5, 10, 10);
            var target = new RectInt(0, 0, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_RectInt_NegativeCoordinates_Separate_ReturnsFalse()
        {
            var src = new RectInt(-20, -20, 10, 10);
            var target = new RectInt(0, 0, 10, 10);

            bool result = MathHelper.CheckIntersect(src, target);

            Assert.IsFalse(result);
        }

        #endregion

        #region CheckIntersect (coordinate overload)

        [Test]
        public void CheckIntersect_Coords_Overlapping_ReturnsTrue()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 5, 5, 10, 10);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_Coords_SameRectangle_ReturnsTrue()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 0, 0, 10, 10);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_Coords_Separate_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 20, 20, 10, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_Coords_AdjacentRight_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 10, 0, 10, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_Coords_AdjacentTop_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 0, 10, 10, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_Coords_ZeroWidth_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 0, 10, 0, 0, 10, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_Coords_ZeroHeight_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 0, 0, 0, 10, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersect_Coords_PartialOverlap_ReturnsTrue()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 8, 8, 10, 10);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_Coords_CrossShapedOverlap_ReturnsTrue()
        {
            bool result = MathHelper.CheckIntersect(0, 5, 20, 10, 8, 0, 5, 20);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersect_Coords_JustTouchingCorner_ReturnsFalse()
        {
            bool result = MathHelper.CheckIntersect(0, 0, 10, 10, 10, 10, 5, 5);

            Assert.IsFalse(result);
        }

        #endregion

        #region CheckIntersectPoints

        [Test]
        public void CheckIntersectPoints_NoOverlap_ReturnsFalse()
        {
            int[] points = new int[100];

            bool result = MathHelper.CheckIntersectPoints(0, 0, 10, 10, 20, 20, 10, 10, points);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIntersectPoints_Overlap_MarksIntersectionPoints()
        {
            int[] points = new int[100];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = 1;
            }

            bool result = MathHelper.CheckIntersectPoints(0, 0, 10, 10, 5, 5, 10, 10, points);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIntersectPoints_Overlap_PointsOutsideIntersectionRemainUnchanged()
        {
            int[] points = new int[100];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = 1;
            }

            MathHelper.CheckIntersectPoints(0, 0, 10, 10, 5, 5, 10, 10, points);

            Assert.AreEqual(0, points[5 * 10 + 5], "Point inside intersection should be cleared");
            Assert.AreEqual(0, points[9 * 10 + 9], "Point inside intersection should be cleared");
            Assert.AreEqual(1, points[0 * 10 + 0], "Point outside intersection should remain");
            Assert.AreEqual(1, points[4 * 10 + 4], "Point outside intersection should remain");
        }

        [Test]
        public void CheckIntersectPoints_FullOverlap_ClearsAllMarkedPoints()
        {
            int[] points = new int[100];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = 1;
            }

            MathHelper.CheckIntersectPoints(0, 0, 10, 10, 0, 0, 10, 10, points);

            for (int i = 0; i < points.Length; i++)
            {
                Assert.AreEqual(0, points[i], "All points should be cleared for identical rectangles");
            }
        }

        [Test]
        public void CheckIntersectPoints_AlreadyZeroPoints_StaysZero()
        {
            int[] points = new int[100];

            MathHelper.CheckIntersectPoints(0, 0, 10, 10, 5, 5, 10, 10, points);

            for (int i = 0; i < points.Length; i++)
            {
                Assert.AreEqual(0, points[i], "Zero points should remain zero");
            }
        }

        #endregion
    }
}
