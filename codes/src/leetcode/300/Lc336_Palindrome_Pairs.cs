using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: prefix tree
 * Time(kn), Space(kn)
 */
namespace leetcode
{
    public class Lc336_Palindrome_Pairs
    {
        public IList<IList<int>> PalindromePairs(string[] words)
        {
            return null;
        }


        public void Test()
        {
            var strings = new string[] { "bat", "tab", "cat" };
            var exp = new List<IList<int>> { new List<int> { 0, 1 }, new List<int> { 1, 0 } };
            Console.WriteLine(exp.SameSet(PalindromePairs(strings)));

            strings = new string[] { "abcd", "dcba", "lls", "s", "sssll" };
            exp = new List<IList<int>> {
                new List<int> { 0, 1 },
                new List<int> { 1, 0 },
                new List<int> { 3, 2 },
                new List<int> { 2, 4 }};
            Console.WriteLine(exp.SameSet(PalindromePairs(strings)));
        }
    }
}

