using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dfs
 * Time(m^2n), Space(mn)
 */
namespace leetcode
{
    public class Lc749_Contain_Virus
    {
        public int ContainVirus(int[,] grid)
        {
            int ret = 0;
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);

            while (true)
            {
                // find the virus region with max border
                var maxreg = new VirusRegion(0, 0);
                var visited = new bool[m, n];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (grid[i, j] == 1 && !visited[i, j])
                        {
                            var reg = new VirusRegion(i, j);
                            DfsBorder(grid, i, j, visited, reg);

                            if (maxreg.Borders.Count < reg.Borders.Count)
                                maxreg = reg;
                        }
                    }
                }

                if (maxreg.Borders.Count == 0) break;

                // create the wall to quarantine this max region
                ret += maxreg.WallCount;
                DfsWall(grid, maxreg.Row, maxreg.Col);

                // infect all border
                var newInfected = new bool[m, n];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (grid[i, j] == 1 && !newInfected[i, j])
                        {
                            foreach (var dir in Dirs)
                            {
                                int r = i + dir[0];
                                int c = j + dir[1];

                                if (0 <= r && r < m && 0 <= c && c < n && grid[r, c] == 0)
                                {
                                    grid[r, c] = 1;
                                    newInfected[r, c] = true;
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }

        int[][] Dirs = new int[][] { new int[] { 0, -1 }, new int[] { 0, 1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

        void DfsBorder(int[,] grid, int row, int col, bool[,] visited, VirusRegion reg)
        {
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            visited[row, col] = true;

            foreach (var dir in Dirs)
            {
                int r = row + dir[0];
                int c = col + dir[1];

                if (0 <= r && r < m && 0 <= c && c < n)
                {
                    if (grid[r, c] == 0)
                    {
                        reg.WallCount++;
                        reg.Borders.Add(r * m + c);
                    }

                    if (grid[r, c] == 1 && !visited[r, c])
                        DfsBorder(grid, r, c, visited, reg);
                }
            }
        }

        void DfsWall(int[,] grid, int row, int col)
        {
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);

            foreach (var dir in Dirs)
            {
                int r = row + dir[0];
                int c = col + dir[1];

                if (0 <= r && r < m && 0 <= c && c < n && grid[r, c] == 1)
                {
                    grid[r, c] = 2;
                    DfsWall(grid, r, c);
                }
            }
        }

        class VirusRegion
        {
            public int WallCount;
            public int Row, Col; // representer
            public HashSet<int> Borders = new HashSet<int>();
            public VirusRegion(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }

        public void Test()
        {
            var grid = new int[,] {
                { 0,1,0,0,0,0,0,1},
                { 0,1,0,0,0,0,0,1},
                { 0,0,0,0,0,0,0,1},
                { 0,0,0,0,0,0,0,0}};
            Console.WriteLine(ContainVirus(grid) == 10);

            grid = new int[,] {
                {1,1,1},
                {1,0,1},
                {1,1,1}};
            Console.WriteLine(ContainVirus(grid) == 4);

            grid = new int[,] {
                {1,1,1,0,0,0,0,0,0},
                {1,0,1,0,1,1,1,1,1},
                {1,1,1,0,0,0,0,0,0}};
            Console.WriteLine(ContainVirus(grid) == 13);
        }
    }
}
