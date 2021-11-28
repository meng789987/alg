using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using leetcode;

namespace alg
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MaxSumOfSubarray");
            new alg.dp.MaxSumOfSubarray().Test();
            //Test();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        static void Test()
        {
            long res = 0;
            for (long i = 1; i < 10000; i++)
                res += i * i * i * i * i * i * i * i * i;
            Console.WriteLine(res);
        }
    }
}
