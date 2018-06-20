
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: stack
 */
namespace leetcode
{
    public class Lc224_Basic_Calculator
    {
        // The expression string may contain '+', '-', '(', ')', digits and spaces.
        public int Calculate(string s)
        {
            int ret = 0;
            int sign = 1;
            var stack = new Stack<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    int val = s[i] - '0';
                    for (; i + 1 < s.Length && char.IsDigit(s[i + 1]); i++)
                        val = val * 10 + (s[i + 1] - '0');
                    ret += val * sign;
                }
                else if (s[i] == '+')
                {
                    sign = 1;
                }
                else if (s[i] == '-')
                {
                    sign = -1;
                }
                else if (s[i] == '(')
                {
                    stack.Push(ret);
                    stack.Push(sign);
                    ret = 0; // reset
                    sign = 1;
                }
                else if (s[i] == ')')
                {
                    ret *= stack.Pop(); // sign
                    ret += stack.Pop();
                }
            }

            return ret;
        }

        // The expression string may contain '+', '-', '*', '/', digits and spaces.
        public int CalculateII(string s)
        {
            int ret = 0, num = 0;
            char op = '+';

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    i--;
                    int val = GetNumber(s, ref i);
                    num = op == '+' ? val : -val;
                }
                else if (s[i] == '+' || s[i] == '-')
                {
                    ret += num;
                    op = s[i];
                }
                else if (s[i] == '*')
                {
                    int val = GetNumber(s, ref i);
                    num *= val;
                }
                else if (s[i] == '/')
                {
                    int val = GetNumber(s, ref i);
                    num /= val;
                }
            }

            return ret + num;
        }

        // The expression string may contain '+', '-', '*', '/', digits and spaces.
        public int Calculate2(string s)
        {
            s = '+' + s;
            var stack = new Stack<int>();

            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '+':
                        stack.Push(GetNumber(s, ref i));
                        break;

                    case '-':
                        stack.Push(-GetNumber(s, ref i));
                        break;

                    case '*':
                        stack.Push(stack.Pop() * GetNumber(s, ref i));
                        break;

                    case '/':
                        stack.Push(stack.Pop() / GetNumber(s, ref i));
                        break;
                }
            }

            int ret = 0;
            while (stack.Count > 0) ret += stack.Pop();

            return ret;
        }

        int GetNumber(string s, ref int pos)
        {
            int ret = 0;
            pos++;
            while (pos < s.Length && s[pos] == ' ') pos++;
            while (pos < s.Length && char.IsDigit(s[pos]))
                ret = ret * 10 + (s[pos++] - '0');
            pos--;
            return ret;
        }

        public void Test()
        {
            Console.WriteLine(Calculate("1 + 1") == 2);
            Console.WriteLine(Calculate(" 2-1 + 2 ") == 3);
            Console.WriteLine(Calculate("(1+(4+5+2)-3)+(6+8)") == 23);

            Console.WriteLine(CalculateII("3+2*2 ") == 7);
            Console.WriteLine(CalculateII("3/2") == 1);
            Console.WriteLine(CalculateII(" 3+5 / 2 ") == 5);

            Console.WriteLine(Calculate2("3+2*2 ") == 7);
            Console.WriteLine(Calculate2("3/2") == 1);
            Console.WriteLine(Calculate2(" 3+5 / 2 ") == 5);
        }
    }
}

