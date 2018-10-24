using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: heap, greedy
 * Time(nlogn), Space(n)
 * sort workers based on their ratio; maintain a heap of quality of the smallest k workers.
 * for each worker i after first k, if worker[i] is selected then the total cost is ratio[i]*(quality[i] + sumOfQualityOfHeap - maxQualityInHeap)
 */
namespace leetcode
{
    public class Lc857_Minimum_Cost_to_Hire_K_Workers
    {
        public double MincostToHireWorkers(int[] quality, int[] wage, int K)
        {
            int n = quality.Length;
            var workers = quality.Zip(wage, (q, w) => new { Qual = q, Ratio = (double)w / q }).ToList();
            workers.Sort((a, b) => a.Ratio.CompareTo(b.Ratio));

            var comp = Comparer<int>.Create((a, b) => workers[a].Qual != workers[b].Qual ? workers[a].Qual.CompareTo(workers[b].Qual) : a - b);
            var heap = new SortedSet<int>(comp); // store index of workers
            double minCost = double.MaxValue;

            for (int qualSum = 0, i = 0; i < n; i++)
            {
                heap.Add(i);
                qualSum += workers[i].Qual;

                if (heap.Count > K)
                {
                    qualSum -= workers[heap.Max].Qual;
                    heap.Remove(heap.Max);
                }

                if (heap.Count == K)
                    minCost = Math.Min(minCost, workers[i].Ratio * qualSum);
            }

            return minCost;
        }

        public void Test()
        {
            var quality = new int[] { 10, 20, 5 };
            var wage = new int[] { 70, 50, 30 };
            Console.WriteLine(MincostToHireWorkers(quality, wage, 2) == 105);

            quality = new int[] { 3, 1, 10, 10, 1 };
            wage = new int[] { 4, 8, 2, 2, 7 };
            Console.WriteLine(Math.Abs(MincostToHireWorkers(quality, wage, 3) - 30.66667) < 1e-5);

            quality = new int[] { 14, 56, 59, 89, 39, 26, 86, 76, 3, 36 };
            wage = new int[] { 90, 217, 301, 202, 294, 445, 473, 245, 415, 487 };
            Console.WriteLine(Math.Abs(MincostToHireWorkers(quality, wage, 2) - 399.53846) < 1e-5);

            quality = new int[] { 32, 43, 66, 9, 94, 57, 25, 44, 99, 19 };
            wage = new int[] { 187, 366, 117, 363, 121, 494, 348, 382, 385, 262 };
            Console.WriteLine(Math.Abs(MincostToHireWorkers(quality, wage, 4) - 1528.0) < 1e-5);
        }
    }
}
