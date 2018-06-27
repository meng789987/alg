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
    public class Lc141_Linked_List_Cycle
    {
        // get their length, then adjust their head
        public bool HasCycle(ListNode head)
        {
            ListNode slow = head, fast = head;

            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
                if (slow == fast) break;
            }

            return fast != null;
        }

        /*
         * return the node where the cycle begins. If there is no cycle, return null.
         * s is the number of nodes before cycle.
         * r is the number of nodes in cycle.
         * m is the number of steps/nodes when slow/fast first meet in the cycle. so
         *     s+m+r = 2(s+m) => s = r-m
         * so when they meet, let a pointer starts from head of B, it will meet the slow at the circle entry point.
         */
        public ListNode DetectCycle(ListNode head)
        {
            ListNode slow = head, fast = head;

            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
                if (slow == fast) break;
            }

            if (fast == null) return null; // no cycle
            fast = head; // reset fast

            while (fast != slow)
            {
                slow = slow.next;
                fast = fast.next;
            }

            return fast;
        }

        /*
         * 287. Find the Duplicate Number
         * Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive). 
         * Assume that there is only one duplicate number, find the duplicate one which might appear more than twice.
         * 
         * If we interpret nums such that for each pair of index i and value vi, the "next" value vj is at index vi, we can reduce this problem to cycle detection.
         * because 0 cannot appear as a value in nums, nums[0] cannot be part of the cycle. 
         */
        public int FindDuplicate(int[] nums)
        {
            int slow = nums[0], fast = nums[0];

            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (slow != fast);

            fast = nums[0]; // reset fast

            while (fast != slow)
            {
                slow = nums[slow];
                fast = nums[fast];
            }

            return fast;
        }

        public void Test()
        {
            var head = new ListNode(new int[] { 2, 7, 11, 15, 5 });
            Console.WriteLine(HasCycle(head) == false);
            Console.WriteLine(DetectCycle(head) == null);

            head.next.next.next.next.next = head.next.next;
            Console.WriteLine(HasCycle(head) == true);
            Console.WriteLine(DetectCycle(head) == head.next.next);

            var nums = new int[] { 1, 3, 4, 2, 2 };
            Console.WriteLine(FindDuplicate(nums) == 2);

            nums = new int[] { 3, 1, 3, 4, 2 };
            Console.WriteLine(FindDuplicate(nums) == 3);

            nums = new int[] { 2, 2, 2, 2, 2 };
            Console.WriteLine(FindDuplicate(nums) == 2);
        }
    }
}
