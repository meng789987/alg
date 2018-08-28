using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: kmp
 * Time(n), Space(n)
 * kmp[j] is the length of longest substring of the pattern ending at j matched the prefix of pattern, so we don't need to restart matching from the beginning,
 * if str[i] != pattern[j], we can reset j=kmp[j-1] instead of j=0, because pattern[0..len] == pattern[j-len..j] where len=kmp[j]-1.
 */
namespace alg.leetcode
{
    public class Lc699_Falling_Squares
    {
        public IList<int> FallingSquares(int[,] positions)
        {
            int n = positions.GetLength(0);
            var sqs = new List<int[]>(n * 2); // {position, height}
            var comp = Comparer<int[]>.Create((a, b) => a[0].CompareTo(b[0]));

            var res = new int[n];
            for (int i = 0; i < n; i++)
            {
                int[] ps = new int[] { positions[i, 0], 0 }, pt = new int[] { positions[i, 0] + positions[i, 1], 0 }; // entering/leaving point

                // insert entering point
                var s = sqs.BinarySearch(ps, comp);
                var t = sqs.BinarySearch(pt, comp);
                if (s < 0) s = ~s;
                if (t < 0) t = ~t;
                int prevh = s == 0 ? 0 : sqs[s - 1][1];
                int s = 0, h = 0;
                for (; s < sqs.Count && (sqs[s][0] < ps || (sqs[s][0] == ps && sqs[s][1] == 1)); s++)
                {
                }
                sqs.Insert(s, new int[] { ps, 0, 0 });

                // insert leaving point
                int t = s + 1;
                for (; t < sqs.Count && (sqs[t][0] < pt || (sqs[t][0] == pt && sqs[t][1] == 1)); t++)
                    h = Math.Max(h, sqs[t][2]);
                sqs.Insert(t, new int[] { pt, 1, 0 });

                // update heights between two points
                h += positions[i, 1];
                for (int k = s; k <= t; k++)
                    sqs[k][2] = h;

                res[i] = i == 0 ? h : Math.Max(res[i - 1], h);
            }

            return res;
        }

        public void Test()
        {
            var input = new int[,] { { 1, 2 }, { 2, 3 }, { 6, 1 } };
            var exp = new int[] { 2, 5, 5 };
            Console.WriteLine(exp.SequenceEqual(FallingSquares(input)));
            
            input = new int[,] { { 100, 100 }, { 200, 100 } };
            exp = new int[] { 100, 100 };
            Console.WriteLine(exp.SequenceEqual(FallingSquares(input)));

            input = new int[,] { { 1, 5 }, { 2, 2 }, { 7, 5 } };
            exp = new int[] { 5, 7, 7 };
            Console.WriteLine(exp.SequenceEqual(FallingSquares(input)));
        }
    }
}
