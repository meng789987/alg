using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: TSP(Travelling Salesman Problem)
 * TSP is a NP-hard problem, it could be O(n!) using backtracking. The decision version of TSP is NP-Complete.
 * However, it can be solved in O(n^2*2^n) using Dynamic Programming with Bit Mask. 
 * If the edges are satisfied the triangle inequality, 
 * then we can use MST to get an approximate solution whose cost is less than twice of the optimal one.
 */
namespace alg.dp
{
    public class TravellingSalesmanProblem
    {
        /*
         * Time(n^2*2^n), Space(n*2^n)
         * dp[S, i] is the shortest path from 0 to i while travaling node subset S, which includes node 0 and i.
         * dp[S, i] = min(dp[S-{i}, j] + dist[j, i]), j is in S-{i}.
         * base case: dp[{1,i}, i] = dist[0, i]
         */
        int Tsp_Dp(int[,] graph)
        {
            int n = graph.GetLength(0), fullSet = (1 << n) - 1;
            var dp = new int[1 << n, n];

            for (int S = 1; S <= fullSet; S += 2) // step is 2 as we always keep node 0.
            {
                for (int i = 1; i < n; i++)
                {
                    if ((S & (1 << i)) == 0) continue; // set S doesn't include node i yet
                    int from = S - (1 << i); // travel to i-th node from other set without i
                    if (from == 1) // base case
                        dp[S, i] = graph[0, i];
                    else
                    {
                        dp[S, i] = int.MaxValue;
                        for (int j = 1; j < n; j++)
                        {
                            if ((from & (1 << j)) == 0) continue;
                            dp[S, i] = Math.Min(dp[S, i], dp[from, j] + graph[j, i]);
                        }
                    }
                }
            }

            int res = int.MaxValue;
            for (int i = 1; i < n; i++)
                res = Math.Min(res, dp[fullSet, i] + graph[i, 0]);

            return res;
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

