using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: math, flow
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc517_Super_Washing_Machines
    {
        /*
         * accumulate gain or lose, all of them are needed to balance
         */
        public int FindMinMoves(int[] machines)
        {
            int n = machines.Length;
            var sum = machines.Sum();
            if (sum % n != 0) return -1;

            int res = 0, bal = 0;
            var mean = sum / n;
            foreach (var load in machines)
            {
                bal += load - mean;
                res = Math.Max(res, Math.Max(Math.Abs(bal), load - mean));
            }

            return res;
        }

        /*
         * the min number of operations is the max dresses flowing (in/out) through machine i, i=[0..n-1]
         */
        public int FindMinMoves2(int[] machines)
        {
            int n = machines.Length;
            var sum = machines.Sum();
            if (sum % n != 0) return -1;

            int res = 0;
            var mean = sum / n;
            for (int lsum = 0, i = 0; i < n; i++)
            {
                int lneed = i * mean - lsum;
                int rneed = (n - i - 1) * mean - (sum - lsum - machines[i]);
                if (lneed > 0 && rneed > 0) res = Math.Max(res, lneed + rneed);
                else res = Math.Max(res, Math.Max(Math.Abs(lneed), Math.Abs(rneed)));
                lsum += machines[i];
            }

            return res;
        }

        public void Test()
        {
            var nums = new int[] { 1, 0, 5 };
            Console.WriteLine(FindMinMoves(nums) == 3);
            Console.WriteLine(FindMinMoves2(nums) == 3);

            nums = new int[] { 0, 3, 0 };
            Console.WriteLine(FindMinMoves(nums) == 2);
            Console.WriteLine(FindMinMoves2(nums) == 2);

            nums = new int[] { 0, 2, 0 };
            Console.WriteLine(FindMinMoves(nums) == -1);
            Console.WriteLine(FindMinMoves2(nums) == -1);

            nums = new int[] { 100000, 0, 100000, 0, 100000, 0, 100000, 0, 100000, 0, 100000, 0 };
            Console.WriteLine(FindMinMoves(nums) == 50000);
            Console.WriteLine(FindMinMoves2(nums) == 50000);

            nums = new int[] { 0, 0, 11, 5 };
            Console.WriteLine(FindMinMoves(nums) == 8);
            Console.WriteLine(FindMinMoves2(nums) == 8);

            nums = new int[] { 0, 0, 10, 0, 0, 0, 10, 0, 0, 0 };
            Console.WriteLine(FindMinMoves(nums) == 8);
            Console.WriteLine(FindMinMoves2(nums) == 8);

            nums = new int[] { 9, 1, 8, 8, 9 };
            Console.WriteLine(FindMinMoves(nums) == 4);
            Console.WriteLine(FindMinMoves2(nums) == 4);
        }
    }
}

