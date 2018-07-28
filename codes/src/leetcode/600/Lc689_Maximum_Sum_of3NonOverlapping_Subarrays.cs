using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(n), Space(n)
 * dp[i, c] is the max sum of c k-element subarrays within [0..i],  
 * dp[i, c] = max(dp[i-1, c], sum[i-k+1..i] + dp[i-k, c-1]), where c=[1..3]
 */
namespace leetcode
{
    public class Lc689_Maximum_Sum_of3NonOverlapping_Subarrays
    {
        public int[] MaxSumOfThreeSubarrays(int[] nums, int k)
        {
            int n = nums.Length;
            int m = 3;
            // second dimension: three sums, one more to guard; 
            var dp = new int[n, m + 1];
            var idx = new int[n, m + 1];

            for (int c = 1; c <= m; c++)
            {
                int sum = dp[k - 1, c] = nums.Take(k).Sum();
                for (int i = k; i < n; i++)
                {
                    sum += nums[i] - nums[i - k];
                    dp[i, c] = dp[i - 1, c];
                    idx[i, c] = idx[i - 1, c];
                    if (dp[i, c] < sum + dp[i - k, c - 1])
                    {
                        dp[i, c] = sum + dp[i - k, c - 1];
                        idx[i, c] = i - k + 1;
                    }
                }
            }

            // construct the path
            var res = new int[m];
            for (int p = n, c = m; c > 0; c--)
                p = res[c - 1] = idx[p - 1, c];

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 1, 2, 1, 2, 6, 7, 5, 1 };
            var exp = new int[] { 0, 3, 5 };
            Console.WriteLine(exp.SequenceEqual(MaxSumOfThreeSubarrays(nums, 2)));
        }
    }
}

