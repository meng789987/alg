using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: convex hull
 * Time(nlogn), Space(n)
 * Graham Scan to construct the convex hull
 * 1. select the bottom-most point as p0
 * 2. sort the other points based on its polar angle from p0, if two points have same angle, only leave the farther one
 * 3. put p0, p1, p2 into a stack, loop the remain points from p3 to pn and then p0,
 * 4. extract top until (2ndTop, top, pi) is in counter clockwise (left turn), put pi into stack.
 */
namespace alg.geometry
{
    public class ConvexHull_GrahamScan
    {
        public IList<Point> ConvexHull(Point[] points)
        {
            if (points.Length <= 3) return points;

            // 1. select the bottom-most point as p0
            var bm = points[0];
            foreach (var p in points)
                if (p.y < bm.y || (p.y == bm.y && p.x < bm.x)) bm = p;

            // 2. sort the other points based on its polar angle from p0
            Array.Sort(points, (a, b) =>
            {
                int aq = GeoCommon.Quadrant(a);
                int bq = GeoCommon.Quadrant(b);
                if (aq != bq) return aq - bq;

                var ori = GeoCommon.Orientation(bm, a, b);
                if (ori != 0) return -ori;

                return GeoCommon.DistanceSquare(bm, a) - GeoCommon.DistanceSquare(bm, b);
            });

            // 3. put p0, p1 into a stack, loop the remain points from p2 to pn and then p0,
            // 4. extract top until (2ndTop, top, pi) is in counter clockwise (left turn), put pi into stack.
            var stack = new Stack<Point>();
            stack.Push(points[0]);
            stack.Push(points[1]);
            for (int i = 2; i < points.Length; i++)
            {
                // if two points have same angle, only leave the farther one
                if (i < points.Length - 1 && GeoCommon.Orientation(bm, points[i], points[i + 1]) == 0)
                    continue;

                var top = stack.Pop();
                while (stack.Count > 0 && GeoCommon.Orientation(stack.Peek(), top, points[i]) <= 0)
                    top = stack.Pop();
                stack.Push(top);
                stack.Push(points[i]);
            }

            return stack.ToList();
        }

        public void Test()
        {
            var points = Point.FromMatrix(new int[,] { { 0, 3 }, { 1, 1 }, { 2, 2 }, { 4, 4 }, { 0, 0 }, { 1, 2 }, { 3, 1 }, { 3, 3 } });
            var exp = Point.FromMatrix(new int[,] { { 0, 0 }, { 3, 1 }, { 4, 4 }, { 0, 3 } });
            Console.WriteLine(exp.SameSet(ConvexHull(points)));

            points = Point.FromMatrix(new int[,] { { 4, 0 }, { 3, 0 }, { 5, 0 }, { 6, 1 }, { 7, 2 }, { 7, 3 }, { 7, 4 }, { 6, 5 }, { 5, 5 },
                { 4, 5 }, { 3, 5 }, { 2, 5 }, { 1, 4 }, { 1, 3 }, { 1, 2 }, { 2, 1 }, { 4, 2 }, { 0, 3 } });
            exp = Point.FromMatrix(new int[,] { { 0, 3 }, { 3, 0 }, { 5, 0 }, { 7, 2 }, { 7, 4 }, { 6, 5 }, { 2, 5 } });
            Console.WriteLine(exp.SameSet(ConvexHull(points)));
        }
    }
}
