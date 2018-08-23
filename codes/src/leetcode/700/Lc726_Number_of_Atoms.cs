using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: hash, stack
 * Time(nlogn), Space(n)
 */
namespace leetcode
{
    public class Lc726_Number_of_Atoms
    {
        public string CountOfAtoms(string formula)
        {
            var map = new SortedDictionary<string, int>();
            var stack = new Stack<int>();
            int cur = 1, prod = 1;

            for (int j, i = formula.Length - 1; i >= 0; i = j)
            {
                j = i;

                if (char.IsDigit(formula[j]))
                {
                    while (j >= 0 && char.IsDigit(formula[j])) j--;
                    cur = int.Parse(formula.Substring(j + 1, i - j));
                }
                else if (char.IsLetter(formula[j]))
                {
                    while (j >= 0 && char.IsLower(formula[j])) j--;
                    j--;
                    var atom = formula.Substring(j + 1, i - j);
                    if (map.ContainsKey(atom)) map[atom] += cur * prod;
                    else map[atom] = cur * prod;
                    cur = 1;
                }
                else if (formula[j] == ')')
                {
                    j--;
                    prod *= cur;
                    stack.Push(cur);
                    cur = 1;
                }
                else // '('
                {
                    j--;
                    prod /= stack.Pop();
                    cur = 1;
                }
            }

            return string.Join("", map.Select(p => p.Value == 1 ? p.Key : p.Key + p.Value));
        }

        public void Test()
        {
            Console.WriteLine(CountOfAtoms("H2O") == "H2O");
            Console.WriteLine(CountOfAtoms("Mg(OH)2") == "H2MgO2");
            Console.WriteLine(CountOfAtoms("K4(ON(SO3)2)2") == "K4N2O14S4");
        }
    }
}
