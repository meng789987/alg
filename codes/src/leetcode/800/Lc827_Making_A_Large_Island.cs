using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dfs, disjoint set
 * Time(mn), Space(mn)
 */
namespace leetcode
{
    public class Lc827_Making_A_Large_Island
    {
        public int LargestIsland(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            var counts = new List<int>();
            int color = 2;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] != 1) continue;
                    counts.Add(DfsPaint(grid, i, j, color++));
                }
            }

            int ret = counts.Max();
            var dir = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] != 0) continue;
                    var list = new List<int>();
                    for (int d = 0; d < 4; d++)
                    {
                        int r = i + dir[d, 0], c = j + dir[d, 1];
                        if (r >= 0 && r < m && c >= 0 && c < n && grid[r][c] != 0 && !list.Contains(grid[r][c]))
                            list.Add(grid[r][c]);
                    }

                    var count = list.Sum(p => counts[p - 2]) + 1;
                    ret = Math.Max(ret, count);
                }
            }

            return ret;
        }

        int DfsPaint(int[][] grid, int row, int col, int color)
        {
            int m = grid.Length, n = grid[0].Length;
            if (row < 0 || row >= m || col < 0 || col >= n || grid[row][col] != 1) return 0;

            grid[row][col] = color;
            int ret = 1;
            ret += DfsPaint(grid, row - 1, col, color);
            ret += DfsPaint(grid, row + 1, col, color);
            ret += DfsPaint(grid, row, col - 1, color);
            ret += DfsPaint(grid, row, col + 1, color);

            return ret;
        }

        public void Test()
        {
            var grid = new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 } };
            Console.WriteLine(LargestIsland(grid) == 3);

            grid = new int[][] { new int[] { 1, 1 }, new int[] { 0, 1 } };
            Console.WriteLine(LargestIsland(grid) == 4);
        }
    }
}
