using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: Moore’s Voting Algorithm
 * Time(n), Space(1)
 */
namespace alg.array
{
    public class FindMajority_Moore
    {
        public int FindMajority(int[] nums)
        {
            int v = FindCandidate(nums);

            // verify it is majority
            if (nums.Count(i => i == v) >= (nums.Length + 1) / 2)
                return v;

            return -1;
        }

        int FindCandidate(int[] nums)
        {
            int ret = nums[0];
            for (int cnt = 1, i = 1; i < nums.Length; i++)
            {
                if (nums[i] == ret) cnt++;
                else if (cnt > 0) cnt--;
                else
                {
                    cnt = 1;
                    ret = nums[i];
                }
            }
            return ret;
        }

        public void Test()
        {
            var nums = new int[] { 2, 2, 3, 5, 2, 2, 6 };
            Console.WriteLine(FindMajority(nums) == 2);
        }
    }
}

