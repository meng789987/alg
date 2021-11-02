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
            int p = Partition3Way(a, lo, hi);
            SortRc(a, lo, p-1);
            SortRc(a, p+1, hi);
        }

        // partition a[lo..hi] into 3 parts: a[lo..i-1] < pivot == a[i] <= a[i+1..hi]
        // in the below, i=new_lo. the right part may have some elements equals with pivot
        private int Partition(int[] a, int lo, int hi)
        {
            int pivot = a[hi]; // swap the pivot element to the right part last one
            for (int k = lo; k <= hi; k++)
            {
                if (a[k] < pivot) Swap(a, k, lo++);
            }
            Swap(a, lo, hi); // swap the pivot to the correct location
            return lo;
        }

         // partition a[lo..hi] into 3 parts: a[lo..i] < pivot == a[j-1] < a[j..hi]
         // in the below, i=new_lo-1, j=new_hi+1
        private int Partition3Way(int[] a, int lo, int hi)
        {
            int pivot = a[hi]; // pivot can be anyone in a[lo..hi]
            for (int k = lo; k <= hi; k++)
            {
                if (a[k] < pivot) Swap(a, k, lo++);
                else if (a[k] > pivot) Swap(a, k--, hi--); // keep current k to verify
            }
            return hi;

        }

        // this requires to recursively call SortRc(a, lo, p) instead of SortRc(a, lo, p - 1);
        // as we don't put the pivot at a[p]. we just partition as a[lo..j] <=  a[j+1..hi]
        private int PartitionHoare(int[] a, int lo, int hi)
        {
            int pivot = a[lo];
            int i = lo - 1, j = hi + 1;
            while (true)
            {
                // no need to check range as either we have a pivot, or we had swapped one in last loop as stopper
                do { i++; } while (a[i] < pivot); 
                do { j--; } while (a[j] > pivot); // 
                if (i >= j) return j; // return j (not i)
                Swap(a, i, j);
            }
        }

        void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public void Test()
        {
            var nums = new int[] { 3, 0, 1 };
            var exp = "0, 1, 3";
            Sort(nums);
            Console.WriteLine(exp == string.Join(", ", nums));

            nums = new int[] { 3, 4, 5, 2, 7, 234, 84, 24 };
            exp = "2, 3, 4, 5, 7, 24, 84, 234";
            Sort(nums);
            Console.WriteLine(exp == string.Join(", ", nums));

            nums = new int[] { 2, 3, 5, 3, 5, 1, 7, 8 };
            exp = "1, 2, 3, 3, 5, 5, 7, 8";
            Sort(nums);
            Console.WriteLine(exp == string.Join(", ", nums));

            nums = new int[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 };
            exp = "0, 1, 2, 3, 4, 5, 6, 7, 9";
            Sort(nums);
            Console.WriteLine(exp == string.Join(", ", nums));
        }
    }
}
