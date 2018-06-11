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

        /*
         * link tail of A to head of A to make a circle, then find the circle entry point in B.
         * s is the number of nodes before circle.
         * r is the number of nodes in circle.
         * m is the number of steps/nodes when slow/fast first meet in the circle.
         *     s+m+r = 2(s+m) => s = r-m
         * so when they meet, let a pointer starts from head of B, it will meet the slow at the circle entry point.
         */
        public ListNode GetIntersectionNode3(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null) return null;

            // link tail of A to head of A to make a circle, then find the circle entry point in B.
            ListNode tail = headA;
            while (tail.next != null) tail = tail.next;
            tail.next = headA;

            ListNode slow = headB, fast = headB;
            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
                if (slow == fast) break;
            }

            if (fast == null)
            {
                tail.next = null;
                return null;
            }

            fast = headB;
            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            tail.next = null;
            return slow;
        }

        public void Test()
        {
            var a = new ListNode(new int[] { 2, 7, 11, 15, 5 });
            var b = new ListNode(new int[] { 4, 22, 55 });
            b.next.next.next = a.next.next;
            Console.WriteLine(GetIntersectionNode(a, b) == a.next.next);
            Console.WriteLine(GetIntersectionNode2(a, b) == a.next.next);
            Console.WriteLine(GetIntersectionNode3(a, b) == a.next.next);
        }
    }
}