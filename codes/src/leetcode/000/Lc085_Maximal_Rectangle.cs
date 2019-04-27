using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack, largest rectangle given histograms
 * Time(mn), Space(n)
 * the max length of the top bar is from the index of 2nd item in the stack
 */
namespace leetcode
{
    public class Lc85_Maximal_Rectangle
    {
        public int MaximalRectangle(char[,] matrix)
        {
            int ret = 0;
            var hist = new int[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == '0') hist[j] = 0;
                    else hist[j]++;
                }
                ret = Math.Max(ret, LargestRectangleArea(hist));
            }

            return ret;
        }

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
            var nums = new char[,]
            {
                {'1','0','1','0','0'},
                {'1','0','1','1','1'},
                {'1','1','1','1','1'},
                {'1','0','0','1','0'}
            };
            Console.WriteLine(MaximalRectangle(nums) == 6);
        }
    }
}

