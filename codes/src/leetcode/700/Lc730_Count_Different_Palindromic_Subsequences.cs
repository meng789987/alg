
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n^2), Space(n^2)
 * dp(i, j, c) = dp(i, j-1, c) if s[j] != c,
 *               1 if s[j] is first c in s[i..j],
 *               sum(dp(first(c)+1, j-1, cc)) + 2, cc is any char
 * dp(i, j, c) is the number of total different palindromic subsequences ending with c of string s[i..j]
 */
namespace leetcode
{
    public class Lc730_Count_Different_Palindromic_Subsequences
    {
        public int CountPalindromicSubsequences(string s)
        {
            const int MOD = 1000000007;
            int n = s.Length;
            var memo = new int[n, n, 4];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int c = 0; c < 4; c++)
                        memo[i, j, c] = -1;

            int ret = 0;
            for (int c = 0; c < 4; c++)
            {
                ret = (ret + Count(s, 0, n - 1, c, memo)) % MOD;
            }

            return ret;
        }

        int Count(string s, int lo, int hi, int c, int[,,] memo)
        {
            const int MOD = 1000000007;
            char ch = (char)(c + 'a');

            if (memo[lo, hi, c] >= 0) return memo[lo, hi, c];
            if (lo == hi) return memo[lo, hi, c] = (s[lo] == ch ? 1 : 0);
            if (s[hi] != ch) return memo[lo, hi, c] = Count(s, lo, hi - 1, c, memo);

            int i = lo;
            while (i < hi && s[i] != ch) i++;
            if (i == hi) return memo[lo, hi, c] = 1;

            int ret = 0;
            for (int cc = 0; cc < 4; cc++)
            {
                ret = (ret + Count(s, i, hi - 1, cc, memo)) % MOD;
            }
            ret = (ret + 2) % MOD;

            return memo[lo, hi, c] = ret;
        }


        public void Test()
        {
            Console.WriteLine(CountPalindromicSubsequences("bccb") == 6);
            Console.WriteLine(CountPalindromicSubsequences("abcdabcdabcdabcdabcdabcdabcdabcddcbadcbadcbadcbadcbadcbadcbadcba") == 104860361);
        }
    }
}

