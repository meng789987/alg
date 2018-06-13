
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: dp
 * Time(kn), Space(k)
 * dp[k, i] = max(dp[k, i-1], dp[k-1, j-1] + prices[i] - prices[j]), j=[0..i-1]
 */
namespace leetcode
{
    public class Lc188_Best_Time_to_Buy_and_Sell_Stock_IV
    {
        public int MaxProfit(int k, int[] prices)
        {
            if (prices.Length < 2) return 0;
            int K = k;
            var dp = new int[K + 1, prices.Length];
            for (k = 1; k <= K; k++)
            {
                int max = -prices[0];
                for (int i = 1; i < prices.Length; i++)
                {
                    max = Math.Max(max, dp[k - 1, i - 1] - prices[i - 1]);
                    dp[k, i] = Math.Max(dp[k, i - 1], prices[i] + max);
                }
            }

            return dp[K, prices.Length - 1];
        }

        public int MaxProfitCompact(int k, int[] prices)
        {
            if (k > prices.Length / 2) // unlimited trade
            {
                int ret = 0;
                for (int i = 1; i < prices.Length; i++)
                    ret += Math.Max(0, prices[i] - prices[i - 1]);
                return ret;
            }

            int K = k;
            var dp = new int[K + 1];
            var max = new int[K + 1];
            Array.Fill(max, -prices[0]);
            for (int i = 1; i < prices.Length; i++)
            {
                for (k = 1; k <= K; k++)
                {
                    max[k] = Math.Max(max[k], dp[k - 1] - prices[i]);
                    dp[k] = Math.Max(dp[k], prices[i] + max[k]);
                }
            }

            return dp[K];
        }

        public void Test()
        {
            var prices = new int[] { 2, 4, 1 };
            Console.WriteLine(MaxProfit(2, prices) == 2);
            Console.WriteLine(MaxProfitCompact(2, prices) == 2);

            prices = new int[] { 3, 2, 6, 5, 0, 3 };
            Console.WriteLine(MaxProfit(2, prices) == 7);
            Console.WriteLine(MaxProfitCompact(2, prices) == 7);

            prices = new int[] { 3, 3, 5, 0, 0, 3, 1, 4 };
            Console.WriteLine(MaxProfit(2, prices) == 6);
            Console.WriteLine(MaxProfitCompact(2, prices) == 6);
        }
    }
}

