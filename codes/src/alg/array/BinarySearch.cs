using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: binary seach, saddle search in a sorted list, in a sorted matrix, in two sorted lists.
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
         * Time(n), Space(n)
         * find two numbers given their sum in a unsorted array
         */
        public int[] SaddleBs(int[] nums, int sum)
        {
            var map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int j;
                if (map.TryGetValue(sum - nums[i], out j)) return new int[] { i, j };
                map[nums[i]] = i;
            }
            return null;
        }

        public void Test()
        {
            var nums = new int[] { 2, 3, 4, 5, 7, 24, 234 };
            Console.WriteLine(BasicBs(nums, 7) == 4);
            Console.WriteLine(BasicBs(nums, 10) );
        }
    }
}
