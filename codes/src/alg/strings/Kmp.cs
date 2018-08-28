using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: kmp
 * Time(n), Space(n)
 * kmp[j] is the length of longest substring of the pattern ending at j matched the prefix of pattern, so we don't need to restart matching from the beginning,
 * if str[i] != pattern[j], we can reset j=kmp[j-1] instead of j=0, because pattern[0..len] == pattern[j-len..j] where len=kmp[j]-1.
 */
namespace alg.strings
{
    public class Kmp
    {
        public int Find(string str, string pattern)
        {
            // build kmp of substr
            var kmp = new int[pattern.Length];
            int i, j;
            for (i = 1, j = 0; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[i] != pattern[j]) j = kmp[j - 1];
                if (pattern[i] == pattern[j]) kmp[i] = ++j;
            }

            // match
            for (i = j = 0; i < str.Length && j < pattern.Length; i++)
            {
                while (j > 0 && str[i] != pattern[j]) j = kmp[j - 1];
                if (str[i] == pattern[j]) j++;
            }

            return j == pattern.Length ? i - j : -1;
        }

        public void Test()
        {
            Console.WriteLine(Find("ABABDABACDABABCABAB", "ABABCABAB") == 10);
            Console.WriteLine(Find("AAACABAAAABA", "AAAA") == 6);
            Console.WriteLine(Find("mississippi", "issip") == 4);
        }
    }
}

