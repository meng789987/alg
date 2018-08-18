using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: sort
 * Time(n), Space(n), n is the count of all elements in k list.
 * sort and then count the sliding window
 */
namespace leetcode
{
    public class Lc632_Smallest_Range
    {
        public int[] SmallestRange(IList<IList<int>> nums)
        {
            int k = nums.Count;
            var data = new List<IList<int[]>>(k);
            for (int i = 0; i < nums.Count; i++)
            {
                var list = new List<int[]>(nums[i].Count);
                foreach (var n in nums[i]) list.Add(new int[] { n, i });
                data.Add(list);
            }

            MergeSort(data, 0, k - 1);
            var ls = data[0];

            int min = ls[0][0], max = ls.Last()[0], cnt = 0;
            var counts = new int[k];
            for (int i = 0, j = 0; j < ls.Count; j++)
            {
                if (++counts[ls[j][1]] == 1) cnt++;
                while (cnt >= k && i < j)
                {
                    if (max - min > ls[j][0] - ls[i][0])
                        (max, min) = (ls[j][0], ls[i][0]);
                    if (--counts[ls[i++][1]] == 0) cnt--;
                }
            }

            return new int[] { min, max };
        }

        void MergeSort(IList<IList<int[]>> nums, int lo, int hi)
        {
            if (lo >= hi) return;

            int mid = (lo + hi) / 2;
            MergeSort(nums, lo, mid);
            MergeSort(nums, mid + 1, hi);

            IList<int[]> lc = nums[lo], rc = nums[mid + 1];
            var aux = new List<int[]>(lc.Count + rc.Count);
            for (int i = 0, j = 0; aux.Count < lc.Count + rc.Count;)
            {
                if (j >= rc.Count || (i < lc.Count && lc[i][0] < rc[j][0]))
                    aux.Add(lc[i++]);
                else aux.Add(rc[j++]);
            }

            nums[lo] = aux;
        }

        public void Test()
        {
            var nums = new List<IList<int>>
            {
                new List<int>{ 4, 10, 15, 24, 26 },
                new List<int>{ 0, 9, 12, 20 },
                new List<int>{ 5, 18, 22, 30 },
            };
            var exp = new int[] { 20, 24 };
            Console.WriteLine(exp.SequenceEqual(SmallestRange(nums)));
        }
    }
}

