using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math, bs
 * Time(logn), Space(1)
 * lcm(a, b) = a*b/gcd(a, b)
 * each lcm is a cycle, which has cnt(lcm/A + lcm/B - 1) magical numbers.
 */
namespace leetcode
{
    public class Lc878_Nth_Magical_Number
    {
        public int NthMagicalNumber(int N, int A, int B)
        {
            int MOD = 1000000007;
            int gcd = Gcd(A, B);
            int lcm = A / gcd * B;
            int cnt = (A + B) / gcd - 1; // lcm/A + lcm/B - 1;

            //int remain = Bs(A, B, lcm, N % cnt);
            int remain = N % cnt == 0 ? 0 : Math.Min((N % cnt) * B / (A + B) * A + A, (N % cnt) * A / (A + B) * B + B);
            return (int)(((long)N / cnt * lcm + remain) % MOD);
        }

        int Gcd(int a, int b)
        {
            while (b > 0)
            {
                int t = a % b;
                a = b;
                b = t;
            }

            return a;
        }

        int Bs(int a, int b, int lcm, int nth)
        {
            for (int lo = 0, hi = lcm; lo <= hi;)
            {
                int m = (lo + hi) / 2;
                int cnt = m / a + m / b;
                if (cnt == nth)
                    return Math.Max(m / a * a, m / b * b);
                if (cnt < nth) lo = m + 1;
                else hi = m - 1;
            }

            return 0;
        }

        public void Test()
        {
            Console.WriteLine(NthMagicalNumber(1, 2, 3) == 2);
            Console.WriteLine(NthMagicalNumber(4, 2, 3) == 6);
            Console.WriteLine(NthMagicalNumber(5, 2, 4) == 10);
            Console.WriteLine(NthMagicalNumber(3, 6, 4) == 8);
        }
    }
}
