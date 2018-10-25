using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: shortest path, FloydWarshall/bfs, permutation/dp
 * Time(2^nn^3), Space(n^2)
 * for each permutation of all nodes, sum the distances of any two continuous nodes.
 */
namespace leetcode
{
    public class Lc847_Shortest_Path_Visiting_All_Nodes
    {
        public int ShortestPathLength(int[][] graph)
        {
            int n = graph.Length;

            var dist = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    dist[i, j] = INF;

                dist[i, i] = 0;

                for (int j = 0; j < graph[i].Length; j++)
                    dist[i, graph[i][j]] = 1;
            }

            FloydWarshall(dist);

            int ret = int.MaxValue;
            var perm = Enumerable.Range(0, n).ToArray();
            int fac = 1;
            for (int i = 1; i <= n; i++) fac *= i;

            for (int p = 0;p<fac ; p++)
            {
                int d = 0;
                for (int i = 1; i < n; i++)
                    d += dist[i - 1, i];

                ret = Math.Min(ret, d);
                NextPermutation(perm);
            }

            return ret;
        }

        // 3 loops of all nodes to tighten the distance.
        void FloydWarshall(int[,] dist)
        {
            var n = dist.GetLength(0);
            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                        dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
        }

        void NextPermutation(int[] a)
        {
            int n = a.Length;

            // find the last one in ascending order
            int i = n - 1;
            while (i > 0 && a[i - 1] >= a[i]) i--;

            // swap it with a[n-1]
            if (i > 0) Swap(ref a[i - 1], ref a[n - 1]);

            // reorder a[i..n-1] in ascending order
            for (int j = n - 1; i < j; i++, j--)
                Swap(ref a[i], ref a[j]);
        }

        void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        const int INF = 100;

        public void Test()
        {
            var grap = new int[][] { new int[] { 1, 2, 3 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 } };
            Console.WriteLine(ShortestPathLength(grap) == 4);

            grap = new int[][] { new int[] { 1 }, new int[] { 0, 2, 4 }, new int[] { 1, 3, 4 }, new int[] { 2 }, new int[] { 1, 2 } };
            Console.WriteLine(ShortestPathLength(grap) == 4);
        }
    }
}
