using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: counting sort, radix sort
 * Time(n), Space(n)
 */
namespace alg.sort
{
    public class RadixSort
    {
        public void Sort(int[] a)
        {
            int b = 10;
            int max = a.Max();
            var aux = new int[a.Length];

            for (int exp = 1; max >= exp; exp *= b)
            {
                // list to buckets
                var counts = new int[b];
                foreach (var n in a) counts[(n / exp) % b]++; // counting sort
                for (int i = 1; i < b; i++) counts[i] += counts[i - 1]; // partial sum

                // buckets to list
                for (int i = a.Length - 1; i >= 0; i--)
                    aux[--counts[(a[i] / exp) % b]] = a[i];
                Array.Copy(aux, a, a.Length);
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
