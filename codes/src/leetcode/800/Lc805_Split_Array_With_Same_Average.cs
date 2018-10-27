using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: backtracking
 * Time(sum(n!/i!), Space(n), where i=[1..n/2]
 */
namespace leetcode
{
    public class Lc805_Split_Array_With_Same_Average
    {
        public bool SplitArraySameAverage(int[] A)
        {
            int n = A.Length;
            if (n < 2) return false;

            var sum = A.Sum();
            Array.Sort(A); // for dedup

            for (int cnt = 1; cnt <= n / 2; cnt++)
            {
                if ((sum * cnt) % n != 0) continue; // prune
                if (Bt(A, 0, cnt, sum * cnt / n)) return true;
            }

            return false;
        }

        bool Bt(int[] a, int start, int count, int target)
        {
            if (count == 0) return target == 0;
            if (a[start] > target / count) return false; // prune

            for (int i = start; i <= a.Length - count; i++)
            {
                if (i > start && a[i - 1] == a[i]) continue; // dedup
                if (Bt(a, i + 1, count - 1, target - a[i]))
                    return true;
            }

            return false;
        }


        public void Test()
        {
            var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Console.WriteLine(SplitArraySameAverage(a) == true);

            a = new int[] { 6, 8, 18, 3, 1 };
            Console.WriteLine(SplitArraySameAverage(a) == false);

            a = new int[] { 60, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            Console.WriteLine(SplitArraySameAverage(a) == false);
        }
    }
}
