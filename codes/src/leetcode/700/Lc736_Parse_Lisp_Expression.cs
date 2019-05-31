using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack
 * Time(n^2), Space(n^2)
 * An expression is either an integer, a let-expression, an add-expression, a mult-expression, or an assigned variable. 
 */
namespace leetcode
{
    public class Lc736_Parse_Lisp_Expression
    {
        public int Evaluate(string expression)
        {
            var exps = expression.Replace("(", " ( ").Replace(")", " ) ").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int pos = 0;
            return Eval(exps, ref pos, new Dictionary<string, int>());
        }

        int Eval(string[] exps, ref int pos, Dictionary<string, int> dict)
        {
            var op = exps[pos];
            if (op == "(") // extra parentheses
                return EvalOneValue(exps, ref pos, dict);

            if (op == "let")
            {
                var copy = new Dictionary<string, int>(dict);
                for (pos++; pos < exps.Length; pos++)
                {
                    if (pos + 1 == exps.Length || exps[pos + 1] == ")" || exps[pos] == "(") // last value of let-exp
                        return EvalOneValue(exps, ref pos, copy);

                    string name = exps[pos++];
                    copy[name] = EvalOneValue(exps, ref pos, copy);
                }
            }

            if (op == "add" || op == "mult")
            {
                pos++;
                int rand1 = EvalOneValue(exps, ref pos, dict);
                pos++;
                int rand2 = EvalOneValue(exps, ref pos, dict);

                return op == "add" ? rand1 + rand2 : rand1 * rand2;
            }

            return 0;
        }

        int EvalOneValue(string[] exps, ref int pos, Dictionary<string, int> dict)
        {
            int value = 0;
            if (exps[pos] == "(")
            {
                pos++; // skip "("
                value = Eval(exps, ref pos, dict);
                pos++; // skip ")"
            }
            else if (char.IsLetter(exps[pos][0]))
            {
                value = dict[exps[pos]];
            }
            else
            {
                value = int.Parse(exps[pos]);
            }

            return value;
        }

        public void Test()
        {
            Console.WriteLine(Evaluate("(add 1 2)") == 3);
            Console.WriteLine(Evaluate("(mult 3 (add 2 3))") == 15);
            Console.WriteLine(Evaluate("(let x 2 (mult x 5))") == 10);
            Console.WriteLine(Evaluate("(let x 2 (mult x (let x 3 y 4 (add x y))))") == 14);
            Console.WriteLine(Evaluate("(let x 3 x 2 x)") == 2);
            Console.WriteLine(Evaluate("(let x 1 y 2 x (add x y) (add x y))") == 5);
            Console.WriteLine(Evaluate("(let x 2 (add (let x 3 (let x 4 x)) x))") == 6);
            Console.WriteLine(Evaluate("(let a1 3 b2 (add a1 1) b2)") == 4);
            Console.WriteLine(Evaluate("(let x 7 -12)") == -12);
        }
    }
}
