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
            Console.WriteLine("Lc218_The_Skyline_Problem:");
            new leetcode.Lc218_The_Skyline_Problem().Test();
            //Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var a = new List<int[]> { new int[] { 1 }, new int[] { 2 } };
            var b = new List<int[]> { new int[] { 2 }, new int[] { 1 } };
            Console.WriteLine(a.SameSet(b));
        }
    }
}
