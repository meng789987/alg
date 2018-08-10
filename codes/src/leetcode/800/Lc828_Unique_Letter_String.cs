
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n), Space(1)
 * dp[i] is sum of unique char in all substring ending at i, then the answer is sum(dp[i]), i=[0..n-1]
 * dp[i] = dp[i-1] + i - s.Substring(0, i).IndexOf(s[i])
 */
namespace leetcode
{
    public class Lc828_Unique_Letter_String
    {
        public int UniqueLetterString(string s)
        {
            const int MOD = 1000000007;
            int res = 0, dp = 0;
            var indice = new int[26];
            Array.Fill(indice, -1);

            for (int i = 0; i < s.Length; i++)
            {
                dp = (dp + i - indice[s[i] - 'A']) % MOD;
                res = (res + dp) % MOD;
                indice[s[i] - 'A'] = i + 1;
            }

            return res;
        }

        long Mirror(long n)
        {
            var cs = n.ToString().ToCharArray();
            for (int i = 0, j = cs.Length - 1; i < j; i++, j--)
                cs[j] = cs[i];
            return long.Parse(new string(cs));
        }


        public void Test()
        {
            Console.WriteLine(UniqueLetterString("ABC") == 10);
            Console.WriteLine(UniqueLetterString("ABA") == 8);
        }
    }
}
