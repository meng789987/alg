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
            Console.WriteLine("Lc719_Find_Kth_Smallest_Pair_Distance:");
            //new leetcode.Lc719_Find_Kth_Smallest_Pair_Distance().Test();
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

            var x = new Solution().KthSmallestPrimeFraction(new int[] { 1, 13, 17, 59}, 1);
            Console.Write(new int[] { 13, 17 }.SequenceEqual(x));
        }
        public int[] KthSmallestPrimeFraction(int[] A, int K)
        {
            var res = new int[] { 1, A.Last() };
            double lo = 1.0/A.Last(), hi = 1;
            double minGap = 1.0 / A[A.Length - 1] / A[A.Length - 2];
            while (lo < hi)
            {
                var mid = (lo + hi) / 2;
                var upperBound = new int[] { 1, 1 };
                if (Count(A, mid, ref upperBound) < K)
                {
                    lo = mid + minGap;
                    res = upperBound;
                }
                else hi = mid - minGap;
            }
            return res;
        }

        int Count(int[] A, double val, ref int[] upperBound)
        {
            int count = 0;
            for (int j = 1, i = 0; i < A.Length - 1; i++)
            {
                while (j < A.Length && A[i] > A[j] * val) j++;
                count += A.Length - j;
                if (A[i] * upperBound[1] < A[j - 1] * upperBound[0])
                    upperBound = new int[] { A[i], A[j - 1] };
            }
            return count;
        }
    }
}
