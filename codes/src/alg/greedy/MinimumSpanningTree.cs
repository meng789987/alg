using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace alg.greedy
{
    public class MinimumSpanningTree
    {
        /*
         * Time(ElogV), Space(V)
         * 1. create a disjoint set for all vertices
         * 2. sort the edges in non-decreasing order of their weights
         * 3. for each edge, add it to MST if the two vertices are in different set. 
         */
        List<int[]> Kruskal(int n, List<int[]> edges)
        {
            var res = new List<int[]>();
            var ds = new DisjointSet(n);
            edges.Sort((u, v) => u[2].CompareTo(v[2]));

            foreach (var e in edges)
            {
                int src = e[0], dst = e[1], w = e[2];
                if (ds.Union(src, dst))
                    res.Add(e);
            }

            return res;
        }

        /*
         * Time(ElogV), Space(V)
         * 1. initialize the distance of vetex 0 to 0 and all others to INF.
         * 2. add all vertice to a minimum heap based on their distances from MST
         * 3. loop until heap is empty, extract min from heap to mst and update the distance of its all adjacent vertice.
         */
        List<int[]> Prim(List<int[]>[] adj)
        {
            int n = adj.Length;
            var res = new List<int[]>();

            var sources = new int[n];
            var dist = new int[n];
            for (int i = 1; i < n; i++)
                dist[i] = INF;
            var heap = new Heap(dist);

            while (heap.Count > 0)
            {
                var node = heap.ExtractMin();
                res.Add(new int[] { sources[node], node, dist[node] });
                dist[node] = 0; // any vertex in MST has 0 distance. We need to set this otherwise we need to track if the node is in MST or not to avoid add them to heap again.

                foreach (var e in adj[node])
                {
                    int src = e[0], dst = e[1], w = e[2];
                    if (dist[dst] > w)
                    {
                        heap.Update(dst, w);
                        sources[dst] = src;
                    }
                }
            }

            res.RemoveAt(0); // remove the fake edge<0, 0, 0>
            return res;
        }

        const int INF = Edge.INF;

        public void Test()
        {
            var matrix = new int[,]
            {
                {INF, 10, 6, 5 },
                {10, INF, INF, 15 },
                {6, INF, INF, 4 },
                {5, 15, 4, INF }
            };
            var expstr = "0, 1, 10; 0, 3, 5; 2, 3, 4";

            Console.WriteLine(expstr == NormalizedEdges(Kruskal(4, Edge.MatrixToListEdges(matrix))));
            Console.WriteLine(expstr == NormalizedEdges(Prim(Edge.MatrixToAdjEdges(matrix))));
            
            var edges = new List<int[]> {
                new int[] {0, 1, 4},
                new int[] {0, 7, 8},
                new int[] {1, 2, 12},
                new int[] {1, 7, 11},
                new int[] {2, 3, 7},
                new int[] {2, 5, 4},
                new int[] {2, 8, 2},
                new int[] {3, 4, 9},
                new int[] {3, 5, 14},
                new int[] {4, 5, 10},
                new int[] {5, 6, 2},
                new int[] {6, 7, 1},
                new int[] {6, 8, 6},
                new int[] {7, 8, 7} };
            expstr = "0, 1, 4; 0, 7, 8; 2, 3, 7; 2, 5, 4; 2, 8, 2; 3, 4, 9; 5, 6, 2; 6, 7, 1";

            Console.WriteLine(expstr == NormalizedEdges(Kruskal(9, edges)));
            Console.WriteLine(expstr == NormalizedEdges(Prim(Edge.ListEdgesToAdjEdges(9, edges))));
        }

        string NormalizedEdges(List<int[]> edges)
        {
            edges = edges.Select(e => e[0] < e[1] ? e : new int[] { e[1], e[0], e[2] }).ToList();
            edges.Sort((a, b) => (a[0] * 100 + a[1]).CompareTo(b[0] * 100 + b[1]));
            var s = string.Join("; ", edges.Select(e => string.Join(", ", e)));
            return s;
        }
    }
}
