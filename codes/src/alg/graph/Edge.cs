using System;
using System.Collections.Generic;
using System.Text;

namespace alg.graph
{
    public class Edge
    {
        public const int INF = int.MaxValue / 2;

        public int src, dst; // source vertex, destination vertex
        public int w; // weight of the edge

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
}
