using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math
 * Time(nlogn), Space(1)
 * Each sequence is a subset of A. Sort the array then calculate the count of same diff of subsets.
 */
namespace leetcode
{
    public class Lc891_Sum_of_Subsequence_Widths
    {
        public int SumSubseqWidths(int[] A)
        {
            const int MOD = 1000000007;
            int n = A.Length;
            Array.Sort(A);
            long sum = 0, cnt = 1;

            for (int i = 0; i < n; i++)
            {
                sum += cnt * (A[i] - A[n - i - 1]);
                sum %= MOD;
                cnt = (cnt << 1) % MOD;
            }

            return (int)sum;
        }

        public void Test()
        {
            var a = new int[] { 2, 1, 3 };
            Console.WriteLine(SumSubseqWidths(a) == 6);

            a = new int[] { 7, 8, 8, 10, 4 };
            Console.WriteLine(SumSubseqWidths(a) == 96);
        }
    }
}
