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

        public override bool Equals(object obj)
        {
            var that = obj as Edge;
            if (that == null) return false;
            return this.src == that.src && this.dst == that.dst && this.w == that.w;
        }

        public override int GetHashCode()
        {
            var hashCode = -1029123829;
            hashCode = hashCode * -1521134295 + src.GetHashCode();
            hashCode = hashCode * -1521134295 + dst.GetHashCode();
            hashCode = hashCode * -1521134295 + w.GetHashCode();
            return hashCode;
        }

        public static IList<Edge> MatrixToFlatEdges(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            var edges = new List<Edge>();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (matrix[i, j] != INF)
                        edges.Add(new Edge(i, j, matrix[i, j]));
            return edges;
        }

        public static int[,] FlatUndirectedEdgesToMatrix(int n, IList<Edge> edges)
        {
            var matrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = INF;

            foreach (var e in edges)
            {
                matrix[e.src, e.dst] = e.w;
                matrix[e.dst, e.src] = e.w;
            }

            return matrix;
        }

        public static LinkedList<Edge>[] MatrixToAdjEdges(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            var edges = new LinkedList<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                edges[i] = new LinkedList<Edge>();
                for (int j = 0; j < n; j++)
                    if (matrix[i, j] != INF)
                        edges[i].AddLast(new Edge(i, j, matrix[i, j]));
            }
            return edges;
        }
    }
}
