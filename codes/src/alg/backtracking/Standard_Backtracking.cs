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
            SolveQueensBt(n, new List<int>(), ret, new bool[n], new bool[2 * n], new bool[2 * n]);
            return ret;
        }

        // parameters: input, path, result, other tracking parameters
        void SolveQueensBt(int n, List<int> path, IList<int[]> res, bool[] cols, bool[] diagf, bool[] diagb)
        {
            if (path.Count == n)
            {
                res.Add(path.ToArray());
                return;
            }

            int row = path.Count;
            for (int j = 0; j < n; j++)
            {
                if (cols[j] || diagf[row + j] || diagb[row - j + n]) continue;
                cols[j] = diagf[row + j] = diagb[row - j + n] = true;
                path.Add(j);
                SolveQueensBt(n, path, res, cols, diagf, diagb);
                path.RemoveAt(path.Count - 1);
                cols[j] = diagf[row + j] = diagb[row - j + n] = false;
            }
        }

        // lc46. Permutations
        public IList<string> Permutation(string s)
        {
            var ret = new List<string>();
            PermutationBt(s, new List<char>(), ret, new bool[s.Length]);
            return ret;
        }

        void PermutationBt(string s, List<char> path, IList<string> res, bool[] used)
        {
            if (path.Count == s.Length)
            {
                res.Add(new string(path.ToArray()));
                return;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (used[i]) continue;
                used[i] = true;
                path.Add(s[i]);
                PermutationBt(s, path, res, used);
                path.RemoveAt(path.Count - 1);
                used[i] = false;
            }
        }

        // lc47. Permutations II
        // s may have duplicate chars
        public IList<string> PermutationDup(string s)
        {
            var ret = new List<string>();
            var cs = s.ToCharArray();
            Array.Sort(cs); // dedup
            PermutationDupBt(cs, new List<char>(), ret, new bool[s.Length]);
            return ret;
        }

        /*
         * 1, sort the array to easy dedup;
         * 2, we make the dup chars in original order. e.g. (here we add the index of the char in subscript to distinguish them)
         * [a, b, b, c] => [a_0, b_1, b_2, c_3], so
         * [... b_1 ... b_2 ...] is same as [... b_2 ... b_1 ...], so we only count/select the first one,
         * that means we will use b_2 only if b_1 has been used.
         */ 
        void PermutationDupBt(char[] cs, List<char> path, IList<string> res, bool[] used)
        {
            if (path.Count == cs.Length)
            {
                res.Add(new string(path.ToArray()));
                return;
            }

            for (int i = 0; i < cs.Length; i++)
            {
                if (used[i] || (i > 0 && cs[i-1] == cs[i] && !used[i-1])) continue; // dedup
                used[i] = true;
                path.Add(cs[i]);
                PermutationDupBt(cs, path, res, used);
                path.RemoveAt(path.Count - 1);
                used[i] = false;
            }
        }

        //========= Permutation variants =======================
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
                Swap(a, i - 1, j);
            }

            // reverse a[i..n-1]
            Array.Reverse(a, i, n - i);
        }

        void PermutationVariant(int pos, char[] path, IList<string> res)
        {
            if (pos == path.Length)
            {
                res.Add(new string(path));
                return;
            }

            for (int i = pos; i < path.Length; i++)
            {
                Swap(path, pos, i);
                PermutationVariant(pos + 1, path, res);
                Swap(path, pos, i);
            }
        }

        void PermutationDupVariant(int pos, char[] path, IList<string> res)
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
                PermutationDupVariant(pos + 1, path.ToArray(), res); // make a copy of path
            }
        }
        //========= Permutation variants end =======================

        public IList<int[]> Subset(int[] nums, int k)
        {
            var ret = new List<int[]>();
            SubsetBt(nums, k, new List<int>(), ret, 0);
            return ret;
        }

        void SubsetBt(int[] nums, int k, List<int> path, IList<int[]> res, int start)
        {
            if (path.Count == k)
            {
                res.Add(path.ToArray());
                return;
            }

            for (int i = start; i < nums.Length; i++) // note: from start, not 0
            {
                path.Add(nums[i]);
                SubsetBt(nums, k, path, res, i + 1);
                path.RemoveAt(path.Count - 1);
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

            var nums = new int[] { 1, 1, 2, 2 };
            NextPermutation(nums);
            Console.WriteLine("1, 2, 1, 2" == string.Join(", ", nums));
            NextPermutation(nums);
            Console.WriteLine("1, 2, 2, 1" == string.Join(", ", nums));

            Console.WriteLine(PermutationDup("abc").Count == Fac(3));
            Console.WriteLine(PermutationDup("abab").Count == Fac(4) / Fac(2) / Fac(2));
            Console.WriteLine(PermutationDup("abcaa").Count == Fac(5) / Fac(3));
            Console.WriteLine(PermutationDup("abcaab").Count == Fac(6) / Fac(3) / Fac(2));
            Console.WriteLine(PermutationDup("acacbacb").Count == Fac(8) / Fac(3) / Fac(3) / Fac(2));
            Console.WriteLine(PermutationDup("adbacbc").Count == Fac(7) / Fac(2) / Fac(2) / Fac(2));

            nums = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine(Subset(nums, 3).Count == Fac(5) / Fac(2) / Fac(3));
        }
    }
}
