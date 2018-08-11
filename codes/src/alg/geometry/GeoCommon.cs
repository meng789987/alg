using System;
using System.Collections.Generic;
using System.Text;

namespace alg.geometry
{
    public static class GeoCommon
    {
        /*
	     * to compare the slope of line (a, b)[(b.y-a.y)/(b.x-a.x)] to the slope of line (b, c)[(c.y-b.y)/(c.x-b.x)]
	     * return:
	     * -1: if (a, b, c) is in clockwise or right turn
	     * 0 : if (a, b, c) is in collinear
	     * 1 : if (a, b, c) is in counter clockwise or left turn
	     */
        public static int Orientation(Point a, Point b, Point c)
        {
            // slope(b, c) - slope(a, b)
            var diff = (c.y - b.y) * (b.x - a.x) - (b.y - a.y) * (c.x - b.x);
            return diff < 0 ? -1 : diff > 0 ? 1 : 0;
        }

        /*
         * quadrant of a point 
         */
        public static int Quadrant(Point p)
        {
            if (p.x >= 0 && p.y >= 0) return 1;
            if (p.x <= 0 && p.y >= 0) return 2;
            if (p.x <= 0 && p.y <= 0) return 3;
            return 4;
        }

        /*
         * if c is on segment (a, b)
         */
        public static bool IsOnSegment(Point a, Point b, Point c)
        {
            return Math.Min(a.x, b.x) <= c.x && c.x <= Math.Max(a.x, b.x)
                && Math.Min(a.y, b.y) <= c.y && c.y <= Math.Max(a.y, b.y)
                && Orientation(a, b, c) == 0;
        }

        /*
         * if segment (a1, a2) intersect with (b1, b2)
         */
        public static bool IsSegmentIntersect(Point a1, Point a2, Point b1, Point b2)
        {
            // general case
            if (Orientation(a1, a2, b1) != Orientation(a1, a2, b2) &&
                Orientation(b1, b2, a1) != Orientation(b1, b2, a2))
                return true;

            // collinear
            if (IsOnSegment(a1, a2, b1) || IsOnSegment(a1, a2, b2) ||
                IsOnSegment(b1, b2, a1) || IsOnSegment(b1, b2, a2))
                return true;

            return false;
        }

        /*
         * distance formula from point c to line (a, b)
         * https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
         */
        public static double DistanceSquare(Point a, Point b, Point c)
        {
            double dist = (b.y - a.y) * c.x - (b.x - a.x) * c.y + b.x * a.y - b.y * a.x;
            return dist * dist / DistanceSquare(a, b);
        }

        public static int DistanceSquare(Point a, Point b)
        {
            return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
        }

        public static void Test()
        {
            Console.WriteLine(IsSegmentIntersect(new Point(1, 1), new Point(10, 1), new Point(1, 2), new Point(10, 2)) == false);
            Console.WriteLine(IsSegmentIntersect(new Point(10, 0), new Point(0, 10), new Point(0, 0), new Point(10, 10)) == true);
            Console.WriteLine(IsSegmentIntersect(new Point(-5, -5), new Point(0, 0), new Point(1, 1), new Point(10, 10)) == false);
        }
    }
}
