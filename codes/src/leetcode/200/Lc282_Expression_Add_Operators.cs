
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: backtracking
 * Time(2^n), Space(n)
 */
namespace leetcode
{
    public class Lc282_Expression_Add_Operators
    {
        /*
         * standard backtracking with corner cases:
         * 1. merge
         * 2. zero leading can't be merged
         * 3. integer overflow
         * 4. multiplication
         */
        public IList<string> AddOperators(string num, int target)
        {
            var ret = new List<string>();
            if (string.IsNullOrEmpty(num)) return ret;
            AddOperatorsBt(num, target, 0, "", ret, 0, 0);
            return ret;
        }

        void AddOperatorsBt(string str, long target, int pos, string path, IList<string> res, long value, long mul)
        {
            if (pos == str.Length)
            {
                if (value == target) res.Add(path);
                return;
            }

            for (int i = pos; i < str.Length; i++)
            {
                if (i != pos && str[pos] == '0') break;
                long cur = long.Parse(str.Substring(pos, i - pos + 1)); // merge
                if (pos == 0)
                {
                    AddOperatorsBt(str, target, i + 1, path + cur, res, cur, cur);
                }
                else
                {
                    AddOperatorsBt(str, target, i + 1, path + "+" + cur, res, value + cur, cur);
                    AddOperatorsBt(str, target, i + 1, path + "-" + cur, res, value - cur, -cur);
                    AddOperatorsBt(str, target, i + 1, path + "*" + cur, res, value - mul + cur * mul, cur * mul);
                }
            }
        }

        public void Test()
        {
            var exp = new string[] { "1+2+3", "1*2*3" };
            Console.WriteLine(exp.SameSet(AddOperators("123", 6)));

            exp = new string[] { "2*3+2", "2+3*2" };
            Console.WriteLine(exp.SameSet(AddOperators("232", 8)));

            exp = new string[] { "1*0+5", "10-5" };
            Console.WriteLine(exp.SameSet(AddOperators("105", 5)));

            exp = new string[] { "0+0", "0-0", "0*0" };
            Console.WriteLine(exp.SameSet(AddOperators("00", 0)));

            exp = new string[] { };
            Console.WriteLine(exp.SameSet(AddOperators("3456237490", 9191)));
            Console.WriteLine(exp.SameSet(AddOperators("2147483648", -2147483648)));
        }
    }
}

