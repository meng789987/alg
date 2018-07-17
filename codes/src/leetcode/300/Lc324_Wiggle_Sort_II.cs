using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: sort, 3-way partition, index sort
 * Given an unsorted array nums, reorder it such that nums[0] < nums[1] > nums[2] < nums[3]....
 */
namespace leetcode
{
    public class Lc324_Wiggle_Sort_II
    {
        /*
         * Time(nlogn), Space(n)
         */
        public void WiggleSort(int[] nums)
        {
            var ns = nums.ToArray();
            Array.Sort(ns);
            for (int i = 0, j = ns.Length / 2, k = ns.Length - 1; k >= 0; k--)
            {
                nums[k] = ns[k % 2 == 0 ? i++ : j++];
            }
        }

        /*
         * 3-way partition, index sort
         * Time(n), Space(1)
         */
        public void WiggleSort2(int[] nums)
        {
            int n = nums.Length;
            int mid = GetKthSmallest(nums, n / 2);
            for (int lo = 0, hi = n - 1, i = 0; i <= hi;)
            {
                if (nums[Remap(n, i)] > mid) Swap(nums, Remap(n, i++), Remap(n, lo++));
                else if (nums[Remap(n, i)] < mid) Swap(nums, Remap(n, i), Remap(n, hi--));
                else i++;
            }
        }


        int GetKthSmallest(int[] nums, int k)
        {
            Array.Sort(nums);
            return nums[k];
        }

        // map index from 0,1,2,..mid to 1,3,5..., and from mid+1,mid+2... to 0,2,4...
        int Remap(int n, int i)
        {
            return (1 + 2 * i) % (n | 1);
        }

        void Swap(int[] nums, int i, int j)
        {
            int t = nums[i];
            nums[i] = nums[j];
            nums[j] = t;
        }

        // expected: nums[0] < nums[1] > nums[2] < nums[3]....
        bool IsExpected(int[] nums)
        {
            for (int i = 0; i < nums.Length; i += 2)
            {
                if (i > 0 && nums[i - 1] <= nums[i]) return false;
                if (i < nums.Length - 1 && nums[i] >= nums[i + 1]) return false;
            }
            return true;
        }

        public void Test()
        {
            var nums = new int[] { 1, 5, 1, 1, 6, 4 };
            var nums2 = nums.ToArray();
            WiggleSort(nums);
            Console.WriteLine(IsExpected(nums));
            WiggleSort2(nums2);
            Console.WriteLine(IsExpected(nums2));

            nums = new int[] { 1, 3, 2, 2, 3, 1 };
            nums2 = nums.ToArray();
            WiggleSort(nums);
            Console.WriteLine(IsExpected(nums));
            WiggleSort2(nums2);
            Console.WriteLine(IsExpected(nums2));
        }

    }
}

