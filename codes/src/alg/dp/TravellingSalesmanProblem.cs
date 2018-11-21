using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: TSP(Travelling Salesman Problem)
 * TSP is a NP-hard problem, and can be solved in O(n^2*2^n) using Dynamic Programming. The decision version of TSP is NP-Complete.
 * If the edges are satisfied the triangle inequality, then we can use MST to get an approximate solution whose cost is less than twice of the optimal one.
 */
namespace alg.dp
{
    public class TravellingSalesmanProblem
    {
        /*
         * Time(2^nn^2), Space(2^nn)
         * dp[S, i] is the shortest path from 0 to i for node set S, which is a subset of graph and always has node 0.
         * dp[S, i] = min(dp[S-{i}, j] + dist[j, i]), j is in S-{i}.
         */
        int Tsp_Dp(int[,] graph)
        {
            int n = graph.GetLength(0), fullSet = (1 << n) - 1;
            var dp = new int[1 << n, n];

            for (int S = 1; S <= fullSet; S += 2)
            {
                for (int i = 1; i < n; i++)
                {
                    if (((S >> i) & 1) == 0) continue;
                    int nS = S ^ (1 << i);
                    if (nS == 1)
                        dp[S, i] = graph[0, i];
                    else
                    {
                        dp[S, i] = int.MaxValue;
                        for (int j = 1; j < n; j++)
                        {
                            if (((nS >> j) & 1) == 0) continue;
                            dp[S, i] = Math.Min(dp[S, i], dp[nS, j] + graph[j, i]);
                        }
                    }
                }
            }

            int ret = int.MaxValue;
            for (int i = 1; i < n; i++)
                ret = Math.Min(ret, dp[fullSet, i] + graph[i, 0]);

            return ret;
        }

        public void Test()
        {
            var graph = new int[,] {
                { 0, 10, 15, 20 },
                { 10, 0, 35, 25 },
                { 15, 35, 0, 30 },
                { 20, 25, 30, 0 } };
            Console.WriteLine(Tsp_Dp(graph) == 80);
        }
    }
}

