using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: dp, stack, two pointers
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc32_Longest_Valid_Parentheses
    {
        /*
         * dp[i] is the longest valid parentheses ending at i
         * dp[i] = 0, if s[i] == '(', else
         *       = 2 + dp[i-2], if s[i-1] == '(', else
         *       = dp[i-1] + 2 + dp[i-2-dp[i-1]], if s[i-1-dp[i-1]] == '('
         */
        public int LongestValidParenthesesDp(string s)
        {
            int res = 0;
            var dp = new int[s.Length + 1];
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '(') continue;
                if (s[i - 1] == '(')
                    dp[i + 1] = 2 + dp[i - 1];
                else if (i - 1 - dp[i] >= 0 && s[i - 1 - dp[i]] == '(')
                    dp[i + 1] = dp[i] + 2 + dp[i - 1 - dp[i]];

                res = Math.Max(res, dp[i + 1]);
            }

            return res;
        }

        public int LongestValidParenthesesStack(string s)
        {
            int res = 0;
            var stack = new Stack<int>();
            stack.Push(-1);

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') stack.Push(i);
                else
                {
                    stack.Pop();
                    if (stack.Count == 0) stack.Push(i);
                    else res = Math.Max(res, i - stack.Peek());
                }
            }

            return res;
        }

        public int LongestValidParentheses2Pointers(string s)
        {
            int res = 0;

            // scan from left to right
            int left = 0, right = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') left++; else right++;
                if (left == right) res = Math.Max(res, 2 * left);
                if (left < right) left = right = 0;
            }

            // scan from right to left
            left = right = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '(') left++; else right++;
                if (left == right) res = Math.Max(res, 2 * left);
                if (left > right) left = right = 0;
            }

            return res;
        }

        public void Test()
        {
            Console.WriteLine(LongestValidParenthesesDp("(()") == 2);
            Console.WriteLine(LongestValidParenthesesStack("(()") == 2);
            Console.WriteLine(LongestValidParentheses2Pointers("(()") == 2);

            Console.WriteLine(LongestValidParenthesesDp(")()())") == 4);
            Console.WriteLine(LongestValidParenthesesStack(")()())") == 4);
            Console.WriteLine(LongestValidParentheses2Pointers(")()())") == 4);

        }
    }
}

