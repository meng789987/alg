
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: math, dp
 * Time(logn), Space(1)
 * X-based representation of target
 * for each bit, it can be positive or negative
 * to get minimum operation, consider 10-based representation of 86
 * 86 = 80 + 6
 * 86 = 90 - 4
 * 86 = 100 - 20 + 6
 * 86 = 100 - 10 - 4
 */
namespace leetcode
{
    public class Lc964_Least_Operators_to_Express_Number
    {
        /// <summary>
        /// X-based representation of target
        /// for each bit, it can be positive or negative
        /// to get minimum operation, consider 10-based representation of 86
        /// 86 = 80 + 6
        /// 86 = 90 - 4
        /// 86 = 100 - 20 + 6
        /// 86 = 100 - 10 - 4
        /// </summary>
        /// <param name="x"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int LeastOpsExpressTarget2(int x, int target)
        {
            int r = target % x;
            int pos = r * 2;
            int neg = (x - r) * 2;
            int i = 1;
            for (target /= x; target > 0; target /= x, i++)
            {
                r = target % x;
                int npos = Math.Min(r * i + pos, (r + 1) * i + neg);
                int nneg = Math.Min((x - r) * i + pos, (x - r - 1) * i + neg);
                (pos, neg) = (npos, nneg);
            }

            return Math.Min(pos - 1, i + neg - 1);
        }

        Dictionary<int, int> memo = new Dictionary<int, int>();
        public int LeastOpsExpressTarget(int x, int target)
        {
            if (target <= x) return Math.Min(target * 2 - 1, (x - target) * 2);
            if (memo.ContainsKey(target)) return memo[target];

            long product = x;
            int times = 0;
            while (product < target)
            {
                product *= x;
                times++;
            }

            if (target == product) return memo[target] = times;

            int res = LeastOpsExpressTarget(x, target - (int)(product / x)) + times; // use addition
            if (product - target < target) // use subtraction
                return memo[target] = Math.Min(res, LeastOpsExpressTarget(x, (int)(product - target)) + times + 1);

            return memo[target] = res;
        }

        public void Test()
        {
            Console.WriteLine(LeastOpsExpressTarget(3, 19) == 5);
            Console.WriteLine(LeastOpsExpressTarget(5, 501) == 8);
            Console.WriteLine(LeastOpsExpressTarget(100, 100000000) == 3);
            Console.WriteLine(LeastOpsExpressTarget(3, 365) == 17);
            Console.WriteLine(LeastOpsExpressTarget(3, 929) == 19);
        }
    }
}
