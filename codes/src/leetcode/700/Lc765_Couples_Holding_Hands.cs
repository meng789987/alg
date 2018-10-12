using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: disjoint set
 * sum(count_of_pair_in_each_disjoint_set - 1)
 */
namespace leetcode
{
    public class Lc765_Couples_Holding_Hands
    {
        public int MinSwapsCouples(int[] row)
        {
            int ret = 0;
            int n = row.Length;
            var map = new int[n];
            for (int i = 0; i < n; i++)
                map[row[i]] = i;

            for (int i = 0; i < n; i += 2)
            {
                if (row[i] / 2 != row[i + 1] / 2)
                {
                    int pi = map[row[i] % 2 == 0 ? row[i] + 1 : row[i] - 1];
                    map[row[i + 1]] = pi;
                    row[pi] = row[i + 1];
                    ret++;
                }
            }

            return ret;
        }

        void Swap(int[] row, int[] map, int i, int j)
        {
            map[row[i]] = j;
            map[row[j]] = i;

            int tmp = row[i];
            row[i] = row[j];
            row[j] = tmp;
        }

        public void Test()
        {
            var row = new int[] { 0, 2, 1, 3 };
            Console.WriteLine(MinSwapsCouples(row) == 1);

            row = new int[] { 3, 2, 0, 1 };
            Console.WriteLine(MinSwapsCouples(row) == 0);

            row = new int[] { 28, 4, 37, 54, 35, 41, 43, 42, 45, 38, 19, 51, 49, 17, 47, 25, 12, 53, 57, 20, 2, 1, 9, 27, 31, 55, 32, 48, 59, 15, 14, 8, 3, 7, 58, 23, 10, 52, 22, 30, 6, 21, 24, 16, 46, 5, 33, 56, 18, 50, 39, 34, 29, 36, 26, 40, 44, 0, 11, 13 };
            Console.WriteLine(MinSwapsCouples(row) == 26);
        }
    }
}
