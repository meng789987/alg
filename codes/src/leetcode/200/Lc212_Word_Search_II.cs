
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: dfs, prefix trie
 * trie: Time(nk + qk), Space(nk)
 */
namespace leetcode
{
    public class Lc212_Word_Search_II
    {
        public IList<string> FindWords(char[,] board, string[] words)
        {
            var ret = new List<string>();
            var root = BuildTrie(words);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    Dfs(board, i, j, root, ret);
            }

            return ret;
        }

        private Node BuildTrie(string[] words)
        {
            var root = new Node();
            foreach (var word in words)
            {
                var node = root;
                foreach (var c in word)
                {
                    int ci = c - 'a';
                    if (node.Children[ci] == null)
                        node.Children[ci] = new Node();
                    node = node.Children[ci];
                }
                node.Word = word;
            }
            return root;
        }

        private void Dfs(char[,] board, int i, int j, Node node, IList<string> res)
        {
            char c = board[i, j];
            if (c == '#' || node.Children[c - 'a'] == null) return;
            node = node.Children[c - 'a'];
            if (node.Word != null)
            {
                res.Add(node.Word);
                node.Word = null; // remove dup
            }

            board[i, j] = '#';
            if (i > 0) Dfs(board, i - 1, j, node, res);
            if (j > 0) Dfs(board, i, j - 1, node, res);
            if (i < board.GetLength(0) - 1) Dfs(board, i + 1, j, node, res);
            if (j < board.GetLength(1) - 1) Dfs(board, i, j + 1, node, res);
            board[i, j] = c;
        }

        private class Node
        {
            public string Word;
            public Node[] Children = new Node[26];
        }

        public void Test()
        {
            var board = new char[,]
            {
                {'o','a','a','n'},
                {'e','t','a','e'},
                {'i','h','k','r'},
                {'i','f','l','v'} };
            var words = new string[] { "oath", "pea", "eat", "rain" };
            var exp = new string[] { "eat", "oath" };
            Console.WriteLine(exp.SameSet(FindWords(board, words)));

            board = new char[,] { {'a'} };
            words = new string[] { "a", "a" };
            exp = new string[] { "a" };
            Console.WriteLine(exp.SameSet(FindWords(board, words)));
        }
    }
}

