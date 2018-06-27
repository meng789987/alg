using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: linked list, two pointers
 * Time(n), Space(1)
 */
namespace leetcode
{
    public class Lc160_Intersection_of_Two_Linked_Lists
    {
        // get their length, then adjust their head
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            int lenA = GetLength(headA);
            int lenB = GetLength(headB);

            // adjust their head
            for (; lenA > lenB; lenA--) headA = headA.next;
            for (; lenB > lenA; lenB--) headB = headB.next;

            while (headA != null && headA != headB)
            {
                headA = headA.next;
                headB = headB.next;
            }

            return headA;
        }

        int GetLength(ListNode head)
        {
            int len = 0;
            for (; head != null; len++) head = head.next;
            return len;
        }

        public ListNode GetIntersectionNode2(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null) return null;
            ListNode a = headA, b = headB;
            while (a != b)
            {
                a = a != null ? a.next : headB;
                b = b != null ? b.next : headA;
            }
            return a;
        }

        public void Test()
        {
            var a = new ListNode(new int[] { 2, 7, 11, 15, 5 });
            var b = new ListNode(new int[] { 4, 22, 55 });
            b.next.next.next = a.next.next;
            Console.WriteLine(GetIntersectionNode(a, b) == a.next.next);
            Console.WriteLine(GetIntersectionNode2(a, b) == a.next.next);
        }
    }
}