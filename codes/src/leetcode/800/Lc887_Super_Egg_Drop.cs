using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math, dp
 * Time(klogn), Space(1)
 * dp[k, n] = min(max(dp[k-1, i-1], dp[k, n-i])) + 1, i=[1..n]
 * The edge cases: dp[1, n] = n, dp[k, n] = L if n>>k == 0, L is length of n in binary.
 * The above dp is Time(kn^2), to reduce time we can check the maximum number of floor given k eggs and t tests/moves.
 * dp[k, t] = dp[k-1, t-1] + dp[k, t-1] + 1
 * Solve the above equation similar with binomial recurrence, dp[K, T] = sum(C(T, k)), k=[1..K], C(T, k)=T!/k!/(T-k)!,
 * then we can use binary search to find t.
 */
namespace leetcode
{
    public class Lc887_Super_Egg_Drop
    {
        public int SuperEggDrop(int K, int N)
        {
            if (K == 1) return N;
            if (N >> K == 0) return Convert.ToString(N, 2).Length;

            var dp = new int[K + 1, N + 1];

            int t = 0;
            while (dp[K, t] < N)
            {
                t++;
                for (int i = 1; i <= K; i++)
                    dp[i, t] = dp[i - 1, t - 1] + dp[i, t - 1] + 1;
            }

            return t;
        }

        public int SuperEggDropCompact(int K, int N)
        {
            if (K == 1) return N;
            if (N >> K == 0) return Convert.ToString(N, 2).Length;

            int[] dp = new int[K + 1];

            int step = 0;
            for (; dp[K] < N; step++)
            {
                for (int i = K; i > 0; i--)
                    dp[i] += dp[i - 1] + 1;
            }

            return step;
        }

        public int SuperEggDropBs(int K, int N)
        {
            if (K == 1) return N;
            if (N >> K == 0) return Convert.ToString(N, 2).Length;

            int lo = 1, hi = N;
            while (lo < hi)
            {
                int m = (lo + hi) / 2;
                if (BinomialSum(m, K, N) < N)
                    lo = m + 1;
                else
                    hi = m;
            }

            return lo;
        }

        // sum of combination(n, k)
        int BinomialSum(int n, int k, int limit)
        {
            int sum = 0;
            for (int r = 1, i = 1; i <= k; i++)
            {
                r = r * (n - i + 1) / i;
                sum += r;
                if (sum > limit) break; // avoid overflow
            }

            return sum;
        }

        public void Test()
        {
            Console.WriteLine(SuperEggDrop(1, 2) == 2);
            Console.WriteLine(SuperEggDrop(2, 6) == 3);
            Console.WriteLine(SuperEggDrop(3, 14) == 4);
            Console.WriteLine(SuperEggDrop(2, 10000) == 141);

            Console.WriteLine(SuperEggDropCompact(1, 2) == 2);
            Console.WriteLine(SuperEggDropCompact(2, 6) == 3);
            Console.WriteLine(SuperEggDropCompact(3, 14) == 4);
            Console.WriteLine(SuperEggDropCompact(2, 10000) == 141);

            Console.WriteLine(SuperEggDropBs(1, 2) == 2);
            Console.WriteLine(SuperEggDropBs(2, 6) == 3);
            Console.WriteLine(SuperEggDropBs(3, 14) == 4);
            Console.WriteLine(SuperEggDropBs(4, 10000) == 23);
        }
    }
}
