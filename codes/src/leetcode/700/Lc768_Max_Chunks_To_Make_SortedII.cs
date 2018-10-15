using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: stack
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc768_Max_Chunks_To_Make_SortedII
    {
        public int MaxChunksToSorted(int[] arr)
        {
            var stack = new Stack<int>();
            for (int i = 0, max = -1; i < arr.Length; i++)
            {
                while (stack.Count > 0 && arr[i] < stack.Peek())
                    stack.Pop();
                max = Math.Max(max, arr[i]);
                stack.Push(max);
            }

            return stack.Count;
        }

        public void Test()
        {
            var arr = new int[] { 5, 4, 3, 2, 1 };
            Console.WriteLine(MaxChunksToSorted(arr) == 1);

            arr = new int[] { 2, 1, 3, 4, 4 };
            Console.WriteLine(MaxChunksToSorted(arr) == 4);
        }
    }
}
