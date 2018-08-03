using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: greedy
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc517_Super_Washing_Machines
    {
        public int FindMinMoves(int[] machines)
        {
            var n = machines.Length;
            if (n <= 1) return 0;

            var sums = new int[n + 1];
            for (int i = 0; i < n; i++)
                sums[i + 1] = sums[i] + machines[i];

            if (sums[n] % n != 0) return -1;
            int mean = sums[n] / n;
            int res = 0;

            int zeroToLeft = 0, zeroToRight = 0;
            for (int i = 0; i < n; i++)
            {
                res = Math.Max(res, machines[i] - mean);
                if (sums[i + 1] >= (i + 1) * mean)
                {
                    res = Math.Max(res, mean - machines[i] + zeroToRight);
                    if (machines[i] == 0) zeroToRight++;
                    zeroToLeft = 0;
                }
                else
                {
                    res = Math.Max(res, mean - machines[i] + zeroToLeft);
                    if (machines[i] == 0) zeroToLeft++;
                    zeroToRight = 0;
                }
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 1, 0, 5 };
            Console.WriteLine(FindMinMoves(nums) == 3);

            nums = new int[] { 0, 3, 0 };
            Console.WriteLine(FindMinMoves(nums) == 2);

            nums = new int[] { 0, 2, 0 };
            Console.WriteLine(FindMinMoves(nums) == -1);
        }
    }
}

