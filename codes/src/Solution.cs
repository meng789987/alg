using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using leetcode;

namespace alg
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lc391_Perfect_Rectangle:");
            //new leetcode.Lc391_Perfect_Rectangle().Test();
            Test();

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        static void Test()
        {
            var table = new System.Data.DataTable();
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));
            Console.WriteLine(table.Compute("(1+2)*(4+3)", null));

            var (a, b) = (3, 4);
            var ss = new SortedSet<int>(new int[] { 1, 2, 1 });
            Console.WriteLine(ss.Count);

            LFUCache cache = new LFUCache(0 /* capacity */ );

            cache.put(0, 0);
            cache.get(0);
            cache.put(1, 1);
            cache.put(2, 2);
            cache.get(1);       // returns 1
            cache.put(3, 3);    // evicts key 2
            cache.get(2);       // returns -1 (not found)
            cache.get(3);       // returns 3.
            cache.put(4, 4);    // evicts key 1.
            cache.get(1);       // returns -1 (not found)
            cache.get(3);       // returns 3
            cache.get(4);       // returns 4
        }

        public class LFUCache
        {

            public LFUCache(int capacity)
            {
                _capacity = capacity;
                _map = new Dictionary<int, LinkedListNode<Node>>();
                _list = new LinkedList<LinkedList<Node>>();
            }

            public int get(int key)
            {
                LinkedListNode<Node> listNode;
                if (!_map.TryGetValue(key, out listNode)) return -1;
                UpdateFrequency(listNode);
                return listNode.Value.v;
            }

            public void put(int key, int value)
            {
                Console.WriteLine(key + "=" + value);
                LinkedListNode<Node> listNode;
                if (!_map.TryGetValue(key, out listNode))
                {
                    if (_map.Count == _capacity)
                    {
                        _map.Remove(_list.First.Value.First.Value.k);
                        _list.First.Value.RemoveFirst();
                        if (_list.First.Value.Count == 0) _list.RemoveFirst();
                    }

                    if (_list.Count == 0 || _list.First.Value.First.Value.f > 0)
                        _list.AddFirst(new LinkedList<Node>());
                    _list.First.Value.AddLast(new Node(key, value, _list.First));
                    _map[key] = _list.First.Value.Last;
                }
                else
                {
                    listNode.Value.v = value;
                    UpdateFrequency(listNode);
                }
            }

            void UpdateFrequency(LinkedListNode<Node> listNode)
            {
                listNode.Value.f++;
                Console.WriteLine(listNode.Value.k + ":" + listNode.Value.p.List);
                if (listNode.Value.p.Next == null || listNode.Value.p.Next.Value.First.Value.f > listNode.Value.f)
                    listNode.Value.p.List.AddAfter(listNode.Value.p, new LinkedList<Node>());
                var pNextNode = listNode.Value.p.Next;
                if (listNode.List.Count > 1)
                    listNode.List.Remove(listNode);
                else
                    listNode.Value.p.List.Remove(listNode.Value.p);
                pNextNode.Value.AddLast(listNode.Value);
                listNode.Value.p = pNextNode;
                _map[listNode.Value.k] = pNextNode.Value.Last;
            }

            int _capacity;
            Dictionary<int, LinkedListNode<Node>> _map; // key to node
            LinkedList<LinkedList<Node>> _list; // list ordered by frequency of list ordered by timestamp of value

            class Node
            {
                public int k, v, f; // key, value, frequency
                public LinkedListNode<LinkedList<Node>> p; // parent, the outer linked list node

                public Node(int key, int value, LinkedListNode<LinkedList<Node>> parent)
                {
                    this.k = key;
                    this.v = value;
                    this.p = parent;
                }
            }
        }
    }
}
