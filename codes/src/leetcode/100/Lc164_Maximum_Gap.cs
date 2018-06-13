using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: radix sort, buckets
 */
namespace leetcode
{
    public class Lc164_Maximum_Gap
    {
        public int MaximumGap(int[] nums)
        {
            if (nums.Length < 2) return 0;
            int min = nums.Min();
            int max = nums.Max();

            int bucketCapacity = Math.Max(1, (max - min) / (nums.Length - 1));
            int bucketCount = (max - min) / bucketCapacity + 1;
            var buckets = new Bucket[bucketCount];
            for (int i = 0; i < bucketCount; i++)
                buckets[i] = new Bucket();

            foreach (var n in nums)
            {
                int bucketIdx = (n - min) / bucketCapacity;
                buckets[bucketIdx].Used = true;
                buckets[bucketIdx].Min = Math.Min(buckets[bucketIdx].Min, n);
                buckets[bucketIdx].Max = Math.Max(buckets[bucketIdx].Max, n);
            }

            int maxGap = 0;
            int prevMax = max;
            foreach (var bucket in buckets)
            {
                if (bucket.Used)
                {
                    maxGap = Math.Max(maxGap, bucket.Min - prevMax);
                    prevMax = bucket.Max;
                }
            }

            return maxGap;
        }

        class Bucket
        {
            public bool Used;
            public int Min = int.MaxValue;
            public int Max = int.MinValue;
        }

        public void Test()
        {
            var nums = new int[] { 3, 6, 9, 1 };
            Console.WriteLine(MaximumGap(nums) == 3);

            nums = new int[] { 3 };
            Console.WriteLine(MaximumGap(nums) == 0);

        }
    }
}

