using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: shortest path, dp, greedy, heap
 * Time(nlogn), Space(n)
 * dp[i] is the length of longest increasing subsequence ending with nums[i].
 * dp[i] = max(dp[k] + 1), where k=[0..i-1] and nums[k] < nums[i].
 * Since dp is ordered, so we can binary search nums[i] in dp 
 * to replace (dp[k] if nums[i]<dp[k], so dp[i] only contains the smallest tail number of all increasing subsequence with same length) 
 * or insert (nums[i] if nums[i]>dp[i-1]).
 */
namespace alg.dp
{
    public class ShortestPath
    {
        /*
         * Time(VE), Space(V)
         * can detect negative cycle; can return shortest paths from vertex 0 to all other vertice.
         * loop n times to 'relax' all edges
         */
        int[] BellmanFord(int n, IList<Edge> edges)
        {
            var dist = new int[n];
            Array.Fill(dist, INF);
            dist[0] = 0;
            var prev = new int[n];

            for (int i = 0; i < n; i++)
            {
                foreach (var e in edges)
                {
                    if (dist[e.dst] > dist[e.src] + e.w)
                    {
                        dist[e.dst] = dist[e.src] + e.w;
                        prev[e.dst] = e.src;
                    }
                }
            }

            bool hasNegativeCyle = edges.Any(e => dist[e.dst] > dist[e.src] + e.w);

            return dist;
        }

        /*
         * Time(V^3), Space(V^2)
         * can detect negative cycle; can return shortest paths between any two vertice.
         * loop n times to 'relax' all two vertice.
         */
        int[] FloydWarshall(int n, int[,] matrix)
        {
            var dist = new int[n, n];
            Array.Copy(matrix, dist, n * n);

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                    }
            }

            // bool hasNegativeCyle = any dist[i, j] > dist[i, k] + dist[k, j];

            var res = new int[n];
            for (int i = 0; i < n; i++)
                res[i] = dist[0, i];
            return res;
        }

        /*
         * Time((V+E)logV), Space(V)
         * can't handle negative distances; can return shortest paths from vertex 0 to all other vertice.
         * 1. add all vertices to a minimum heap
         * 2. loop until heap is empty, extract min from heap and update the distance of its all adjacent vertices.
         */
        int[] Dijkstra(int n, LinkedList<Edge>[] adj)
        {
            var dist = new int[n];
            Array.Fill(dist, INF);
            dist[0] = 0;
            var prev = new int[n];

            var q = new SortedSet<Edge>(Comparer<Edge>.Create((a, b) => a.w != b.w ? a.w - b.w : a.dst - b.dst));
            q.Add(new Edge(0, 0, 0));
            for (int i = 1; i < n; i++)
                q.Add(new Edge(0, i, INF));

            while (q.Count > 0)
            {
                var min = q.Min; q.Remove(min);
                foreach (var e in adj[min.dst])
                {
                    if (dist[e.dst] > dist[e.src] + e.w)
                    {
                        q.Remove(new Edge(0, e.dst, dist[e.dst])); q.Add(new Edge(0, e.dst, dist[e.src] + e.w));
                        dist[e.dst] = dist[e.src] + e.w;
                        prev[e.dst] = e.src;
                    }
                }
            }

            return dist;
        }

        const int INF = int.MaxValue / 2;

        class Edge
        {
            public int src, dst, w; // source vertex, destination vertex, weight of the edge
            public Edge(int src, int dst, int weight)
            {
                this.src = src;
                this.dst = dst;
                this.w = weight;
            }

            public static IList<Edge> MatrixToFlatEdges(int[,] matrx)
            {
                int n = matrx.GetLength(0);
                var edges = new List<Edge>();
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (matrx[i, j] != INF)
                            edges.Add(new Edge(i, j, matrx[i, j]));
                return edges;
            }

            public static LinkedList<Edge>[] MatrixToAdjEdges(int[,] matrx)
            {
                int n = matrx.GetLength(0);
                var edges = new LinkedList<Edge>[n];
                for (int i = 0; i < n; i++)
                {
                    edges[i] = new LinkedList<Edge>();
                    for (int j = 0; j < n; j++)
                        if (matrx[i, j] != INF)
                            edges[i].AddLast(new Edge(i, j, matrx[i, j]));
                }
                return edges;
            }
        }

        public void Test()
        {
            var matrix = new int[,]
            {
                {INF, -1, 4, INF, INF },
                {INF, INF, 3, INF, 2 },
                {INF, INF, INF, INF, INF },
                {INF, 1, 5, INF, INF },
                {INF, INF, INF, -3, INF },
            };
            var exp = new int[] { 0, -1, 2, -2, 1 };
            Console.WriteLine(exp.SequenceEqual(BellmanFord(5, Edge.MatrixToFlatEdges(matrix))));
            Console.WriteLine(exp.SequenceEqual(FloydWarshall(5, matrix)));

            matrix = new int[,]
            {
                {INF, 1, 4, INF, INF },
                {INF, INF, 3, INF, 2 },
                {INF, INF, INF, INF, INF },
                {INF, 1, 5, INF, INF },
                {INF, INF, INF, 3, INF },
            };
            exp = new int[] { 0, 1, 4, 6, 3 };
            Console.WriteLine(exp.SequenceEqual(Dijkstra(5, Edge.MatrixToAdjEdges(matrix))));

        }
    }
}

