using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: dp
 * Time(mn), Space(mn)
 * dp[i, j] is the length of LCS for a[0..i-1] and b[0..j-1]
 * dp[i, j] = dp[i-1, j-1] + 1, if a[i-1]==b[j-1]
 *         or max(dp[i-1, j], dp[i, j-1])
 * base case: dp[0, j]=dp[i, 0]=0, i=[0..m], j=[0..n]
 * This can be optimized for character set, see diff.exe
 */
namespace alg.dp
{
    public class LongestCommonSubsequence
    {
        int Lcs(string a, string b)
        {
            var dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; i++)
                {
                    if (a[i] == b[j]) dp[i + 1, j + 1] = dp[i, j] + 1;
                    else dp[i + 1, j + 1] = Math.Max(dp[i, j + 1], dp[i + 1, j]);
                }
            }

            return dp[a.Length, b.Length];
        }

        string LcsPath(string a, string b)
        {
            // same with Lcs()
            var dp = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; i++)
                {
                    if (a[i] == b[j]) dp[i + 1, j + 1] = dp[i, j] + 1;
                    else dp[i + 1, j + 1] = Math.Max(dp[i, j + 1], dp[i + 1, j]);
                }
            }

            // construct the path independently
            var cs = new char[dp[a.Length, b.Length]];
            for (int i = a.Length - 1, j = b.Length - 1, k = cs.Length - 1; k >= 0;)
            {
                if (a[i] == b[j])
                {
                    cs[k] = a[i];
                    i--; j--; k--;
                }
                else if (dp[i, j + 1] > dp[i + 1, j])
                    i--;
                else j--;
            }

            return new string(cs);
        }

        public void Test()
        {
            Console.WriteLine(Lcs("AGGTAB", "GXTXAYB") == 4);
            Console.WriteLine(LcsPath("AGGTAB", "GXTXAYB") == "GTAB");
        }
    }
}
