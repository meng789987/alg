using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: greedy, stack
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc316_Remove_Duplicate_Letters
    {
        public string RemoveDuplicateLetters(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            var counts = new int[26];
            foreach (var c in s) counts[c - 'a']++;

            int smallestCharPos = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[smallestCharPos] > s[i])
                    smallestCharPos = i;
                if (--counts[s[i] - 'a'] == 0) break; // prefix is duplicate
            }

            var rs = s.Substring(smallestCharPos + 1).Replace(s[smallestCharPos].ToString(), "");
            return s[smallestCharPos] + RemoveDuplicateLetters(rs);
        }

        public string RemoveDuplicateLettersStack(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            var counts = new int[26];
            foreach (var c in s) counts[c - 'a']++;
            var visited = new bool[26];

            var stack = new Stack<char>();
            foreach (var c in s)
            {
                counts[c - 'a']--;
                if (visited[c - 'a']) continue;

                // there is another top char later
                while (stack.Count > 0 && c < stack.Peek() && counts[stack.Peek() - 'a'] > 0)
                {
                    visited[stack.Pop() - 'a'] = false; // will visit later
                }

                stack.Push(c);
                visited[c - 'a'] = true;
            }

            var cs = new char[stack.Count];
            for (int i = cs.Length - 1; i >= 0; i--)
                cs[i] = stack.Pop();
            return new string(cs);
        }

        public void Test()
        {
            Console.WriteLine(RemoveDuplicateLetters("bcabc") == "abc");
            Console.WriteLine(RemoveDuplicateLettersStack("bcabc") == "abc");
            Console.WriteLine(RemoveDuplicateLetters("cbacdcbc") == "acdb");
            Console.WriteLine(RemoveDuplicateLettersStack("cbacdcbc") == "acdb");
        }
    }
}

