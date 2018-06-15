
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: prefix trie
 * two prefix trie:  Time(nk + q(n+k)), Space(nk)
 * one wrapped trie: Time(nk^2 + qk), Space(nk^2)
 */
namespace leetcode
{
    public class Lc745_Prefix_and_Suffix_Search
    {
        /*
         * wrap the suffix to prefix of the word, e.g. for "apple", those words will be added to trie:
         * "apple#apple", "pple#apple", "ple#apple", "le#apple", "e#apple", "#apple".
         * '{' is used actually because it just follow 'z' in ASCII table.
         */
        public void WordFilter(string[] words)
        {
            _root = new Node();
            _root.Weight = words.Length - 1;
            for (int weight = 0; weight < words.Length; weight++)
            {
                var word = words[weight] + '{';
                for (int i = 0; i < word.Length; i++)
                {
                    var node = _root;
                    for (int j = i; j < 2 * word.Length - 1; j++)
                    {
                        int ci = word[j % word.Length] - 'a';
                        if (node.Children[ci] == null)
                            node.Children[ci] = new Node();
                        node = node.Children[ci];
                        node.Weight = weight;
                    }
                }
            }
        }

        public int F(string prefix, string suffix)
        {
            var node = _root;
            var word = suffix + '{' + prefix;
            foreach (var c in word)
            {
                if (node.Children[c - 'a'] == null) return -1;
                node = node.Children[c - 'a'];
            }
            return node.Weight;
        }

        private Node _root;
        private class Node
        {
            public int Weight;
            public Node[] Children = new Node[27];
        }

        public void Test()
        {
            var tree = this;
            WordFilter(new string[] { "apple", "baaabbabbb" });
            Console.WriteLine(tree.F("a", "e") == 0);
            Console.WriteLine(tree.F("bf", "") == -1);
            Console.WriteLine(tree.F("", "le") == 0);
            Console.WriteLine(tree.F("", "") == 1);
            Console.WriteLine(tree.F("baaabba", "b") == 1);
        }
    }
}

