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
            Console.WriteLine("Lc745_Prefix_and_Suffix_Search:");
            new leetcode.Lc745_Prefix_and_Suffix_Search().Test();
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
