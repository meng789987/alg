using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bs
 * Time(mlog(mn)), Space(1)
 * binary test value=[1..m*n] to check if there are k or more number of multiplications less than the value;
 * we can count for each row to get the total number of multiplications less than the value
 */
namespace leetcode
{
    public class Lc668_Kth_Smallest_Number_in_Multiplication_Table
    {
        public int FindKthNumber(int m, int n, int k)
        {
            int lo = 1, hi = m * n;
            while (lo < hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (Count(m, n, mid) < k) lo = mid + 1;
                else hi = mid;
            }

            return lo;
        }

        int Count(int m, int n, int value)
        {
            int count = 0;
            for (int i = 1; i <= m; i++)
                count += Math.Min(n, value / i);
            return count;
        }

        public void Test()
        {
            Console.WriteLine(FindKthNumber(3, 3, 5) == 3);
            Console.WriteLine(FindKthNumber(2, 3, 6) == 6);
        }
    }
}

