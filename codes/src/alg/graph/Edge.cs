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

        public static List<int[]> MatrixToListEdges(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            var edges = new List<int[]>();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (matrix[i, j] != INF)
                        edges.Add(new int[] { i, j, matrix[i, j] });
            return edges;
        }

        public static List<int[]>[] ListEdgesToAdjEdges(int n, List<int[]> edges)
        {
            var adj = new List<int[]>[n];
            for (int i = 0; i < n; i++)
                adj[i] = new List<int[]>();

            foreach (var e in edges)
            {
                adj[e[0]].Add(new int[] { e[0], e[1], e[2] });
                adj[e[1]].Add(new int[] { e[1], e[0], e[2] });
            }

            return adj;
        }

        public static List<int[]>[] MatrixToAdjEdges(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            var edges = new List<int[]>[n];
            for (int i = 0; i < n; i++)
            {
                edges[i] = new List<int[]>();
                for (int j = 0; j < n; j++)
                    if (matrix[i, j] != INF)
                        edges[i].Add(new int[] { i, j, matrix[i, j] });
            }
            return edges;
        }
    }
}
