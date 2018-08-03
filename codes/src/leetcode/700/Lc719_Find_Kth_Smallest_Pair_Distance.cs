
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: bs
 * math: According to geometric sequence, b^0 + b^1 + ... + b^(w-1) = (b^w - 1) / (b - 1), that is the num n=11111 with width w and base b.
 * b^(w-1) < n < b^w, so b should be [n^(1/w), n^(1/(w-1)], then binary search; or
 * b^(w-1) < n < (b+1)^(w-1), so b=n^(1/(w-1)) is what we want to verify.
 */
namespace leetcode
{
    public class Lc719_Find_Kth_Smallest_Pair_Distance
    {
        public int SmallestDistancePair(int[] nums, int k)
        {
            int n = nums.Length;
            Array.Sort(nums);
            var q = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) =>
                nums[a[1]] - nums[a[0]] != nums[b[1]] - nums[b[0]]
                ? nums[a[1]] - nums[a[0]] - nums[b[1]] + nums[b[0]]
                : a[0] - b[0]
            ));

            for (int i = 0; i < n - 1; i++)
                q.Add(new int[] { i, i + 1 });

            for (int i = 0; i < k - 1; i++)
            {
                var node = q.Min; q.Remove(q.Min);
                if (node[1] < n - 1)
                    q.Add(new int[] { node[0], node[1] + 1 });
            }

            return nums[q.Min[1]] - nums[q.Min[0]];
        }


        public void Test()
        {
            var nums = new int[] { 1, 3, 1 };
            Console.WriteLine(SmallestDistancePair(nums, 1) == 0);
        }
    }
}

