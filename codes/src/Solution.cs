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
            Console.WriteLine("Lc878_Nth_Magical_Number:");
            new leetcode.Lc878_Nth_Magical_Number().Test();
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

            var m = new int[,] { { 1, 2 }, { 3, 4 } };
            var n = new int[2, 2];
            Array.Copy(m, n, 3);

            int x = 3;
            Console.WriteLine(Convert.ToString(x, 2));
        }
    }
}
