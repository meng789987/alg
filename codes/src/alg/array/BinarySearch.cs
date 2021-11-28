using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: binary seach, min-max, saddleback binary search in a sorted list, in a sorted matrix, in two sorted lists.
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
            // removed due to complication, see the change history for implementation
            return SaddlebackBs(matrix, k);
        }

        /*
         * tags: min-max
         * Time(nlogw), Space(1), w is maxValue-minValue
         * Lc 875. Koko Eating Bananas
         * Lc 2064. Minimized Maximum of Products Distributed to Any Store.
         * similar to, place n-color balls into m buckets, each bucket should have same color of balls, 
         * there is a bucket with maximum number of ball, find the mimimized maximum number 
         */
        public int MinimizedMaximum(int n, int[] balls)
        {
            int lo = 0, hi = balls.Max();
            while (lo < hi)
            {
                int mi = (lo + hi) / 2;

                // is possible to fit?
                int count = 0; // needed buckets if place at-most mi balls in each bucket
                foreach (var cnt in balls)
                    count += (cnt + mi - 1) / mi;

                if (count <= n) // yes, possible
                    hi = mi; // try less
                else
                    lo = mi + 1;
            }

            return lo;
        }

        int LowerBound(int[] nums, long val)
        {
            if (nums.Last() < val)
                return nums.Length;

            int lo = 0, hi = nums.Length - 1;
            while (lo < hi)
            {
                int mi = lo + (hi - lo) / 2;
                if (nums[mi] >= val) hi = mi; // greater than or equal to
                else lo = mi + 1;
            }

            return lo;
        }

        int UpperBound(int[] nums, long val)
        {
            if (nums.Last() <= val)
                return nums.Length;

            int lo = 0, hi = nums.Length - 1;
            while (lo < hi)
            {
                int mi = lo + (hi - lo) / 2;
                if (nums[mi] > val) hi = mi; // strictly greater
                else lo = mi + 1;
            }

            return lo;
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

            var balls = new int[] { 11, 6 };
            Console.WriteLine(MinimizedMaximum(6, balls) == 3);
            Console.WriteLine(MinimizedMaximum(4, balls) == 6);

            balls = new int[] { 15, 10, 10 };
            Console.WriteLine(MinimizedMaximum(7, balls) == 5);

            nums = new int[] { 2, 3, 4, 4, 4, 24, 234 };
            Console.WriteLine(LowerBound(nums, 4) == 2);
            Console.WriteLine(UpperBound(nums, 4) == 5);
        }
    }
}
