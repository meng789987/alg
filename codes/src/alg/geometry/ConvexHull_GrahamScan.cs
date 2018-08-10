using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: convex hull
 * Time(nlogn), Space(n)
 * Graham Scan to construct the convex hull
 * 1. select the bottom-most point as p0
 * 2. sort the other points based on its polar angle from p0, if two points have same angle, only leave the further one
 * 3. put p0, p1, p2 into a stack, loop the remain points from p3 to pn and then p0,
 * 4. extract top until (2ndTop, top, pi) is in counter clockwise (left turn), put pi into stack.
 */
namespace alg.geometry
{
    public class ConvexHull_GrahamScan
    {
        public Point[] ConvexHull(Point[] points)
        {
            // 1. select the bottom-most point as p0
            int idx = 0;
            for (int i = 1; i < points.Length; i++)
                if (points[i].y < points[idx].y) idx = i;

            // 2. sort the other points based on its polar angle from p0, if two points have same angle, only leave the further one
            var ps = Sort(points, idx);
            if (ps.Length <= 3) return ps;

            // 3. put p0, p1, p2 into a stack, loop the remain points from p3 to pn and then p0,
            // 4. extract top until (2ndTop, top, pi) is in counter clockwise (left turn), put pi into stack.
            var stack = new Stack<Point>();
            stack.Push(ps[0]);
            stack.Push(ps[1]);
            stack.Push(ps[2]);
            for (int i = 3; i < ps.Length; i++)
            {
                var top = stack.Pop();
                while (GeoCommon.Orientation(stack.Peek(), top, ps[i]) <= 0)
                    top = stack.Pop();
                stack.Push(top);
                stack.Push(ps[i]);
            }

            return stack.ToArray().Reverse().ToArray();
        }

        Point[] Sort(Point[] points, int bottomMostIdex)
        {
            var p = points[bottomMostIdex];
            points[bottomMostIdex] = points[0];
            points[0] = p;

            var sortedPoints = new SortedSet<Point>(Comparer<Point>.Create((a, b) =>
            {
                int aq = GeoCommon.Quadrant(a);
                int bq = GeoCommon.Quadrant(b);
                if (aq != bq) return aq - bq;

                return (a.y - p.y) * (b.x - p.x) - (b.y - p.y) * (a.x - p.x);
            }));

            Point tmpPoint;
            for (int i = 1; i < points.Length; i++)
            {
                if (sortedPoints.TryGetValue(points[i], out tmpPoint) &&
                        (tmpPoint.x - p.x) * (tmpPoint.x - p.x) + (tmpPoint.y - p.y) * (tmpPoint.y - p.y) <
                        (points[i].x - p.x) * (points[i].x - p.x) + (points[i].y - p.y) * (points[i].y - p.y))
                    sortedPoints.Remove(tmpPoint);
                sortedPoints.Add(points[i]);
            }

            var res = new List<Point>(sortedPoints.Count + 1);
            res.Add(p);
            res.AddRange(sortedPoints);
            return res.ToArray();
        }

        public void Test()
        {
            var points = new Point[] {
                new Point( 0, 3), new Point( 1, 1), new Point(2, 2), new Point(4, 4),
                new Point(0, 0 ), new Point(1, 2), new Point(3, 1 ), new Point(3, 3 ) };
            var exp = new Point[] { new Point(0, 0), new Point(3, 1), new Point(4, 4), new Point(0, 3) };
            Console.WriteLine(exp.SequenceEqual(ConvexHull(points)));
        }
    }
}
