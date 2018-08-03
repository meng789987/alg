using System;
using System.Collections.Generic;
using System.Text;

/*
 * tags: dp, matrix exponentiation
 * Time(logn), Space(1)
 * 
 * any DP linear formula can be reduced to matrix exponentiation, then Time(n) will become Time(logn)
 * https://www.hackerearth.com/practice/notes/matrix-exponentiation-1/
 * 
 * e.g1. Fibonacci formula, F[i] = F[i-1] + F[i-2], with intial values F[0]=F[1]=1, can be reduce to
 * {F[i]  } = { 1, 1 } * {F[i-1]} = ... = M^(i-1) * {F[1]}
 * {F[i-1]}   { 1, 0 }   {F[i-2]}                   {F[0]}
 * 
 * e.g2. sum of Fibonacci number formula, S[i] = sum(F[0..i]), can be reduce to
 * {S[i]  }   { 1, 1, 1 }   {S[i-1]}                   {S[1]}
 * {F[i]  } = { 0, 1, 1 } * {F[i-1]} = ... = M^(i-1) * {F[1]}
 * {F[i-1]}   { 0, 1, 0 }   {F[i-2]}                   {F[0]}
 *
 * e.g3. formula F[i] = F[i-1]*4 + G[i-1]*2, G[i] = G[i-1]*3 + 2^i, can be reduce to
 * {F[i]}   { 4, 2, 0 }   {F[i-1] }                   {S[1]}
 * {G[i]} = { 0, 3, 1 } * {G[i-1] } = ... = M^(i-1) * {F[1]}
 * {2^i }   { 0, 0, 2 }   {2^(i-1)}                   {F[0]}
 *                                                    
 * generally, F[i] = c[1]*F[i-1] + ... + c[k]*F[i-k], then M is a kxk matrix
 */
namespace alg.dp
{
    public class Dp2Matrix
    {
        int Fibonacci(long n)
        {
            if (n <= 1) return 1;
            var m = new int[,] { { 1, 1 }, { 1, 0 } };
            m = Pow(m, n - 1);
            return (m[0, 0] + m[0, 1]) % MOD;
        }

        const int MOD = 1000000007;

        int[,] Mul(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            var c = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        c[i, j] = ((c[i, j] + a[i, k]) % MOD + b[k, j]) % MOD;
            return c;
        }

        int[,] Pow(int[,] a, long n)
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

        public void Test()
        {
            Console.WriteLine(Extensions.ElapsedMilliseconds(() => Fibonacci(12345678901234567L), 100));
        }
    }
}
