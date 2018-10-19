using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: disjoint set
 * Time(mnqa), Space(mn), where q=hits.length, a is Inverse-Ackermann function.
 * 1. inverse the operation sequences. drop all bricks in hits, get its disjoint sets, then get the total size of all disjoint sets connected to the top as topsize.
 *    and then recover the last hit brick, the difference of current topsize and previous topsize is the anwser of last hit.
 * 2. connect all bricks on top row to a fake source vertex to make them as one jointed set.
 */
namespace leetcode
{
    public class Lc803_Bricks_Falling_When_Hit
    {
        public int[] HitBricks(int[][] grid, int[][] hits)
        {
            int m = grid.Length, n = grid[0].Length;

            // make a copy of grid, first drop all bricks in hits
            var copy = new int[m][];
            for (int r = 0; r < m; r++)
                copy[r] = grid[r].ToArray();
            foreach (var h in hits)
                copy[h[0]][h[1]] = 0;

            // build the disjoint set after all bricks in hits are dropped.
            var ds = new DisjointSet(m * n + 1);
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (copy[r][c] == 0) continue;
                    if (r == 0) ds.Union(r * n + c, m * n);
                    if (r > 0 && copy[r - 1][c] == 1) ds.Union(r * n + c, (r - 1) * n + c);
                    if (c > 0 && copy[r][c - 1] == 1) ds.Union(r * n + c, r * n + c - 1);
                }
            }

            // recover the bricks in hits in reverse order to get the anwser
            var ret = new int[hits.Length];
            int[,] dirs = { { -1, 0 }, { 0, -1 }, { 0, 1 }, { 1, 0 } };
            for (int i = hits.Length - 1; i >= 0; i--)
            {
                int r = hits[i][0], c = hits[i][1];
                if (grid[r][c] == 0) continue;

                int preTopSize = ds.TopSize;
                copy[hits[i][0]][hits[i][1]] = 1; // recover the brick
                if (r == 0) ds.Union(r * n + c, m * n);

                for (int d = 0; d < 4; d++)
                {
                    int nr = r + dirs[d, 0];
                    int nc = c + dirs[d, 1];
                    if (0 <= nr && nr < m && 0 <= nc && nc < n && copy[nr][nc] == 1)
                        ds.Union(r * n + c, nr * n + nc);
                }

                ret[i] = Math.Max(0, ds.TopSize - preTopSize - 1);
            }

            return ret;
        }


        class DisjointSet
        {
            public DisjointSet(int n)
            {
                _parents = new int[n];
                _sizes = new int[n];
                for (int i = 0; i < n; i++)
                {
                    _parents[i] = i;
                    _sizes[i] = 1;
                }
            }

            public void Union(int i, int j)
            {
                int pi = Find(i);
                int pj = Find(j);
                if (pi != pj)
                {
                    _parents[pi] = pj;
                    _sizes[pj] += _sizes[pi];
                }
            }

            public int TopSize
            {
                get { return _sizes[Find(_sizes.Length - 1)]; }
            }

            int Find(int i)
            {
                if (_parents[i] != i)
                    _parents[i] = Find(_parents[i]);
                return _parents[i];
            }

            int[] _parents;
            int[] _sizes;
        }

        public void Test()
        {
            var grid = new int[][] { new int[] { 1, 0, 0, 0 }, new int[] { 1, 1, 1, 0 } };
            var hits = new int[][] { new int[] { 1, 0 } };
            var exp = new int[] { 2 };
            Console.WriteLine(exp.SequenceEqual(HitBricks(grid, hits)));

            grid = new int[][] { new int[] { 1, 0, 0, 0 }, new int[] { 1, 1, 0, 0 } };
            hits = new int[][] { new int[] { 1, 1 }, new int[] { 1, 0 } };
            exp = new int[] { 0, 0 };
            Console.WriteLine(exp.SequenceEqual(HitBricks(grid, hits)));
        }
    }
}
