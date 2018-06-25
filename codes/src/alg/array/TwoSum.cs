using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: hash, two pointers
 * find two numbers given their sum in a sorted/unsorted array
 */
namespace alg.array
{
    public class TwoSum
    {
        /*
         * Time(n), Space(1)
         * find two numbers given their sum in a sorted array
         */
        public int[] TwoSumSorted(int[] nums, int sum)
        {
            for (int i = 0, j = nums.Length - 1; i < j; )
            {
                if (nums[i] + nums[j] == sum) return new int[] { i, j };
                if (nums[i] + nums[j] < sum) i++;
                else j--;
            }
            return null;
        }

        /*
         * Time(n), Space(n)
         * find two numbers given their sum in a unsorted array
         */
        public int[] TwoSumUnsorted(int[] nums, int sum)
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
            var exp = new int[] { 1, 4 };
            Console.WriteLine(exp.SameSet(TwoSumSorted(nums, 10)));
            Console.WriteLine(exp.SameSet(TwoSumUnsorted(nums, 10)));
        }
    }
}
