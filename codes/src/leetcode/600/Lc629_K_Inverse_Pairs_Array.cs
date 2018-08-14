
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n), Space(n)
 * dp[i, k] is list of arrays consist of number [1..i], each of which has k inverse pairs.
 * dp[i, k] can be constructed from dp[i-1, s] by inserting a[i] in the proper position
 * dp[i, k] = sum(dp[i-1, k-j]), j=[0..min(k,i-1)]
 */
namespace leetcode
{
    public class Lc629_K_Inverse_Pairs_Array
    {
        public int KInversePairs(int n, int k)
        {
            const int MOD = 1000000007;
            var dp = new int[k + 1];
            for (int i = 1; i <= n; i++)
            {
                var newdp = new int[k + 1];
                newdp[0] = 1;
                int maxk = Math.Min(k, i * (i - 1) / 2);
                for (int s = 1; s <= maxk; s++)
                {
                    var diff = (dp[s] - (s >= i ? dp[s - i] : 0) + MOD) % MOD;
                    newdp[s] = (newdp[s - 1] + diff) % MOD;
                }
                dp = newdp;
            }

            return dp[k];
        }


        /*
         *    k  0  1  2  3  4  5  6  7  8  9 10
         * n--+----------------------------
         * 1  |  1  
         * 2  |  1  1
         * 3  |  1  2  2  1
         * 4  |  1  3  5  6  5  3  1
         * 5  |  1  4  9 15 20 22 20 15  9  4  1
         */
        public void Test()
        {
            Console.WriteLine(KInversePairs(3, 0) == 1);
            Console.WriteLine(KInversePairs(3, 1) == 2);
            Console.WriteLine(KInversePairs(4, 2) == 5);
            Console.WriteLine(KInversePairs(4, 3) == 6);
            Console.WriteLine(KInversePairs(5, 3) == 15);
            Console.WriteLine(KInversePairs(5, 5) == 22);
            Console.WriteLine(KInversePairs(1000, 1000) == 663677020);
        }
    }
}
