using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(mn), Space(n)
 * dp[i, j] is true if s[0..i-1] matches p[0..j-1]
 * dp[i, j] = dp[i-1, j-1], if s[i-1] match p[j-1] (s[i-1]==p[j-1] or p[j-1]=='.'), otherwise if p[j-1] is '*'
 *          = dp[i, j-2], if s[i-1] not match p[j-2], "a*" match empty, otherwise s[i-1] match p[j-2]
 *          = dp[i, j-2], "x*" match empty
 *         or dp[i, j-1], "x*" match one
 *         or dp[i-1, j], "x*" match two or more
 *         
 *  or similar "Edit Distance", just consider add and delete operations on pattern
 */
namespace leetcode
{
    public class Lc010_Regular_Expression_Matching
    {
        /*
         * Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.
         *     '.' Matches any single character.
         *     '*' Matches zero or more of the preceding element.
        */
        public bool IsMatch(string s, string p)
        {
            int m = s.Length, n = p.Length;
            var dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;
            for (int j = 2; j <= n && p[j - 1] == '*'; j += 2)
                dp[0, j] = true;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s[i - 1] == p[j - 1] || p[j - 1] == '.')
                        dp[i, j] = dp[i - 1, j - 1];
                    else if (p[j - 1] == '*')
                    {
                        dp[i, j] = dp[i, j - 1] | dp[i, j - 2];  // "x*" match empty or one x, delete "x*"
                        if (s[i - 1] == p[j - 2] || p[j - 2] == '.')
                            dp[i, j] |= dp[i - 1, j]; // "x*" match more x, add more "x"
                    }
                }
            }

            return dp[m, n];
        }

        public void Test()
        {
            Console.WriteLine(IsMatch("aa", "a") == false);
            Console.WriteLine(IsMatch("aa", "a*") == true);
            Console.WriteLine(IsMatch("ab", ".*") == true);
            Console.WriteLine(IsMatch("aab", "c*a*b") == true);
            Console.WriteLine(IsMatch("mississippi", "mis*is*p*.") == false);
            Console.WriteLine(IsMatch("mississippi", "mis*is*ip*.") == true);
            Console.WriteLine(IsMatch("bbbba", ".*a*a") == true);
        }
    }
}

