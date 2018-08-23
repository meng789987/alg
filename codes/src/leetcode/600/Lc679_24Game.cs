using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bt
 * Time(1), Space(1)
 * Permutation: 4*3*2*1;
 * Operator in 3 positions: 4*4*4;
 * Parentheses, sequence of 3 op: 3*2*1
 * total: 24*64*6=9216
 */
namespace leetcode
{
    public class Lc679_24Game
    {
        public bool JudgePoint24(int[] nums)
        {
            Array.Sort(nums);
            return DfsPermutation(nums, new List<int>(), new bool[4]);
        }

        bool DfsPermutation(int[] nums, List<int> selected, bool[] visited)
        {
            if (selected.Count == nums.Length)
                return DfsOperator(selected.ToArray(), 0, new List<int>());

            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i - 1] == nums[i]) continue; // dup
                if (!visited[i])
                {
                    visited[i] = true;
                    selected.Add(nums[i]);
                    if (DfsPermutation(nums, selected, visited)) return true;
                    selected.RemoveAt(selected.Count - 1);
                    visited[i] = false;
                }
            }

            return false;
        }

        bool DfsOperator(int[] nums, int start, List<int> selected)
        {
            if (selected.Count == nums.Length - 1)
                return Calc(nums, selected.ToArray());

            for (int i = 0; i < 4; i++)
            {
                    selected.Add(i);
                    if (DfsOperator(nums,start +1, selected)) return true;
                    selected.RemoveAt(selected.Count - 1);
            }

            return false;
        }

        bool Calc(int[] nums, int[] ops)
        {

            return false;
        }

        public void Test()
        {
            Console.WriteLine(JudgePoint24(new int[] { 4, 1, 8, 7 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 1, 2, 1, 2 }) == false);
        }
    }
}

