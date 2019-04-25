using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: greedy, DP
 * Time(mn), Space(1)
 * Greedy is tricky, start from empty match, if match failed restart from last '*' position
 * 
 * DP is similar with Edit Distance, '*' can be deleted, or add one char
 * dp[i, j] is true when s[0..i-1] match p[0..j-1], then
 * dp[i, j] = dp[i-1, j-1], if s[i-1]==p[j-1] or p[j-1]=='?'
 *          = dp[i-1, j] | dp[i, j-1], if p[j-1]=='*',   // add one char, or delete '*'
 */
namespace leetcode
{
    public class Lc044_Wildcard_Matching
    {
        // Regular expression, support only '?' and '*' which matches 0-n any chars
        public bool IsMatch2(string s, string p)
        {
            int i = 0, j = 0, n = p.Length;
            int match = -1, star = -1; // index of last star '*'

            while (i < s.Length)
            {
                if (j < n && (p[j] == '?' || p[j] == s[i])) { i++; j++; }
                else if (j < n && p[j] == '*') { match = i; star = j++; }
                else if (star >= 0) { i = ++match; j = star + 1; }
                else return false;
            }

            while (j < n && p[j] == '*') j++;
            return j == n;
        }

        public bool IsMatchDp(string s, string p)
        {
            int m = s.Length, n = p.Length;
            var dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;
            for (int j = 1; j <= n && p[j - 1] == '*'; j++)
                dp[0, j] = true;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s[i - 1] == p[j - 1] || p[j - 1] == '?')
                        dp[i, j] = dp[i - 1, j - 1];
                    else if (p[j - 1] == '*')
                        dp[i, j] = dp[i - 1, j] | dp[i, j - 1];
                }
            }

            return dp[m, n];
        }

        public bool IsMatch(string s, string p)
        {
            int m = s.Length, n = p.Length;
            var dp = new bool[n + 1];
            dp[0] = true;
            for (int j = 1; j <= n && p[j - 1] == '*'; j++)
                dp[j] = true;

            for (int i = 1; i <= m; i++)
            {
                var pre = dp[0];
                dp[0] = false;
                for (int j = 1; j <= n; j++)
                {
                    var tmp = dp[j];
                    if (s[i - 1] == p[j - 1] || p[j - 1] == '?')
                        dp[j] = pre;
                    else
                        dp[j] = (dp[j] | dp[j - 1]) && (p[j - 1] == '*');
                    pre = tmp;
                }
            }

            return dp[n];
        }

        public void Test()
        {
            Console.WriteLine(IsMatch("aa", "a") == false);
            Console.WriteLine(IsMatch("aa", "*") == true);
            Console.WriteLine(IsMatch("cb", "?a") == false);
            Console.WriteLine(IsMatch("adceb", "*a*b") == true);
            Console.WriteLine(IsMatch("acdcb", "a*c?b") == false);
            Console.WriteLine(IsMatch("baababbaaaaabbababbbbbabaabaabaaabbaabbbbbbaabbbaaabbabbaabaaaaabaabbbaabbabababaaababbaaabaababbabaababbaababaabbbaaaaabbabbabababbbbaaaaaabaabbbbaababbbaabbaabbbbbbbbabbbabababbabababaaababbaaababaabb",
                                        "*ba***b***a*ab**b***bb*b***ab**aa***baba*b***bb**a*abbb*aa*b**baba**aa**b*b*a****aabbbabba*b*abaaa*aa**b") == false);
        }
    }
}
