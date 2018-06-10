using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: palindrome, Manacher, dp
 * Time(n), Space(n)
 * dp[i] is the length of palindromic string center at i, i can be at between two continuous chars in s, so i=[0..2n+1]
 * dp[i] >= Min(farest - i, dp[farest_center * 2 - i]), initialized as its left mirror one.
 * when s[i-d..i..i+d] is palindrome then palindrome s[..i+k..] (where k=[0..d]) at least has length Min(d, dp[i-k])
 */
namespace alg.strings
{
    public class Palindrome_Manacher
    {
        public string LongestPalindrome(string s)
        {
            if (s.Length == 0) return null;
            var dp = new int[s.Length * 2 + 1];
            int farestCenter = 1, farest = 2; // farest = farestCenter + dp[farestCenter]
            int maxi = 1;
            for (int i = 1; i < dp.Length; i++)
            {
                int far = Math.Max(i, Math.Min(farest, i + dp[farestCenter * 2 - i])) + 1;
                while (far % 2 == 0 || (far < dp.Length && i * 2 >= far && s[(i * 2 - far) / 2] == s[far / 2])) far++;
                dp[i] = far - 1 - i;
                if (far - 1 >= farest)
                {
                    farest = far - 1;
                    farestCenter = i;
                }
                if (dp[i] > dp[maxi]) maxi = i;
            }
            return s.Substring((maxi - dp[maxi]) / 2, dp[maxi]);
        }

        public void Test()
        {
            Console.WriteLine(LongestPalindrome("babad") == "bab");
            Console.WriteLine(LongestPalindrome("babcbabcbaccba") == "abcbabcba");
        }
    }
}

