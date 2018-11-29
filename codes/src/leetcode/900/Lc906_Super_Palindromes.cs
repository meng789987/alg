using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math
 * Time(n^(1/4)logn), Space(1)
 */
namespace leetcode
{
    public class Lc906_Super_Palindromes
    {
        public int SuperpalindromesInRange(string L, string R)
        {
            int res = 0;
            long lnum = long.Parse(L), rnum = long.Parse(R);
            for (long i = 1; ; i++)
            {
                long t = FlipB(i, i / 10), tt = t * t;
                if (tt > rnum) return res;
                if (lnum <= tt && tt <= rnum && IsPalindrome(tt)) res++;
                t = FlipB(i, i); tt = t * t;
                if (lnum <= tt && tt <= rnum && IsPalindrome(tt)) res++;
            }
        }

        bool IsPalindrome(long b)
        {
            long a = 0;
            while (a < b)
            {
                a = a * 10 + (b % 10);
                b /= 10;
            }
            return a == b || a / 10 == b;
        }

        long FlipB(long a, long b)
        {
            while (b > 0)
            {
                a = a * 10 + (b % 10);
                b /= 10;
            }
            return a;
        }

        public void Test()
        {
            Console.WriteLine(SuperpalindromesInRange("4", "1000") == 4);
        }
    }
}
