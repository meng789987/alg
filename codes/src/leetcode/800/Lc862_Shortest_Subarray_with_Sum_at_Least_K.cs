using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: queue
 * 1. enqueue every pre-sum [index], dequeue all same or bigger item before pushing
 * 2. from the low end, compare the subarray length and dequeue if current_presum - lowest_presum
 */
namespace leetcode
{
    public class Lc862_Shortest_Subarray_with_Sum_at_Least_K
    {
        public int ShortestSubarray(int[] A, int K)
        {
            int n = A.Length;
            var presums = new int[n + 1];
            for (int i = 0; i < n; i++)
                presums[i + 1] = presums[i] + A[i];

            var indice = new int[n + 1];
            int lo = 0, hi = -1;

            int ret = int.MaxValue;
            for (int i = 0; i <= n; i++)
            {
                while (lo <= hi && presums[i] <= presums[indice[hi]]) hi--;
                while (lo <= hi && presums[i] - presums[indice[lo]] >= K)
                    ret = Math.Min(ret, i - indice[lo++]);

                indice[++hi] = i;
            }

            return ret == int.MaxValue ? -1 : ret;
        }

        public void Test()
        {
            Console.WriteLine(ShortestSubarray(new int[] { 1 }, 1) == 1);
            Console.WriteLine(ShortestSubarray(new int[] { 1, 2 }, 4) == -1);
            Console.WriteLine(ShortestSubarray(new int[] { 2, -1, 2 }, 3) == 3);
            Console.WriteLine(ShortestSubarray(new int[] { 56, -21, 56, 35, -9 }, 61) == 2);
        }
    }
}

