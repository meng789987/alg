using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: two pointers, stack, heap, bfs
 */
namespace leetcode
{
    public class Lc42_Trapping_Rain_Water
    {
        /*
         * the width of each bar is 1
         * Time(n), Space(1)
         */
        public int Trap(int[] heights)
        {
            int ret = 0;

            int maxi = 0;
            for (int i = 0; i < heights.Length; i++)
                if (heights[i] > heights[maxi]) maxi = i;

            // left trapped water
            for (int i = 0, max = 0; i < maxi; i++)
            {
                max = Math.Max(max, heights[i]);
                ret += max - heights[i];
            }

            // right trapped water
            for (int i = heights.Length - 1, max = 0; i > maxi; i--)
            {
                max = Math.Max(max, heights[i]);
                ret += max - heights[i];
            }

            return ret;
        }

        /*
         * Lc11_Container_With_Most_Water
         * the width of each bar is 0
         * Time(n), Space(1)
         */
        public int MaxArea(int[] heights)
        {
            int ret = 0;
            for (int i = 0, j = heights.Length - 1; i < j;)
            {
                ret = Math.Max(ret, Math.Min(heights[i], heights[j]) * (j - i));
                if (heights[i] < heights[j]) i++;
                else j--;
            }

            return ret;
        }

        /*
         * Lc407_Trapping_Rain_Water_II
         * 2D, each bar is 1x1
         * Time(mnlog(mn)), Space(mn)
         */
        public int TrapRainWater(int[,] heightMap)
        {
            int m = heightMap.GetLength(0), n = heightMap.GetLength(1);
            var visited = new bool[m, n];
            var q = new SortedSet<Cell>(Comparer<Cell>.Create(
                (a, b) => a.h != b.h ? a.h - b.h : a.r != b.r ? a.r - b.r : a.c - b.c));

            // boundary enqueue
            for (int r = 0; r < m; r++)
            {
                visited[r, 0] = visited[r, n - 1] = true;
                q.Add(new Cell(r, 0, heightMap[r, 0]));
                q.Add(new Cell(r, n - 1, heightMap[r, n - 1]));
            }
            for (int c = 0; c < n; c++)
            {
                visited[0, c] = visited[m - 1, c] = true;
                q.Add(new Cell(0, c, heightMap[0, c]));
                q.Add(new Cell(m - 1, c, heightMap[m - 1, c]));
            }

            int ret = 0;
            var dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            while (q.Count > 0)
            {
                var cell = q.Min; q.Remove(cell);

                // calculate the trapped water of all its inner neighbors and enqueue them
                for (int i = 0; i < dirs.GetLength(0); i++)
                {
                    int row = cell.r + dirs[i, 0];
                    int col = cell.c + dirs[i, 1];
                    if (0 <= row && row < m && 0 <= col && col < n && !visited[row, col])
                    {
                        visited[row, col] = true;
                        ret += Math.Max(0, cell.h - heightMap[row, col]);
                        q.Add(new Cell(row, col, Math.Max(heightMap[row, col], cell.h)));
                    }
                }
            }

            return ret;
        }

        class Cell
        {
            public int r; // row
            public int c; // col
            public int h; // height
            public Cell(int r, int c, int h)
            {
                this.r = r;
                this.c = c;
                this.h = h;
            }
        }

        public void Test()
        {
            var nums = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            Console.WriteLine(Trap(nums) == 6);

            nums = new int[] { 1, 1 };
            Console.WriteLine(MaxArea(nums) == 1);

            var heightMap = new int[,]
            {
                { 1, 4, 3, 1, 3, 2 },
                { 3, 2, 1, 3, 2, 4 },
                { 2, 3, 3, 2, 3, 1 }
            };
            Console.WriteLine(TrapRainWater(heightMap) == 4);

            heightMap = new int[,]
            {
                { 12, 13, 1, 12 },
                { 13, 4, 13, 12 },
                { 13, 8, 10, 12 },
                { 12, 13, 12, 12 },
                { 13, 13, 13, 13 }
            };
            Console.WriteLine(TrapRainWater(heightMap) == 14);

            heightMap = new int[,]
            {
                {5,8,7,7},
                {5,2,1,5},
                {7,1,7,1},
                {8,9,6,9},
                {9,8,9,9}
            };
            Console.WriteLine(TrapRainWater(heightMap) == 12);
        }

    }
}

