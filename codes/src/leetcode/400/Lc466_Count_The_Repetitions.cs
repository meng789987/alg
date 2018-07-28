using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(mn), Space(n), where m/n is the length of s1/s2.
 * find the repeated pattern then calculate it.
 * Let's say one block is one s1 that can match [multiple s2 and] some chars of s2,
 * if we found the same index of s2 when start to match from a new block, that is the repeated pattern.
 */
namespace leetcode
{
    public class Lc466_Count_The_Repetitions
    {

        public int GetMaxRepetitions(string s1, int n1, string s2, int n2)
        {
            if (n1 == 0) return 0;
            var indexers = new int[s2.Length + 1];
            var counters = new int[s2.Length + 1];
            int index = 0, count = 0;

            for (int cnt = 0; cnt < n1; cnt++)
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    if (s1[i] == s2[index]) index++;
                    if (index == s2.Length)
                    {
                        index = 0;
                        count++;
                    }
                }

                indexers[cnt] = index;
                counters[cnt] = count;

                // check repeated pattern
                for (int k = 0; k < cnt; k++)
                {
                    if (indexers[k] == indexers[cnt])
                    {
                        int repeatedS1Count = cnt - k;
                        int repeatedS2Count = counters[cnt] - counters[k];

                        int totalRepeatedS2Count = (n1 - 1 - k) / repeatedS1Count * repeatedS2Count;
                        int totalOtherS2Count = counters[k + (n1 - 1 - k) % repeatedS1Count];
                        return (totalRepeatedS2Count + totalOtherS2Count) / n2;
                    }
                }

            }

            // no repeated pattern
            return counters[n1 - 1] / n2;
        }

        public void Test()
        {
            Console.WriteLine(GetMaxRepetitions("acb", 4, "ab", 2) == 2);
            Console.WriteLine(GetMaxRepetitions("aaa", 3, "aa", 1) == 4);
        }
    }
}

