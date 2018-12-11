using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: Disjoint set
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc952_Largest_Component_Size_by_Common_Factor
    {
        public int LargestComponentSize(int[] A)
        {
            if (_primes == null)
                FillPrimes();

            var n = A.Length;
            var ds = new DisjointSet(n);
            var list = new LinkedList<int[]>(); // [index, value]
            for (int i = 0; i < n; i++)
                list.AddLast(new int[] { i, A[i] });

            foreach (var p in _primes)
            {
                if (list.Count <= 1) break;
                int groupIdx = -1;
                for (var item = list.First; item != null;)
                {
                    var next = item.Next;
                    var val = item.Value[1];
                    while (val % p == 0) val /= p;
                    if (val != item.Value[1])
                    {
                        item.Value[1] = val;
                        if (groupIdx == -1) groupIdx = item.Value[0];
                        else ds.Union(item.Value[0], groupIdx);
                    }
                    if (val == 1) list.Remove(item);
                    item = next;
                }
            }

            // for bigger prime
            var rest = list.ToList();
            rest.Sort((a, b) => a[1] - b[1]);
            for (int i = 0; i < rest.Count - 1; i++)
                if (rest[i][1] == rest[i + 1][1]) ds.Union(rest[i][0], rest[i + 1][0]);

            int res = 0;
            for (int i = 0; i < n; i++)
                res = Math.Max(res, ds.Size(i));

            return res;
        }

        static List<int> _primes;

        static void FillPrimes()
        {
            const int MAX = 100000;
            _primes = new List<int> { 2, 3, 5, 7 };

            for (int i = 11; i * i <= MAX; i += 2)
            {
                var isPrime = true;
                foreach (var p in _primes)
                    if (i % p == 0) { isPrime = false; break; }
                if (isPrime) _primes.Add(i);
            }
        }

        class DisjointSet
        {
            public DisjointSet(int n)
            {
                _parents = new int[n];
                _sizes = new int[n];
                for (int i = 0; i < n; i++)
                {
                    _parents[i] = i;
                    _sizes[i] = 1;
                }
            }

            public int Find(int i)
            {
                if (_parents[i] != i)
                    _parents[i] = Find(_parents[i]);
                return _parents[i];
            }

            public void Union(int i, int j)
            {
                var pi = Find(i);
                var pj = Find(j);
                if (pi == pj) return;
                _parents[pi] = pj;
                _sizes[pj] += _sizes[pi];
            }

            public int Size(int i)
            {
                return _sizes[Find(i)];
            }

            int[] _parents;
            int[] _sizes;
        }

        public int LargestComponentSize2(int[] A)
        {
            var n = A.Length;
            var ds = new DisjointSet(n);
            var values = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
                values[A[i]] = i;

            // sieve the primes
            var isPrimes = new bool[A.Max() + 1];
            Array.Fill(isPrimes, true);
            for (int p = 2; p < isPrimes.Length; p++)
            {
                if (!isPrimes[p]) continue;
                for (int i = p + p; i < isPrimes.Length; i += p)
                {
                    isPrimes[i] = false;
                    if (values.ContainsKey(i))
                    {
                        if (!values.ContainsKey(p)) values[p] = values[i];
                        else ds.Union(values[i], values[p]);
                    }
                }
            }

            int res = 0;
            for (int i = 0; i < n; i++)
                res = Math.Max(res, ds.Size(i));

            return res;
        }

        public void Test()
        {
            var a = new int[] { 4, 6, 15, 35 };
            Console.WriteLine(LargestComponentSize(a) == 4);
            Console.WriteLine(LargestComponentSize2(a) == 4);

            a = new int[] { 20, 50, 9, 63 };
            Console.WriteLine(LargestComponentSize(a) == 2);
            Console.WriteLine(LargestComponentSize2(a) == 2);

            a = new int[] { 2, 3, 6, 7, 4, 12, 21, 39 };
            Console.WriteLine(LargestComponentSize(a) == 8);
            Console.WriteLine(LargestComponentSize2(a) == 8);

            a = new int[] { 2, 7, 522, 526, 535, 26, 944, 35, 519, 45, 48, 567, 266, 68, 74, 591, 81, 86, 602, 93, 610, 621, 111, 114, 629, 641, 131, 651, 142, 659, 669, 161, 674, 163, 180, 187, 190, 194, 195, 206, 207, 218, 737, 229, 240, 757, 770, 260, 778, 270, 272, 785, 274, 290, 291, 292, 296, 810, 816, 314, 829, 833, 841, 349, 880, 369, 147, 897, 387, 390, 905, 405, 406, 407, 414, 416, 417, 425, 938, 429, 432, 926, 959, 960, 449, 963, 966, 929, 457, 463, 981, 985, 79, 487, 1000, 494, 508 };
            Console.WriteLine(LargestComponentSize(a) == 84);
            Console.WriteLine(LargestComponentSize2(a) == 84);
        }
    }
}
