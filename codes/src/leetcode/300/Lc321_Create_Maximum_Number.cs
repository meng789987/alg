using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: dp
 * Time(kmn), Space(kmn)
 * dp[i, j, k] = max(dp[i+1, j, k], dp[i, j+1, k], nums1[i] * 10^x + dp[i+1, j, k-1], nums2[j] * 10^y + dp[i, j+1, k-1]), where x/y is number of digit of dp[i+1, j, k-1]/dp[i, j+1, k-1]
 */
namespace leetcode
{
    public class Lc321_Create_Maximum_Number
    {
        public int[] MaxNumber(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length, n = nums2.Length;
            var dp = new List<int>[m + 2, n + 2, k + 1];

            for (int i = m; i >= 0; i--)
            {
                for (int j = n; j >= 0; j--)
                {
                    for (int kk = 1; kk <= k; kk++)
                    {
                        var x = new List<int>();
                        if (i < m) x.Add(nums1[i]);
                        if (dp[i + 1, j, kk - 1] != null)
                            x.AddRange(dp[i + 1, j, kk - 1]);

                        var y = new List<int>();
                        if (j < n) x.Add(nums2[j]);
                        if (dp[i, j + 1, kk - 1] != null)
                            y.AddRange(dp[i, j + 1, kk - 1]);

                        dp[i, j, kk] = Math.Max(dp[i + 1, j, kk], Math.Max(dp[i, j + 1, kk], Math.Max(x, y)));
                    }
                }
            }

            var res = new List<int>();
            for (long v = dp[0, 0, k]; v > 0; v /= 10)
                res.Add((int)(v % 10));
            if (res.Count == 0) res.Add(0);
            res.Reverse();

            return res.ToArray();
        }

        List<int> Max(List<int> a, List<int> b, List<int> c, List<int> d)
        {

        }

        int MinStickersTopdown(int[,] stickerCounts, string target, Dictionary<string, int> memo)
        {
            if (memo.ContainsKey(target)) return memo[target];

            var targetCount = new int[26];
            foreach (var c in target)
                targetCount[c - 'a']++;

            int min = int.MaxValue;
            for (int i = 0; i < stickerCounts.GetLength(0); i++)
            {
                if (stickerCounts[i, target[0] - 'a'] == 0) continue;
                var sb = new StringBuilder();
                for (int j = 0; j < 26; j++)
                {
                    if (targetCount[j] > stickerCounts[i, j])
                        sb.Append((char)(j + 'a'), targetCount[j] - stickerCounts[i, j]);
                }
                int r = MinStickersTopdown(stickerCounts, sb.ToString(), memo);
                if (0 <= r && r < min) min = r;
            }

            return memo[target] = min == int.MaxValue ? -1 : 1 + min;
        }

        public void Test()
        {
            var nums1 = new int[] { 3, 4, 6, 5 };
            var nums2 = new int[] { 9, 1, 2, 5, 8, 3 };
            var exp = new int[] { 9, 8, 6, 5, 3 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 5)));

            nums1 = new int[] { 6, 7 };
            nums2 = new int[] { 6, 0, 4 };
            exp = new int[] { 6, 7, 6, 0, 4 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 5)));

            nums1 = new int[] { 3, 9 };
            nums2 = new int[] { 8, 9 };
            exp = new int[] { 9, 8, 9 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 3)));
        }
    }
}

