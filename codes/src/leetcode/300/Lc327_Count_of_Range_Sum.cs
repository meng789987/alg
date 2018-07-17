using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bit, merge sort
 * Time(nlogn), Space(n)
 * merge sort: result[i] is the the count of numbers moved from the right of i-th element to its left,
 *         so we track the moving count when merge.
 * bit: bitree[i] is the count of numbers whose value is sitting at i-th in the sorted array.
 */
namespace leetcode
{
    public class Lc327_Count_of_Range_Sum
    {
        public int CountRangeSum(int[] nums, int lower, int upper)
        {
            int ret = 0;
            int n = nums.Length;
            if (n == 0) return 0;

            var sums = new long[n];
            sums[0] = nums[0];
            for (int i = 1; i < n; i++)
                sums[i] = sums[i - 1] + nums[i];

            var sumSet = new SortedSet<long>(sums);
            var sumsSorted = sumSet.ToArray();
            var map = new Dictionary<long, int>();
            for (int i = 0; i < n; i++)
                map[sumsSorted[i]] = i;

            var bitree = new int[n + 1];
            for (int i = 0; i < n; i++)
            {
                AddBit(bitree, map[sums[i]] + 1, 1);

                var posLower = Array.BinarySearch(sumsSorted, sums[i] - lower);
                if (posLower < 0) posLower = -2 - posLower;
                var posUpper = Array.BinarySearch(sumsSorted, sums[i] - upper);
                if (posUpper < 0) posUpper = -1 - posUpper;

                ret += QueryBit(bitree, posLower + 1) - QueryBit(bitree, posUpper);
            }

            return ret;
        }

        int LowBit(int n)
        {
            return n & (-n);
        }

        void AddBit(int[] bitree, int pos, int diff)
        {
            while (pos < bitree.Length)
            {
                bitree[pos] += diff;
                pos += LowBit(pos);
            }
        }

        int QueryBit(int[] bitree, int pos)
        {
            int r = 0;
            while (pos > 0)
            {
                r += bitree[pos];
                pos -= LowBit(pos);
            }
            return r;
        }

        public void Test()
        {
            var nums = new int[] { -2, 5, -1 };
            Console.WriteLine(CountRangeSum(nums, -2, 2) == 3);
        }
    }
}

