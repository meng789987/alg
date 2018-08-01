using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: dc, dp
 * Time(n^4), Space(n^3)
 * similar to lc312 busting balloons, when we try to define the subproblem dp[i, j] as the max points to remove box[i..j],
 * it's not enough as it depends on the number of same box at its border. let's define dp[i, j, c] as the max points to remove box[i..j]
 * with c same boxes as box[i] before i. then
 * dp[i, j, c] = (c+1)^2 + dp[i+1, j, 0], if we remove boxes[i], then boxes[i+1..j]; or
 *             = max(dp[i+1, k-1, 0] + dp[k, j, c+1]), where k=[i+1..j] and boxes[k]==boxes[i], leave boxes[i] to remove later together with previous c same boxes.
 */
namespace leetcode
{
    public class Lc546_Remove_Boxes
    {
        public int RemoveBoxesTopdown(int[] boxes)
        {
            int n = boxes.Length;
            return Dfs(boxes, 0, n - 1, 0, new int[n, n, n]);
        }

        int Dfs(int[] boxes, int lo, int hi, int c, int[,,] memo)
        {
            if (lo > hi) return 0;
            if (memo[lo, hi, c] > 0) return memo[lo, hi, c];

            // optimize for dup
            for (; lo < hi && boxes[lo] == boxes[lo+1]; lo++) c++;

            var res = (c + 1) * (c + 1) + Dfs(boxes, lo + 1, hi, 0, memo);
            for (int k = lo + 1; k <= hi; k++)
            {
                if (boxes[k] != boxes[lo]) continue;
                res = Math.Max(res, Dfs(boxes, lo + 1, k - 1, 0, memo) + Dfs(boxes, k, hi, c + 1, memo));
            }

            return memo[lo, hi, c] = res;
        }

        public int RemoveBoxesDp(int[] boxes)
        {
            int n = boxes.Length;
            var dp = new int[n + 1, n + 1, n + 1];

            for (int len = 1; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    for (int c = 0; c <= i; c++)
                    {
                        var max = (c + 1) * (c + 1) + dp[i + 1, j, 0];
                        for (int k = i + 1; k <= j; k++)
                        {
                            if (boxes[k] != boxes[i]) continue;
                            max = Math.Max(max, dp[i + 1, k - 1, 0] + dp[k, j, c + 1]);
                        }

                        dp[i, j, c] = max;
                    }
                }
            }

            return dp[0, n - 1, 0];
        }

        public void Test()
        {
            var nums = new int[] { 1, 3, 2, 2, 2, 3, 4, 3, 1 };
            Console.WriteLine(RemoveBoxesTopdown(nums) == 23);
            Console.WriteLine(RemoveBoxesDp(nums) == 23);

            nums = new int[] { 1 };
            Console.WriteLine(RemoveBoxesTopdown(nums) == 1);
            Console.WriteLine(RemoveBoxesDp(nums) == 1);

            nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine(RemoveBoxesTopdown(nums) == 10);
            Console.WriteLine(RemoveBoxesDp(nums) == 10);

            nums = new int[] { 3, 8, 8, 5, 5, 3, 9, 2, 4, 4, 6, 5, 8, 4, 8, 6, 9, 6, 2, 8, 6, 4, 1, 9, 5, 3, 10, 5, 3, 3, 9, 8, 8, 6, 5, 3, 7, 4, 9, 6, 3, 9, 4, 3, 5, 10, 7, 6, 10, 7 };
            Console.WriteLine(RemoveBoxesTopdown(nums) == 136);
            Console.WriteLine(RemoveBoxesDp(nums) == 136);

            Console.WriteLine(Extensions.ElapsedMilliseconds(() => RemoveBoxesTopdown(nums), 500));
            Console.WriteLine(Extensions.ElapsedMilliseconds(() => RemoveBoxesDp(nums), 500));
        }
    }
}

