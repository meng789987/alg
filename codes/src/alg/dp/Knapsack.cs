using alg.graph;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * tags: knapsack
 * bounded: each item can only be selected once at most.
 * unbounded: each item can be selected as many as you can.
 * fractional knapsack: can select a part of an item;     => greedy O(nlogn)
 * 0-1 knapsack: must select an item as a whole;          => dp O(nW) for int, backtracking O(2^n)
 */
namespace alg.dp
{
    public class Knapsack
    {
        /*
         * Time(nlogn), Space(n)
         * fractional knapsack only, unbounded is trivial. Here is for bounded.
         * sort them in descending order of ratio of value/weight, select from first until knapsack full
         */
        double Knapsack_Greedy_Bounded(double[] values, double[] weights, double capacity)
        {
            var items = values.Zip(weights, (v, w) => new { Weight = w, Ratio = v / w }).ToList();
            items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));

            double ret = 0;
            for (int i = 0; i < values.Length && capacity > 0; i++)
            {
                ret += items[i].Ratio * Math.Min(capacity, items[i].Weight);
                capacity -= items[i].Weight;
            }

            return ret;
        }

        /*
         * Time(2^n), Space(n)
         */
        double Knapsack_Backtracking_Bounded(double[] values, double[] weights, double capacity, int start)
        {
            double ret = 0;
            for (int i = start; i < values.Length; i++)
            {
                if (capacity >= weights[i])
                    ret = Math.Max(ret, values[i] + Knapsack_Backtracking_Bounded(values, weights, capacity - weights[i], i + 1));
            }

            return ret;
        }

        /*
         * Time(2^n), Space(n), where n=sum(capacity/weights[i])
         */
        double Knapsack_Backtracking_Unbounded(double[] values, double[] weights, double capacity)
        {
            double ret = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (capacity >= weights[i])
                    ret = Math.Max(ret, values[i] + Knapsack_Backtracking_Unbounded(values, weights, capacity - weights[i]));
            }

            return ret;
        }

        /*
         * Time(nc), Space(c)
         * only work for integer weights/capacity, where c is capacity
         * dp[i, c] = max(dp[i-1, c], dp[i-1, c-weight[i]] + values[i])
         */
        int Knapsack_Dp_Bounded(int[] values, int[] weights, int capacity)
        {
            // compact space from nc to c
            var dp = new int[capacity + 1];
            for (int i = 0; i < values.Length; i++)
            {
                // reverse iteration to avoid items being reused.
                for (int c = capacity; c >= weights[i]; c--)
                    dp[c] = Math.Max(dp[c], dp[c - weights[i]] + values[i]);
            }

            return dp[capacity];
        }

        /*
         * Time(nc), Space(c)
         * only work for integer weights/capacity, where c is capacity
         * dp[c] = max(dp[c-weight[i]] + values[i]), i=[0..n-1]
         */
        int Knapsack_Dp_Unbounded(int[] values, int[] weights, int capacity)
        {
            // compact space from nc to c
            var dp = new int[capacity + 1];
            for (int i = 0; i < values.Length; i++)
            {
                for (int c = weights[i]; c <= capacity; c++)
                    dp[c] = Math.Max(dp[c], dp[c - weights[i]] + values[i]);
            }

            return dp[capacity];
        }

        public void Test()
        {
            var values = new double[] { 100, 60, 120 };
            var weights = new double[] { 20, 10, 30 };
            Console.WriteLine(Knapsack_Greedy_Bounded(values, weights, 50) == 240);
            Console.WriteLine(Knapsack_Backtracking_Bounded(values, weights, 50, 0) == 220);
            Console.WriteLine(Knapsack_Backtracking_Unbounded(values, weights, 50) == 300);
            Console.WriteLine(Knapsack_Dp_Bounded(values.Select(d => (int)d).ToArray(), weights.Select(d => (int)d).ToArray(), 50) == 220);
            Console.WriteLine(Knapsack_Dp_Unbounded(values.Select(d => (int)d).ToArray(), weights.Select(d => (int)d).ToArray(), 50) == 300);

            values = new double[] { 40, 50, 100, 95, 30 };
            weights = new double[] { 2, 3.14, 1.98, 5, 3 };
            Console.WriteLine(Math.Abs(Knapsack_Greedy_Bounded(values, weights, 10) - 251.24204) < 1e-5);
            Console.WriteLine(Knapsack_Backtracking_Bounded(values, weights, 10, 0) == 235);
            Console.WriteLine(Knapsack_Backtracking_Unbounded(values, weights, 10) == 500);

        }
    }
}

