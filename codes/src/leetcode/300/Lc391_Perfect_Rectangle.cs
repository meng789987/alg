using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: math/hash, topological sort/sweep line
 * hash: Time(n), Space(n)
 * sort: Time(nlogn), Space(n)
 */
namespace leetcode
{
    public class Lc391_Perfect_Rectangle
    {
        /*
         * only 2 or 4 rectangles can adhere together
         * use a hashset to remove the case of 2, then the rest should be the four corner-most points.
         */
        public bool IsRectangleCover(int[,] rectangles)
        {
            int n = rectangles.GetLength(0);
            var edge = new int[] { int.MaxValue, int.MaxValue, int.MinValue, int.MinValue };
            var rs = new HashSet<Tuple<int, int>>();

            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += (rectangles[i, 2] - rectangles[i, 0]) * (rectangles[i, 3] - rectangles[i, 1]);
                edge[0] = Math.Min(edge[0], rectangles[i, 0]);
                edge[1] = Math.Min(edge[1], rectangles[i, 1]);
                edge[2] = Math.Max(edge[2], rectangles[i, 2]);
                edge[3] = Math.Max(edge[3], rectangles[i, 3]);

                var p0 = new Tuple<int, int>(rectangles[i, 0], rectangles[i, 1]);
                var p1 = new Tuple<int, int>(rectangles[i, 0], rectangles[i, 3]);
                var p2 = new Tuple<int, int>(rectangles[i, 2], rectangles[i, 1]);
                var p3 = new Tuple<int, int>(rectangles[i, 2], rectangles[i, 3]);

                if (!rs.Add(p0)) rs.Remove(p0);
                if (!rs.Add(p1)) rs.Remove(p1);
                if (!rs.Add(p2)) rs.Remove(p2);
                if (!rs.Add(p3)) rs.Remove(p3);
            }

            return (edge[2] - edge[0]) * (edge[3] - edge[1]) == sum && rs.Count == 4
                && rs.Contains(new Tuple<int, int>(edge[0], edge[1]))
                && rs.Contains(new Tuple<int, int>(edge[0], edge[3]))
                && rs.Contains(new Tuple<int, int>(edge[2], edge[1]))
                && rs.Contains(new Tuple<int, int>(edge[2], edge[3]));
        }

        public bool IsRectangleCoverTopsort(int[,] rectangles)
        {
            int n = rectangles.GetLength(0);

            // 1. check if sum of area of all rect is the area of outfit
            int sum = 0;
            var edge = new int[] { int.MaxValue, int.MaxValue, int.MinValue, int.MinValue };
            for (int i = 0; i < n; i++)
            {
                sum += (rectangles[i, 2] - rectangles[i, 0]) * (rectangles[i, 3] - rectangles[i, 1]);
                edge[0] = Math.Min(edge[0], rectangles[i, 0]);
                edge[1] = Math.Min(edge[1], rectangles[i, 1]);
                edge[2] = Math.Max(edge[2], rectangles[i, 2]);
                edge[3] = Math.Max(edge[3], rectangles[i, 3]);
            }
            if ((edge[2] - edge[0]) * (edge[3] - edge[1]) != sum)
                return false;

            // 2. check if overlap

            // 2.1 sort rectangles by its two x. leaving precedes entering.
            // {index; flag(0:leaving, 1:entering), x}
            var rectx = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) =>
                    a[2] != b[2] ? a[2] - b[2] : a[1] != b[1] ? a[1] - b[1] : a[0] - b[0]));
            for (int i = 0; i < n; i++)
            {
                rectx.Add(new int[] { i, 1, rectangles[i, 0] });
                rectx.Add(new int[] { i, 0, rectangles[i, 2] });
            }

            // 2.2 sort the current rects with overlapping x by y, and check if the neighbors are overlapped
            var recty = new SortedSet<int>(Comparer<int>.Create((a, b) =>
            {
                if (rectangles[a, 3] <= rectangles[b, 1]) return -1;
                if (rectangles[b, 3] <= rectangles[a, 1]) return 1;
                return 0;
            }));

            // 2.3 scan from left to right, if a rect enters, add it to the current rect set; if a rect leave, remove it from rects
            foreach (var x in rectx)
            {
                if (x[1] == 0) recty.Remove(x[0]);
                else if (!recty.Add(x[0])) return false;
            }

            return true;
        }

        public void Test()
        {
            var rect = new int[,] { { 1, 1, 3, 3 }, { 3, 1, 4, 2}, { 3, 2, 4, 4}, { 1, 3, 2, 4}, { 2, 3, 3, 4} };
            Console.WriteLine(IsRectangleCover(rect) == true);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == true);

            rect = new int[,] { {1,1,2,3}, {1,3,2,4}, {3,1,4,2}, {3,2,4,4} };
            Console.WriteLine(IsRectangleCover(rect) == false);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == false);

            rect = new int[,] { {1,1,3,3}, {3,1,4,2}, {1,3,2,4}, {3,2,4,4} };
            Console.WriteLine(IsRectangleCover(rect) == false);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == false);

            rect = new int[,] { {1,1,3,3}, {3,1,4,2}, {1,3,2,4}, {2,2,4,4} };
            Console.WriteLine(IsRectangleCover(rect) == false);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == false);

            rect = new int[,] {
                {0,0,4,1},{7,0,8,2},{6,2,8,3},{5,1,6,3},
                {4,0,5,1},{6,0,7,2},{4,2,5,3},{2,1,4,3},
                {0,1,2,2},{0,2,2,3},{4,1,5,2},{5,0,6,1} };
            Console.WriteLine(IsRectangleCover(rect) == true);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == true);

            rect = new int[,] { {0, 0, 1, 1},{0,1,3,2},{1,0,2,2} };
            Console.WriteLine(IsRectangleCover(rect) == false);
            Console.WriteLine(IsRectangleCoverTopsort(rect) == false);
        }
    }
}

