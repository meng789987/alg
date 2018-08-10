
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n), Space(1)
 * dp[i] is sum of unique char in all substring ending at i, then the answer is sum(dp[i]), i=[0..n-1]
 * dp[i] = dp[i-1] + (i - first_from_i(s[i])) - (first_from_i(s[i]) - second_from_i(s[i]))
 */
namespace leetcode
{
    public class Lc828_Unique_Letter_String
    {
        public int UniqueLetterString(string s)
        {
            const int MOD = 1000000007;
            int res = 0, dp = 0;
            var indice = new int[26, 2];

            for (int i = 0; i < s.Length; i++)
            {
                int ci = s[i] - 'A';
                dp = dp + 1 + i - indice[ci, 0] * 2 + indice[ci, 1];
                res = (res + dp) % MOD;

                indice[ci, 1] = indice[ci, 0];
                indice[ci, 0] = i + 1;
            }

            return res;
        }


        public void Test()
        {
            Console.WriteLine(UniqueLetterString("ABC") == 10);
            Console.WriteLine(UniqueLetterString("ABA") == 8);
        }
    }
}
