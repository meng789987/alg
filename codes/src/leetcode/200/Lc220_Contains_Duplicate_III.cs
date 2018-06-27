using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: buckets, bst
 * c# doesn't have a bst with an API to get the closest value, like lower_bound() in c++
 */
namespace leetcode
{
    public class Lc220_Contains_Duplicate_III
    {
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            if (k < 1 || t < 0) return false;
            var map = new Dictionary<long, long>();
            for (int i = 0; i < nums.Length; i++)
            {
                var value = (long)nums[i] - int.MinValue; // rearrange to avoid negative numbers
                var bucket = value / ((long)t + 1);
                if (map.ContainsKey(bucket)
                    || (map.ContainsKey(bucket - 1) && value - map[bucket - 1] <= t)
                    || (map.ContainsKey(bucket + 1) && map[bucket + 1] - value <= t))
                    return true;
                map[bucket] = value;
                if (i >= k) map.Remove(((long)nums[i - k] - int.MinValue) / ((long)t + 1));
            }

            return false;
        }

        public void Test()
        {
            var nums = new int[] { 1, 2, 3, 1 };
            Console.WriteLine(ContainsNearbyAlmostDuplicate(nums, 3, 0) == true);

            nums = new int[] { 1, 0, 1, 1 };
            Console.WriteLine(ContainsNearbyAlmostDuplicate(nums, 1, 2) == true);

            nums = new int[] { 1, 5, 9, 1, 5, 9 };
            Console.WriteLine(ContainsNearbyAlmostDuplicate(nums, 2, 3) == false);
        }
    }
}

