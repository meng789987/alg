using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: dp
 * Time(nlogn), Space(n)
 * dp[i] is the length of longest increasing subsequence ending with nums[i].
 * dp[i] = max(dp[k] + 1), where k=[0..i-1] and nums[k] < nums[i].
 * Since dp is ordered, so we can binary find nums[i] in dp 
 * to replace (dp[k] if nums[i]<dp[k], so dp[i] only contains the smallest tail number of all increasing subsequence with same length) 
 * or insert (nums[i] if nums[i]>dp[i-1]).
 */
namespace alg.dp
{
    public class LongestIncreasingSubsequence
    {
        int Lis(int[] nums)
        {
            var dp = new int[nums.Length];
            int dplen = 0;

            foreach (var num in nums)
            {
                int idx = Array.BinarySearch(dp, 0, dplen, num);
                if (idx < 0)
                {
                    idx = ~idx;
                    dp[idx] = num;
                    if (idx == dplen) dplen++;
                }
            }

            return dplen;
        }

        int LisDp(int[] nums)
        {
            int n = nums.Length;
            var dp = new int[n];
            int max = 0;

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    if (dp[i] < dp[k] && nums[k] < nums[i])
                        dp[i] = dp[k];
                }

                dp[i]++;
                if (max < dp[i]) max = dp[i];
            }

            return max;
        }

        IList<int> LisPath(int[] nums)
        {
            int n = nums.Length;
            int[] lens = new int[n], idx=new int[n];
            int maxi = 0;

            for (int i = 0; i < n; i++)
            {
                for (int k=0; k<i ; k++)
                {
                    if (lens[i]<lens[k] && nums[k]<nums[i])
                    {
                        lens[i] = lens[k];
                        idx[i] = k;
                    }
                }

                lens[i]++;
                if (lens[maxi] < lens[i]) maxi = i;
            }

            var res = new int[lens[maxi]];
            for (int i = res.Length - 1, p = maxi; i >= 0; i--, p = idx[p])
                res[i] = nums[p];
                
            return res.ToList();
        }

        public void Test()
        {
            var nums = new int[] { 5, 2, 7, 4, 3, 8 };
            var exp = new int[] { 5, 7, 8 };
            Console.WriteLine(Lis(nums) == 3);
            Console.WriteLine(LisDp(nums) == 3);
            Console.WriteLine(exp.SequenceEqual(LisPath(nums)));

            nums = new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 };
            exp = new int[] { 15, 27, 38, 55, 65, 85 };
            Console.WriteLine(Lis(nums) == 6);
            Console.WriteLine(LisDp(nums) == 6);
            Console.WriteLine(exp.SequenceEqual(LisPath(nums)));
        }
    }
}
