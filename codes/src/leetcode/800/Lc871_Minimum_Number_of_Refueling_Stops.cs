using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: heap, dp
 * heap: Time(nlogn), Space(n)
 * maintain a max heap of gas of current reachable stations.
 * 
 * dp: Time(n^2), Space(n)
 * dp[i] is the total fuel (that is the furthest position) with i stops
 * dp[i] = max(dp[i], dp[i-1] + gas[i])
 */
namespace leetcode
{
    public class Lc871_Minimum_Number_of_Refueling_Stops
    {
        public int MinRefuelStops(int target, int startFuel, int[][] stations)
        {
            // store the max gas of current reachable stations
            var heap = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) =>
                a[0] != b[0] ? a[0] - b[0] : a[1] - b[1]));

            int res = 0, n = stations.Length;

            for (int i = 0; i <= n; i++)
            {
                int miles = i == n ? target : stations[i][0];
                int gas = i == n ? 0 : stations[i][1];
                while (startFuel < miles && heap.Count > 0)
                {
                    startFuel += heap.Max[0];
                    heap.Remove(heap.Max);
                    res++;
                }

                if (startFuel < miles) return -1;
                heap.Add(new int[] { gas, i });
            }

            return res;
        }

        public int MinRefuelStopsDp(int target, int startFuel, int[][] stations)
        {
            int n = stations.Length;
            var fuels = new long[n + 1]; // index is stop count
            fuels[0] = startFuel;

            for (int i = 0; i < n; i++)
            {
                for (int s = i; s >= 0 && fuels[s] >= stations[i][0]; s--) // downside to avoid accumulate dup
                    fuels[s + 1] = Math.Max(fuels[s + 1], fuels[s] + stations[i][1]);
            }

            for (int i = 0; i <= n; i++)
                if (fuels[i] >= target) return i;

            return -1;
        }

        public void Test()
        {
            var s = new int[][] { };
            Console.WriteLine(MinRefuelStops(1, 1, s) == 0);
            Console.WriteLine(MinRefuelStopsDp(1, 1, s) == 0);

            s = new int[][] { new int[] { 10, 100 } };
            Console.WriteLine(MinRefuelStops(100, 1, s) == -1);
            Console.WriteLine(MinRefuelStopsDp(100, 1, s) == -1);

            s = new int[][] { new int[] { 10, 60 }, new int[] { 20, 30 }, new int[] { 30, 30 }, new int[] { 60, 40 }, };
            Console.WriteLine(MinRefuelStops(100, 10, s) == 2);
            Console.WriteLine(MinRefuelStopsDp(100, 10, s) == 2);

            s = new int[][] { new int[] { 5, 1000000000 }, new int[] { 1000, 1000000000 }, new int[] { 100000, 1000000000 }, };
            Console.WriteLine(MinRefuelStops(1000000000, 1000000000, s) == 0);
            Console.WriteLine(MinRefuelStopsDp(1000000000, 1000000000, s) == 0);

            s = new int[][] { new int[] { 25, 27 }, new int[] { 36, 187 }, new int[] { 140, 186 }, new int[] { 378, 6 }, new int[] { 492, 202 }, new int[] { 517, 89 }, new int[] { 579, 234 }, new int[] { 673, 86 }, new int[] { 808, 53 }, new int[] { 954, 49 }, };
            Console.WriteLine(MinRefuelStops(1000, 83, s) == -1);
            Console.WriteLine(MinRefuelStopsDp(1000, 83, s) == -1);
        }
    }
}
