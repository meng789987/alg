using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: greedy
 * Time(kmn), Space(k)
 * 1. get max number from one array
 * 2. get max number from two arrays using all of their digits.
 */
namespace leetcode
{
    public class Lc321_Create_Maximum_Number
    {
        public int[] MaxNumber(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length, n = nums2.Length;
            k = Math.Min(k, m + n);
            var ret = new int[k];
            for (int i = Math.Max(0, k - n); i <= k && i <= m; i++)
            {
                var max = Merge(MaxNumber(nums1, i), MaxNumber(nums2, k - i));
                if (IsGreater(max, 0, ret, 0)) ret = max;
            }
            return ret;
        }

        // return the max number from nums using k digits
        int[] MaxNumber(int[] nums, int k)
        {
            var ret = new int[k];
            int ri = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                while (ri > 0 && i + k - ri < nums.Length && ret[ri - 1] < nums[i]) ri--;
                if (ri < k) ret[ri++] = nums[i];
            }
            return ret;
        }

        int[] Merge(int[] nums1, int[] nums2)
        {
            int m = nums1.Length, n = nums2.Length;
            var ret = new int[m + n];
            for (int i = 0, j = 0, k = 0; k < m + n; k++)
            {
                ret[k] = IsGreater(nums1, i, nums2, j) ? nums1[i++] : nums2[j++];
            }
            return ret;
        }

        bool IsGreater(int[] nums1, int i, int[] nums2, int j)
        {
            while (i < nums1.Length && j < nums2.Length && nums1[i] == nums2[j])
            {
                i++;
                j++;
            }

            return j >= nums2.Length || (i < nums1.Length && nums1[i] > nums2[j]);
        }

        public void Test()
        {
            var nums1 = new int[] { 3, 4, 6, 5 };
            var nums2 = new int[] { 9, 1, 2, 5, 8, 3 };
            var exp = new int[] { 9, 8, 6, 5, 3 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 5)));

            nums1 = new int[] { 6, 7 };
            nums2 = new int[] { 6, 0, 4 };
            exp = new int[] { 6, 7, 6, 0, 4 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 5)));

            nums1 = new int[] { 3, 9 };
            nums2 = new int[] { 8, 9 };
            exp = new int[] { 9, 8, 9 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 3)));

            nums1 = new int[] { 2, 5, 6, 4, 4, 0 };
            nums2 = new int[] { 7, 3, 8, 0, 6, 5, 7, 6, 2 };
            exp = new int[] { 7, 3, 8, 2, 5, 6, 4, 4, 0, 6, 5, 7, 6, 2, 0 };
            Console.WriteLine(exp.SequenceEqual(MaxNumber(nums1, nums2, 15)));
        }
    }
}

