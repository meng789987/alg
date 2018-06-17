
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: dfs, prefix trie
 * trie: Time(nk + q(n+k)), Space(nk)
 */
namespace leetcode
{
    public class Lc212_Word_Search_II
    {
        //    public IList<string> FindWords(char[,] board, string[] words)
        //    {
        //        var ret = new List<string>();
        //        var root = new Node();
        //        for (int i = 0; i < board.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < board.GetLength(1); j++)
        //                BuildTrie(board, i, j, root);
        //        }

        //        _root.Weight = words.Length - 1;
        //        for (int weight = 0; weight < words.Length; weight++)
        //        {
        //            var word = words[weight] + '{';
        //            for (int i = 0; i < word.Length; i++)
        //            {
        //                var node = _root;
        //                for (int j = i; j < 2 * word.Length - 1; j++)
        //                {
        //                    int ci = word[j % word.Length] - 'a';
        //                    if (node.Children[ci] == null)
        //                        node.Children[ci] = new Node();
        //                    node = node.Children[ci];
        //                    node.Weight = weight;
        //                }
        //            }
        //        }
        //    }

        //    public int F(string prefix, string suffix)
        //    {
        //        var node = _root;
        //        var word = suffix + '{' + prefix;
        //        foreach (var c in word)
        //        {
        //            if (node.Children[c - 'a'] == null) return -1;
        //            node = node.Children[c - 'a'];
        //        }
        //        return node.Weight;
        //    }

        //    private Node _root;
        //    private class Node
        //    {
        //        public Node[] Children = new Node[26];
        //    }

        //    public void Test()
        //    {
        //        var tree = this;
        //        WordFilter(new string[] { "apple", "baaabbabbb" });
        //        Console.WriteLine(tree.F("a", "e") == 0);
        //        Console.WriteLine(tree.F("bf", "") == -1);
        //        Console.WriteLine(tree.F("", "le") == 0);
        //        Console.WriteLine(tree.F("", "") == 1);
        //        Console.WriteLine(tree.F("baaabba", "b") == 1);
        //    }
    }
}

