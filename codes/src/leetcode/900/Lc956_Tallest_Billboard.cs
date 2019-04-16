using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(nw), Space(w), w=sum(rods[0..n-1])
 * Assume left post is not taller than right, dp[b, i] is the max height of left post using rods[0..i] and balance (difference of height between two posts) is b.
 * put rod[i]=h on right: dp[b+h, i] = max(dp[b+h, i], dp[b, i-1])
 * put rod[i]=h on left:  dp[abs(b-h)] = max(dp[abs(b-h)], dp[b, i-1] + min(b, h))  //-- left might be higher than right after adding rod[i]
 */
namespace leetcode
{
    public class Lc956_Tallest_Billboard
    {
        public int TallestBillboard(int[] rods)
        {
            int maxsum = rods.Sum(), cursum = 0;
            int[] dp = new int[maxsum + 1], dpold = new int[maxsum + 1];
            Array.Fill(dp, int.MinValue);
            dp[0] = 0;

            foreach (var h in rods)
            {
                cursum += h;
                Array.Copy(dp, dpold, cursum + 1);
                for (int b = 0; b <= cursum - h; b++)
                {
                    dp[b + h] = Math.Max(dp[b + h], dpold[b]); // put it on right
                    dp[Math.Abs(b-h)] = Math.Max(dp[Math.Abs(b - h)], dpold[b] + Math.Min(b, h)); // put it on left
                }
            }


            return dp[0];
        }

        public void Test()
        {
            var rods = new int[] { 1, 2, 3, 6 };
            Console.WriteLine(TallestBillboard(rods) == 6);

            rods = new int[] { 1, 2, 3, 4, 5, 6 };
            Console.WriteLine(TallestBillboard(rods) == 10);

            rods = new int[] { 1, 2 };
            Console.WriteLine(TallestBillboard(rods) == 0);

            rods = new int[] { 3, 4, 3, 3, 2 };
            Console.WriteLine(TallestBillboard(rods) == 6);

            rods = new int[] { 61, 45, 43, 54, 40, 53, 55, 47, 51, 59, 42};
            Console.WriteLine(TallestBillboard(rods) == 275);
        }
    }
}

