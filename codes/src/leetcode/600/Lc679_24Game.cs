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
 * count of all possible 4 digits: 9^4=6561
 * There are really only 495 possible sorted inputs, of which 404 are solvable and 91 aren't.
 */
namespace leetcode
{
    public class Lc679_24Game
    {
        public bool JudgePoint24(int[] nums)
        {
            return Dfs(nums.Select(n => (double)n).ToArray());
        }

        bool Dfs(double[] nums)
        {
            if (nums.Length == 1)
                return Math.Abs(nums[0] - 24) < 1e-6;

            var nums2 = nums.ToArray();
            Array.Sort(nums2);
            nums = nums2;

            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i - 1] == nums[i]) continue; // dup
                for (int j = i + 1; j < nums.Length; j++)
                {
                    var ns = new double[nums.Length - 1];
                    int ni = 0;
                    for (int k = 0; k < nums.Length; k++)
                        if (k != i && k != j) ns[ni++] = nums[k];

                    double a = nums[i], b = nums[j];
                    var opres = new double[] { a + b, a - b, b - a, a * b, a / b, b / a };
                    foreach (var r in opres)
                    {
                        ns[ni] = r;
                        if (Dfs(ns)) return true;
                    }
                }
            }

            return false;
        }

        public void Test()
        {
            Console.WriteLine(JudgePoint24(new int[] { 4, 1, 8, 7 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 1, 2, 1, 2 }) == false);
            Console.WriteLine(JudgePoint24(new int[] { 1, 4, 6, 1 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 3, 9, 7, 7 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 3, 3, 8, 8 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 7, 6, 7, 5 }) == true);
            Console.WriteLine(JudgePoint24(new int[] { 1, 9, 1, 2 }) == true);
        }
    }
}

