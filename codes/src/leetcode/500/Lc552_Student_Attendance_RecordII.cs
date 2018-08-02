using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: dp
 * Time(n), Space(n)
 * dp[i] is the total rewardable records ending at i with 'P', same for dl[i] and da[i].
 * dpNoA[i] is the total rewardable records ending at i with 'P' and never has 'A'.
 * da[i] = dpNoA[i-1] + dlNoA[i-1]      \
 * dpNoA[i] = dpNoA[i-1] + dlNoA[i-1]    |>===> da[i] = da[i-1] + da[i-2] + da[i-3]
 * dlNoA[i] = dpNoA[i-1] + dpNoA[i-2]   /
 * 
 * advance DP: any DP linear formula can be reduced to matrix exponentiation, then Time(n) will become Time(logn)
 * /dp[i][0]\     /0, 0, 1, 0, 0, 0\   /dp[i-1][0]\     // without 'A' and no tailing 'L'
 * |dp[i][1]|     |1, 0, 1, 0, 0, 0|   |dp[i-1][1]|     // without 'A' and at most 1 tailing 'L'
 * |dp[i][2]|     |0, 1, 1, 0, 0, 0|   |dp[i-1][2]|     // without 'A' and at most 2 tailing 'L'
 * |dp[i][3]| =   |0, 0, 1, 0, 0, 1| * |dp[i-1][3]|     // at most 1 'A' and no tailing 'L'
 * |dp[i][4]|     |0, 0, 1, 1, 0, 1|   |dp[i-1][4]|     // at most 1 'A' and at most 1 tailing 'L'
 * \dp[i][5]/     \0, 0, 1, 0, 1, 1/   \dp[i-1][5]/     // at most 1 'A' and at most 2 tailing 'L'
 * 
 * Let M be the matrix, then dp[n] = M^n * dp[0].  Time complexity is logn to calculate M^n using exponentiation by squaring.
 * dp[0] = {1, 1, 1, 1, 1, 1}; dp[n][5] is the anwser.
 * Looking at dp[n][5], it is equal to M^(n+1)[5][2].
 */
namespace leetcode
{
    public class Lc552_Student_Attendance_RecordII
    {
        public int CheckRecord(int n)
        {
            const long N = 1000000007;
            long[] da = new long[n + 2], dl = new long[n + 2], dp = new long[n + 2];
            dp[1] = dl[1] = da[1] = da[0] = 1;
            dp[2] = dl[2] = 3;
            da[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                dp[i] = (da[i - 1] + dl[i - 1] + dp[i - 1]) % N;
                dl[i] = (da[i - 1] + dp[i - 1] + da[i - 2] + dp[i - 2]) % N;
                da[i] = (da[i - 1] + da[i - 2] + da[i - 3]) % N;
            }

            return (int)((da[n] + dl[n] + dp[n]) % N);
        }

        public int CheckRecordReduceDpToMatrix(int n)
        {
            var a = new int[,] {
                {0, 0, 1, 0, 0, 0},
                {1, 0, 1, 0, 0, 0},
                {0, 1, 1, 0, 0, 0},
                {0, 0, 1, 0, 0, 1},
                {0, 0, 1, 1, 0, 1},
                {0, 0, 1, 0, 1, 1}};
            return Pow(a, n + 1)[5, 2];
        }

        int[,] Pow(int[,] a, int n)
        {
            var res = a;
            n--;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = Mul(res, a);
                a = Mul(a, a);
                n /= 2;
            }

            return res;
        }

        int[,] Mul(int[,] a, int[,] b)
        {
            int m = a.GetLength(0);
            var c = new int[m, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < m; k++)
                        c[i, j] += a[i, k] * b[k, j];
            return c;
        }

        public void Test()
        {
            Console.WriteLine(CheckRecord(2) == 8);
            Console.WriteLine(CheckRecord(3) == 19);
            Console.WriteLine(CheckRecord(4) == 43);
            Console.WriteLine(CheckRecord(5) == 94);
            Console.WriteLine(CheckRecord(6) == 200);

            Console.WriteLine(CheckRecordReduceDpToMatrix(2) == 8);
            Console.WriteLine(CheckRecordReduceDpToMatrix(3) == 19);
            Console.WriteLine(CheckRecordReduceDpToMatrix(4) == 43);
            Console.WriteLine(CheckRecordReduceDpToMatrix(5) == 94);
            Console.WriteLine(CheckRecordReduceDpToMatrix(6) == 200);

            Console.WriteLine(CheckRecord(28));
            Console.WriteLine(CheckRecordReduceDpToMatrix(28));
            Console.WriteLine(Extensions.ElapsedMilliseconds(() => CheckRecord(10000000), 10));
            Console.WriteLine(Extensions.ElapsedMilliseconds(() => CheckRecordReduceDpToMatrix(10000000), 10));
        }
    }
}

