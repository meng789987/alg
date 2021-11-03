using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: dp
 * Time(nlogn), Space(n)
 * dp[i] is the length of LIS ending with nums[i].
 * dp[i] = max(dp[k] + 1), where k=[0..i-1] and nums[k] < nums[i].
 * base case: dp[0]=1
 * Since dp is ordered, like monotonic stack. so we can binary search nums[i] in dp 
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
            int len = 0;
            var dp = new int[n];
            var dpidx = new int[n]; // store the index of the number instead of the number itself to easy reconstructing the path
            var pre = new int[n]; // store the index of predecessor of nums[i] in the LIS ending at nums[i]

            for (int i = 0; i < n; i++)
            {
                int idx = Array.BinarySearch(dp, 0, len, nums[i]);
                if (idx < 0)
                {
                    idx = ~idx;
                    dp[idx] = nums[i];
                    dpidx[idx] = i;
                    if (idx == len) len++;
                }

                if (idx > 0) pre[i] = dpidx[idx - 1];
            }

            // constuct the path from pre backward
            var res = new int[len];
            int ni = dpidx[len - 1];
            for (int k = len - 1; k >= 0; k--)
            {
                res[k] = nums[ni];
                ni = pre[ni];
            }

            return res.ToList();
        }

        public void Test()
        {
            var nums = new int[] { 5, 2, 7, 4, 3, 8 };
            Console.WriteLine(Lis(nums) == 3);
            var path = LisPath(nums);
            Console.WriteLine(path.Count == 3 && path.Select((a, i) => i).All(i => i == 0 || path[i - 1] < path[i]));

            nums = new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 };
            Console.WriteLine(Lis(nums) == 6);
            path = LisPath(nums);
            Console.WriteLine(path.Count == 6 && path.Select((a, i) => i).All(i => i == 0 || path[i - 1] < path[i]));
        }
    }
}
