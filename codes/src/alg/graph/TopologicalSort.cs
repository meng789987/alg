using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: Topological Sort
 * Time(n), Space(n)
 */
namespace alg.graph
{
    public class TopologicalSort
    {
        /* 1. transform to adjacent edges, and record the incoming degrees
         * 2. visit/add the vetice with 0 incoming degrees into queue [and visit them], 
         * 3. then decrease the incoming degree of its adjacent nodes, if any node has 0 in-degree put it into queue
         * Lc 210. Course Schedule II
         * Lc 2050. Parallel Courses III
         */
        public int MinimumTime(int n, int[][] relations, int[] times)
        {
            var endTimes = new int[n];

            var inDegrees = new int[n]; // incoming degree is the number of prerequisite course
            var adj = new List<int>[n]; // adjacent edges are list of next courses
            for (int i = 0; i < n; i++)
            {
                adj[i] = new List<int>();
                endTimes[i] = times[i];
            }

            foreach (var r in relations)
            {
                int u = r[0] - 1, v = r[1] - 1;
                inDegrees[v]++;
                adj[u].Add(v);
            }

            var q = new Queue<int>();
            for (int i = 0; i < n; i++)
                if (inDegrees[i] == 0) q.Enqueue(i);

            while (q.Count > 0)
            {
                var u = q.Dequeue(); // visit the node

                // update incoming degrees
                foreach (var v in adj[u])
                {
                    if (--inDegrees[v] == 0) q.Enqueue(v);

                    endTimes[v] = Math.Max(endTimes[v], endTimes[u] + times[v]);
                }
            }

            return endTimes.Max();
        }

        public void Test()
        {
            var relationStr = "1,3],[2,3";
            var relations = relationStr.Split("],[").Select(s => s.Split(',').Select(a => int.Parse(a)).ToArray()).ToArray();
            var times = new int[] { 3, 2, 5 };
            Console.WriteLine(MinimumTime(3, relations, times) == 8);

            relationStr = "1,5],[2,5],[3,5],[3,4],[4,5";
            relations = relationStr.Split("],[").Select(s => s.Split(',').Select(a => int.Parse(a)).ToArray()).ToArray();
            times = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine(MinimumTime(5, relations, times) == 12);
        }
    }
}
