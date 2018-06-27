using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: palindrome, Manacher, dp
 * Time(n), Space(n)
 * lens[i] is the length of palindromic string center at i, i can be at between two continuous chars in s, so i=[0..2n+1]
 * lens[i] >= Min(r - i, lens[2*c - i]), initialized as its left mirror one.
 * when s[i-d..i..i+d] is palindrome then palindrome s[..i+k..] (where k=[0..d]) at least has length Min(d, lens[i-k])
 */
namespace alg.strings
{
    public class Palindrome_Manacher
    {
        public string LongestPalindrome(string s)
        {
            if (s.Length == 0) return null;
            var lens = new int[s.Length * 2 + 1];
            lens[1] = 1;
            int c = 1;
            int r = 2; // r = c + lens[c]
            int maxc = 1;
            for (int i = 2; i < lens.Length; i++)
            {
                lens[i] = r < i ? 0 : Math.Min(r - i, lens[2 * c - i]);
                while ((i + lens[i]) % 2 == 1 || (i > lens[i] && (i + lens[i] + 1) / 2 < s.Length
                    && s[(i - lens[i] - 1) / 2] == s[(i + lens[i] + 1) / 2])) lens[i]++;
                if (r < i + lens[i])
                {
                    r = i + lens[i];
                    c = i;
                }
                if (lens[i] > lens[maxc]) maxc = i;
            }
            return s.Substring((maxc - lens[maxc]) / 2, lens[maxc]);
        }

        public void Test()
        {
            Console.WriteLine(LongestPalindrome("babad") == "bab");
            Console.WriteLine(LongestPalindrome("babcbabcbaccba") == "abcbabcba");
        }
    }
}

