using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: disjoint set, minimum spanning tree
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc685_Redundant_ConnectionII
    {
        // Kruskal: minimum spanning tree
        public int[] FindRedundantDirectedConnection(int[,] edges)
        {
            int n = edges.GetLength(0);

            // find the two edges having a same child
            int e1 = -1, e2 = -1;
            var parents = new int[n + 1];
            for (int i = 0; i < n; i++)
            {
                int u = edges[i, 0], v = edges[i, 1];
                if (parents[v] > 0)
                {
                    e1 = parents[v] - 1;
                    e2 = i;
                    break;
                }
                parents[v] = i + 1;
            }

            // detect the cycle.
            var ds = new DisjointSet(n + 1);
            for (int i = 0; i < n; i++)
            {
                if (i == e2) continue;
                int u = edges[i, 0], v = edges[i, 1];
                if (!ds.Union(u, v))
                {
                    if (e1 >= 0) return new int[] { edges[e1, 0], edges[e1, 1] };
                    return new int[] { u, v };
                }
            }
            
            return new int[] { edges[e2, 0], edges[e2, 1] };
        }

        public void Test()
        {
            var edges = new int[,] { { 1, 2 }, { 1, 3 }, { 2, 3 } };
            var exp = new int[] { 2, 3 };
            Console.WriteLine(exp.SequenceEqual(FindRedundantDirectedConnection(edges)));

            edges = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 1 }, { 1, 5 } };
            exp = new int[] { 4, 1 };
            Console.WriteLine(exp.SequenceEqual(FindRedundantDirectedConnection(edges)));

            edges = new int[,] { { 2, 1 }, { 3, 1 }, { 4, 2 }, { 1, 4 } };
            exp = new int[] { 2, 1 };
            Console.WriteLine(exp.SequenceEqual(FindRedundantDirectedConnection(edges)));
        }
    }
}

