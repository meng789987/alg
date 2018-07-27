using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: LIS(longest increasing subsequence), greedy/bs, dp
 * greedy/bs: Time(nlogn), Space(n)
 * dp: Time(n^2), Space(n)
 * rearrange them by increasing width and decreasing height, then the problem is to find the LIS(longest increasing subsequence)
 * dp[i] is the max length of dolls ending at i
 * dp[i] = max(1+dp[j]), where j=[0..i-1], env[j].w<env[i].w && env[j].h<env[i].h
 */
namespace leetcode
{
    public class Lc354_Russian_Doll_Envelopes
    {
        /*
         * Time(nlogn), Space(n)
         */
        public int MaxEnvelopesGreedyBs(int[,] envelopes)
        {
            int n = envelopes.GetLength(0);
            var envs = new int[n][];
            for (int i = 0; i < n; i++)
                envs[i] = new int[] { envelopes[i, 0], envelopes[i, 1] };
            // sort the env by increasing width and decreasing height
            Array.Sort(envs, (a, b) => a[0] != b[0] ? a[0] - b[0] : b[1] - a[1]);
            var lis = new int[n];
            int len = 0;

            foreach (var env in envs)
            {
                int idx = Array.BinarySearch(lis, 0, len, env[1]);
                if (idx < 0)
                {
                    idx = ~idx;
                    lis[idx] = env[1]; // got a smaller height
                    if (idx == len) len++;
                }
            }

            return len;
        }

        /*
         * Time(n^2), Space(n)
         */
        public int MaxEnvelopesDp(int[,] envelopes)
        {
            int n = envelopes.GetLength(0);
            var envs = new int[n][];
            for (int i = 0; i < n; i++)
                envs[i] = new int[] { envelopes[i, 0], envelopes[i, 1] };
            Array.Sort(envs, (a, b) => a[0] - b[0]);
            var dp = new int[n];

            int max = 0;
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                for (int j = 0; j < i; j++)
                {
                    if (envs[i][0] > envs[j][0] && envs[i][1] > envs[j][1])
                        dp[i] = Math.Max(dp[i], 1 + dp[j]);
                }
                max = Math.Max(max, dp[i]);
            }

            return max;
        }


        public void Test()
        {
            var env = new int[,] { { 5, 4 }, { 6, 4 }, { 6, 7 }, { 2, 3 } };
            Console.WriteLine(MaxEnvelopesGreedyBs(env) == 3);
            Console.WriteLine(MaxEnvelopesDp(env) == 3);

            env = new int[,] { { 4, 5 }, { 4, 6 }, { 6, 7 }, { 2, 3 }, { 1, 1 } };
            Console.WriteLine(MaxEnvelopesGreedyBs(env) == 4);
            Console.WriteLine(MaxEnvelopesDp(env) == 4);

            env = new int[,] { { 30, 50 }, { 12, 2 }, { 3, 4 }, { 12, 15 } };
            Console.WriteLine(MaxEnvelopesGreedyBs(env) == 3);
            Console.WriteLine(MaxEnvelopesDp(env) == 3);
        }
    }
}

