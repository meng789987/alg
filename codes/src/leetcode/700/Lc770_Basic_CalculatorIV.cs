
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: stack
 */
namespace leetcode
{
    public class Lc770_Basic_CalculatorIV
    {
        // The expression string may contain '+', '-', '*', '(', ')', digits and spaces and symbols.
        public IList<string> BasicCalculatorIV(string expression, string[] evalvars, int[] evalints)
        {
            var vars = new Dictionary<string, int>();
            for (int i = 0; i < evalvars.Length; i++)
                vars.Add(evalvars[i], evalints[i]);

            var exps = expression.Replace("(", " ( ").Replace(")", " ) ").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int pos = 0;
            var list = Expand(exps, ref pos, vars, new HashSet<string>());
            list = Merge(list);

            return list.Select(t => t.C + (t.Vars.Count == 0 ? "" : "*" + string.Join("*", t.Vars))).ToList();
        }

        // expand the parenthese '()' and multiplication '*'
        List<Term> Expand(string[] exps, ref int pos, Dictionary<string, int> vars, HashSet<string> syms)
        {
            var ret = new List<Term>();
            var last = GetTerms(exps, ref pos, vars, syms);
            while (pos < exps.Length && exps[pos] != ")")
            {
                char op = exps[pos][0];
                pos++;
                var t = GetTerms(exps, ref pos, vars, syms);
                if (op == '+')
                {
                    ret.AddRange(last);
                    last = t;
                }
                else if (op == '-')
                {
                    ret.AddRange(last);
                    last = Mul(new List<Term> { new Term(-1, null) }, t);
                }
                else // '*'
                {
                    last = Mul(last, t);
                }
            }

            if (last != null) ret.AddRange(last);
            return ret;
        }

        List<Term> GetTerms(string[] exps, ref int pos, Dictionary<string, int> vars, HashSet<string> syms)
        {
            if (exps[pos][0] == '(')
            {
                pos++; // skip '('
                var t = Expand(exps, ref pos, vars, syms);
                pos++; // skip ')'
                return t;
            }

            var ret = new List<Term>();
            if (char.IsDigit(exps[pos][0]))
            {
                ret.Add(new Term(int.Parse(exps[pos]), null));
            }
            else if (char.IsLower(exps[pos][0]))
            {
                if (vars.ContainsKey(exps[pos]))
                    ret.Add(new Term(vars[exps[pos]], null));
                else
                {
                    syms.Add(exps[pos]);
                    ret.Add(new Term(1, exps[pos]));
                }
            }
            else // op
            {
                ret.Add(new Term(exps[pos][0]));
            }

            pos++;
            return ret;
        }

        List<Term> Merge(List<Term> list)
        {
            var comp = Comparer<Term>.Create((a, b) =>
            {
                if (a.Vars.Count != b.Vars.Count)
                    return b.Vars.Count.CompareTo(a.Vars.Count);
                for (int i = 0; i < a.Vars.Count; i++)
                {
                    int c = a.Vars[i].CompareTo(b.Vars[i]);
                    if (c != 0) return c;
                }

                return 0;
            });


            foreach (var t in list)
                t.Vars.Sort();
            list.Sort(comp);

            var ret = new List<Term>();
            var last = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (comp.Compare(last, list[i]) == 0)
                    last.C += list[i].C;
                else
                {
                    if (last.C != 0) ret.Add(last);
                    last = list[i];
                }
            }

            if (last.C != 0) ret.Add(last);
            return ret;
        }

        List<Term> Mul(List<Term> lista, List<Term> listb)
        {
            var ret = new List<Term>();
            foreach (var a in lista)
            {
                foreach (var b in listb)
                {
                    var t = new Term(a.C * b.C, null);
                    if (a.Vars != null) t.Vars.AddRange(a.Vars);
                    if (b.Vars != null) t.Vars.AddRange(b.Vars);
                    ret.Add(t);
                }
            }

            return ret;
        }

        class Term
        {
            public int C; // coefficient
            public List<string> Vars; // list of variable
            public char Op; // non-zero('+', '-', '*', '(', ')') means this term is an operator
            public Term(char op) { Op = op; }
            public Term(int c, string var)
            {
                C = c;
                Vars = new List<string>();
                if (var != null) Vars.Add(var);
            }
        }

        public void Test()
        {
            var input = "e + 8 - a + 5";
            var exp = new List<string> { "-1*a", "14" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { "e" }, new int[] { 1 })));

            input = "e - 8 + temperature - pressure";
            exp = new List<string> { "-1*pressure", "5" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { "e", "temperature" }, new int[] { 1, 12 })));

            input = "(e + 8) * (e - 8)";
            exp = new List<string> { "1*e*e", "-64" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { }, new int[] { })));

            input = "7 - 7";
            exp = new List<string> { };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { }, new int[] { })));

            input = "a * b * c + b * a * c * 4";
            exp = new List<string> { "5*a*b*c" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { }, new int[] { })));

            input = "((a - b) * (b - c) + (c - a)) * ((a - b) + (b - c) * (c - a))";
            exp = new List<string> { "-1*a*a*b*b", "2*a*a*b*c", "-1*a*a*c*c", "1*a*b*b*b", "-1*a*b*b*c", "-1*a*b*c*c", "1*a*c*c*c", "-1*b*b*b*c", "2*b*b*c*c", "-1*b*c*c*c", "2*a*a*b", "-2*a*a*c", "-2*a*b*b", "2*a*c*c", "1*b*b*b", "-1*b*b*c", "1*b*c*c", "-1*c*c*c", "-1*a*a", "1*a*b", "1*a*c", "-1*b*c" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { }, new int[] { })));

            input = "(a + 1) * (b + 1) * (c + 1) * (d + 1) * (e + 1) * (f + 1) * (g + 1) * (h + 1) * (i + 1) * (j + 1) * (k + 1) * (l + 1) * (m + 1) * (n + 1) * (o + 1) * (p + 1) * (q + 1) * (r + 1) * (s + 1) * (t + 1) * (u + 1) * (v + 1) * (w + 1) * (x + 1) * (y + 1) * (z + 1)";
            exp = new List<string> { "-1*a*a*b*b", "2*a*a*b*c", "-1*a*a*c*c", "1*a*b*b*b", "-1*a*b*b*c", "-1*a*b*c*c", "1*a*c*c*c", "-1*b*b*b*c", "2*b*b*c*c", "-1*b*c*c*c", "2*a*a*b", "-2*a*a*c", "-2*a*b*b", "2*a*c*c", "1*b*b*b", "-1*b*b*c", "1*b*c*c", "-1*c*c*c", "-1*a*a", "1*a*b", "1*a*c", "-1*b*c" };
            Console.WriteLine(exp.SequenceEqual(BasicCalculatorIV(input, new string[] { }, new int[] { })));
        }
    }
}

