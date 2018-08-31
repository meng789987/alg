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
         * 3. for each edge, add it to mst if the two vertices are in different set. 
         */
        IList<Edge> Kruskal(int n, List<Edge> edges)
        {
            var res = new List<Edge>();
            var ds = new DisjointSet(n);
            edges.Sort((u, v) => u.w.CompareTo(v.w));

            foreach (var e in edges)
            {
                if (ds.Union(e.src, e.dst))
                    res.Add(e);
            }

            return res;
        }

        /*
         * Time(ElogV), Space(V)
         * 1. add all vertice to a minimum heap
         * 2. loop until heap is empty, extract min from heap to mst and update the distance of its all adjacent vertice.
         */
        IList<Edge> Prim(LinkedList<Edge>[] edges)
        {
            int n = edges.Length;
            var res = new List<Edge>();

            var weights = new int[n];
            Array.Fill(weights, INF);
            weights[0] = 0;

            var heap = new SortedSet<Edge>(Comparer<Edge>.Create(
                (a, b) => a.w != b.w ? a.w.CompareTo(b.w) : a.dst - b.dst));
            for (int i = 0; i < n; i++)
                heap.Add(new Edge(0, i, weights[i]));

            while (heap.Count > 0)
            {
                var node = heap.Min; heap.Remove(node);
                res.Add(node);
                weights[node.dst] = 0; // distance of vertice in mst is 0

                foreach (var e in edges[node.dst])
                {
                    if (weights[e.dst] > e.w)
                    {
                        heap.Remove(new Edge(e.src, e.dst, weights[e.dst]));
                        weights[e.dst] = e.w;
                        heap.Add(e);
                    }
                }
            }

            res.RemoveAt(0);
            return res;
        }

        const int INF = Edge.INF;

        public void Test()
        {
            var edges = new List<Edge> {
                new Edge(0, 1, 10),
                new Edge(0, 2, 6),
                new Edge(0, 3, 5),
                new Edge(1, 3, 15),
                new Edge(2, 3, 4) };
            var exp = new List<Edge> {
                new Edge(0, 1, 10),
                //new Edge(0, 2, 6),
                new Edge(0, 3, 5),
                //new Edge(1, 3, 15),
                new Edge(2, 3, 4) };
            Console.WriteLine(exp.SameSet(Kruskal(4, edges)));
            var r = Prim(Edge.MatrixToAdjEdges(Edge.FlatUndirectedEdgesToMatrix(4, edges)));
            foreach (var e in r)
            {
                int min = Math.Min(e.src, e.dst);
                e.dst = Math.Max(e.src, e.dst);
                e.src = min;
            }
            Console.WriteLine(exp.SameSet(r));

            edges = new List<Edge> {
                new Edge(0, 1, 4),
                new Edge(0, 7, 8),
                new Edge(1, 2, 12),
                new Edge(1, 7, 11),
                new Edge(2, 3, 7),
                new Edge(2, 8, 2),
                new Edge(2, 5, 4),
                new Edge(3, 4, 9),
                new Edge(3, 5, 14),
                new Edge(4, 5, 10),
                new Edge(5, 6, 2),
                new Edge(6, 7, 1),
                new Edge(6, 8, 6),
                new Edge(7, 8, 7) };
            exp = new List<Edge> {
                new Edge(0, 1, 4),
                new Edge(0, 7, 8),
                //new Edge(1, 2, 12),
                //new Edge(1, 7, 11),
                new Edge(2, 3, 7),
                new Edge(2, 8, 2),
                new Edge(2, 5, 4),
                new Edge(3, 4, 9),
                //new Edge(3, 5, 14),
                //new Edge(4, 5, 10),
                new Edge(5, 6, 2),
                new Edge(6, 7, 1), };
            //new Edge(6, 8, 6),
            //new Edge(7, 8, 7) };
            Console.WriteLine(exp.SameSet(Kruskal(9, edges)));
            r = Prim(Edge.MatrixToAdjEdges(Edge.FlatUndirectedEdgesToMatrix(9, edges)));
            foreach (var e in r)
            {
                int min = Math.Min(e.src, e.dst);
                e.dst = Math.Max(e.src, e.dst);
                e.src = min;
            }
            Console.WriteLine(exp.SameSet(r));
        }
    }
}
