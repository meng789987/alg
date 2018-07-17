using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bit, dc, merge sort
 * Time(nlogn), Space(n)
 * merge sort: divide the prefix sum array into two parts, sums[0..m] and sums[m+1..n-1],
 *      to get count of pair (i,j) for lower <= sums[j] - sums[i] <= upper, it is sum of
 *      MergeSort(sums, 0, m),       0<=i<j<=m
 *      MergeSort(sums, m+1, n-1), m+1<=i<j<=n-1
 *      and those 0<=i<=m<j<=n-1, to calculate count of this part, for each i, 
 *      to find the first jl where lower <= sums[jl] - sums[i], and
 *      to find the first ju where          sums[ju] - sums[i] <= upper, then it is ju - jl.
 * bit: bitree[i] is the count of numbers whose value is sitting at i-th in the sorted array.
 */
namespace leetcode
{
    public class Lc327_Count_of_Range_Sum
    {
        public int CountRangeSum(int[] nums, int lower, int upper)
        {
            int n = nums.Length;
            var sums = new long[n + 1];
            for (int i = 0; i < n; i++) sums[i + 1] = sums[i] + nums[i];
            return MergeSort(sums, 0, n, new long[n + 1], lower, upper);
        }

        int MergeSort(long[] sums, int lo, int hi, long[] aux, int lower, int upper)
        {
            if (lo >= hi) return 0;
            int m = (lo + hi) / 2;
            return MergeSort(sums, lo, m, aux, lower, upper)
                + MergeSort(sums, m + 1, hi, aux, lower, upper)
                + Merge(sums, lo, m, hi, aux, lower, upper);
        }

        int Merge(long[] sums, int lo, int m, int hi, long[] aux, int lower, int upper)
        {
            int count = 0;
            for (int i = lo, jl = m + 1, ju = m + 1; i <= m; i++)
            {
                while (jl <= hi && sums[jl] - sums[i] < lower) jl++;
                while (ju <= hi && sums[ju] - sums[i] <= upper) ju++;
                count += ju - jl;
            }

            Array.Copy(sums, lo, aux, lo, hi - lo + 1);
            for (int i = lo, j = m + 1, k = lo; k <= hi; k++)
            {
                if (j > hi || (i <= m && aux[i] < aux[j])) sums[k] = aux[i++];
                else sums[k] = aux[j++];
            }

            return count;
        }

        public int CountRangeSumBit(int[] nums, int lower, int upper)
        {
            int n = nums.Length;
            var sums = new long[n + 1];
            for (int i = 0; i < n; i++)
                sums[i + 1] = sums[i] + nums[i];
            var sortedSums = sums.ToArray();
            Array.Sort(sortedSums);

            int ret = 0;
            var bitree = new int[n + 2];
            foreach (var sum in sums)
            {
                ret += QueryBit(bitree, Find(sortedSums, sum - lower - 0.5)) 
                    - QueryBit(bitree, Find(sortedSums, sum - upper + 0.5));
                AddBit(bitree, Find(sortedSums, sum - 0.5) + 2, 1);
            }

            return ret;
        }

        // use double to avoid dup
        int Find(long[] nums, double value)
        {
            int lo = 0;
            for (int hi = nums.Length - 1; lo < hi;)
            {
                int m = (lo + hi) / 2;
                if (nums[m] < value) lo = m + 1;
                else hi = m - 1;
            }
            return lo;
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
            Console.WriteLine(CountRangeSumBit(nums, -2, 2) == 3);
        }
    }
}

