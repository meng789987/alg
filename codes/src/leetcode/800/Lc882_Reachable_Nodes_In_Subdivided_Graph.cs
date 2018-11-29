using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: Dijkstra
 * Time(ElogN), Space(E)
 * Extract the closest one in the heap, then follow its edges to add the connected nodes to the heap.
 */
namespace leetcode
{
    public class Lc882_Reachable_Nodes_In_Subdivided_Graph
    {
        public int ReachableNodes(int[][] edges, int M, int N)
        {
            var nodes = new Node[N];
            for (int i = 0; i < N; i++)
                nodes[i] = new Node(i, M + 1);
            foreach (var e in edges)
            {
                int u = e[0], v = e[1], d = e[2];
                nodes[u].Edges[v] = d;
                nodes[v].Edges[u] = d;
            }

            int res = 0;
            var h = new SortedSet<Node>(Comparer<Node>.Create((a, b) => a.Dist != b.Dist ? a.Dist - b.Dist : a.Id - b.Id));
            nodes[0].Dist = 0;
            h.Add(nodes[0]);

            while (h.Count > 0)
            {
                var cur = h.Min;
                h.Remove(h.Min);
                res++; // reachable nodes

                foreach (var kv in cur.Edges)
                {
                    int v = kv.Key, d = kv.Value;
                    nodes[v].Edges[cur.Id] -= Math.Min(d, M - cur.Dist); // shorten edge v->cur
                    res += Math.Min(d, M - cur.Dist);
                    if (nodes[v].Dist > cur.Dist + d + 1)
                    {
                        h.Remove(nodes[v]);
                        nodes[v].Dist = cur.Dist + d + 1;
                        h.Add(nodes[v]);
                    }
                }
            }

            return res;
        }

        class Node
        {
            public int Id, Dist;
            public Dictionary<int, int> Edges = new Dictionary<int, int>(); // key: dest node id, value: weight(new node count)
            public Node(int id, int dist) { Id = id; Dist = dist; }
        }

        public void Test()
        {
            var a = new int[][] { new int[] { 0, 1, 10 }, new int[] { 0, 2, 1 }, new int[] { 1, 2, 2 } };
            Console.WriteLine(ReachableNodes(a, 6, 3) == 13);

            a = new int[][] { new int[] { 0, 1, 4 }, new int[] { 1, 2, 6 }, new int[] { 0, 2, 8 }, new int[] { 1, 3, 1 } };
            Console.WriteLine(ReachableNodes(a, 10, 4) == 23);

            a = new int[][] { new int[] { 0, 2, 3 }, new int[] { 0, 4, 4 }, new int[] { 2, 3, 8 }, new int[] { 1, 3, 5 }, new int[] { 0, 3, 9 }, new int[] { 3, 4, 6 }, new int[] { 0, 1, 5 }, new int[] { 2, 4, 6 }, new int[] { 1, 2, 3 }, new int[] { 1, 4, 1 } };
            Console.WriteLine(ReachableNodes(a, 8, 5) == 43);
        }
    }
}
