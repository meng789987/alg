using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: backtracking
 * queens, permutation, permutation with dup, subset
 */
namespace alg.backtracking
{
    public class Standard_Backtracking
    {
        public IList<int[]> SolveQueens(int n)
        {
            var ret = new List<int[]>();
            SolveQueensBt(n, 0, new int[n], new bool[n], new bool[n], new bool[2 * n], new bool[2 * n], ret);
            return ret;
        }

        void SolveQueensBt(int n, int pos, int[] path, bool[] rows, bool[] cols, bool[] diagf, bool[] diagb, IList<int[]> res)
        {
            if (pos == n)
            {
                res.Add(path.ToArray());
                return;
            }

            for (int j = 0; j < n; j++)
            {
                if (rows[pos] || cols[j] || diagf[pos + j] || diagb[n + pos - j]) continue;
                path[pos] = j;
                rows[pos] = cols[j] = diagf[pos + j] = diagb[n + pos - j] = true;
                SolveQueensBt(n, pos + 1, path, rows, cols, diagf, diagb, res);
                rows[pos] = cols[j] = diagf[pos + j] = diagb[n + pos - j] = false;
            }
        }

        public IList<string> Permutation(string s)
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
                Swap(path, pos, i);
                PermutationBt(pos + 1, path, res);
                Swap(path, pos, i);
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
                Swap(path, pos, i); // transit to next permutation
                PermutationDupBt(pos + 1, path.ToArray(), res); // make a copy of path
            }
        }

        public IList<int[]> Subset(int[] nums, int k)
        {
            var ret = new List<int[]>();
            SubsetBt(nums, 0, new int[k], 0, ret);
            return ret;
        }

        void SubsetBt(int[] nums, int pos, int[] path, int start, IList<int[]> res)
        {
            if (pos == path.Length)
            {
                res.Add(path.ToArray());
                return;
            }

            for (int i = start; i < nums.Length; i++)
            {
                path[pos] = nums[i];
                SubsetBt(nums, pos + 1, path, i + 1, res);
            }
        }

        void Swap<T>(T[] a, int i, int j)
        {
            T tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }

        int Fac(int n)
        {
            int res = 1;
            while (n > 1) res *= n--;
            return res;
        }

        public void Test()
        {
            Console.WriteLine(SolveQueens(4).Count == 2);
            Console.WriteLine(SolveQueens(6).Count == 4);
            Console.WriteLine(SolveQueens(8).Count == 92);
            Console.WriteLine(SolveQueens(10).Count == 724);

            Console.WriteLine(Permutation("abc").Count == Fac(3));
            Console.WriteLine(Permutation("abcd").Count == Fac(4));
            Console.WriteLine(Permutation("abcde").Count == Fac(5));

            Console.WriteLine(PermutationDup("abc").Count == Fac(3));
            Console.WriteLine(PermutationDup("abab").Count == Fac(4) / Fac(2) / Fac(2));
            Console.WriteLine(PermutationDup("abcaa").Count == Fac(5) / Fac(3));
            Console.WriteLine(PermutationDup("abcaab").Count == Fac(6) / Fac(3) / Fac(2));
            Console.WriteLine(PermutationDup("acacbacb").Count == Fac(8) / Fac(3) / Fac(3) / Fac(2));
            Console.WriteLine(PermutationDup("adbacbc").Count == Fac(7) / Fac(2) / Fac(2) / Fac(2));

            var nums = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine(Subset(nums, 3).Count == Fac(5) / Fac(2) / Fac(3));
        }
    }
}
