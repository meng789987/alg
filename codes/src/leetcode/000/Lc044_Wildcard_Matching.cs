using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: regex
 * Time(mn), Space(1)
 * Regular expression, support only '?' and '*' which matches 0-n any chars
 */
namespace leetcode
{
    public class Lc044_Wildcard_Matching
    {
        public bool IsMatch(string s, string p)
        {
            int i = 0, j = 0, n = p.Length;
            int si = -1, sj = -1; // index of last star '*'

            while (i < s.Length)
            {
                if (j < n && (p[j] == '?' || p[j] == s[i])) { i++; j++; }
                else if (j < n && p[j] == '*') { si = i; sj = j++; }
                else if (si >= 0) { i = ++si; j = sj + 1; }
                else return false;
            }

            while (j < n && p[j] == '*') j++;
            return j == n;
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
