using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack
 * Time(n), Space(n)
 * the max length of the top bar is from the index of 2nd item in the stack
 */
namespace leetcode
{
    public class Lc84_Largest_Rectangle_in_Histogram
    {
        public int LargestRectangleArea(int[] heights)
        {
            int ret = 0;
            var stack = new Stack<int>();
            for (int i = 0; i <= heights.Length; i++)
            {
                // an extra final loop (i == heights.Length) to avoid pop the stack after the loop
                while (stack.Count > 0 && (i == heights.Length || heights[stack.Peek()] >= heights[i]))
                {
                    int j = stack.Pop();
                    var area = heights[j] * (stack.Count == 0 ? i : i - stack.Peek() - 1);
                    ret = Math.Max(ret, area);
                }
                stack.Push(i);
            }

            return ret;
        }

        public void Test()
        {
            var nums = new int[] { 2, 1, 5, 6, 2, 3 };
            Console.WriteLine(LargestRectangleArea(nums) == 10);
        }
    }
}

