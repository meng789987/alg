using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: greedy, heap
 * Time(klogn), Space(n)
 */
namespace leetcode
{
    public class Lc502_IPO
    {
        public int FindMaximizedCapital(int k, int W, int[] Profits, int[] Capital)
        {
            var proSet = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => a[0] != b[0] ? a[0] - b[0] : a[2] - b[2]));
            var capSet = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => a[1] != b[1] ? a[1] - b[1] : a[2] - b[2]));
            for (int i = 0; i < Profits.Length; i++)
                capSet.Add(new int[] { Profits[i], Capital[i], i });

            for (int s = 0; s < k; s++)
            {
                while (capSet.Count > 0 && capSet.Min[1] <= W)
                {
                    proSet.Add(capSet.Min);
                    capSet.Remove(capSet.Min);
                }

                if (proSet.Count == 0) break;

                W += proSet.Max[0];
                proSet.Remove(proSet.Max);
            }

            return W;
        }

        public void Test()
        {
            var profits = new int[] { 1, 2, 3 };
            var capitals = new int[] { 0, 1, 1 };
            Console.WriteLine(FindMaximizedCapital(2, 0, profits, capitals) == 4);

            profits = new int[] { 1, 2, 3 };
            capitals = new int[] { 0, 1, 2 };
            Console.WriteLine(FindMaximizedCapital(12, 0, profits, capitals) == 6);
        }
    }
}

