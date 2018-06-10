
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: hash
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc003_Longest_Substring_Without_Repeating_Characters
    {
        public int LengthOfLongestSubstring(string s)
        {
            int ret = 0;
            var lasts = new int[256];
            for (int start = 0, i = 0; i < s.Length; i++)
            {
                start = Math.Max(start, lasts[s[i]]);
                lasts[s[i]] = i + 1;
                ret = Math.Max(ret, i - start + 1);
            }
            return ret;
        }

        public void Test()
        {
            Console.WriteLine(LengthOfLongestSubstring("abcabcbb") == 3);
            Console.WriteLine(LengthOfLongestSubstring("bbbbb") == 1);
            Console.WriteLine(LengthOfLongestSubstring("pwwkew") == 3);
            Console.WriteLine(LengthOfLongestSubstring("bpfbhmipx") == 7);
        }
    }
}
