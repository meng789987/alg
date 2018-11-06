using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: permutation, backtracking
 */
namespace alg.math
{
    public class Permutation
    {

        void NextPermutation(int[] a)
        {
            int n = a.Length;

            // find the rightmost item that is greater than its previous item
            int i = n - 1;
            while (i > 0 && a[i - 1] >= a[i]) i--;

            // swap it(a[i-1]) with the smallest item in a[i..n-1] that is greater than it
            if (i > 0)
            {
                int j = n - 1;
                while (j > i && a[j] < a[i - 1]) j--;
                Swap(ref a[i - 1], ref a[j]);
            }

            // reverse a[i..n-1]
            Array.Reverse(a, i, n - i);
        }

        public IList<string> PermutationNodup(string s)
        {
            var ret = new List<string>();
            PermutationBt(0, s.ToCharArray(), ret);
            return ret;
        }

        void PermutationBt(int pos, char[] path, IList<string> res)
        {
            if (pos == path.Length)
            {
                res.Add(new string(path));
                return;
            }

            for (int i = pos; i < path.Length; i++)
            {
                Swap(ref path[pos], ref path[i]);
                PermutationBt(pos + 1, path, res);
                Swap(ref path[pos], ref path[i]);
            }
        }

        // s may have duplicate chars
        public IList<string> PermutationDup(string s)
        {
            var ret = new List<string>();
            var cs = s.ToCharArray();
            Array.Sort(cs);
            PermutationDupBt(0, cs, ret);
            return ret;
        }

        void PermutationDupBt(int pos, char[] path, IList<string> res)
        {
            if (pos == path.Length)
            {
                res.Add(new string(path));
                return;
            }

            for (int i = pos; i < path.Length; i++)
            {
                if (i > pos && path[pos] == path[i]) continue; // dedup
                Swap(ref path[pos], ref path[i]); // transit to next permutation
                PermutationDupBt(pos + 1, path.ToArray(), res); // make a copy of path
            }
        }

        void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        public void Test()
        {
            var nums = new int[] { 1, 1, 2, 2 };
            NextPermutation(nums);
            var exp = new int[] { 1, 2, 1, 2 };
            Console.WriteLine(exp.SequenceEqual(nums));
            NextPermutation(nums);
            exp = new int[] { 1, 2, 2, 1 };
            Console.WriteLine(exp.SequenceEqual(nums));

            Console.WriteLine(PermutationNodup("abc").Count == 1 * 2 * 3);
            Console.WriteLine(PermutationNodup("abcd").Count == 1 * 2 * 3 * 4);
            Console.WriteLine(PermutationNodup("abcde").Count == 1 * 2 * 3 * 4 * 5);

            Console.WriteLine(PermutationDup("abc").Count == 1 * 2 * 3);
            Console.WriteLine(PermutationDup("abab").Count == 1 * 2 * 3 * 4 / (1 * 2) / (1 * 2));
            Console.WriteLine(PermutationDup("abcaa").Count == 1 * 2 * 3 * 4 * 5 / (1 * 2 * 3));
            Console.WriteLine(PermutationDup("abcaab").Count == 1 * 2 * 3 * 4 * 5 * 6 / (1 * 2 * 3) / (1 * 2));
            Console.WriteLine(PermutationDup("acacbacb").Count == 1 * 2 * 3 * 4 * 5 * 6 * 7 * 8 / (1 * 2 * 3) / (1 * 2 * 3) / (1 * 2));
            Console.WriteLine(PermutationDup("adbacbc").Count == 1 * 2 * 3 * 4 * 5 * 6 * 7 / 2 / 2 / 2);
        }
    }
}
