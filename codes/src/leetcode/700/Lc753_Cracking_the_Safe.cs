using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bt, dfs
 * Time(k^n), Space(k^n)
 */
namespace leetcode
{
    public class Lc753_Cracking_the_Safe
    {
        public string CrackSafe(int n, int k)
        {
            int len = (int)Math.Pow(k, n) + n - 1;
            var res = new char[len];
            Array.Fill(res, '0', 0, n);
            var seen = new HashSet<string>(len);
            seen.Add(new string(res, 0, n));
            Dfs(n, k, res, n, seen);
            return new string(res);
        }

        bool Dfs(int n, int k, char[] res, int idx, HashSet<string> seen)
        {
            if (idx >= res.Length) return true;

            for (int i = 0; i < k; i++)
            {
                res[idx] = (char)(i + '0');
                var s = new string(res, idx - n + 1, n);
                if (seen.Add(s))
                {
                    if (Dfs(n, k, res, idx + 1, seen)) return true;
                    seen.Remove(s);
                }
            }

            return false;
        }

        public void Test()
        {
            Console.WriteLine(CrackSafe(1, 2) == "01");
            Console.WriteLine(CrackSafe(2, 2) == "00110");
        }
    }
}

