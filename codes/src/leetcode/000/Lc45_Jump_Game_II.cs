using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: greedy
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc45_Jump_Game_II
    {
        public int Jump(int[] nums)
        {
            int res = 0;
            for (int far = 0, farest = 0, i = 0; i < nums.Length - 1; i++)
            {
                far = Math.Max(far, i + nums[i]);
                if (i == farest) // one more jump
                {
                    farest = far;
                    res++;
                }
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 2, 3, 1, 1, 4 };
            Console.WriteLine(Jump(nums) == 2);
        }
    }
}

