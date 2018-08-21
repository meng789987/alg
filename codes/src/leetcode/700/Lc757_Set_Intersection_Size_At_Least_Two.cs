
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: greedy
 * Time(n^2), Space(n)
 */
namespace leetcode
{
    public class Lc757_Set_Intersection_Size_At_Least_Two
    {
        public int IntersectionSizeTwo(int[,] intervals)
        {
            int n = intervals.GetLength(0);
            var ps = new int[n * 2][]; // {x, entering(0)/leaving(1), first(0)/second(1) round, index}
            for (int i = 0; i < n; i++)
            {
                ps[i * 2] = new int[] { intervals[i, 0], 0, 0, i };
                ps[i * 2 + 1] = new int[] { intervals[i, 1], 1, 0, i };
            }
            Array.Sort(ps, (a, b) => a[0] != b[0] ? a[0] - b[0] : b[1] - a[1]); // leaving first

            var map = new int[n, 2];
            for (int i = 0; i < ps.Length; i++)
                map[ps[i][3], ps[i][1]] = i;

            int res = 0;
            for (int r = 0; r <= 1; r++)
            {
                while (true)
                {
                    int x = 0;
                    var max = new List<int>();
                    var cur = new HashSet<int>();
                    var sameLeaving = new List<int>();
                    foreach (var p in ps)
                    {
                        if (p[2] != r) continue;
                        if (p[1] == 0) cur.Add(p[3]);
                        else cur.Remove(p[3]);

                        if (sameLeaving.Count > 0 && ps[map[sameLeaving.Last(), 1]][0] != p[0])
                            sameLeaving.Clear();
                        if (p[1] == 1) sameLeaving.Add(p[3]);

                        if (max.Count < cur.Count + sameLeaving.Count)
                        {
                            x = p[0];
                            max = cur.ToList();
                            max.AddRange(sameLeaving);
                        }
                    }

                    if (max.Count == 0) break;
                    res++;

                    foreach (var i in max)
                    {
                        int[] enter = ps[map[i, 0]], leave = ps[map[i, 1]];
                        if (enter[0] == x) enter[0]++;
                        else if (leave[0] == x) leave[0]--;
                        enter[2] = leave[2] = r + 1;
                    }
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
        }
    }
}
