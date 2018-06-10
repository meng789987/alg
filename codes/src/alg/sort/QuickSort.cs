using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: sort, partition
 * Time(nlogn), Space(logn)
 */
namespace alg.sort
{
    public class QuickSort
    {
        public void Sort(int[] a)
        {
            SortRc(a, 0, a.Length - 1);
        }

        private void SortRc(int[] a, int lo, int hi)
        {
            if (lo >= hi) return;
            int m = Partition(a, lo, hi);
            SortRc(a, lo, m - 1);
            SortRc(a, m + 1, hi);
        }

        private int Partition(int[] a, int lo, int hi)
        {
            int val = a[hi];
            while (lo < hi)
            {
                while (lo < hi && a[lo] < val) lo++;
                while (lo < hi && a[hi] > val) hi--;
                int t = a[lo];
                a[lo] = a[hi];
                a[hi] = t;
            }
            return lo;
        }

        public void Test()
        {
            var nums = new int[] { 3, 4, 5, 2, 7, 234, 84, 24 };
            var exp = nums.ToArray();
            Sort(nums);
            Array.Sort(exp);
            Console.WriteLine(exp.SequenceEqual(nums));
        }
    }
}
