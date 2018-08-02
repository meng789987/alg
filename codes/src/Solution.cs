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
            Console.WriteLine("Lc552_Student_Attendance_RecordII:");
            new leetcode.Lc552_Student_Attendance_RecordII().Test();
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

            Console.WriteLine(new Solution().PyramidTransition("AABA", new List<string> { "AAA", "AAB", "ABA", "ABB", "BAC" }));
            Console.WriteLine(new Solution().PyramidTransition("ABCD", new List<string> { "BCE", "BCF", "ABA", "CDA", "AEG", "FAG", "GGG" }));
            Console.WriteLine(new Solution().PyramidTransition("AAAA", new List<string> { "AAB", "AAC", "BCD", "BBE", "DEF" }));
        }

        // bitmap: 'A'=>1, 'B'=>2, 'C'=>4
        public bool PyramidTransition(string bottom, IList<string> allowed)
        {
            int m = 7; // number of charset
            var map = new int[m, m];
            foreach (var s in allowed)
                map[s[0]-'A', s[1]-'A'] |= 1 << (s[2]-'A');

            int n = bottom.Length;
            var bs = new int[n];
            for (int i = 0; i < n; i++) bs[i] = 1 << (bottom[i]-'A');

            for (int row = n - 1; row >= 1; row--)
            {
                for (int col = 0; col < row; col++)
                {
                    int a =0, b = 0;
                    for (int i = 0; i < m; i++)
                    {
                        if ((bs[col] & (1 << i)) > 0)
                        {
                            for (int j = 0; j < m; j++)
                            {
                                if ((bs[col + 1] & (1 << j)) > 0)
                                {
                                    a |= map[i, j];
                                    if (map[i, j] > 0) b |= 1 << j;
                                }
                            }
                        }
                    }
                    if (a == 0) return false;
                    bs[col] = a;
                     bs[col + 1] = b;
                }
            }

            return true;
        }
    }
}
