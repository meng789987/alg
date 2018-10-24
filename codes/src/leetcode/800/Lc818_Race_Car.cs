using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dp
 * Time(nlogn), Space(n)
 * dp[t] = k, if t==2^k-1 (drive A^k);
 *         min(dp[t], dp[t - 2^(k-1) + 2^j)] + k - 1 + j + 2), where j=[0..k-2] (drive to u then A^(k-1)RA^jR)
 *         min(dp[t], dp[2^k - 1 - t] + k + 1) (drive A^kR then to t)
 */
namespace leetcode
{
    public class Lc818_Race_Car
    {
        public int Racecar(int target)
        {
            if (_dp[target] > 0) return _dp[target];

            int k = Convert.ToString(target, 2).Length;
            if (target == (1 << k) - 1) return _dp[target] = k;

            _dp[target] = Racecar((1 << k) - 1 - target) + k + 1;
            for (int j = 0; j < k - 1; ++j)
            {
                var step = Racecar(target - (1 << (k - 1)) + (1 << j)) + k - 1 + j + 2;
                _dp[target] = Math.Min(_dp[target], step);
            }

            return _dp[target];
        }

        public int Racecar2(int target)
        {
            if (_dp[target] > 0) return _dp[target];

            Array.Fill(_dp, int.MaxValue);
            _dp[0] = 0; _dp[1] = 1;

            for (int t = 2; t < _dp.Length; t++)
            {
                int k = Convert.ToString(t, 2).Length;

                // case 1: exact 2^k-1. drive A^k
                if (t == (1 << k) - 1)
                {
                    _dp[t] = k;
                    continue;
                }

                // case 2: not cross 2^k-1. drive to u then A^(k-1)RA^jR
                for (int j = 0; j < k - 1; ++j)
                    _dp[t] = Math.Min(_dp[t], _dp[t - (1 << (k - 1)) + (1 << j)] + k - 1 + j + 2);

                // case 3: cross 2^k-1. drive A^kR then to t
                if ((1 << k) - 1 - t < t)
                    _dp[t] = Math.Min(_dp[t], _dp[(1 << k) - 1 - t] + k + 1);
            }

            return _dp[target];
        }

        static int[] _dp = new int[10001];

        public void Test()
        {
            Console.WriteLine(Racecar(3) == 2);
            Console.WriteLine(Racecar(6) == 5);
            Console.WriteLine(Racecar(5) == 7);
            Console.WriteLine(alg.Extensions.ElapsedMilliseconds(() => Racecar(10000), 10));
        }
    }
}
