
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: greedy
 * Time(nlogn), Space(1)
 */
namespace leetcode
{
    public class Lc757_Set_Intersection_Size_At_Least_Two
    {
        public int IntersectionSizeTwo(int[,] intervals)
        {
            int n = intervals.GetLength(0);
            var ps = new int[n][];
            for (int i = 0; i < n; i++)
                ps[i] = new int[] { intervals[i, 0], intervals[i, 1] };

            Array.Sort(ps, (a, b) => a[1] - b[1]);

            int res = 0, prev = -1, last = -1;
            foreach (var p in ps)
            {
                if (p[0] > last)
                {
                    prev = p[1] - 1;
                    last = p[1];
                    res += 2;
                }
                else if (p[0] > prev)
                {
                    prev = last;
                    last = p[1];
                    res++;
                }
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[,] { { 1, 3 }, { 1, 4 }, { 2, 5 }, { 3, 5 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 3);

            nums = new int[,] { { 1, 2 }, { 2, 3 }, { 2, 4 }, { 4, 5 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 5);

            nums = new int[,] { { 4, 14 }, { 6, 17 }, { 7, 14 }, { 14, 21 }, { 4, 7 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 4);

            nums = new int[,] { { 1, 15 }, { 0, 8 }, { 13, 14 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 4);
        }
    }
}
