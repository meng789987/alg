using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dc, dp
 * Time(n^3), Space(n^2)
 * dp(i, j) = max(dp(i, k) + nums[i] * nums[k] * nums[j] + dp(k, j)), k=(i..j)
 * burst ballons(i..k) first, then (k..j), then k
 */
namespace leetcode
{
    public class Lc312_Burst_Balloons
    {
        public int MaxCoins(int[] nums)
        {
            var ns = new int[nums.Length + 2];
            int n = 1;
            foreach (var num in nums)
                if (num != 0) ns[n++] = num; // burst 0 first
            ns[0] = ns[n++] = 1;

            return MaxCoinsTopdown(ns, n, 0, n - 1, new int[n, n]);
        }

        int MaxCoinsTopdown(int[] nums, int n, int lo, int hi, int[,] memo)
        {
            if (lo >= hi - 1) return 0;
            if (memo[lo, hi] != 0) return memo[lo, hi];

            int max = 0;
            for (int k = lo + 1; k < hi; k++)
            {
                int c = MaxCoinsTopdown(nums, n, lo, k, memo) + MaxCoinsTopdown(nums, n, k, hi, memo)
                    + nums[lo] * nums[k] * nums[hi];
                if (max < c) max = c;
            }
            memo[lo, hi] = max;
            return max;
        }

        public int MaxCoinsBottomUp(int[] nums)
        {
            var ns = new int[nums.Length + 2];
            int n = 1;
            foreach (var num in nums)
                if (num != 0) ns[n++] = num; // burst 0 first
            ns[0] = ns[n++] = 1;

            return MaxCoinsTopdown(ns, n, 0, n - 1, new int[n, n]);
        }

        public void Test()
        {
            var nums = new int[] { 3, 1, 5, 8 };
            Console.WriteLine(MaxCoins(nums) == 167);
        }
    }
}

