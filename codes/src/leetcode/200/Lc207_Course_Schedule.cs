
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: graph, dfs, bfs, topological sort
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc207_Course_Schedule
    {
        public bool CanFinish(int numCourses, int[,] prerequisites)
        {
            List<int>[] adjs = MakeAdjacentGraph(numCourses, prerequisites);

            var states = new int[numCourses]; // 0: not taken; 1: trying; 2: taken
            for (int i = 0; i < numCourses; i++)
            {
                if (!TryFinishDfs(adjs, i, states)) return false;
            }
            return true;
        }

        List<int>[] MakeAdjacentGraph(int nodeCount, int[,] edgePairs)
        {
            var adjs = new List<int>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adjs[i] = new List<int>();
            for (int i = 0; i < edgePairs.GetLength(0); i++)
                adjs[edgePairs[i, 0]].Add(edgePairs[i, 1]);

            return adjs;
        }

        bool TryFinishDfs(List<int>[] adjs, int c, int[] states)
        {
            if (states[c] == 2) return true;
            if (states[c] == 1) return false;
            states[c] = 1;
            foreach (var j in adjs[c])
            {
                if (!TryFinishDfs(adjs, j, states)) return false;
                states[j] = 2;
            }
            states[c] = 2;
            return true;
        }

        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            var ret = new List<int>();
            List<int>[] adjs = MakeAdjacentGraphInverse(numCourses, prerequisites);

            // a priority queue should be better
            var inDegrees = new int[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                foreach (var to in adjs[i])
                    inDegrees[to]++;
            }
            var queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
            {
                if (inDegrees[i] == 0) queue.Enqueue(i);
            }

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();

                ret.Add(node);
                //inDegrees[node] = -1; // remove from the queue
                // update the queue based on its adjacence
                foreach (var to in adjs[node])
                {
                    if (--inDegrees[to] == 0) queue.Enqueue(to);
                }
            }

            return ret.Count == numCourses ? ret.ToArray() : new int[0];
        }

        List<int>[] MakeAdjacentGraphInverse(int nodeCount, int[,] edgePairs)
        {
            var adjs = new List<int>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adjs[i] = new List<int>();
            for (int i = 0; i < edgePairs.GetLength(0); i++)
                adjs[edgePairs[i, 1]].Add(edgePairs[i, 0]);

            return adjs;
        }

        public void Test()
        {
            var prerequisites = new int[,] { { 1, 0 } };
            var expOrder = new int[] { 0, 1 };
            Console.WriteLine(CanFinish(2, prerequisites) == true);
            Console.WriteLine(expOrder.SequenceEqual(FindOrder(2, prerequisites)));

            prerequisites = new int[,] { { 1, 0 }, { 0, 1 } };
            expOrder = new int[] { };
            Console.WriteLine(CanFinish(2, prerequisites) == false);
            Console.WriteLine(expOrder.SequenceEqual(FindOrder(2, prerequisites)));

            prerequisites = new int[,] { { 1, 0 }, { 2, 0 }, { 3, 1 }, { 3, 2 } };
            expOrder = new int[] { 0, 1, 2, 3 };
            Console.WriteLine(CanFinish(4, prerequisites) == true);
            Console.WriteLine(expOrder.SequenceEqual(FindOrder(4, prerequisites)));
        }
    }
}

