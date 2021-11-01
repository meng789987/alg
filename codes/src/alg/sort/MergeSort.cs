using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: sort, merge inplace
 * Time(nlogn), Space(n)
 */
namespace alg.sort
{
    public class MergeSort
    {
        int[] copy;
        public void Sort(int[] a)
        {
            copy = new int[a.Length];
            SortRc(a, 0, a.Length - 1);
        }

        private void SortRc(int[] a, int lo, int hi)
        {
            if (lo >= hi) return;
            int m = (lo + hi) / 2;
            SortRc(a, lo, m);
            SortRc(a, m + 1, hi);
            MergeInPlace(a, lo, m, hi);
        }

        private void Merge(int[] a, int lo, int m, int hi)
        {
            Array.Copy(a, lo, copy, lo, hi - lo + 1);
            for (int i = lo, j = m + 1, k = lo; k <= hi; k++)
            {
                if (j > hi || (i <= m && copy[i] < copy[j]))
                    a[k] = copy[i++];
                else
                    a[k] = copy[j++];
            }
        }

        // Time(n^2), Space(1) for insertion sort
        // Time(n), Space(n) for using extra space with different 'inplace' meaning
        // others Time(nlogn), Space(1) but super complex, e.g.
        // http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.88.1155&rep=rep1&type=pdf
        private void MergeInPlace(int[] a, int lo, int m, int hi)
        {
            for (int j = m + 1; j <= hi; j++)
            {
                // insert a[j] into a[lo..j-1]
                int val = a[j];
                int i = j;
                for (; i > lo && a[i - 1] > val; i--) a[i] = a[i - 1];
                a[i] = val;
            }
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
