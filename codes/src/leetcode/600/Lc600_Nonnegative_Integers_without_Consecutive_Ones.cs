
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: dp
 * Time(n), Space(n), n is width of num
 */
namespace leetcode
{
    public class Lc600_Nonnegative_Integers_without_Consecutive_Ones
    {
        public int FindIntegers(int num)
        {
            var dp = new int[32];
            dp[0] = 1; dp[1] = 2;
            for (int i = 2; i < dp.Length; i++)
                dp[i] = dp[i - 1] + dp[i - 2];

            int res = 1;
            for (int prevBit = 0, i = 30; i >= 0; i--)
            {
                if ((num & (1 << i)) != 0)
                {
                    res += dp[i];
                    if (prevBit == 1) { res--; break; }
                    prevBit = 1;
                }
                else prevBit = 0;
            }

            return res;
        }

        public int FindIntegers2(int num)
        {
            var s = Convert.ToString(num, 2);
            int n = s.Length;
            var dp = GetDp(n);
            int res = dp[0, n - 1] + dp[1, n - 1];
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '0' && s[i - 1] == '0') res -= dp[1, n - 1 - i];
                if (s[i] == '1' && s[i - 1] == '1') break;
            }

            return res;
        }

        /*
         * dp[0, i] is count of satisfied numbers with width (i+1) and first bit is 0
         * dp[1, i] is count of satisfied numbers with width (i+1) and first bit is 1
         */
        int[,] GetDp(int n)
        {
            var dp = new int[2, n];
            dp[0, 0] = dp[1, 0] = 1;
            for (int i = 1; i < n; i++)
            {
                dp[0, i] = dp[0, i - 1] + dp[1, i - 1];
                dp[1, i] = dp[0, i - 1];
            }
            return dp;
        }


        public void Test()
        {
            Console.WriteLine(FindIntegers(5) == 5);
            Console.WriteLine(FindIntegers(6) == 5);
            Console.WriteLine(FindIntegers(10) == 8);

            Console.WriteLine(FindIntegers2(5) == 5);
            Console.WriteLine(FindIntegers2(6) == 5);
            Console.WriteLine(FindIntegers2(10) == 8);
        }
    }
}
