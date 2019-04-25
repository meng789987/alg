using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: greedy, bfs
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc45_Jump_Game_II
    {
        public int Jump(int[] nums)
        {
            int res = 0;
            for (int far = 0, farthest = 0, i = 0; i < nums.Length - 1; i++)
            {
                far = Math.Max(far, i + nums[i]);
                if (i == farthest) // one more jump
                {
                    farthest = far;
                    res++;
                }
            }

            return res;
        }

        public int JumpBfs(int[] nums)
        {
            int res = 0;
            for (int i = 0, q = 0; q < nums.Length - 1; )
            {
                res++;
                for (int currq = q; i <= currq; i++)
                    q = Math.Max(q, i + nums[i]);
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 2, 3, 1, 1, 4 };
            Console.WriteLine(Jump(nums) == 2);
            Console.WriteLine(JumpBfs(nums) == 2);
        }
    }
}

