using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: denary tree (each node has 10 children)
 * Time(logn), Space(1)
 * the problem is to find the kth node in preorder of the tree. 
 * To avoid traverse k nodes fully, we can move the curr node to its right sibling directly by calculating the count of descendant of curr node.
 */
namespace leetcode
{
    public class Lc440_Kth_Smallest_in_Lexicographical_Order
    {
        public int FindKthNumber(int n, int k)
        {
            int curr = 1;
            k--;
            while (k > 0)
            {
                int steps = GetSteps(n, curr, curr + 1);
                if (steps <= k)
                {
                    curr++;
                    k -= steps;
                }
                else
                {
                    curr *= 10;
                    k--;
                }
            }

            return curr;
        }

        // steps from n1 to n2. use long to avoid overflow
        int GetSteps(long n, long n1, long n2)
        {
            if (n1 > n) return 0;
            return (int)(Math.Min(n + 1, n2) - n1 + GetSteps(n, n1 * 10, n2 * 10));
        }


        public void Test()
        {
            // The lexicographical order is [1, 10, 11, 12, 13, 2, 3, 4, 5, 6, 7, 8, 9], so the second smallest number is 10.
            Console.WriteLine(FindKthNumber(13, 2) == 10);
            Console.WriteLine(FindKthNumber(1, 1) == 1);
            Console.WriteLine(FindKthNumber(1234, 113) == 11);
        }
    }
}

