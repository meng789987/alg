using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: minimax, Topological Traversal
 * Time(n^3), Space(n^3)
 * 1. Topological traversal starts from the ending status, or use minimax game strategy, 
 *    to determine who will win in each state[m, c, t] where m is mouse position, c is cat position, t is 1-mouse's turn or 2-cat's turn.
 *    color the state with MOUSE(1) if mouse wins or CAT(2) if cat wins, or DRAW(0) if unknown or draw.
 *    State transition or a move is based on the graph, a parent is previous state and a child is next state. Degree of a state is count of children.
 * 2. initially state[0, c, t]=1, state[i, i, t]=2, put them into queue, for all parents of the current dequeue state, 
 *    if it is a winning move (e.g. mouse to MOUSE, its state is mouse turn and a child is MOUSE, then it is MOUSE) then color it;
 *    else decrease its degree of DRAW, if the degree is 0 then it is a losing move, then color it.
 */
namespace leetcode
{
    public class Lc913_Cat_and_Mouse
    {
        public int CatMouseGame(int[][] graph)
        {
            int n = graph.Length;
            var color = new int[n, n, 3];
            var degree = new int[n, n, 3];
            var q = new Queue<Node>();

            // degree[node] : the number of neutral children of this node
            for (int m = 0; m < n; m++)
            {
                for (int c = 0; c < n; c++)
                {
                    degree[m, c, MOUSE] = graph[m].Length;
                    degree[m, c, CAT] = graph[c].Length;
                    if (graph[c].Any(nc => nc == 0))// cat can't move to the hole
                        degree[m, c, CAT]--;
                }
            }

            // color and enqueue the winning states
            for (int c = 1; c < n; c++)
            {
                for (int t = 1; t <= 2; t++)
                {
                    color[0, c, t] = MOUSE;
                    q.Enqueue(new Node(0, c, t, MOUSE));
                    color[c, c, t] = CAT;
                    q.Enqueue(new Node(c, c, t, CAT));
                }
            }

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                var parents = GetParents(graph, node);
                foreach (var p in parents)
                {
                    if (color[p.M, p.C, p.Turn] != DRAW) continue;
                    if (p.Turn == node.Color) // winning move
                    {
                        color[p.M, p.C, p.Turn] = p.Turn;
                        q.Enqueue(new Node(p.M, p.C, p.Turn, p.Turn));
                    }
                    else
                    {
                        if (--degree[p.M, p.C, p.Turn] == 0) // losing move
                        {
                            color[p.M, p.C, p.Turn] = p.Turn == MOUSE ? CAT : MOUSE;
                            q.Enqueue(new Node(p.M, p.C, p.Turn, p.Turn == MOUSE ? CAT : MOUSE));
                        }
                    }
                }
            }

            return color[1, 2, MOUSE];
        }

        List<Node> GetParents(int[][] graph, Node node)
        {
            var parents = new List<Node>();
            if (node.Turn == MOUSE)
            {
                foreach (var c in graph[node.C])
                    if (c > 0) parents.Add(new Node(node.M, c, CAT, 0));
            }
            else
            {
                foreach (var m in graph[node.M])
                    parents.Add(new Node(m, node.C, MOUSE, 0));
            }

            return parents;
        }

        const int DRAW = 0, MOUSE = 1, CAT = 2;

        class Node
        {
            public int M, C, Turn, Color; // M=mouse, C=Cat

            public Node(int mouse, int cat, int turn, int color)
            {
                M = mouse; C = cat; Turn = turn; Color = color;
            }
        }

        public void Test()
        {
            var a = new int[][] { new int[] { 2, 5 }, new int[] { 3 }, new int[] { 0, 4, 5 }, new int[] { 1, 4, 5 }, new int[] { 2, 3 }, new int[] { 0, 2, 3 } };
            Console.WriteLine(CatMouseGame(a) == 0);

            a = new int[][] { new int[] { 1, 3 }, new int[] { 0 }, new int[] { 3 }, new int[] { 0, 2 } };
            Console.WriteLine(CatMouseGame(a) == 1);

            a = new int[][] { new int[] { 2, 3 }, new int[] { 2 }, new int[] { 0, 1 }, new int[] { 0, 4 }, new int[] { 3 } };
            Console.WriteLine(CatMouseGame(a) == 2);
        }
    }
}
