using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bfs, dfs
 * Time(n^2logn), Space(n^2)
 * dfs: set a limit from 0 to max, use dfs to test feasibility
 */
namespace leetcode
{
    public class Lc778_Swim_in_Rising_Water
    {
        int[,] _dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        public int SwimInWaterBfs(int[,] grid)
        {
            int max = 0;
            int n = grid.GetLength(0);
            var q = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => grid[a[0], a[1]] - grid[b[0], b[1]]));
            q.Add(new int[] { 0, 0 });
            var visited = new bool[n, n];
            visited[0, 0] = true;

            while (q.Count > 0)
            {
                var s = q.Min; q.Remove(s);
                max = Math.Max(max, grid[s[0], s[1]]);
                if (s[0] == n - 1 && s[1] == n - 1) break;

                for (int d = 0; d < 4; d++)
                {
                    int i = s[0] + _dirs[d, 0], j = s[1] + _dirs[d, 1];
                    if (0 <= i && i < n && 0 <= j && j < n && !visited[i, j])
                    {
                        visited[i, j] = true;
                        q.Add(new int[] { i, j });
                    }
                }
            }

            return max;
        }

        public int SwimInWaterBfsOpt(int[,] grid)
        {
            int n = grid.GetLength(0);
            if (n == 0) return 0;
            if (n == 1) return grid[0, 0];
            int max = Math.Max(grid[0, 0], grid[n - 1, n - 1]);
            var q = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => grid[a[0], a[1]] - grid[b[0], b[1]]));
            q.Add(new int[] { 0, 0 });
            var visited = new bool[n, n];
            visited[0, 0] = true;

            var levelq = new Queue<int[]>();
            while (q.Count > 0)
            {
                var first = q.Min; q.Remove(first);
                max = Math.Max(max, grid[first[0], first[1]]);

                levelq.Enqueue(first);
                while (levelq.Count > 0)
                {
                    var node = levelq.Dequeue();
                    for (int d = 0; d < 4; d++)
                    {
                        int i = node[0] + _dirs[d, 0];
                        int j = node[1] + _dirs[d, 1];
                        if (0 <= i && i < n && 0 <= j && j < n && !visited[i, j])
                        {
                            visited[i, j] = true;
                            if (grid[i, j] <= max)
                                levelq.Enqueue(new int[] { i, j });
                            else
                                q.Add(new int[] { i, j });
                            if (i == n - 1 && j == n - 1) return max;
                        }
                    }
                }
            }

            return max;
        }

        public void Test()
        {
            var grid = new int[,] { { 0, 2 }, { 1, 3 } };
            Console.WriteLine(SwimInWaterBfs(grid) == 3);
            Console.WriteLine(SwimInWaterBfsOpt(grid) == 3);

            grid = new int[,] { { 0, 1, 2, 3, 4 }, { 24, 23, 22, 21, 5 }, { 12, 13, 14, 15, 16 }, { 11, 17, 18, 19, 20 }, { 10, 9, 8, 7, 6 } };
            Console.WriteLine(SwimInWaterBfs(grid) == 16);
            Console.WriteLine(SwimInWaterBfsOpt(grid) == 16);
        }
    }
}

