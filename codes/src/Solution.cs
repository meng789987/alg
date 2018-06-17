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
            Console.WriteLine("Lc212_Word_Search_II:");
            new leetcode.Lc212_Word_Search_II().Test();
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
