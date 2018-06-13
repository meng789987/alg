using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: sort
 */
namespace leetcode
{
    public class Lc179_Largest_Number
    {
        public string LargestNumber(int[] nums)
        {
            var strs = new List<string>(nums.Select(n => n.ToString()));
            strs.Sort((a, b) => (b + a).CompareTo(a + b));
            return strs[0][0] == '0' ? "0" : string.Join("", strs);
        }

        public void Test()
        {
            var nums = new int[] { 10, 2 };
            Console.WriteLine(LargestNumber(nums) == "210");

            nums = new int[] { 3, 30, 34, 5, 9 };
            Console.WriteLine(LargestNumber(nums) == "9534330");

            nums = new int[] { 0, 0 };
            Console.WriteLine(LargestNumber(nums) == "0");

            nums = new int[] { 824, 938, 1399, 5607, 6973, 5703, 9609, 4398, 8247 };
            Console.WriteLine(LargestNumber(nums) == "9609938824824769735703560743981399");

        }
    }
}

