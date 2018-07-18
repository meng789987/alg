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
        public int CountRangeSumMergeSort(int[] nums, int lower, int upper)
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

        public int CountRangeSumSegTree(int[] nums, int lower, int upper)
        {
            // prefix sum; use long to avoid overflow
            var sums = new long[nums.Length + 1];
            for (int i = 0; i < nums.Length; i++)
                sums[i + 1] = sums[i] + nums[i];

            // intially the Count of all prefix sum is 0; use set to remove duplicates
            var tree = new SegmentCountTree(new SortedSet<long>(sums).ToArray());

            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                // set the Count of previous prefix sum
                tree.Update(sums[i]);

                // query the count of j, where lower <= sum[i] - sum[j] <= upper, that is sum[i] - upper <= sum[j] <= sum[i] - lower
                ret += tree.Query(sums[i + 1] - upper, sums[i + 1] - lower);
            }

            return ret;
        }

        class SegmentCountTree
        {
            public SegmentCountTree(long[] nums)
            {
                _root = Build(nums, 0, nums.Length - 1);
            }

            Node Build(long[] nums, int lo, int hi)
            {
                if (lo > hi) return null;
                var node = new Node(nums[lo], nums[hi]);
                if (lo == hi) return node;

                int m = (lo + hi) / 2;
                node.Left = Build(nums, lo, m);
                node.Right = Build(nums, m + 1, hi);
                return node;
            }

            public void Update(long value)
            {
                Update(_root, value);
            }

            void Update(Node node, long value)
            {
                if (node == null || value < node.Min || value > node.Max) return;
                node.Count++;
                Update(node.Left, value);
                Update(node.Right, value);
            }

            public int Query(long min, long max)
            {
                return Query(_root, min, max);
            }

            int Query(Node node, long min, long max)
            {
                if (node == null || node.Max < min || max < node.Min) return 0;
                if (min <= node.Min && node.Max <= max) return node.Count;
                return Query(node.Left, min, max) + Query(node.Right, min, max);
            }

            Node _root;

            class Node
            {
                public int Count;
                public long Min, Max;
                public Node Left, Right;
                public Node(long min, long max)
                {
                    Min = min;
                    Max = max;
                }
            }
        }

        public void Test()
        {
            var nums = new int[] { -2, 5, -1 };
            Console.WriteLine(CountRangeSumMergeSort(nums, -2, 2) == 3);
            Console.WriteLine(CountRangeSumSegTree(nums, -2, 2) == 3);
        }
    }
}

