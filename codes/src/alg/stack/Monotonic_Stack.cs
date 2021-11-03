using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack
 * Time(n), Space(n)
 * maintain a stack with an order
 */
namespace alg.stack
{
    public class Monotonic_Stack
    {
        /*
         * Lc84_Largest_Rectangle_in_Histogram:
         * Given n non-negative integers representing the histogram's bar height where the width of each bar is 1, find the area of largest rectangle in the histogram.
         * 
         * maintain a stack with increasing heights
         * whenever a bar which is shorter than the top on stack is coming, we can get the length of area with height is top on stack.
         */
        public int LargestRectangleArea(int[] heights)
        {
            int res = 0, n = heights.Length;
            var stack = new Stack<int>(); // store the indice
            for (int i = 0; i <= n; i++)
            {
                // an extra final loop to pop all the stack
                while (stack.Count > 0 && (i == n || heights[i] <= heights[stack.Peek()]))
                {
                    // bar at stack top is the lowest from index (2nd-stack-top+1) to i-1
                    int val = heights[stack.Pop()];
                    int len = i - (stack.Count == 0 ? 0 : stack.Peek() + 1);
                    res = Math.Max(res, val * len);
                }
                stack.Push(i);
            }

            return res;
        }

        /*
         * Lc85_Maximal_Rectangle:
         * Given a 2D binary matrix filled with 0's and 1's, find the largest rectangle containing only 1's and return its area.
         */
        public int MaximalRectangle(char[][] matrix)
        {
            if (matrix == null || matrix.Length == 0) return 0;
            int res = 0, n = matrix[0].Length;
            var hist = new int[n];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < n; j++)
                    hist[j] = matrix[i][j] == '0' ? 0 : hist[j] + 1;
                res = Math.Max(res, LargestRectangleArea(hist));
            }

            return res;
        }

        /*
         * dp likes the monotonic stack, details see the dp folder
         */ 
        IList<int> LongestIncreasingSubsequence(int[] nums)
        {
            int n = nums.Length;
            int len = 0;
            var dp = new int[n];
            var dpidx = new int[n]; // store the index of the number instead of the number itself to easy reconstructing the path
            var pre = new int[n]; // store the index of predecessor of nums[i] in the LIS ending at nums[i]

            for (int i = 0; i < n; i++)
            {
                int idx = Array.BinarySearch(dp, 0, len, nums[i]);
                if (idx < 0)
                {
                    idx = ~idx;
                    dp[idx] = nums[i];
                    dpidx[idx] = i;
                    if (idx == len) len++;
                }

                if (idx > 0) pre[i] = dpidx[idx - 1];
            }

            // constuct the path from pre backward
            var res = new int[len];
            int ni = dpidx[len - 1];
            for (int k = len - 1; k >= 0; k--)
            {
                res[k] = nums[ni];
                ni = pre[ni];
            }

            return res.ToList();
        }

        public void Test()
        {
            var bars = new int[] { 2, 1, 5, 6, 2, 3 };
            Console.WriteLine(LargestRectangleArea(bars) == 10);

            var matrix = new char[][]
            {
                new char[]{'1','0','1','0','0'},
                new char[]{'1','0','1','1','1'},
                new char[]{'1','1','1','1','1'},
                new char[]{'1','0','0','1','0'}
            };
            Console.WriteLine(MaximalRectangle(matrix) == 6);

            var nums = new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 };
            var path = LongestIncreasingSubsequence(nums);
            Console.WriteLine(path.Count == 6 && path.Select((a, i) => i).All(i => i == 0 || path[i - 1] < path[i]));
        }
    }
}

