using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: dp
 * Time(nlogn), Space(n)
 * dp[i] is the length of longest increasing subsequence ending with nums[i].
 * dp[i] = max(dp[k] + 1), where k=[0..i-1] and nums[k] < nums[i].
 * Since dp is ordered, so we can binary search nums[i] in dp 
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
            int len = 0;

            foreach (var num in nums)
            {
                int idx = Array.BinarySearch(dp, 0, len, num);
                if (idx < 0)
                {
                    idx = ~idx;
                    dp[idx] = num;
                    if (idx == len) len++;
                }
            }

            return len;
        }

        IList<int> LisPath(int[] nums)
        {
            int n = nums.Length;
            var idx = new int[n]; // store the index of the number instead of the number itself to easy reconstructing the path
            int len = 0;
            var pre = new int[n]; // store the index of predecessor of nums[i] in the LIS ending at nums[i]

            for (int i = 0; i < n; i++)
            {
                int lo = 0, hi = len - 1;
                while (lo <= hi)
                {
                    int mid = (lo + hi) / 2;
                    if (nums[idx[mid]] < nums[i]) lo = mid + 1;
                    else hi = mid - 1;
                }

                idx[lo] = i;
                if (lo == len) len++;
                if (lo > 0) pre[i] = idx[lo - 1];
            }

            var res = new int[len];
            for (int p = idx[len - 1], i = len - 1; i >= 0; i--, p = pre[p])
                res[i] = nums[p];

            return res.ToList();
        }

        public void Test()
        {
            var nums = new int[] { 5, 2, 7, 4, 3, 8 };
            Console.WriteLine(Lis(nums) == 3);
            var path = LisPath(nums);
            Console.WriteLine(path.Count == 3 && path.AllIndex(i => i == 0 || path[i-1] < path[i]));

            nums = new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 };
            Console.WriteLine(Lis(nums) == 6);
            path = LisPath(nums);
            Console.WriteLine(path.Count == 6 && path.AllIndex(i => i == 0 || path[i - 1] < path[i]));
        }
    }
}
