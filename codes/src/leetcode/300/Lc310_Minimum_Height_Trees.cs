using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: graph, bfs
 * Time(n), Space(n)
 * the root of the minimum height tree should be the middle one of the longest path.
 * the middle one of the longest path can be found by moving two pointers from two ends to center with same speed.
 */
namespace leetcode
{
    public class Lc310_Minimum_Height_Trees
    {
        public IList<int> FindMinHeightTrees(int n, int[,] edges)
        {
            if (n == 1) return new List<int> { 0 };

            var g = BuildGraph(n, edges);
            var q = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (g[i].Count == 1) // start from leaves
                    q.Add(i);
            }

            for (int i = q.Count; i < n; i += q.Count)
            {
                var nq = new List<int>();
                foreach (var node in q)
                {
                    var peer = g[node].First();
                    g[peer].Remove(node);
                    if (g[peer].Count == 1) nq.Add(peer);
                }
                q = nq;
            }

            return q;
        }

        HashSet<int>[] BuildGraph(int n, int[,] edges)
        {
            var g = new HashSet<int>[n];
            for (int i = 0; i < n; i++)
                g[i] = new HashSet<int>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                g[edges[i, 0]].Add(edges[i, 1]);
                g[edges[i, 1]].Add(edges[i, 0]);
            }
            return g;
        }

        public void Test()
        {
            var edges = new int[,] { { 1, 0 }, { 1, 2 }, { 1, 3 } };
            var exp = new List<int> { 1 };
            Console.WriteLine(exp.SameSet(FindMinHeightTrees(4, edges)));

            edges = new int[,] { { 0, 3 }, { 1, 3 }, { 2, 3 }, { 4, 3 }, { 5, 4 } };
            exp = new List<int> { 3, 4 };
            Console.WriteLine(exp.SameSet(FindMinHeightTrees(6, edges)));

        }
    }
}

