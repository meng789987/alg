
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: math
 * < 10:   1
 * < 100:  x1 and 1x, x=[0..9]
 * < 1000: xx1 and x1x and 1xx
 * For n,
 * No. of 1s in ones place: n/10 + min(1, n%10)
 * No. of 1s in tens place: n/100*10 + min(10, n%100 - 10 + 1)
 * No. of 1s in hundrens place: n/1000*100 + min(100, n%1000 - 100 + 1)
 */
namespace leetcode
{
    public class Lc233_Number_of_Digit_One
    {
        public int CountDigitOne(int n)
        {
            long ret = 0;
            for (long b = 1; b <= n; b *= 10)
            {
                ret += n / (b * 10) * b + Math.Max(0, Math.Min(b, n % (b * 10) - b + 1));
            }

            return (int)ret;
        }

        public void Test()
        {
            Console.WriteLine(CountDigitOne(3) == 1);
            Console.WriteLine(CountDigitOne(13) == 6);
            Console.WriteLine(CountDigitOne(123) == 57);
        }
    }
}
