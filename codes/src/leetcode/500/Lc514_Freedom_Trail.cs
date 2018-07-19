using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp, dfs, greedy
 * Time(mn), Space(mn)
 * preprocess the index of previous char c in ring from i.
 */
namespace leetcode
{
    public class Lc514_Freedom_Trail
    {
        public int FindRotateStepsDp(string ring, string key)
        {
            int n = ring.Length, m = key.Length;
            // clock[i][c] is the index of previous char c in ring from i.
            int[][] clock = GetIndice(ring, 1), anti = GetIndice(ring, -1);
            var dp = new int[m + 1, n];

            for (int k = m - 1; k >= 0; k--)
            {
                for (int i = 0; i < n; i++)
                {
                    int previ = clock[i][key[k] - 'a'];
                    int nexti = anti[i][key[k] - 'a'];
                    dp[k, i] = Math.Min((i - previ + n) % n + dp[k + 1, previ], (nexti - i + n) % n + dp[k + 1, nexti]);
                }
            }

            return dp[0, 0] + m;
        }

        int[][] GetIndice(string ring, int inc)
        {
            var n = ring.Length;
            var res = new int[n][];
            var map = new int[26];
            // loop twice n to make sure it can jump to the prev/next char
            for (int i = 0, k = 0; k < 2*n; k++)
            {
                map[ring[i] - 'a'] = i;
                res[i] = map.ToArray();
                i = (i + inc + n) % n;
            }

            return res;
        }

        public int FindRotateSteps(string ring, string key)
        {
            int n = ring.Length;
            int m = key.Length;
            var rings = new List<int>[26];
            for (int i = 0; i < 26; i++)
                rings[i] = new List<int>();
            for (int i = 0; i < n; i++)
                rings[ring[i] - 'a'].Add(i);

            var memo = new int[n + 1, m + 1];
            return Dfs(rings, 0, key, 0, memo);
        }

        int Dfs(List<int>[] rings, int rp, string key, int kp, int[,] memo)
        {
            if (kp >= key.Length) return 0;
            if (memo[rp, kp] != 0) return memo[rp, kp];
            int ri = key[kp] - 'a';
            int pos = rings[ri].BinarySearch(rp);

            // the char at 12:00 is key[kp]
            if (pos >= 0) return memo[rp, kp] = 1 + Dfs(rings, rp, key, kp + 1, memo);

            pos = -1 - pos;
            int n = memo.GetLength(0) - 1;
            int rc = rings[ri].Count;

            int rpPrev = rings[ri][(pos - 1 + rc) % rc];
            int rpNext = rings[ri][(pos + rc) % rc];
            int stepsClockwise = (rp - rpPrev + n) % n;
            int stepsAnticlockwise = (rpNext - rp + n) % n;
            return memo[rp, kp] = 1 + Math.Min(stepsClockwise + Dfs(rings, rpPrev, key, kp + 1, memo),
                stepsAnticlockwise + Dfs(rings, rpNext, key, kp + 1, memo));
        }

        public void Test()
        {
            Console.WriteLine(FindRotateStepsDp("godding", "gd") == 4);
            Console.WriteLine(FindRotateSteps("godding", "gd") == 4);

            Console.WriteLine(FindRotateStepsDp("pqwcx", "cpqwx") == 13);
            Console.WriteLine(FindRotateSteps("pqwcx", "cpqwx") == 13);
        }
    }
}

