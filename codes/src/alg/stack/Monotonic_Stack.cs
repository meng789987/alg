using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack
 * Time(n), Space(n)
 * maintain a stack with increasing heights
 */
namespace stack
{
    public class Largest_Rectangle_in_Histogram
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
            var stack = new Stack<int>();
            for (int i = 0; i <= n; i++)
            {
                // an extra final loop to pop all the stack
                while (stack.Count > 0 && (i == n || heights[i] <= heights[stack.Peek()]))
                {
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
        }
    }
}

