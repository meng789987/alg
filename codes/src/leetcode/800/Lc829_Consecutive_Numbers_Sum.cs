using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math
 * Time(n^(1/2)), Space(1)
 */
namespace leetcode
{
    public class Lc829_Consecutive_Numbers_Sum
    {
        public int ConsecutiveNumbersSum(int N)
        {
            int ret = 0;

            for (int k = 1; k < Math.Sqrt(2 * N); k++)
                if ((N - k * (k - 1) / 2) % k == 0) ret++;

            return ret;
        }

        public int ConsecutiveNumbersSum2(int N)
        {
            int ret = 0;

            for (int sum = 1, i = 1; sum <= N; i++, sum += i)
                if (N % i == (i % 2 == 1 ? 0 : i / 2)) ret++;

            return ret;
        }

        public void Test()
        {
            Console.WriteLine(ConsecutiveNumbersSum(5) == 2);
            Console.WriteLine(ConsecutiveNumbersSum(9) == 3);
            Console.WriteLine(ConsecutiveNumbersSum(15) == 4);
        }
    }
}
