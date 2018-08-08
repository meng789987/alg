using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using leetcode;

namespace alg
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BinarySearch:");
            //new array.BinarySearch().Test();
            Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var table = new System.Data.DataTable();
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));

            var (a, b) = (3, 4);
            var ss = new SortedSet<int>();
            Console.WriteLine(ss.Min);

            var x = new Solution().KthSmallestPrimeFraction(new int[] { 1, 2, 3, 5 }, 3);
            Console.WriteLine(x);
        }
        public int[] KthSmallestPrimeFraction(int[] A, int K)
        {
            int n = A.Length;
            var indice = new int[n];
            for (int i = 0; i < n; i++) indice[i] = i;

            var res = Biselect(A, indice, K, K);
            return new int[] { (int)res[0][1], (int)res[0][2] };
        }

        // return lk-th and rk-th elements { p/q, p, q }
        double[][] Biselect(int[] A, int[] selected, int lk, int rk)
        {
            int n = selected.Length;
            // base case
            if (n <= 2)
            {
                var nums = new double[n * n][];
                int k = 0;
                foreach (var i in selected)
                    foreach (var j in selected)
                        nums[k++] = MatrixItem(A, i, j);
                Array.Sort(nums, (a, b) => a[0].CompareTo(b[0]));
                return new double[][] { nums[lk - 1], nums[rk - 1] };
            }

            // subproblem
            var nselected = new int[n / 2 + 1];
            for (int i = 0; i < n / 2; i++)
                nselected[i] = selected[i * 2];
            nselected[n / 2] = selected[n - 1];
            int nlk = (lk + 3) / 4;
            int nrk = n % 2 == 0 ? (rk + 4 * n + 3) / 4 + 1 : (rk + 2 * n) / 4 + 1;
            var nr = Biselect(A, nselected, nlk, nrk);
            double[] loValue = nr[0], hiValue = nr[1];

            // calculate rank-(hi), rank+(lo) with saddleback binary search
            int cntSmaller = 0, cntGreater = 0;
            double[][][] listBetween = new double[n][][];
            for (int js = n, jg = n, i = 0; i < n; i++)
            {
                while (js > 0 && MatrixItem(A, selected[i], selected[js - 1])[0] >= hiValue[0]) js--;
                while (jg > 0 && MatrixItem(A, selected[i], selected[jg - 1])[0] > loValue[0]) jg--;
                cntSmaller += js;
                cntGreater += n - jg;
                listBetween[i] = new double[Math.Max(0, js - jg)][];
                for (int j = jg; j < js; j++)
                    listBetween[i][j - jg] = MatrixItem(A, selected[i], selected[j]);
            }

            // compute result
            bool hasMerged = false;
            double[] lo = cntSmaller < lk ? hiValue
                : lk + cntGreater <= n * n ? loValue
                : Pick(listBetween, lk + cntGreater - n * n, ref hasMerged);
            double[] hi = cntSmaller < rk ? hiValue
                : rk + cntGreater <= n * n ? loValue
                : Pick(listBetween, rk + cntGreater - n * n, ref hasMerged);

            return new double[][] { lo, hi };
        }

        double[] Pick(double[][][] data, int k, ref bool hasMerged)
        {
            if (!hasMerged)
            {
                hasMerged = true;
                Merge(data, 0, data.Length - 1);
            }
            return data[0][k - 1];
        }

        void Merge(double[][][] data, int lo, int hi)
        {
            if (lo >= hi) return;

            int mid = (lo + hi) / 2;
            Merge(data, lo, mid);
            Merge(data, mid + 1, hi);

            double[][] ls = data[lo], rs = data[mid + 1];
            double[][] aux = new double[ls.Length + rs.Length][];
            for (int i = 0, j = 0, k = 0; k < aux.Length; k++)
            {
                if (i < ls.Length && (j >= rs.Length || ls[i][0] < rs[j][0]))
                    aux[k] = ls[i++];
                else aux[k] = rs[j++];
            }
            data[lo] = aux;
        }

        double[] MatrixItem(int[] A, int row, int col)
        {
            return new double[] { (double)A[row] / A[A.Length - 1 - col], A[row], A[A.Length - 1 - col] };
        }
    }
}
