using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: palindrome, Manacher, dp
 * Time(n), Space(n)
 * since palindrome can be centered at between two adjacent chars, so extend s to ss by inserting a bar before each char.
 * lens[i] is the arm length of palindrome of ss centered at i, i=[0..2n], that is also the length of palindrome of s starting at (i-lens[i])/2
 * if ss[2c-r..c..r] is the longest palindrome center at c, then lens[c] = r-c, so from its mirror left for i=[c..r]
 * lens[i] >= Min(r-i, lens[2c-i])
 */
namespace alg.strings
{
    public class Palindrome_Manacher
    {
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var ss = new char[s.Length * 2]; // can be optimized to omit
            for (int i = 0; i < s.Length; i++)
                ss[2 * i + 1] = s[i];

            var lens = new int[ss.Length];
            lens[1] = 1;
            int c = 1;
            int r = 2; // r = c + lens[c]
            int maxc = 1;

            for (int i = 2; i < lens.Length; i++)
            {
                int k = r < i ? 0 : Math.Min(r - i, lens[2 * c - i]);
                for (int nk = k + 1; i - nk >= 0 && i + nk < ss.Length && ss[i - nk] == ss[i + nk]; nk++)
                    k++;

                if (r < i + k)
                {
                    r = i + k;
                    c = i;
                }

                lens[i] = k;
                if (lens[i] > lens[maxc]) maxc = i;
            }

            return s.Substring((maxc - lens[maxc]) / 2, lens[maxc]);
        }

        public string LongestPalindrome2(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var lens = new int[s.Length * 2 + 1];
            lens[1] = 1;
            int c = 1;
            int r = 2; // r = c + lens[c]
            int maxc = 1;
            for (int i = 2; i < lens.Length; i++)
            {
                int k = r < i ? 0 : Math.Min(r - i, lens[2 * c - i]);
                while ((i + k) % 2 == 1 || (i > k && (i + k + 1) / 2 < s.Length
                    && s[(i - k - 1) / 2] == s[(i + k + 1) / 2])) k++;
                if (r < i + k)
                {
                    r = i + k;
                    c = i;
                }
                lens[i] = k;
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

