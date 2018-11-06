using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math
 * Time(n), Space(1)
 * let S = a[0]^...^a[n-1], then return S == 0 || n%2 == 0
 * if n is even and no such move, that is S != 0 and S^a[i] == 0 for any i (Alice picks a[i] then remains xor sum is S^a[i]);
 * (S^a[0]) ^...^ (S^a[n-1]) = (S^...^S) ^ (a[0]^...^a[n-1]) = 0^S = S != 0, contradiction!
 */
namespace leetcode
{
    public class Lc810_Chalkboard_XOR_Game
    {
        public bool XorGame(int[] nums)
        {
            int x = 0;
            foreach (var n in nums) x ^= n;
            return x == 0 || nums.Length % 2 == 0;
        }

        public void Test()
        {
            var nums = new int[] { 1, 1, 2 };
            Console.WriteLine(XorGame(nums) == false);
        }
    }
}
