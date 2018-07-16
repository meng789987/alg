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
            int p = Partition3Way(a, lo, hi, a[hi]);
            SortRc(a, lo, p - 1);
            SortRc(a, p + 1, hi);
        }

        private int Partition(int[] a, int lo, int hi)
        {
            int pivot = a[hi];
            int i = lo;
            for (int j = lo; j < hi; j++)
            {
                if (a[j] < pivot)
                {
                    Swap(a, i, j);
                    i++;
                }
            }
            Swap(a, i, hi);
            return i;
        }

        // this requires to recursively call SortRc(a, lo, p) instead of SortRc(a, lo, p - 1);
        private int PartitionHoare(int[] a, int lo, int hi)
        {
            int pivot = a[lo];
            int i = lo - 1, j = hi + 1;
            while (true)
            {
                do { i++; } while (a[i] < pivot);
                do { j--; } while (a[j] > pivot);
                if (i >= j) return j;
                Swap(a, i, j);
            }
        }

        /*
         * partition a into 3 parts: a[lo..i-1] < pivot == a[i..j] < a[j+1..hi]
         */
        private int Partition3Way(int[] a, int lo, int hi, int pivot)
        {
            int i = lo;
            while (i <= hi)
            {
                if (a[i] < pivot) Swap(a, i++, lo++);
                else if (a[i] > pivot) Swap(a, i, hi--);
                else i++;
            }
            return i - 1;
        }

        void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public void Test()
        {
            var nums = new int[] { 3, 4, 5, 2, 7, 234, 84, 24 };
            var exp = nums.ToArray();
            Sort(nums);
            Array.Sort(exp);
            Console.WriteLine(exp.SequenceEqual(nums));

            nums = new int[] { 2, 3, 5, 3, 5, 1, 7, 8 };
            exp = nums.ToArray();
            Sort(nums);
            Array.Sort(exp);
            Console.WriteLine(exp.SequenceEqual(nums));
        }
    }
}
