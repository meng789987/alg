using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: Euclid gcd
 * Time(logn), Space(1)
 */
namespace leetcode
{
    public class Lc780_Reaching_Points
    {
        public bool ReachingPoints(int sx, int sy, int tx, int ty)
        {
            while (sx < tx && sy < ty)
            {
                if (tx > ty) tx %= ty;
                else ty %= tx;
            }

            return (sx == tx && (ty - sy) % sx == 0) || (sy == ty && (tx - sx) % sy == 0);
        }

        public bool ReachingPoints2(int sx, int sy, int tx, int ty)
        {
            while (sx <= tx && sy <= ty)
            {
                if (sx == tx && sy == ty) return true;
                if (ty >= tx) ty = Math.Min(ty - tx, sy / tx * tx + (ty % tx) + tx);
                else tx = Math.Min(tx - ty, sx / ty * ty + (tx % ty) + ty);
            }

            return false;
        }

        public void Test()
        {
            Console.WriteLine(ReachingPoints(1, 1, 3, 5) == true);
            Console.WriteLine(ReachingPoints(1, 1, 2, 2) == false);
            Console.WriteLine(ReachingPoints(1, 1, 1, 1) == true);
            Console.WriteLine(ReachingPoints(3, 3, 12, 9) == true);
        }
    }
}

