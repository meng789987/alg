
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: dp
 * Time(nlogn), Space(1)
 * dp[i] = dp[i-1] or dp[i-1]*9 if s[i]=='*'
 *       + dp[i-2] if 11<=s[i-1..i]<=26
 */
namespace leetcode
{
    public class Lc639_Decode_WaysII
    {
        // '*' can be digit 1 to 9, not 0.
        public int NumDecodings(string s)
        {
            const int MOD = 1000000007;
            if (string.IsNullOrEmpty(s) || s[0] == '0') return 0;
            long res = s[0] == '*' ? 9 : 1, prev = 1;
            for (int i = 1; i < s.Length; i++)
            {
                var tmp = res;
                if (s[i] == '*')
                {
                    res *= 9;
                    if (s[i - 1] == '*') res += prev * 15;
                    else if (s[i - 1] == '1') res += prev * 9;
                    else if (s[i - 1] == '2') res += prev * 6;
                }
                else
                {
                    int d = s[i] - '0';
                    if (d == 0) // s[i-1..i] must form together
                    {
                        if (s[i - 1] != '*' && s[i - 1] != '1' && s[i - 1] != '2') return 0;
                        res = prev + (s[i - 1] == '*' ? prev : 0);
                    }
                    else if (s[i - 1] == '*') res += prev + (d <= 6 ? prev : 0);
                    else if (s[i - 1] == '1') res += prev;
                    else if (s[i - 1] == '2') res += (d <= 6 ? prev : 0);
                }

                prev = tmp;
                res %= MOD;
            }

            return (int)res;
        }

        public void Test()
        {
            Console.WriteLine(NumDecodings("*") == 9);
            Console.WriteLine(NumDecodings("1*") == 18);
            Console.WriteLine(NumDecodings("*1*1*0") == 404);
            Console.WriteLine(NumDecodings("1003") == 0);
        }
    }
}
