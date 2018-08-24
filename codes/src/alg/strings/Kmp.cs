using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: kmp
 * Time(n), Space(n)
 * kmp[j] is the length of longest same prefix within the pattern, so we don't need to restart matching from the begin,
 * if str[i] != pattern[j], we can reset j=kmp[j-1] instead of j=0, because pattern[0..len] == pattern[j-len..j] where len=kmp[j]-1.
 */
namespace alg.strings
{
    public class Kmp
    {
        public int Find(string str, string substr)
        {
            // build kmp of substr
            var kmp = new int[substr.Length];
            for (int p = 0, k = 1; k < substr.Length; k++)
            {
                while (p > 0 && substr[k] != substr[p]) p = kmp[p-1];
                if (substr[k] == substr[p]) kmp[k] = ++p;
            }

            // match
            int i = 0, j = 0;
            for (; i < str.Length && j < substr.Length; i++)
            {
                while (j > 0 && str[i] != substr[j]) j = kmp[j - 1];
                if (str[i] == substr[j]) j++;
            }

            return j == substr.Length ? i - j : -1;
        }

        public void Test()
        {
            Console.WriteLine(Find("ABABDABACDABABCABAB", "ABABCABAB") == 10);
            Console.WriteLine(Find("AAACABAAAABA", "AAAA") == 6);
            Console.WriteLine(Find("mississippi", "issip") == 4);
        }
    }
}

