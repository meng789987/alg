
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: saddleback binary search
 * Time(nlogw), Space(1), where w=maxValue-minValue
 */
namespace leetcode
{
    public class Lc719_Find_Kth_Smallest_Pair_Distance
    {
        public int SmallestDistancePair(int[] nums, int k)
        {
            Array.Sort(nums);
            int lo = 0, hi = nums[nums.Length - 1] - nums[0];

            while (lo < hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (Count(nums, mid) < k) lo = mid + 1;
                else hi = mid;
            }

            return lo;
        }

        // saddleback binary search to return the count of distances less than or equal to the given value
        int Count(int[] nums, int value)
        {
            int count = 0;
            int n = nums.Length;
            for (int j = 1, i = 0; i < n - 1; i++)
            {
                while (j < n && nums[j] - nums[i] <= value) j++;
                count += j - i - 1;
            }
            return count;
        }


        public void Test()
        {
            var nums = new int[] { 1, 3, 1 };
            Console.WriteLine(SmallestDistancePair(nums, 1) == 0);
            Console.WriteLine(SmallestDistancePair(nums, 2) == 2);
            Console.WriteLine(SmallestDistancePair(nums, 3) == 2);

            nums = new int[] { 1, 4, 6, 7, 10, 15 };
            Console.WriteLine(SmallestDistancePair(nums, 1) == 1);
            Console.WriteLine(SmallestDistancePair(nums, 8) == 5);
            Console.WriteLine(SmallestDistancePair(nums, 13) == 9);
        }
    }
}

