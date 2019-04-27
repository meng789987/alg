using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: shortest path, dp, greedy, heap
 * BellmanFord: Time(VE), Space(V); can detect negative cycle; can return shortest paths from vertex 0 to all other vertice.
 * FloydWarshall: Time(V^3), Space(V^2); can detect negative cycle; can return shortest paths between any two vertice.
 * Dijkstra: Time((V+E)logV), Space(V); can't handle negative distances; can return shortest paths from vertex 0 to all other vertice.
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
            var prev = new int[n];  // used for recovering the path

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
         * Time((V+E)logV), Space(V)
         * can't handle negative distances; can return shortest paths from vertex 0 to all other vertice.
         * 1. add all vertices to a minimum heap
         * 2. loop until heap is empty, extract min from heap and update the distance of its all adjacent vertices.
         */
        int[] Dijkstra(int n, LinkedList<Edge>[] adj)
        {
            var prev = new int[n]; // used to recover path
            var heap = new Heap(n);
            heap.Update(0, 0);

            for (int i = 0; i < n-1; i++)
            {
                var min = heap.ExtractMin();
                foreach (var e in adj[min])
                {
                    if (heap.Dist[e.dst] > heap.Dist[e.src] + e.w)
                    {
                        heap.Update(e.dst, heap.Dist[e.src] + e.w);
                        prev[e.dst] = e.src;
                    }
                }
            }

            return heap.Dist;
        }

        class Heap
        {
            public Heap(int n)
            {
                Dist = new int[n];
                Array.Fill(Dist, INF);
                Nodes = new SortedSet<int>(Comparer<int>.Create((i, j) => Dist[i] != Dist[j] ? Dist[i] - Dist[j] : i - j));
            }

            public int ExtractMin()
            {
                int min = Nodes.Min;
                Nodes.Remove(min);
                return min;
            }

            public void Update(int i, int value)
            {
                Nodes.Remove(i);
                Dist[i] = value;
                Nodes.Add(i);
            }

            public int[] Dist;
            private SortedSet<int> Nodes;
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
            var exp = new int[] { 0, -1, 2, -2, 1 };
            Console.WriteLine(exp.SequenceEqual(BellmanFord(5, Edge.MatrixToFlatEdges(matrix))));
            Console.WriteLine(exp.SequenceEqual(FloydWarshall(5, matrix)));

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = Math.Abs(matrix[i, j]);
            exp = new int[] { 0, 1, 4, 6, 3 };
            Console.WriteLine(exp.SequenceEqual(Dijkstra(5, Edge.MatrixToAdjEdges(matrix))));

        }
    }
}

