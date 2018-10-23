using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dp
 * Time(nlogn), Space(n)
 */
namespace leetcode
{
    public class Lc818_Race_Car
    {
        public int Racecar(int target)
        {
            if (_dp != null)
                return _dp[target];

            _dp = new int[10003];
            Array.Fill(_dp, int.MaxValue);
            _dp[0] = 0; _dp[1] = 1; _dp[2] = 4;

            for (int t = 3; t < _dp.Length; ++t)
            {
                int k = Convert.ToString(t, 2).Length;
                if (t == (1 << k) - 1)
                {
                    _dp[t] = k;
                    continue;
                }
                for (int j = 0; j < k - 1; ++j)
                    _dp[t] = Math.Min(_dp[t], _dp[t - (1 << (k - 1)) + (1 << j)] + k - 1 + j + 2);
                if ((1 << k) - 1 - t < t)
                    _dp[t] = Math.Min(_dp[t], _dp[(1 << k) - 1 - t] + k + 1);
            }

            return _dp[target];
        }

        static int[] _dp;

        public void Test()
        {
            Console.WriteLine(Racecar(3) == 2);
            Console.WriteLine(Racecar(6) == 5);
            Console.WriteLine(Racecar(5) == 7);
            Console.WriteLine(alg.Extensions.ElapsedMilliseconds(() => Racecar(10000), 10));
        }
    }
}
