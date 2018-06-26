
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: bucket sort
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc274_H_Index
    {
        public int HIndex(int[] citations)
        {
            int n = citations.Length;
            var buckets = new int[n + 1];
            foreach (var c in citations)
                buckets[c < n ? c : n]++;

            for (int count = 0, i = n; i >= 0; i--)
            {
                count += buckets[i];
                if (count >= i) return i;
            }

            return 0;
        }

        public void Test()
        {
            var nums = new int[] { 3, 0, 6, 1, 5 };
            Console.WriteLine(HIndex(nums) == 3);
        }
    }
}

