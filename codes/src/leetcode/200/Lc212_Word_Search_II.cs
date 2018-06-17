
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
            var root = new Node();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    BuildTrie(board, i, j, root);
            }

            var wordset = new HashSet<string>(words);
            foreach (var word in wordset)
            {
                if (Find(root, word)) ret.Add(word);
            }

            return ret;
        }

        private void BuildTrie(char[,] board, int i, int j, Node node)
        {
            if (i < 0 || i >= board.GetLength(0)
                || j < 0 || j >= board.GetLength(1)
                || board[i, j] < 'a') return;
            int ci = board[i, j] - 'a';
            if (node.Children[ci] == null)
                node.Children[ci] = new Node();
            node = node.Children[ci];
            board[i, j] = (char)(board[i, j] - 26);
            BuildTrie(board, i - 1, j, node);
            BuildTrie(board, i + 1, j, node);
            BuildTrie(board, i, j - 1, node);
            BuildTrie(board, i, j + 1, node);
            board[i, j] = (char)(board[i, j] + 26);
        }

        private bool Find(Node node, string word)
        {
            foreach (var c in word)
            {
                if (node.Children[c - 'a'] == null) return false;
                node = node.Children[c - 'a'];
            }
            return true;
        }
        
        private class Node
        {
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
        }
    }
}

