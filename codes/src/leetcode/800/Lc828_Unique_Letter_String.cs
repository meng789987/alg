
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
            int[] first = new int[26], second = new int[26];
            Array.Fill(first, -1);
            Array.Fill(second, -1);

            for (int i = 0; i < s.Length; i++)
            {
                int ci = s[i] - 'A';
                dp = dp + i - first[ci] * 2 + second[ci];
                res = (res + dp) % MOD;

                second[ci] = first[ci];
                first[ci] = i;
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
