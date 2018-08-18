
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: hash
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc757_Set_Intersection_Size_At_Least_Two
    {
        public int IntersectionSizeTwo(int[,] intervals)
        {
            int ret = 0;
            var lasts = new int[256];
            for (int start = 0, i = 0; i < s.Length; i++)
            {
                start = Math.Max(start, lasts[s[i]]);
                lasts[s[i]] = i + 1;
                ret = Math.Max(ret, i - start + 1);
            }
            return ret;
        }

        public void Test()
        {
            var nums = new int[,] { { 1, 3 }, { 1, 4 }, { 2, 5 }, { 3, 5 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 3);

            nums = new int[,] { { 1, 2 }, { 2, 3 }, { 2, 4 }, { 4, 5 } };
            Console.WriteLine(IntersectionSizeTwo(nums) == 5);
        }
    }
}
