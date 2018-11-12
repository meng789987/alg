using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: bfs
 * Time(n^2), Space(n^2)
 */
namespace leetcode
{
    public class Lc815_Bus_Routes
    {
        public int NumBusesToDestination(int[][] routes, int S, int T)
        {
            if (S == T) return 0;
            var stopToBus = new Dictionary<int, List<int>>();
            for (int b = 0; b < routes.Length; b++)
            {
                foreach (var s in routes[b])
                {
                    if (!stopToBus.ContainsKey(s))
                        stopToBus.Add(s, new List<int>());
                    stopToBus[s].Add(b);
                }
            }

            int ret = 0;
            var taken = new HashSet<int>();
            var q = new Queue<int>();
            q.Enqueue(S);

            while (q.Count > 0)
            {
                ret++;
                for (int count = q.Count; count > 0; count--)
                {
                    var stop = q.Dequeue();
                    foreach (var bus in stopToBus[stop])
                    {
                        if (taken.Contains(bus)) continue;
                        taken.Add(bus);
                        foreach (var s in routes[bus])
                        {
                            if (s == T) return ret;
                            q.Enqueue(s);
                        }
                    }
                }
            }

            return -1;
        }

        public void Test()
        {
            var routes = new int[][] { new int[] { 1, 2, 7 }, new int[] { 3, 6, 7 } };
            Console.WriteLine(NumBusesToDestination(routes, 1, 6) == 2);
        }
    }
}
