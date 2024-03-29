﻿using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: array
 */
namespace alg.array
{
    public class RotateArray
    {
        public void Rotate(int[] a, int k)
        {
            int n = a.Length;
            if (n == 0) return;
            k %= n;
            if (k < 0) k += n;
            if (k == 0) return;
            for (int cnt = 0, start = 0; cnt < n; start++)
            {
                int tmp = a[start];
                int j = start;
                while (true)
                {
                    cnt++;
                    var jn = j - k;
                    if (jn < 0) jn += n;
                    if (jn == start)
                    {
                        a[j] = tmp;
                        break;
                    }
                    else
                    {
                        a[j] = a[jn];
                        j = jn;
                    }
                }
            }
        }

        public void Test()
        {
            var nums = new int[] { 3, 4, 5, 2, 7, 234, 84, 24 };
            Rotate(nums, 2);
            var exp = "84, 24, 3, 4, 5, 2, 7, 234";
            Console.WriteLine(exp == string.Join(", ", nums));
        }
    }
}
