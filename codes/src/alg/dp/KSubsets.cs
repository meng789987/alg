using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: K subsets with equal sum, TSP
 * It is similar with TSP(Travelling Salesman Problem). It's easy to solve using backtracking with much more complex.
 * Here, it can be solved in O(n^2*2^n) using Dynamic Programming with Bit Mask. 
 */
namespace alg.dp
{
    public class KSubsets
    {
        /*
         * Time(n*2^n), Space(2^n)
         * dp[S] is the sum of nodes in subset S
         * dp[S] is initialized to invalid number[-1], and is set only if dp[S-{i}] is valid and num[i] can fit into the group
         *   (dp[S-{i}] % target) + nums[i] <= target,  where target is sum of a partition = sum(nums)/k
         * Lc698. Partition to K Equal Sum Subsets
         */
        bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (k == 1) return true;
            int n = nums.Length, sum = nums.Sum();
            if (sum % k != 0) return false;
            Array.Sort(nums);
            if (nums.Last() > sum / k) return false;

            int target = sum / k;
            var dp = new int[1 << n];
            Array.Fill(dp, -1);
            dp[0] = 0;

            for (int fromMask = 0; fromMask < dp.Length; fromMask++)
            {
                if (dp[fromMask] == -1) continue;
                for (int i = 0; i < n; i++)
                {
                    int toMask = fromMask | (1 << i);
                    if (toMask == fromMask) continue;
                    if ((dp[fromMask] % target) + nums[i] > target) // a little short cut,
                        break; // as the current one [and all after as nums are sorted] is too big to fit into current group
                    dp[toMask] = dp[fromMask] + nums[i]; // set the valid sum
                }
            }

            return dp.Last() == sum;
        }

        public void Test()
        {
            var nums = new int[] { 4, 3, 2, 3, 5, 2, 1 };
            Console.WriteLine(CanPartitionKSubsets(nums, 4) == true);

            nums = new int[] { 1, 2, 3, 4 };
            Console.WriteLine(CanPartitionKSubsets(nums, 3) == false);

            nums = new int[] { 18, 20, 39, 73, 96, 99, 101, 111, 114, 190, 207, 295, 471, 649, 700, 1037 };
            Console.WriteLine(CanPartitionKSubsets(nums, 4) == true);
        }
    }
}

