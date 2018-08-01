using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: dp
 * Time(n^3), Space(n^2)
 * dp[i, j] = min(dp[i, k] + dp[k+1, j-1]), where k=[i..j-1] and s[k]==s[j], print s[k..j] then s[k+1..j-1].
 */
namespace leetcode
{
    public class Lc664_Strange_Printer
    {
        public int StrangePrinter(string s)
        {
            int n = s.Length;
            return Dfs(s, 0, n - 1, new int[n, n]);
        }

        int Dfs(string s, int lo, int hi, int[,] memo)
        {
            if (lo > hi) return 0;
            if (lo == hi) return 1;
            if (memo[lo, hi] > 0) return memo[lo, hi];

            // optimize for dup
            if (s[hi - 1] == s[hi]) return memo[lo, hi] = Dfs(s, lo, hi - 1, memo);

            var res = 1 + Dfs(s, lo, hi - 1, memo);
            for (int k = lo; k < hi; k++)
            {
                if (s[k] != s[hi]) continue;
                res = Math.Min(res, Dfs(s, lo, k, memo) + Dfs(s, k + 1, hi - 1, memo));
            }

            return memo[lo, hi] = res;
        }

        public void Test()
        {
            Console.WriteLine(StrangePrinter("aaabbb") == 2);
            Console.WriteLine(StrangePrinter("aba") == 2);
        }
    }
}

