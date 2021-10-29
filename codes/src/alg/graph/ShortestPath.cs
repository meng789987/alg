using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: shortest path, dp, greedy, heap
 * FloydWarshall: Time(V^3), Space(V^2); can detect negative cycle; can return shortest paths between any two vertice.
 * BellmanFord: Time(VE), Space(V); can detect negative cycle; can return shortest paths from vertex 0 to all other vertice.
 * Dijkstra: Time((V+E)logV), Space(V); can't handle negative distances; can return shortest paths from vertex 0 to all other vertice.
 */
namespace alg.graph
{
    public class ShortestPath
    {
        /*
         * Time(V^3), Space(V^2)
         * can detect negative cycle; can return shortest paths between any two vertice.
         * loop n times to 'relax' all pairs of two vertice.
         */
        int[] FloydWarshall(int n, int[,] matrix)
        {
            var dist = new int[n, n];
            Array.Copy(matrix, dist, n * n);
            dist[0, 0] = 0;

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
         * Time(VE), Space(V)
         * can detect negative cycle; can return shortest paths from vertex 0 to all other vertice.
         * loop n times to 'relax' all edges
         */
        int[] BellmanFord(int n, IList<int[]> edges)
        {
            var dist = new int[n];
            Array.Fill(dist, INF);
            dist[0] = 0;
            var prev = new int[n];  // used for recovering the path

            for (int i = 0; i < n; i++)
            {
                foreach (var e in edges)
                {
                    int src = e[0], dst = e[1], w = e[2];
                    if (dist[dst] > dist[src] + w)
                    {
                        dist[dst] = dist[src] + w;
                        prev[dst] = src;
                    }
                }
            }

            bool hasNegativeCyle = edges.Any(e => dist[e[1]] > dist[e[0]] + e[2]);

            return dist;
        }

        /*
         * Time((V+E)logV), Space(V)
         * can't handle negative distances; can return shortest paths from vertex 0 to all other vertice.
         * 1. initialize the distance of vetex 0 to 0 and all others to INF.
         * 2. add all vertice to a minimum heap based on their distances from vetex 0
         * 2. loop until heap is empty, extract min from heap and update the distance of its all adjacent vertices.
         */
        int[] Dijkstra(int n, IList<int[]>[] adj)
        {
            var dist = new int[n];
            for (int i = 1; i < n; i++)
                dist[i] = INF;
            var heap = new HeapArray(dist);

            var prev = new int[n]; // used to recover the path

            for (int i = 0; i < n-1; i++)
            {
                var min = heap.ExtractMin();
                foreach (var e in adj[min])
                {
                    int src = e[0], dst = e[1], w = e[2];
                    if (dist[dst] > dist[src] + w)
                    {
                        heap.Update(dst, dist[src] + w);
                        dist[dst] = dist[src] + w;
                        prev[dst] = src;
                    }
                }
            }

            return dist;
        }

        const int INF = Edge.INF;

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
            var exp = "0, -1, 2, -2, 1";
            Console.WriteLine(exp == string.Join(", ", FloydWarshall(5, matrix)));
            Console.WriteLine(exp == string.Join(", ", BellmanFord(5, Edge.MatrixToListEdges(matrix))));

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = Math.Abs(matrix[i, j]);
            exp = "0, 1, 4, 6, 3";
            Console.WriteLine(exp == string.Join(", ", FloydWarshall(5, matrix)));
            Console.WriteLine(exp == string.Join(", ", BellmanFord(5, Edge.MatrixToListEdges(matrix))));
            Console.WriteLine(exp == string.Join(", ", Dijkstra(5, Edge.MatrixToAdjEdges(matrix))));

        }
    }
}

