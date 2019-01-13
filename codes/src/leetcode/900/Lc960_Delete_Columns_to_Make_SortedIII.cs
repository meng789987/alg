using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * tags: dp, LIS(Longest Increasing Subsequence)
 * Time(mnlogn), Space(n), m=row(A), n=col(A)
 * dp[i] is length of longest non-decreasing subsequence of A ending with A[i]
 * dp[i] = max(dp[j]) + 1, if A[j]<=A[i], where j=[0..i-1]
 */
namespace leetcode
{
    public class Lc960_Delete_Columns_to_Make_SortedIII
    {
        public int MinDeletionSize(string[] A)
        {
            int n = A[0].Length;
            var dp = new int[n];
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                for (int j = 0; j < i; j++)
                {
                    if (dp[i] < dp[j] + 1 && A.All(s => s[j] <= s[i]))
                        dp[i] = dp[j] + 1;
                }
            }

            return n - dp.Max();
        }

        public void Test()
        {
            var a = new string[] { "babca", "bbazb" };
            Console.WriteLine(MinDeletionSize(a) == 3);

            a = new string[] { "edcba" };
            Console.WriteLine(MinDeletionSize(a) == 4);

            a = new string[] { "ghi", "def", "abc" };
            Console.WriteLine(MinDeletionSize(a) == 0);
        }
    }
}
