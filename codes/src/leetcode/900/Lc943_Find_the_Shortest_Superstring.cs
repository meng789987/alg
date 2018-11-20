using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * tags: dp, TSP(Travelling Salesman Problem)
 * Time(2^nn^2), Space(2^nn)
 * TSP: 1. graph[i][j] means the length of string to append when A[i] followed by A[j]. eg. A[i] = abcd, A[j] = bcde, then graph[i][j] = 1
 * 2. Then the problem becomes to: find the shortest path in this graph which visits every node exactly once. This is a Travelling Salesman Problem.
 * 3. Apply TSP DP solution. Remember to record the path.
 * dp[S, i] is the shortest superstring starting with A[i] for string set S, which is a subset of A.
 * dp[S, i] = min(len(A[i]) - overlap(A[i], A[j]) + dp[S-{i}, j]), j is in S-{i}
 */
namespace leetcode
{
    public class Lc943_Find_the_Shortest_Superstring
    {
        public string ShortestSuperstring(string[] A)
        {
            int n = A.Length, finalState = (1 << n) - 1;
            var overlaps = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int len = Math.Min(A[i].Length, A[j].Length); len > 0; len--)
                        if (A[i].EndsWith(A[j].Substring(0, len)))
                        {
                            overlaps[i, j] = len;
                            break;
                        }
                }
            }

            var dp = new int[1 << n, n]; // len<<4 | nextIndex
            for (int state = 1; state <= finalState; state++)
            {
                for (int i = 0; i < n; i++)
                {
                    // if i is not in the current state
                    if (((state >> i) & 1) == 0) continue;

                    int nextState = state ^ (1 << i); // clear i-th bit
                    if (nextState == 0)
                    {
                        dp[state, i] = A[i].Length << 4;
                        continue;
                    }

                    int minj = -1, len = int.MaxValue;
                    for (int j = 0; j < n; j++)
                    {
                        if (((nextState >> j) & 1) == 0) continue;
                        int nlen = A[i].Length - overlaps[i, j] + (dp[nextState, j] >> 4);
                        if (len > nlen)
                        {
                            len = nlen;
                            minj = j;
                        }
                    }

                    dp[state, i] = (len << 4) | minj;
                }
            }

            int minIdx = -1, minLen = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (minLen > (dp[finalState, i] >> 4))
                {
                    minLen = dp[finalState, i] >> 4;
                    minIdx = i;
                }
            }

            var ret = new StringBuilder();
            for (int state = finalState, i = minIdx, overlap = 0; state > 0;)
            {
                ret.Append(A[i].Substring(overlap));
                int v = dp[state, i];
                overlap = overlaps[i, v & 15];
                state ^= 1 << i;
                i = v & 15;
            }

            return ret.ToString();
        }

        public void Test()
        {
            var a = new string[] { "alex", "loves", "leetcode" };
            Console.WriteLine(ShortestSuperstring(a) == "alexlovesleetcode");

            a = new string[] { "catg", "ctaagt", "gcta", "ttca", "atgcatc" };
            Console.WriteLine(ShortestSuperstring(a) == "gctaagttcatgcatc");

            a = new string[] { "a" };
            Console.WriteLine(ShortestSuperstring(a) == "a");
        }
    }
}
