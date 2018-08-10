
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: math
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc564_Find_the_Closest_Palindrome
    {
        public string NearestPalindromic(string n)
        {
            int halfOrder = (int)Math.Pow(10, n.Length / 2);
            long num = long.Parse(n);

            long a = Mirror(num);
            long larger = Mirror(num / halfOrder * halfOrder + halfOrder + 1);
            long smaller = Mirror(num / halfOrder * halfOrder - 1);

            if (a > num)
                larger = Math.Min(a, larger);
            else if (a < num)
                smaller = Math.Max(a, smaller);

            var res = num - smaller <= larger - num ? smaller : larger;
            return res.ToString();
        }

        long Mirror(long n)
        {
            var cs = n.ToString().ToCharArray();
            for (int i = 0, j = cs.Length - 1; i < j; i++, j--)
                cs[j] = cs[i];
            return long.Parse(new string(cs));
        }


        public void Test()
        {
            Console.WriteLine(NearestPalindromic("1") == "0");
            Console.WriteLine(NearestPalindromic("9") == "8");
            Console.WriteLine(NearestPalindromic("10") == "9");
            Console.WriteLine(NearestPalindromic("123") == "121");
            Console.WriteLine(NearestPalindromic("1283") == "1331");
            Console.WriteLine(NearestPalindromic("19999") == "20002");
        }
    }
}

