using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: prefix tree
 * Insert/Search: Time(n), Space(n)
 */
namespace alg.tree
{
    public class PrefixTree
    {
        public PrefixTree()
        {
            _root = new Node();
        }

        public void Insert(string word)
        {
            if (string.IsNullOrEmpty(word)) return;
            var node = _root;
            foreach (var c in word)
            {
                int ci = c - 'a';
                if (node.Children[ci] == null)
                    node.Children[ci] = new Node();
                node = node.Children[ci];
            }
            node.IsLeaf = true;
        }


        public bool Search(string word)
        {
            var node = SearchPrefix(word);
            return node != null && node.IsLeaf;
        }

        public bool StartsWith(string word)
        {
            var node = SearchPrefix(word);
            return node != null;
        }

        Node SearchPrefix(string word)
        {
            if (string.IsNullOrEmpty(word)) return null;
            var node = _root;
            foreach (var w in word)
            {
                int ci = w - 'a';
                if (node.Children[ci] == null) return null;
                node = node.Children[ci];
            }
            return node;
        }

        private Node _root;

        class Node
        {
            public Node[] Children = new Node[26];
            public bool IsLeaf;
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

            trie = new PrefixTree();
            trie.Insert("app");
            trie.Insert("apple");
            trie.Insert("beer");
            trie.Insert("add");
            trie.Insert("jam");
            trie.Insert("rental");
            Console.WriteLine(trie.Search("apps") == false);
            Console.WriteLine(trie.Search("app") == true);
        }
    }
}

