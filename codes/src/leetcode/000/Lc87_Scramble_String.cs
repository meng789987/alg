using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * tags: dp, LIS(Longest Increasing Subsequence)
 * Time(mnlogn), Space(n), m=row(A), n=col(A)
 * dp[i] is length of longest non-decreasing subsequence of A ending with A[i]
 * dp[i] = max(dp[j]) + 1, if A[j]<=A[i], where j=[0..i-1]
 */
namespace leetcode
{
    public class Lc87_Scramble_String
    {
        // try find first cut, then left and right should be scrambled strings
        public bool IsScramble(string s1, string s2)
        {
            if (s1 == s2) return true;
            int n = s1.Length;
            var count = new int[256];

            // make sure they have same char set
            for (int i = 0; i < n; i++)
            {
                count[s1[i]]++;
                count[s2[i]]--;
            }
            if (count.Any(c => c != 0))
                return false;

            for (int i = 0; i < n-1; i++)
            {
                if (IsScramble(s1.Substring(0, i + 1), s2.Substring(0, i + 1))
                    && IsScramble(s1.Substring(i + 1), s2.Substring(i + 1)))
                    return true;
                if (IsScramble(s1.Substring(n - 1 - i), s2.Substring(0, i + 1))
                    && IsScramble(s1.Substring(0, n - 1 - i), s2.Substring(i + 1)))
                    return true;
            }

            return false;
        }

        public bool IsScramble2(string s1, string s2)
        {
            if (s1 == s2) return true;
            int[] head = new int[256], tail = new int[256];
            int balHead = 0, balTail = 0, n = s1.Length;

            for (int i = 0; i < n-1; i++)
            {
                balHead += ++head[s1[i]] <= 0 ? -1 : 1;
                balHead += --head[s2[i]] >= 0 ? -1 : 1;
                balTail += ++tail[s1[n - 1 - i]] <= 0 ? -1 : 1;
                balTail += --tail[s2[i]] >= 0 ? -1 : 1;

                // good cut
                if (balHead == 0 && IsScramble(s1.Substring(0, i + 1), s2.Substring(0, i + 1))
                    && IsScramble(s1.Substring(i + 1), s2.Substring(i + 1)))
                    return true;
                if (balTail == 0 && IsScramble(s1.Substring(n - 1 - i), s2.Substring(0, i + 1))
                    && IsScramble(s1.Substring(0, n - 1 - i), s2.Substring(i + 1)))
                    return true;
            }

            return false;
        }

        public void Test()
        {
            Console.WriteLine(IsScramble("great", "rgeat") == true);
            Console.WriteLine(IsScramble("abcde", "caebd") == false);
            Console.WriteLine(IsScramble("unuzp", "nzuup") == true);
        }
    }
}
