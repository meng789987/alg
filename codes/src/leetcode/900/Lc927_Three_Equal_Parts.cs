using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: math, bit
 * Time(n), Space(1)
 * The three parts must have same number of 1s.
 */
namespace leetcode
{
    public class Lc927_Three_Equal_Parts
    {
        public int[] ThreeEqualParts(int[] A)
        {
            int n = A.Length;
            int count = A.Sum();

            if (count == 0) return new int[] { 0, 2 };
            if (count % 3 != 0) return new int[] { -1, -1 };

            int i = 0, s1, s2, s3;
            while (A[i] == 0) i++;
            s1 = i++;

            for (int c = 0; c < count / 3; i++)
                if (A[i] == 1) c++;
            s2 = i - 1;

            for (int c = 0; c < count / 3; i++)
                if (A[i] == 1) c++;
            s3 = i - 1;

            // verify they are same
            while (s3 < n && A[s1] == A[s2] && A[s2] == A[s3])
            { s1++; s2++; s3++; }
            if (s3 != n) return new int[] { -1, -1 };

            return new int[] { s1 - 1, s2 };
        }

        public void Test()
        {
            var a = new int[] { 1, 0, 1, 0, 1 };
            var exp = new int[] { 0, 3 };
            Console.WriteLine(exp.SequenceEqual(ThreeEqualParts(a)));

            a = new int[] { 1, 1, 0, 1, 1 };
            exp = new int[] { -1, -1 };
            Console.WriteLine(exp.SequenceEqual(ThreeEqualParts(a)));
        }
    }
}
