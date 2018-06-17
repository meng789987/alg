using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace alg
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("QuickSort:");
            new sort.QuickSort().Test();
            //Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var word = "abc";
            Console.WriteLine(new string(word.Reverse().ToArray()));
        }
    }
}
