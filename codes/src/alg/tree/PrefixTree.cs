using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: counting sort, radix sort
 * Time(n), Space(n)
 */
namespace alg.tree
{
    public class PrefixTree
    {
        public PrefixTree()
        {
            _root = new Node("");
        }

        public void Insert(string word)
        {
            if (string.IsNullOrEmpty(word)) return;
            InsertRc(_root, word);
        }

        void InsertRc(Node root, string word)
        {
            int ci = word[0] - 'a';
            if (root.Children[ci] == null)
            {
                root.Children[ci] = new Node(word);
                root.Children[ci].IsLeaf = true;
                return;
            }

            var node = root.Children[ci];
            int i = 0;
            while (i < node.Value.Length && i < word.Length && node.Value[i] == word[i]) i++;

            if (i < node.Value.Length) // split
            {
                root.Children[ci] = new Node(word);
                root.Children[ci].IsLeaf = true;
                root.Children[ci].Children[node.Value[i] - 'a'] = node;
                node.Value = node.Value.Substring(0, i);
            }
            else if (i == word.Length) // same length
            {
                node.IsLeaf = true;
            }
            else
            {
                InsertRc(node, word.Substring(i));
            }
        }


        public bool Search(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            return SearchRc(_root, word, true);
        }

        bool SearchRc(Node root, string word, bool leafOnly)
        {
            int ci = word[0] - 'a';
            if (root.Children[ci] == null) return false;

            var node = root.Children[ci];
            int i = 0;
            while (i < node.Value.Length && i < word.Length && node.Value[i] == word[i]) i++;

            if (i < word.Length) return SearchRc(node, word.Substring(i), leafOnly);
            return !leafOnly || (i == node.Value.Length && node.IsLeaf);
        }

        public bool StartsWith(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            return SearchRc(_root, word, false);
        }

        private Node _root;

        class Node
        {
            public string Value;
            public Node[] Children = new Node[26];
            public bool IsLeaf;
            public Node(string value) { Value = value; }
        }

        public void Test()
        {
            var trie = new PrefixTree();
            trie.Insert("apple");
            Console.WriteLine(trie.Search("apple") == true);
            Console.WriteLine(trie.Search("app") == false);
            Console.WriteLine(trie.StartsWith("app") == true);
            trie.Insert("app");
            Console.WriteLine(trie.Search("app") == true);
        }
    }
}

