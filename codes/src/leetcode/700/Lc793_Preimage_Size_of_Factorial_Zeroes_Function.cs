using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bs
 * actually, the anwser must be 0 or 5.
 */
namespace leetcode
{
    public class Lc793_Preimage_Size_of_Factorial_Zeroes_Function
    {
        public int PreimageSizeFZF(int K)
        {
            return (int)(LowerBound(K + 1) - LowerBound(K));
        }

        long LowerBound(long K)
        {
            long lo = 0, hi = 5L * (K + 1);
            while (lo <= hi)
            {
                long mid = lo + (hi - lo) / 2;
                long cnt = ZeroCount(mid);
                if (cnt >= K) hi = mid - 1;
                else lo = mid + 1;
            }

            return lo;
        }

        long ZeroCount(long n)
        {
            long ret = 0;
            while (n > 0)
            {
                ret += n / 5;
                n /= 5;
            }

            return ret;
        }

        public void Test()
        {
            Console.WriteLine(PreimageSizeFZF(0) == 5);
            Console.WriteLine(PreimageSizeFZF(5) == 0);
        }
    }
}

