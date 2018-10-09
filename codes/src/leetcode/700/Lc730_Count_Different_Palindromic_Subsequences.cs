
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n^2), Space(n^2)
 * dp(i, j, c) = s[i] == c ? 1 : 0, if i == j,
 *               dp(i, j-1, c) if s[j] != c,
 *               dp(i+1, j, c) if s[i] != c,
 *               sum(dp(i+1, j-1, cc)) + 2, cc is any char
 * dp(i, j, c) is the number of total different palindromic subsequences ending with c of string s[i..j]
 */
namespace leetcode
{
    public class Lc730_Count_Different_Palindromic_Subsequences
    {
        public int CountPalindromicSubsequences(string s)
        {
            int n = s.Length;
            var memo = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    memo[i, j] = -1;

            return Count(s, 0, n - 1, memo);
        }

        int Count(string s, int lo, int hi, int[,] memo)
        {
            const int MOD = 1000000007;

            if (lo > hi) return 0;
            if (lo == hi) return memo[lo, hi] = 1;
            if (memo[lo, hi] >= 0) return memo[lo, hi];

            int ret = 0;
            if (s[lo] != s[hi])
            {
                // all answers ending (any char in s[lo+1..hi-1] and s[hi])
                // plus answers ending (any char in s[lo+1..hi-1] and s[lo])
                // minus dup answers ending (any char in s[lo+1..hi-1])
                ret = Count(s, lo + 1, hi, memo) + Count(s, lo, hi - 1, memo) - Count(s, lo + 1, hi - 1, memo);
            }
            else
            {
                int i = lo + 1, j = hi - 1;
                while (i <= j && s[i] != s[lo]) i++;
                while (i <= j && s[j] != s[lo]) j--;

                if (i > j) // no s[lo] in substring s[lo+1..hi-1]
                    ret = Count(s, lo + 1, hi - 1, memo) * 2 + 2;
                else if (i == j)  // only one s[lo] in substring s[lo+1..hi-1]
                    ret = Count(s, lo + 1, hi - 1, memo) * 2 + 1;
                else // two or more s[lo] in substring s[lo+1..hi-1]
                    ret = Count(s, lo + 1, hi - 1, memo) * 2 - Count(s, i + 1, j - 1, memo);
            }

            return memo[lo, hi] = (ret >= 0 ? ret : ret + MOD) % MOD;
        }

        public int CountPalindromicSubsequences2(string s)
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
                ret = (ret + Count2(s, 0, n - 1, c, memo)) % MOD;
            }

            return ret;
        }

        int Count2(string s, int lo, int hi, int c, int[,,] memo)
        {
            const int MOD = 1000000007;
            char ch = (char)(c + 'a');

            if (lo > hi) return 0;
            if (memo[lo, hi, c] >= 0) return memo[lo, hi, c];
            if (lo == hi) return memo[lo, hi, c] = (s[lo] == ch ? 1 : 0);
            if (s[lo] != ch) return memo[lo, hi, c] = Count2(s, lo + 1, hi, c, memo);
            if (s[hi] != ch) return memo[lo, hi, c] = Count2(s, lo, hi - 1, c, memo);

            int ret = 0;
            for (int cc = 0; cc < 4; cc++)
            {
                ret = (ret + Count2(s, lo + 1, hi - 1, cc, memo)) % MOD;
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

