using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(n^2), Space(n^2)
 * dp[i][d] is the count of slices ending at i with difference d, plus potential slices containing only two elements (A[i], A[j]) 
 * dp[i][d] = sum(dp[j][d] + 1), where j=[0..i-1], d=A[i]-A[j].
 */
namespace leetcode
{
    public class Lc446_Arithmetic_Slices_II_Subsequence
    {
        public int NumberOfArithmeticSlices(int[] A)
        {
            int res = 0;
            var dp = new Dictionary<int, int>[A.Length]; // key: diff, value: count
            for (int i = 0; i < A.Length; i++)
            {
                dp[i] = new Dictionary<int, int>();
                for (int j = 0; j < i; j++)
                {
                    if ((long)A[i] - A[j] < int.MinValue || int.MaxValue < (long)A[i] - A[j]) continue;
                    int d = A[i] - A[j];
                    int cnt = dp[j].GetValueOrDefault(d, 0);
                    dp[i][d] = dp[i].GetValueOrDefault(d, 0) + cnt + 1;
                    res += cnt;
                }
            }

            return res;
        }


        public void Test()
        {
            var nums = new int[] { 1, 3, 5, 7, 9 };
            Console.WriteLine(NumberOfArithmeticSlices(nums) == 7);

            nums = new int[] { 2, 2, 3, 4 };
            Console.WriteLine(NumberOfArithmeticSlices(nums) == 2);
        }
    }
}

