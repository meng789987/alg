using System;

/*
 * tags: Euclid
 */
namespace alg.math
{
    public class GreatestCommonDivisor
    {
        public int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int t = a % b;
                a = b;
                b = t;
            }
            return a;
        }

        public void Test()
        {
            Console.WriteLine(Gcd(30, 45) == 15);
            Console.WriteLine(Gcd(8, 12) == 4);
        }
    }
}