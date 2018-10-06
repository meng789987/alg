using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: recursion
 * 1. Split s into shortest special strings (as many as possible).
 * 2. Shortest special string starts with 1 and ends with 0. Recursive apply steps 1-4 on the middle part.
 * 3. Sort all shortest special strings in lexicographically largest order.
 * 4. Join and output all strings.
 * Similar with valid parentheses if '1'=>'(', '0'=>')'.
 */
namespace leetcode
{
    public class Lc761_Special_Binary_String
    {
        public string MakeLargestSpecial(string s)
        {
            var list = new List<string>();
            for (int b = 0, i = 0, j = 0; j < s.Length; j++)
            {
                b += s[j] == '1' ? 1 : -1;
                if (b == 0)
                {
                    list.Add('1' + MakeLargestSpecial(s.Substring(i + 1, j - i - 1)) + '0');
                    i = j + 1;
                }
            }

            list.Sort((a, b) => b.CompareTo(a));
            return string.Join("", list);
        }

        public void Test()
        {
            Console.WriteLine(MakeLargestSpecial("11011000") == "11100100");
            Console.WriteLine(MakeLargestSpecial("11100010") == "11100010");
            Console.WriteLine(MakeLargestSpecial("101100101100") == "110011001010");
            Console.WriteLine(MakeLargestSpecial("1101001110001101010110010010") == "1110010101010011100011010010");
        }
    }
}

