using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: interval sort/scan line
 * Time(n), Space(n)
 * shift i+1 to make A[i] be the last, then interval[0, n-1-A[i]] will get 1 point.
 * that is before shift, the interval[i+1, n-A[i]+i] will get 1 point. Need handle wrap around.
 */
namespace leetcode
{
    public class Lc798_Smallest_Rotation_with_Highest_Score
    {
        public int BestRotation(int[] A)
        {
            int n = A.Length;
            var points = new int[n + 1];

            for (int i = 0; i < n; i++)
            {
                if (A[i] >= n) continue;

                points[i + 1]++;
                points[A[i] <= i + 1 ? n : i - A[i] + n + 1]--;

                if (A[i] <= i) // wrap
                {
                    points[0]++;
                    points[i - A[i] + 1]--;
                }
            }

            int ret = 0, maxScore = 0, score = 0;
            for (int i = 0; i < n; i++)
            {
                score += points[i];
                if (maxScore < score)
                {
                    maxScore = score;
                    ret = i;
                }
            }

            return ret;
        }

        public void Test()
        {
            var a = new int[] { 2, 3, 1, 4, 0 };
            Console.WriteLine(BestRotation(a) == 3);

            a = new int[] { 1, 3, 0, 2, 4 };
            Console.WriteLine(BestRotation(a) == 0);
        }
    }
}
