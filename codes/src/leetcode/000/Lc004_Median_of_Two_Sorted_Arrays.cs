
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: bs
 * Time(n), Space(1)
 * divide into two parts, left is a[0..i-1] and b[0..j-1], right is a[i..m-1] and b[j..n-1], where i=[0..m], j=[0..n]
 * make sure max(left) <= min(right) and count(left)==count(right) [+ 1 if odd]
 */
namespace leetcode
{
    public class Lc004_Median_of_Two_Sorted_Arrays
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                var t = nums1;
                nums1 = nums2;
                nums2 = t;
            }
            int m = nums1.Length, n = nums2.Length;

            for (int lo = 0, hi = m; ;)
            {
                int i = (lo + hi) / 2;
                int j = (m + n + 1) / 2 - i;

                int maxLeft1 = i > 0 ? nums1[i - 1] : int.MinValue;
                int maxLeft2 = j > 0 ? nums2[j - 1] : int.MinValue;
                int minRight1 = i < m ? nums1[i] : int.MaxValue;
                int minRight2 = j < n ? nums2[j] : int.MaxValue;

                if (maxLeft1 > minRight2) hi = i - 1;
                else if (maxLeft2 > minRight1) lo = i + 1;
                else if ((m + n) % 2 == 1) return Math.Max(maxLeft1, maxLeft2);
                else return (Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2.0;
            }
        }

        public void Test()
        {
            var nums1 = new int[] { 1, 2 };
            var nums2 = new int[] { 2 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2) == 2);

            nums1 = new int[] { 1, 2 };
            nums2 = new int[] { 3, 4 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2) == 2.5);

            nums1 = new int[] { 2 };
            nums2 = new int[] { 1 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2) == 1.5);

            nums1 = new int[] { };
            nums2 = new int[] { 1 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2) == 1);

            nums1 = new int[] { 3 };
            nums2 = new int[] { 1, 2 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2) == 2);
        }
    }
}
