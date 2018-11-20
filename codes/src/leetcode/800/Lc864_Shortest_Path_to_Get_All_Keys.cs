using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: bfs, dfs/permutation, Dijkstra/heap
 * Time(mnK!), Space(mnK!), K is number of keys
 */
namespace leetcode
{
    public class Lc864_Shortest_Path_to_Get_All_Keys
    {
        public int ShortestPathAllKeys(string[] grid)
        {
            State start = null, end = new State(0, 0, 0);
            int keyCount = 0;
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (grid[r][c] == '@')
                        start = new State(r, c, 0);
                    else if (char.IsLower(grid[r][c]))
                        keyCount = Math.Max(keyCount, grid[r][c] - 'a' + 1);
                }
            }

            end.Keys = (1 << keyCount) - 1;
            int[,] dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            int m = grid.Length, n = grid[0].Length;
            var visited = new HashSet<int>();
            visited.Add(start.ToInt());
            var q = new Queue<State>();
            q.Enqueue(start);

            for (int steps = 0; q.Count > 0; steps++)
            {
                for (int qCount = q.Count; qCount > 0; qCount--)
                {
                    var node = q.Dequeue();
                    if (node.Keys == end.Keys) return steps;

                    for (int d = 0; d < 4; d++)
                    {
                        int r = node.Row + dirs[d, 0], c = node.Col + dirs[d, 1];
                        if (r < 0 || r >= m || c < 0 || c >= n) continue;
                        if (grid[r][c] == '#') continue;
                        if (char.IsUpper(grid[r][c]) && ((1 << (grid[r][c] - 'A')) & node.Keys) == 0) continue;

                        var next = new State(r, c, node.Keys);
                        if (char.IsLower(grid[r][c])) next.Keys |= 1 << (grid[r][c] - 'a');
                        if (visited.Contains(next.ToInt())) continue;
                        visited.Add(next.ToInt());
                        q.Enqueue(next);
                    }
                }
            }

            return -1;
        }

        class State
        {
            public int Row, Col;
            public int Keys;

            public State(int row, int col, int keys)
            {
                Row = row;
                Col = col;
                Keys = keys;
            }

            public int ToInt()
            {
                return (Keys << 10) | (Row << 5) | Col;
            }
        }

        public void Test()
        {
            var a = new string[] { "@.a.#", "###.#", "b.A.B" };
            Console.WriteLine(ShortestPathAllKeys(a) == 8);

            a = new string[] { "@..aA", "..B#.", "....b" };
            Console.WriteLine(ShortestPathAllKeys(a) == 6);

            a = new string[] { "@fedcbBCDEFaA" };
            Console.WriteLine(ShortestPathAllKeys(a) == 11);
        }
    }
}
