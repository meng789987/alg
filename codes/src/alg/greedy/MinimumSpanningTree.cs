using alg.graph;
using System;
using System.Collections.Generic;
using System.Text;

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
                var u = ds.Find(e.src);
                var v = ds.Find(e.dst);
                if (u != v)
                {

                }
            }

            return null;
        }
    }
}
