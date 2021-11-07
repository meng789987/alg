using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: regular expression, wildcard matching, dp, greedy
 * Lc010_Regular_Expression_Matching: support only '.' and '*' in pattern. '*' Matches zero or more of the preceding char.
 * Lc044_Wildcard_Matching: support only '?' and '*' in pattern. '*' can match 0-n any chars
 * Lc072_Edit_Distance: Given two words word1 and word2, find the minimum number of operations required to convert word1 to word2.
 */
namespace alg.dp
{
    public class RegularExpression
    {
        /*
         * Lc044_Wildcard_Matching: support only '?' and '*' in pattern. '*' can match 0-n any chars
         * Greedy: Time(mn), Space(1)
         * 1. For each '*', we try match 0 char, if it fails then restart from here to try match 1 char, and then next, ...
         * 2. We only restart from only last '*'
         */
        public bool IsMatch_Greedy(string s, string p)
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

        /*
         * Lc072_Edit_Distance: Given two words word1 and word2, find the minimum number of operations required to convert word1 to word2.
         * Only 3 operations are permitted on a word: Insert/Delete/Replace a character.
         * DP: Time(mn), Space(n)
         * dp[i+1, j+1] is the minimum number of operations required to convert a[0..i] to b[0..j]
         * dp[i+1, j+1] = dp[i, j], if a[i]==b[j]
         *         or 1 + min(dp[i, j+1], //insert
         *                    dp[i+1, j], //delete
         *                    dp[i, j]),  //Replace a character
         * base case: dp[0, j]=j, dp[i, 0]=i
         */
        public int EditDistance(string word1, string word2)
        {
            int m = word1.Length, n = word2.Length;
            var dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = i;
            for (int j = 0; j <= n; j++) dp[0, j] = j;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (word1[i] == word2[j])
                        dp[i + 1, j + 1] = dp[i, j];
                    else
                        dp[i + 1, j + 1] = 1 + Math.Min(Math.Min(dp[i, j + 1], dp[i + 1, j]), dp[i, j]);
                }
            }

            return dp[m, n];
        }

        /*
         * Lc044_Wildcard_Matching: support only '?' and '*' in pattern. '*' can match 0-n any chars
         * DP: Time(mn), Space(n)
         * dp[i+1, j+1] is true if s[0..i] match to p[0..j]
         * dp[i+1, j+1] = dp[i, j], if s[i]==p[j] or p[j]=='?'
         *         or dp[i+1, j] // '*' match 0, respective to Delete
         *         || dp[i, j+1], if p[j]=='*', // '*' match 1, respective to Insert
         * base case: dp[0, 0]=true, dp[0, j]=true if all p[0..j-1] are '*'
         */
        public bool IsMatch_Dp(string s, string p)
        {
            int m = s.Length, n = p.Length;
            var dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;
            for (int j = 1; j <= n && p[j - 1] == '*'; j++) dp[0, j] = true;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (s[i] == p[j] || p[j] == '?')
                        dp[i + 1, j + 1] = dp[i, j];
                    else if (p[j] == '*')
                        dp[i + 1, j + 1] = dp[i + 1, j] || dp[i, j + 1];
                }
            }

            return dp[m, n];
        }

        /*
         * Lc010_Regular_Expression_Matching: support only '.' and '*' in pattern. '*' Matches zero or more of the preceding char.
         * DP: Time(mn), Space(n)
         * dp[i+1, j+1] is true if s[0..i] match to p[0..j]
         * dp[i+1, j+1] = dp[i, j], if s[i] match p[j]
         *         // 'x*' match 0 or 1 or 2, respective to Delete/Insert a character
         *         or dp[i+1, j-1] // 'x*' match 0, respective to Delete
         *         || dp[i+1, j] // 'x*' match 1 'x', respective to Delete '*'
         *         || (dp[i, j+1] && s[i] match p[j]), if p[j]=='*', // 'x*' match 2 'xx', respective to Insert
         * base case: dp[0, 0]=true, dp[0, 2j]=true if all p[0..2i-1] are '*', i<j
         */
        public bool IsMatch_Regex(string s, string p)
        {
            int m = s.Length, n = p.Length;
            var dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;
            for (int j = 2; j <= n && p[j - 1] == '*'; j += 2) dp[0, j] = true;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (s[i] == p[j] || p[j] == '.')
                        dp[i + 1, j + 1] = dp[i, j];
                    else if (p[j] == '*')
                        dp[i + 1, j + 1] = dp[i + 1, j - 1]
                            || dp[i + 1, j]
                            || (dp[i, j + 1] && (s[i] == p[j - 1] || p[j - 1] == '.'));
                }
            }

            return dp[m, n];
        }

        public void Test()
        {
            Console.WriteLine(IsMatch_Greedy("aa", "a") == false);
            Console.WriteLine(IsMatch_Greedy("aa", "*") == true);
            Console.WriteLine(IsMatch_Greedy("cb", "?a") == false);
            Console.WriteLine(IsMatch_Greedy("adceb", "*a*b") == true);
            Console.WriteLine(IsMatch_Greedy("acdcb", "a*c?b") == false);
            Console.WriteLine(IsMatch_Greedy("baababbaaaaabbababbbbbabaabaabaaabbaabbbbbbaabbbaaabbabbaabaaaaabaabbbaabbabababaaababbaaabaababbabaababbaababaabbbaaaaabbabbabababbbbaaaaaabaabbbbaababbbaabbaabbbbbbbbabbbabababbabababaaababbaaababaabb",
                                        "*ba***b***a*ab**b***bb*b***ab**aa***baba*b***bb**a*abbb*aa*b**baba**aa**b*b*a****aabbbabba*b*abaaa*aa**b") == false);

            Console.WriteLine(IsMatch_Dp("aa", "a") == false);
            Console.WriteLine(IsMatch_Dp("aa", "*") == true);
            Console.WriteLine(IsMatch_Dp("cb", "?a") == false);
            Console.WriteLine(IsMatch_Dp("adceb", "*a*b") == true);
            Console.WriteLine(IsMatch_Dp("acdcb", "a*c?b") == false);
            Console.WriteLine(IsMatch_Dp("baababbaaaaabbababbbbbabaabaabaaabbaabbbbbbaabbbaaabbabbaabaaaaabaabbbaabbabababaaababbaaabaababbabaababbaababaabbbaaaaabbabbabababbbbaaaaaabaabbbbaababbbaabbaabbbbbbbbabbbabababbabababaaababbaaababaabb",
                                        "*ba***b***a*ab**b***bb*b***ab**aa***baba*b***bb**a*abbb*aa*b**baba**aa**b*b*a****aabbbabba*b*abaaa*aa**b") == false);

            Console.WriteLine(IsMatch_Regex("aa", "a") == false);
            Console.WriteLine(IsMatch_Regex("aa", "a*") == true);
            Console.WriteLine(IsMatch_Regex("cb", ".*") == true);
            Console.WriteLine(IsMatch_Regex("aab", "c*a*b") == true);
            Console.WriteLine(IsMatch_Regex("mississippi", "mis*is*p*.") == false);
            Console.WriteLine(IsMatch_Regex("bbbba", ".*a*a") == true);


            Console.WriteLine(EditDistance("horse", "ros") == 3);
            Console.WriteLine(EditDistance("intention", "execution") == 5);
        }
    }
}
