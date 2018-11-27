using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: graph, bfs
 * Time(E), Space(N)
 * From a heap of reachable nodes, extract the one with biggest remained move, then follow its edges to add the connected nodes to the heap.
 */
namespace leetcode
{
    public class Lc882_Reachable_Nodes_In_Subdivided_Graph
    {
        public int ReachableNodes(int[][] edges, int M, int N)
        {
            var nodes = new Node[N];
            for (int i = 0; i < N; i++)
                nodes[i] = new Node(i);
            foreach (var e in edges)
            {
                int s = e[0], t = e[1], d = e[2];
                nodes[s].Vertice.Add(t);
                nodes[s].Edges[t] = d;
                nodes[t].Vertice.Add(s);
                nodes[t].Edges[s] = d;
            }

            int res = 0;
            var h = new SortedSet<Node>(Comparer<Node>.Create((a, b) => a.Move != b.Move ? b.Move - a.Move : a.Id - b.Id));
            nodes[0].Move = M;
            h.Add(nodes[0]);

            while (h.Count > 0)
            {
                var node = h.Min;
                h.Remove(h.Min);
                res++;

                foreach (var t in node.Vertice)
                {
                    if (!node.Edges.ContainsKey(t)) continue; // cur has no edge to t.
                    if (nodes[t].Edges.ContainsKey(node.Id)) // they are still connected.
                    {
                        if (node.Move > node.Edges[t])
                        {
                            res += node.Edges[t];
                            h.Remove(nodes[t]);
                            nodes[t].Move = Math.Max(nodes[t].Move, node.Move - node.Edges[t] - 1);
                            nodes[t].Edges.Remove(node.Id); // cut the edge t->cur to avoid cycle.
                            h.Add(nodes[t]);
                        }
                        else // unable to reach t
                        {
                            res += node.Move;
                            nodes[t].Edges[node.Id] -= node.Move; // shrink the edge
                            node.Edges.Remove(t); // cut the edge cur->t to avoid duplicate
                        }
                    }
                    else // t has no edge back to cur
                    {
                        res += Math.Min(node.Move, node.Edges[t]);
                    }
                }
            }

            return res;
        }

        class Node
        {
            public int Move, Id;
            public List<int> Vertice = new List<int>();
            public Dictionary<int, int> Edges = new Dictionary<int, int>(); // [dest node id, distance(new node count)]
            public Node(int id) { Id = id; }
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
