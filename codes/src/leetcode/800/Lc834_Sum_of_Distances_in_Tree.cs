using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dfs
 * Time(n), Space(1)
 * first round, postoder from bottom up to get child count of each node, and distantce of root
 * second round, preorder from top down to update distantce of each node
 */
namespace leetcode
{
    public class Lc834_Sum_of_Distances_in_Tree
    {
        public int[] SumOfDistancesInTree(int N, int[][] edges)
        {
            var graph = new List<int>[N];
            for (int i = 0; i < N; i++)
                graph[i] = new List<int>();
            foreach (var e in edges)
            {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }

            var counts = new int[N];
            var dist = new int[N];
            Array.Fill(counts, 1);

            Dfs(graph, -1, 0, counts, dist, true);
            Dfs(graph, -1, 0, counts, dist, false);

            return dist;
        }

        void Dfs(List<int>[] graph, int parent, int node, int[] counts, int[] dist, bool firstRound)
        {
            foreach (var e in graph[node])
            {
                if (e == parent) continue;
                if (!firstRound)
                    dist[e] = dist[node] + graph.Length - counts[e] * 2;
                Dfs(graph, node, e, counts, dist, firstRound);
                if (firstRound)
                {
                    counts[node] += counts[e];
                    dist[node] += counts[e] + dist[e];
                }
            }
        }

        public void Test()
        {
            var edges = new int[][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 2, 3 }, new int[] { 2, 4 }, new int[] { 2, 5 } };
            var exp = new int[] { 8, 12, 6, 10, 10, 10 };
            Console.WriteLine(exp.SequenceEqual(SumOfDistancesInTree(6, edges)));
        }
    }
}
