using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: reservoir sampling
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc382_Linked_List_Random_Node
    {
        public Lc382_Linked_List_Random_Node() { }
        public Lc382_Linked_List_Random_Node(ListNode head)
        {
            _head = head;
            _rand = new Random();
        }

        // Returns a random node's value.
        public int GetRandom()
        {
            int r = _head.val;
            int k = 1; // testing size
            for (ListNode p = _head; p != null; p = p.next)
            {
                if (_rand.Next(k++) == 0) r = p.val;
            }
            return r;
        }

        private ListNode _head;
        private Random _rand;

        public void Test()
        {
            var head = new ListNode(1);
            head.next = new ListNode(2);
            //head.next.next = new ListNode(3);
            //head.next.next.next = new ListNode(4);
            var g = new Lc382_Linked_List_Random_Node(head);
            for (int i = 0; i < 1000; i++)
                Console.Write(g.GetRandom() + ",");
        }
    }
}

