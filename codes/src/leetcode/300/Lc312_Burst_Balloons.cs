using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dc, dp
 * Time(n^3), Space(n^2)
 * dp(i, j) = max(dp(i, k) + nums[i] * nums[k] * nums[j] + dp(k, j)), k=(i..j)
 * burst ballons(i..k) first, then (k..j), then k
 * The subproblem has to include the two guarding balloons making it n^2 complexity. It't not enough to define the subprolem dp[k] as the max points 
 * when ballon k is last burst, as it's not self contained, it depends on its external conditions, its border ballons.
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
            int n = nums.Length;
            var ns = new int[n + 2];
            Array.Copy(nums, 0, ns, 1, n);
            ns[0] = ns[n + 1] = 1;
            var dp = new int[n + 2, n + 2];

            for (int len = 1; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len + 1;
                    for (int k = i + 1; k < j; k++)
                    {
                        int c = dp[i, k] + dp[k, j] + ns[i] * ns[k] * ns[j];
                        dp[i, j] = Math.Max(dp[i, j], c);
                    }
                }
            }

            return dp[0, n + 1];
        }

        public void Test()
        {
            var nums = new int[] { 3, 1, 5, 8 };
            Console.WriteLine(MaxCoins(nums) == 167);
            Console.WriteLine(MaxCoinsBottomUp(nums) == 167);
        }
    }
}

