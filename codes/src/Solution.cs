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
            Console.WriteLine("Lc753_Cracking_the_Safe:");
            new leetcode.Lc753_Cracking_the_Safe().Test();
            //Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var table = new System.Data.DataTable();
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));

            var (a, b) = (3, 4);
            var ss = new SortedSet<int>();
            Console.WriteLine(ss.Min);

            var list = new backtracking.Standard_Backtracking().Permutation("123456");
            var ps = new List<string>[20];
            for (int i = 0; i < ps.Length; i++) ps[i] = new List<string>();
            foreach (var s in list)
            {
                var nums = s.Select(c => c - '1').ToArray();
                var count = new leetcode.Lc315_Count_of_Smaller_Numbers_After_Self().CountSmallerMergeSort(nums).Sum();
                ps[count].Add(s);
            }
            foreach (var p in ps)
                Console.WriteLine(p);
        }
    }
}
