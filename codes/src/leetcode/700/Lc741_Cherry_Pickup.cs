using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(n^3), Space(n^2)
 * similar to two people to walk together and get the max sum of their cherries.
 * They will be at the same diagonal. Try any two positions within the diagonal.
 */
namespace leetcode
{
    public class Lc741_Cherry_Pickup
    {
        public int CherryPickupTopdown(int[,] grid)
        {
            int n = grid.GetLength(0);
            var memo = new int[n, n, n]; // r1+c1==r2+c2, so we can save one dimension.
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        memo[i, j, k] = int.MinValue;

            return Math.Max(0, Dp(grid, n - 1, n - 1, n - 1, n - 1, memo));
        }

        int Dp(int[,] grid, int r1, int c1, int r2, int c2, int[,,] memo)
        {
            if (r1 < 0 || c1 < 0 || r2 < 0 || c2 < 0 ||
                grid[r1, c1] == -1 || grid[r2, c2] == -1) return -1;
            if (memo[r1, c1, r2] != int.MinValue) return memo[r1, c1, r2];
            if (r1 == 0 && c1 == 0) return grid[0, 0];

            int val = grid[r1, c1];
            if (r1 != r2 || c1 != c2) val += grid[r2, c2];
            var max = Math.Max(Math.Max(
                Dp(grid, r1 - 1, c1, r2 - 1, c2, memo),   // person1 down, person2 down
                Dp(grid, r1 - 1, c1, r2, c2 - 1, memo)), Math.Max(
                Dp(grid, r1, c1 - 1, r2 - 1, c2, memo),   // person1 right, person2 down
                Dp(grid, r1, c1 - 1, r2, c2 - 1, memo))); // person1 right, person2 right
            return memo[r1, c1, r2] = max == -1 ? -1 : val + max;
        }

        public int CherryPickupBottomup(int[,] grid)
        {
            int n = grid.GetLength(0);
            var dp = new int[n + 1, n + 1]; // r1+c1==r2+c2, so we can save one dimension.
            var next = new int[n + 1, n + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= n; j++)
                    dp[i, j] = -1;
            dp[0, 0]  = 0;

            for (int step = 0; step <= 2 * n - 2; step++)
            {
                for (int i = 0; i <= n; i++)
                {
                    for (int j = 0; j <= n; j++)
                        next[i, j] = -1;
                }

                for (int r1 = Math.Max(0, step - n + 1); r1 <= Math.Min(n - 1, step); r1++)
                {
                    for (int r2 = Math.Max(0, step - n + 1); r2 <= Math.Min(n - 1, step); r2++)
                    {
                        int c1 = step - r1;
                        int c2 = step - r2;
                        if (grid[r1, c1] == -1 || grid[r2, c2] == -1) continue;

                        int val = grid[r1, c1];
                        if (r1 != r2 || c1 != c2) val += grid[r2, c2];

                        var max = Math.Max(
                            Math.Max(dp[r1, r2], dp[r1, r2 + 1]), 
                            Math.Max(dp[r1 + 1, r2], dp[r1 + 1, r2 + 1]));
                        next[r1 + 1, r2 + 1] = max == -1 ? -1 : val + max;
                    }
                }

                var tmp = dp;
                dp = next;
                next = tmp;
            }

            return Math.Max(0, dp[n, n]);
        }

        public void Test()
        {
            var grid = new int[,] { { 0, 1, -1 }, { 1, 0, -1 }, { 1, 1, 1 } };
            Console.WriteLine(CherryPickupTopdown(grid) == 5);
            Console.WriteLine(CherryPickupBottomup(grid) == 5);

            grid = new int[,]{{1,1,1,1,0,0,0},{0,0,0,1,0,0,0},{0,0,0,1,0,0,1},
                {1,0,0,1,0,0,0},{0,0,0,1,0,0,0},{0,0,0,1,0,0,0},{0,0,0,1,1,1,1}};
            Console.WriteLine(CherryPickupTopdown(grid) == 15);
            Console.WriteLine(CherryPickupBottomup(grid) == 15);

            grid = new int[,] { 
                { 1,-1, 1,-1, 1, 1, 1, 1, 1,-1},
                {-1, 1, 1,-1,-1, 1, 1, 1, 1, 1},
                { 1, 1, 1,-1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {-1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 1,-1, 1, 1, 1, 1,-1, 1, 1, 1},
                { 1, 1, 1,-1, 1, 1,-1, 1, 1, 1},
                { 1,-1, 1,-1,-1, 1, 1, 1, 1, 1},
                { 1, 1,-1,-1, 1, 1, 1,-1, 1,-1},
                { 1, 1,-1, 1, 1, 1, 1, 1, 1, 1} };
            Console.WriteLine(CherryPickupTopdown(grid) == 0);
            Console.WriteLine(CherryPickupBottomup(grid) == 0);
        }
    }
}

