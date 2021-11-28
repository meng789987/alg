using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace alg.alg.dp
{

    public class MaxSumOfSubarray
    {
        /*
         * Time(n), Space(1)
         * Kadane's Algorithm
         * dp[i] is the max sum of a subarray of nums[0..i]
         * dp[i] = max(num[i], // only this number
         *             num[i] + dp[i-1])
         */
        public long MaxSum(int[] nums)
        {
            // initialize to 0 when empty subarray is permitted
            long maxSum = int.MinValue, curSum = int.MinValue;
            foreach (int num in nums)
            {
                curSum = Math.Max(num, num + curSum);
                maxSum = Math.Max(maxSum, curSum);
            }

            return maxSum;
        }

        /*
         * variant of Kadane's Algorithm
         * Lc2036. Maximum Alternating Subarray Sum
         * Alt sum = nums[i] - nums[i+1] + nums[i+2] - ... +/- nums[j]
         */
        public long MaximumAlternatingSubarraySum(int[] nums)
        {
            long res = int.MinValue, pos = int.MinValue, neg = int.MinValue;

            foreach (var num in nums)
            {
                long newpos = Math.Max(num, num + neg);
                neg = pos - num;
                pos = newpos;

                res = Math.Max(res, Math.Max(pos, neg));
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 3, -1, 1, 2 };
            Console.WriteLine(MaxSum(nums) == 5);
            Console.WriteLine(MaximumAlternatingSubarraySum(nums) == 5);

            nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            Console.WriteLine(MaxSum(nums) == 6);
            Console.WriteLine(MaximumAlternatingSubarraySum(nums) == 11);
        }
    }
}
