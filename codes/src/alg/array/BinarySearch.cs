using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: binary seach, saddleback binary search in a sorted list, in a sorted matrix, in two sorted lists.
 */
namespace alg.array
{
    public class BinarySearch
    {
        /*
         * Time(logn), Space(1)
         */
        public int BasicBs(int[] nums, int value)
        {
            int lo = 0, hi = nums.Length - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                if (nums[mid] == value) return mid;
                if (nums[mid] < value) lo = mid + 1;
                else hi = mid - 1;
            }

            return ~lo;
        }

        /*
         * Time(nlogw), Space(1), w is maxValue-minValue
         * find the k-th smallest number in a sorted matrix. both rows and columns are sorted in ascending order.
         */
        public int SaddlebackBs(int[,] matrix, int k)
        {
            int n = matrix.GetLength(0);
            int lo = matrix[0, 0], hi = matrix[n - 1, n - 1];
            while (lo < hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (Count(matrix, mid) < k) lo = mid + 1;
                else hi = mid;
            }

            return lo;
        }

        // count of numbers smaller than or equal to the given value
        int Count(int[,] matrix, int value)
        {
            int count = 0;
            int n = matrix.GetLength(0);
            for (int i = 0, j = n; i < n; i++)
            {
                while (j > 0 && matrix[i, j - 1] > value) j--;
                count += j;
            }

            return count;
        }

        /*
         * Time(n), Space(n)
         * find the k-th smallest number in a sorted matrix. both rows and columns are sorted in ascending order.
         * http://www.cse.yorku.ca/~andy/pubs/X+Y.pdf
         */
        public int Kth(int[,] matrix, int k)
        {
            int n = matrix.GetLength(0);
            var indice = new int[n];
            for (int i = 0; i < n; i++) indice[i] = i;

            return Biselect(matrix, indice, k, k)[0];
        }

        int[] Biselect(int[,] matrix, int[] selected, int lk, int rk)
        {
            int n = selected.Length;
            // base case
            if (n <= 2)
            {
                var nums = new int[n * n];
                int k = 0;
                foreach (var i in selected)
                    foreach (var j in selected)
                        nums[k++] = matrix[i, j];
                Array.Sort(nums);
                return new int[] { nums[lk - 1], nums[rk - 1] };
            }

            // subproblem
            var nselected = new int[n / 2 + 1];
            for (int i = 0; i < n / 2; i++)
                nselected[i] = selected[2 * i];
            nselected[n / 2] = selected[n - 1];
            int nlk = (lk + 3) / 4;
            int nrk = n % 2 == 0 ? (rk + 4 * n + 3) / 4 + 1 : (rk + 2 * n) / 4 + 1;
            var nr = Biselect(matrix, nselected, nlk, nrk);
            int loValue = nr[0], hiValue = nr[1];

            // prepare rank-(hi), rank+(lo) with saddleback search
            int cntSmaller = 0, cntGreater = 0;
            int[][] listBetween = new int[n][];
            for (int js = n, jg = n, i = 0; i < n; i++)
            {
                while (js > 0 && matrix[selected[i], selected[js - 1]] >= hiValue) js--;
                while (jg > 0 && matrix[selected[i], selected[jg - 1]] > loValue) jg--;
                cntSmaller += js;
                cntGreater += n - jg;
                listBetween[i] = new int[Math.Max(0, js - jg)];
                for (int j = jg; j < js; j++)
                    //if (nr.loValue < matrix[selected[i], selected[j]] && matrix[selected[i], selected[j]] < nr.hiValue)
                    listBetween[i][j - jg] = matrix[selected[i], selected[j]];
            }

            // compute result
            bool hasMerged = false;
            int lo = cntSmaller < lk ? hiValue
                : lk + cntGreater <= n * n ? loValue
                : Pick(listBetween, lk + cntGreater - n * n, ref hasMerged);
            int hi = cntSmaller < rk ? hiValue
                : rk + cntGreater <= n * n ? loValue
                : Pick(listBetween, rk + cntGreater - n * n, ref hasMerged);

            return new int[] { lo, hi };
        }

        // return k-th smallest number in an unsorted list
        // use median-of-medians [quicksort] partition function to complete within strict O(n)
        // or use n-way merge sort as each row is sorted to complete within strict O(n)
        int Pick(int[][] data, int k, ref bool hasMerged)
        {
            if (!hasMerged)
            {
                hasMerged = true;
                Merge(data, 0, data.Length - 1);
            }
            return data[0][k - 1];
        }

        void Merge(int[][] data, int lo, int hi)
        {
            if (lo >= hi) return;

            int mid = (lo + hi) / 2;
            Merge(data, lo, mid);
            Merge(data, mid + 1, hi);

            int[] ls = data[lo], rs = data[mid + 1];
            int[] aux = new int[ls.Length + rs.Length];
            for (int i = 0, j = 0, k = 0; k < aux.Length; k++)
            {
                if (i < ls.Length && (j >= rs.Length || ls[i] < rs[j]))
                    aux[k] = ls[i++];
                else aux[k] = rs[j++];
            }
            data[lo] = aux;
        }

        public void Test()
        {
            var nums = new int[] { 2, 3, 4, 5, 7, 24, 234 };
            Console.WriteLine(BasicBs(nums, 7) == 4);
            Console.WriteLine(BasicBs(nums, 10) == -6);

            var matrix = new int[,] { { 1, 5, 9 }, { 10, 11, 13 }, { 12, 13, 15 } };
            Console.WriteLine(SaddlebackBs(matrix, 8) == 13);
            Console.WriteLine(Kth(matrix, 8) == 13);
            Console.WriteLine(SaddlebackBs(matrix, 1) == 1);
            Console.WriteLine(Kth(matrix, 1) == 1);

            matrix = new int[,] { { 1, 4, 7, 11, 15 }, { 2, 5, 8, 12, 19 }, { 3, 6, 9, 16, 22 }, { 10, 13, 14, 17, 24 }, { 18, 21, 23, 26, 30 } };
            Console.WriteLine(SaddlebackBs(matrix, 20) == 21);
            Console.WriteLine(Kth(matrix, 20) == 21);

            matrix = new int[,] { { 1, 3, 5 }, { 6, 7, 12 }, { 11, 14, 14 } };
            Console.WriteLine(SaddlebackBs(matrix, 5) == 7);
            Console.WriteLine(Kth(matrix, 8) == 14);
        }
    }
}
