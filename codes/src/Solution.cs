using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace alg
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lc224_Basic_Calculator:");
            new leetcode.Lc224_Basic_Calculator().Test();
            //Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var count = 1000000000;
            var samples = 0;
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                var x = rand.NextDouble();
                var y = rand.NextDouble();
                if (x * x + y * y < 1) samples++;
            }
            Console.WriteLine($"pi={4.0*samples/count}");
        }
    }
}
