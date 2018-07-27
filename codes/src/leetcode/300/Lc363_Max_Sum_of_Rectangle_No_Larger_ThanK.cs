using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp, bs
 * Time(n^2mlogm), Space(m)
 * for 1-D, max subarray: Time(n): dp[i] = max(a[i], dp[i-1]+a[i]); d[i] is the subarray ending at i.
 * for 1-D, max subarray less than k: Time(nlogn)
 * for 2-D, max rect: Time(mn^2)
 */
namespace leetcode
{
    public class Lc363_Max_Sum_of_Rectangle_No_Larger_ThanK
    {
        public int MaxSumSubmatrix(int[,] matrix, int k)
        {
            int res = int.MinValue/10;
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            for (int cl = 0; cl < n; cl++)
            {
                var sums = new int[m];
                for (int cr = cl; cr < n; cr++)
                {
                    for (int r = 0; r < m; r++)
                        sums[r] += matrix[r, cr];

                    var sumset = new SortedSet<int>();
                    sumset.Add(0);
                    int sum = 0;
                    foreach (var s in sums)
                    {
                        sum += s;
                        var lowers = sumset.GetViewBetween(sum - k, sum - res);
                        if (lowers.Count > 0) res = Math.Max(res, sum - lowers.Min);
                        sumset.Add(sum);
                    }
                }
            }

            return res;
        }

        public void Test()
        {
            var matrix = new int[,] { { 1, 0, 1 }, { 0, -2, 3 } };
            Console.WriteLine(MaxSumSubmatrix(matrix, 2) == 2);

            matrix = new int[,] { { 5, -4, -3, 4 }, { -3,-4,4,5 }, { 5,1,5,-4 } };
            Console.WriteLine(MaxSumSubmatrix(matrix, 8) == 8);
        }
    }
}

