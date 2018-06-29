using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: backtracking
 */
namespace leetcode
{
    public class Lc301_Remove_Invalid_Parentheses
    {
        public IList<string> RemoveInvalidParentheses(string s)
        {
            var ret = new HashSet<string>();
            var lefts = helper(s);
            foreach (var str in lefts)
            {
                var rights = helper(mirror(str));
                foreach (var ts in rights)
                    ret.Add(mirror(ts));
            }
            return ret.ToList();
        }

        IList<string> helper(string s)
        {
            var res = new List<string> { "" };
            for (int bal = 0, i = 0; i < s.Length; i++)
            {
                if (s[i] == ')' && bal == 0)
                {
                    for (int k = res.Count - 1; k >= 0; k--)
                    {
                        int j = res[k].Length - 1;
                        while (j > 0 && res[k][j] == ')') j--;
                        for (; j > 0; j--)
                        {
                            if (res[k][j] == ')')
                                res.Add(res[k].Substring(0, j) + res[k].Substring(j + 1) + ')');
                        }
                    }
                }
                else
                {
                    if (s[i] == '(') bal++;
                    if (s[i] == ')') bal--;
                    for (int k = 0; k < res.Count; k++)
                        res[k] += s[i];
                }
            }

            return res;
        }

        string mirror(string s)
        {
            var cs = s.ToCharArray();
            for (int i = 0, j = s.Length - 1; i <= j; i++, j--)
            {
                char c = mirror(cs[i]);
                cs[i] = mirror(cs[j]);
                cs[j] = c;
            }

            return new string(cs);
        }

        char mirror(char c)
        {
            if (c == '(') return ')';
            if (c == ')') return '(';
            return c;
        }

        public void Test()
        {
            var exp = new List<string> { "()()()", "(())()" };
            Console.WriteLine(exp.SameSet(RemoveInvalidParentheses("()())()")));

            exp = new List<string> { "(a)()()", "(a())()" };
            Console.WriteLine(exp.SameSet(RemoveInvalidParentheses("(a)())()")));

            exp = new List<string> { "" };
            Console.WriteLine(exp.SameSet(RemoveInvalidParentheses(")(")));

            exp = new List<string> { "r()()", "r(())", "(r)()", "(r())" };
            Console.WriteLine(exp.SameSet(RemoveInvalidParentheses("(r(()()(")));

        }
    }
}

