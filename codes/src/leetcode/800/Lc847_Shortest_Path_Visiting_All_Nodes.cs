using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: bfs/dp
 * Time(n2^n), Space(n2^n)
 * dp(state|next, next) = min(dp(state|next, next), dp(state, head) + 1), for each edge (head -> next)
 * the state can be a bitwise integer. 
 * Relax/tighten the distance from state with head vertex to state|next with head next vertex until we get the final state(2^n-1)
 */
namespace leetcode
{
    public class Lc847_Shortest_Path_Visiting_All_Nodes
    {
        public int ShortestPathLength(int[][] graph)
        {
            int n = graph.Length;
            int INF = n * n;
            int finalState = (1 << n) - 1;

            var dist = new int[1 << n, n];
            var q = new LinkedList<State>();

            for (int v = 0; v < n; v++)
            {
                for (int s = 0; s < dist.GetLength(0); s++)
                    dist[s, v] = INF;
                dist[1 << v, v] = 0;
                q.AddLast(new State(1 << v, v));
            }

            while (q.Count > 0)
            {
                var currState = q.First.Value;
                q.RemoveFirst();
                if (currState.Cover == finalState)
                    return dist[currState.Cover, currState.Head];

                foreach (var next in graph[currState.Head])
                {
                    var nextState = new State(currState.Cover | (1 << next), next);
                    if (dist[nextState.Cover, nextState.Head] > dist[currState.Cover, currState.Head] + 1)
                    {
                        dist[nextState.Cover, nextState.Head] = dist[currState.Cover, currState.Head] + 1;
                        q.AddLast(nextState);
                    }
                }
            }

            return -1;
        }

        class State
        {
            public int Cover;
            public int Head;
            public State(int cover, int head)
            { Cover = cover; Head = head; }
        }

        public void Test()
        {
            var grap = new int[][] { new int[] { 1, 2, 3 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 } };
            Console.WriteLine(ShortestPathLength(grap) == 4);

            grap = new int[][] { new int[] { 1 }, new int[] { 0, 2, 4 }, new int[] { 1, 3, 4 }, new int[] { 2 }, new int[] { 1, 2 } };
            Console.WriteLine(ShortestPathLength(grap) == 4);

            grap = new int[][] { new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 4, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 6, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 7, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } };
            Console.WriteLine(ShortestPathLength(grap) == 11);
        }
    }
}
