using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bs, dp
 * bs: Time(nlogw), Space(1)
 * dp[i, m] = min(max(dp[j, m-1], sum[j+1..i])), j=[0..i]
 */
namespace leetcode
{
    public class Lc410_Split_Array_Largest_Sum
    {

        public int SplitArrayBs(int[] nums, int m)
        {
            long max = 0;
            long sum = 0;
            foreach (var num in nums)
            {
                max = Math.Max(max, num);
                sum += num;
            }

            long lo = max, hi = sum;
            while (lo < hi)
            {
                long mid = (lo + hi) / 2;
                if (IsGoodSize(nums, m, mid)) hi = mid;
                else lo = mid + 1;
            }

            return (int)hi;
        }

        bool IsGoodSize(int[] nums, int m, long size)
        {
            long sum = 0;
            foreach (var num in nums)
            {
                sum += num;
                if (sum > size)
                {
                    sum = num;
                    if (--m == 0) return false;
                }
            }

            return true;
        }

        public int SplitArrayDp(int[] nums, int m)
        {
            int n = nums.Length;
            var sums = new long[n + 1];
            for (int i = 0; i < n; i++)
                sums[i + 1] = sums[i] + nums[i];

            var dp = new long[n + 1, m + 1];
            for (int i = 1; i <= n; i++)
            {
                dp[i, 1] = sums[i];
                for (int k = 2; k <= m; k++)
                {
                    dp[i, k] = dp[i, k - 1];
                    for (int j = i - 1; j >= 0 && sums[i] - sums[j] < dp[i, k]; j--)
                        dp[i, k] = Math.Min(dp[i, k], Math.Max(dp[j, k - 1], sums[i] - sums[j]));
                }
            }

            return (int)dp[n, m];
        }

        public void Test()
        {
            var nums = new int[] { 7, 2, 5, 10, 8 };
            Console.WriteLine(SplitArrayBs(nums, 2) == 18);
            Console.WriteLine(SplitArrayDp(nums, 2) == 18);
        }
    }
}

