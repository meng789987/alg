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
            var sqs = new List<int[]>(); // {position, height}
            sqs.Add(new int[] { 0, 0 });
            var comp = Comparer<int[]>.Create((a, b) => a[0].CompareTo(b[0]));

            int max = 0;
            var res = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int s = positions[i, 0], t = s + positions[i, 1];

                var si = sqs.BinarySearch(new int[] { s, 0 }, comp);
                var ti = sqs.BinarySearch(new int[] { t, 0 }, comp);
                if (ti < 0)
                {
                    ti = ~ti;
                    sqs.Insert(ti, new int[] { t, sqs[ti - 1][1] });
                }

                int curh = 0;
                if (si < 0)
                {
                    si = ~si;
                    curh = sqs[si - 1][1];
                }
                for (int j = si; j < ti; j++)
                    curh = Math.Max(curh, sqs[j][1]);
                curh += positions[i, 1];

                sqs.RemoveRange(si, ti - si);
                sqs.Insert(si, new int[] { s, curh });

                max = Math.Max(max, curh);
                res.Add(max);
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
