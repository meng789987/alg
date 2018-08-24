using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bfs
 * Time((mn)^2), Space(mn)
 */
namespace leetcode
{
    public class Lc675_Cut_Off_Trees_for_Golf_Event
    {
        public int CutOffTree(IList<IList<int>> forest)
        {
            if (forest[0][0] == 0) return -1;
            int res = 0;
            var from = new int[] { 0, 0 };
            while (true)
            {
                forest[from[0]][from[1]] = 1;
                var to = LowestTree(forest);
                if (to == null) return res;
                var step = Walk(forest, from[0], from[1], to[0], to[1]);
                if (step == -1) return -1;
                res += step;
                from = to;
            }
        }

        int[] LowestTree(IList<IList<int>> forest)
        {
            int minr = -1, minc = -1, minh = int.MaxValue;
            for (int i = 0; i < forest.Count; i++)
            {
                for (int j = 0; j < forest[i].Count; j++)
                {
                    if (1 < forest[i][j] && forest[i][j] < minh)
                    {
                        minr = i;
                        minc = j;
                        minh = forest[i][j];
                    }
                }
            }

            return minh == int.MaxValue ? null : new int[] { minr, minc };
        }

        // step count to walk from [sr, sc] to [tr, tc]
        int Walk(IList<IList<int>> forest, int sr, int sc, int tr, int tc)
        {
            int res = 0;
            int m = forest.Count, n = forest[0].Count;
            var dirs = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            var q = new Queue<int[]>();
            var visited = new bool[forest.Count, forest[0].Count];
            q.Enqueue(new[] { sr, sc });
            visited[sr, sc] = true;
            while (q.Count > 0)
            {
                res++;
                for (int cnt = q.Count; cnt > 0; cnt--)
                {
                    var node = q.Dequeue();
                    for (int k = 0; k < 4; k++)
                    {
                        int i = node[0] + dirs[k, 0], j = node[1] + dirs[k, 1];
                        if (i == tr && j == tc) return res;
                        if (0 <= i && i < m && 0 <= j && j < n && !visited[i, j] && forest[i][j] != 0)
                        {
                            visited[i, j] = true;
                            q.Enqueue(new int[] { i, j });
                        }
                    }
                }

            }

            return -1;
        }

        public void Test()
        {
            var forest = new List<IList<int>> {
                    new List<int>{1,2,3},
                    new List<int>{0,0,4},
                    new List<int>{7,6,5} };
            Console.WriteLine(CutOffTree(forest) == 6);

            forest = new List<IList<int>> {
                    new List<int>{1,2,3},
                    new List<int>{0,0,0},
                    new List<int>{7,6,5} };
            Console.WriteLine(CutOffTree(forest) == -1);

            forest = new List<IList<int>> {
                    new List<int>{2,3,4},
                    new List<int>{0,0,5},
                    new List<int>{8,7,6} };
            Console.WriteLine(CutOffTree(forest) == 6);
        }
    }
}

