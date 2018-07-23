using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math, state machine
 * Time(n), Space(1)
 *        +-----------+
 *        |           |
 *        +--> long --+
 *            /  \
 *           /    \
 *         \/      \/
 *       equal ==> short <==> short
 *       
 *  e=>s: crossing x[i-4]? x[i] + x[i - 4] >= x[i - 2]
 *  s=>s: crossing x[i-5]? prev is e or s or x[i] + x[i - 4] >= x[i - 2] && x[i - 1] + x[i - 5] >= x[i - 3]
 */
namespace leetcode
{
    public class Lc335_Self_Crossing
    {
        enum Status { L, S, E }
        public bool IsSelfCrossing(int[] x)
        {
            if (x.Length <= 3) return false;
            Status pps, ps, s;
            pps = ps = s = Status.L;
            for (int i = 2; i < x.Length; i++)
            {
                pps = ps;
                ps = s;
                s = GetStatus(x, i);

                if (ps == Status.E && (s == Status.L || s == Status.E || i >= 4 && x[i] + x[i - 4] >= x[i - 2]))
                    return true;
                if (ps == Status.S && (s == Status.L || s == Status.E ||
                        pps == Status.L && i >= 5 && x[i] + x[i - 4] >= x[i - 2] && x[i - 1] + x[i - 5] >= x[i - 3]))
                    return true;
            }

            return false;
        }

        Status GetStatus(int[] x, int i)
        {
            if (x[i] > x[i - 2]) return Status.L;
            if (x[i] < x[i - 2]) return Status.S;
            return Status.E;
        }

        public bool IsSelfCrossingDir(int[] x)
        {
            if (x.Length <= 3) return false;
            bool expanding = x[2] > x[0];
            for (int i = 3; i < x.Length; i++)
            {
                if (!expanding && x[i] >= x[i - 2]) return true;
                if (expanding && x[i] <= x[i - 2])
                {
                    expanding = false;
                    if (x[i] + (i >= 4 ? x[i - 4] : 0) >= x[i - 2])
                        x[i - 1] -= x[i - 3]; // update boundary
                }
            }

            return false;
        }

        public void Test()
        {
            var nums = new int[] { 2, 1, 1, 2 };
            Console.WriteLine(IsSelfCrossing(nums) == true);
            Console.WriteLine(IsSelfCrossingDir(nums) == true);

            nums = new int[] { 1, 2, 3, 4 };
            Console.WriteLine(IsSelfCrossing(nums) == false);
            Console.WriteLine(IsSelfCrossingDir(nums) == false);

            nums = new int[] { 1, 1, 1, 1 };
            Console.WriteLine(IsSelfCrossing(nums) == true);
            Console.WriteLine(IsSelfCrossingDir(nums) == true);
        }
    }
}

