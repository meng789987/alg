
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: hash
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc001_Two_Sum
    {
        public int[] TwoSum(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();
            for (int j, i = 0; i < nums.Length; i++)
            {
                if (map.TryGetValue(target - nums[i], out j))
                    return new int[] { j, i };
                map[nums[i]] = i;
            }
            return null;
        }

        public void Test()
        {
            var nums = new int[] { 2, 7, 11, 15 };
            var exp = new int[] { 0, 1 };
            Console.WriteLine(exp.SameSet(TwoSum(nums, 9)));
        }
    }
}