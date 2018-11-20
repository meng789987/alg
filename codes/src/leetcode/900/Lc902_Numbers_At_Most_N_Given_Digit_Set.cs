using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * tags: math
 * Time(logn), Space(logn)
 */
namespace leetcode
{
    public class Lc902_Numbers_At_Most_N_Given_Digit_Set
    {
        public int AtMostNGivenDigitSet(string[] D, int N)
        {
            int ret = 0, n = D.Length;
            var dp = new int[11];
            dp[0] = 1;
            int ten = 1, dlen = 1;

            for (; ten <= N / 10; ten *= 10, dlen++)
                ret += dp[dlen] = dp[dlen - 1] * n;

            for (; N > 0; ten /= 10, dlen--)
            {
                if (N > ten) ret += (N / ten - 1) * dp[dlen - 1];
                N %= ten;
            }

            return ret;
        }

        public void Test()
        {
            var d = new string[] { "1", "3", "5", "7" };
            Console.WriteLine(AtMostNGivenDigitSet(d, 100) == 20);

            d = new string[] { "1", "4", "9" };
            Console.WriteLine(AtMostNGivenDigitSet(d, 1000000000) == 29523);

            d = new string[] { "4" };
            Console.WriteLine(AtMostNGivenDigitSet(d, 5) == 1);
        }
    }
}
