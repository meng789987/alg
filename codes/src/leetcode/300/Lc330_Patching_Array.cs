using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: greedy
 * Time(logn), Space(1)
 * we need to add a new number of sum+1, where sum is sum of all previous numbers.
 */
namespace leetcode
{
    public class Lc330_Patching_Array
    {
        public int MinPatches(int[] nums, int n)
        {
            int res = 0;
            long sum = 0;
            for (int i = 0; sum < n;)
            {
                if (i < nums.Length && nums[i] <= sum + 1)
                    sum += nums[i++];
                else
                {
                    sum += sum + 1;
                    res++;
                }
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 1, 3 };
            Console.WriteLine(MinPatches(nums, 6) == 1); // add {2}

            nums = new int[] { 1, 5, 10 };
            Console.WriteLine(MinPatches(nums, 20) == 2); // add {2, 4}

            nums = new int[] { 1, 2, 2 };
            Console.WriteLine(MinPatches(nums, 5) == 0); // add {}

            nums = new int[] { 1, 2, 31, 33 };
            Console.WriteLine(MinPatches(nums, 2147483647) == 28); // add {}
        }
    }
}

