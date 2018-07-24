using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: prefix tree, hash
 * Time(nk^2), Space(kn)
 */
namespace leetcode
{
    public class Lc336_Palindrome_Pairs
    {
        public IList<IList<int>> PalindromePairs(string[] words)
        {
            int n = words.Length;
            var map = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
                map[words[i]] = i;

            var res = new List<IList<int>>();
            for (int j, i = 0; i < n; i++)
            {
                for (int len = 0; len <= words[i].Length; len++)
                {
                    var str1 = words[i].Substring(0, len);
                    var str2 = words[i].Substring(len);

                    if (IsPalindrome(str1))
                    {
                        var str2r = new string(str2.ToCharArray().Reverse().ToArray());
                        if (map.TryGetValue(str2r, out j) && i != j)
                            res.Add(new List<int> { j, i });
                    }

                    if (IsPalindrome(str2))
                    {
                        var str1r = new string(str1.ToCharArray().Reverse().ToArray());
                        if (map.TryGetValue(str1r, out j) && i != j && str2 != "") // avoid dup
                            res.Add(new List<int> { i, j });
                    }
                }
            }

            return res;
        }

        bool IsPalindrome(string s)
        {
            for (int i = 0, j = s.Length - 1; i < j; i++, j--)
                if (s[i] != s[j]) return false;
            return true;
        }

        public IList<IList<int>> PalindromePairsTrie(string[] words)
        {

            var res = new List<IList<int>>();
            var trie = new Node();

            for (int i = 0; i < words.Length; i++)
                AddTrie(trie, words, i);

            for (int i = 0; i < words.Length; i++)
            {
                for (int len = 0; len <= words[i].Length; len++)
                {
                    var str1 = words[i].Substring(0, len);
                    var str2 = words[i].Substring(len);

                    if (IsPalindrome(str1))
                    {
                        var str2r = str2.ToCharArray().Reverse().ToArray();
                        int j = GetIndex(trie, str2r);
                        if (j >= 0 && i != j) res.Add(new List<int> { j, i });
                    }

                    if (IsPalindrome(str2))
                    {
                        var str1r = str1.ToCharArray().Reverse().ToArray();
                        int j = GetIndex(trie, str1r);
                        if (j >= 0 && i != j && str2 != "") // avoid dup
                            res.Add(new List<int> { i, j });
                    }
                }
            }

            return res;
        }

        void AddTrie(Node node, string[] words, int index)
        {
            for (int i = 0; i < words[index].Length; i++)
            {
                var ci = words[index][i] - 'a';
                if (node.Children[ci] == null)
                    node.Children[ci] = new Node();
                node = node.Children[ci];
            }
            node.Index = index;
        }

        int GetIndex(Node node, char[] cs)
        {
            for (int i = 0; i < cs.Length && node != null; i++)
            {
                var ci = cs[i] - 'a';
                node = node.Children[ci];
            }
            return node == null ? -1 : node.Index;
        }

        class Node
        {
            public int Index;
            public Node[] Children;

            public Node()
            {
                Index = -1;
                Children = new Node[26];
            }
        }

        public void Test()
        {
            var strings = new string[] { "bat", "tab", "cat" };
            var exp = new List<IList<int>> { new List<int> { 0, 1 }, new List<int> { 1, 0 } };
            Console.WriteLine(exp.SameSet(PalindromePairs(strings)));
            Console.WriteLine(exp.SameSet(PalindromePairsTrie(strings)));

            strings = new string[] { "abcd", "dcba", "lls", "s", "sssll" };
            exp = new List<IList<int>> {
                new List<int> { 0, 1 },
                new List<int> { 1, 0 },
                new List<int> { 3, 2 },
                new List<int> { 2, 4 }};
            Console.WriteLine(exp.SameSet(PalindromePairs(strings)));
            Console.WriteLine(exp.SameSet(PalindromePairsTrie(strings)));
        }
    }
}

