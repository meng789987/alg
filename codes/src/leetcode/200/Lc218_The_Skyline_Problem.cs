
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: dfs, prefix trie
 * trie: Time(nk + qk), Space(nk)
 */
namespace leetcode
{
    public class Lc218_The_Skyline_Problem
    {
        public IList<int[]> GetSkyline(int[,] buildings)
        {
            var ret = new List<int[]>();
            var sky = new Sky();
            for (int i = 0; i < buildings.GetLength(0); i++)
            {
                while (sky.Count > 0 && sky.MinX < buildings[i, 0])
                {
                    int x = sky.MinX;
                    int y = sky.Remove();
                    if (y > sky.MaxY) ret.Add(new int[] { x, sky.MaxY });
                }
                if (ret.Count > 0 && buildings[i, 0] == ret[ret.Count - 1][0]) // same x
                    ret[ret.Count - 1][1] = Math.Max(ret[ret.Count - 1][1], buildings[i, 2]);
                else if (ret.Count == 0 || buildings[i, 2] > ret[ret.Count - 1][1]) // a new higher y
                    ret.Add(new int[] { buildings[i, 0], buildings[i, 2] });
                sky.Add(buildings[i, 1], buildings[i, 2]); // add the ending of this building
            }

            while (sky.Count > 0)
            {
                int x = sky.MinX;
                int y = sky.Remove();
                if (y > sky.MaxY) ret.Add(new int[] { x, sky.MaxY });
            }

            return ret;
        }

        class Sky
        {
            public void Add(int x, int y)
            {
                if (_dict.ContainsKey(x)) // merge same x
                    _dict[x] = Math.Max(y, _dict[x]);
                else _dict[x] = y;
            }

            public int Remove()
            {
                var first = _dict.First();
                _dict.Remove(first.Key);
                return first.Value;
            }

            public int MinX
            {
                get { return _dict.First().Key; }
            }

            public int MaxY
            {
                get { return MaxValue().Value; }
            }

            public int Count
            {
                get { return _dict.Count; }
            }

            private SortedDictionary<int, int> _dict = new SortedDictionary<int, int>();
            private KeyValuePair<int, int> MaxValue()
            {
                var max = new KeyValuePair<int, int>(0, 0);
                foreach (var kv in _dict)
                {
                    if (max.Value < kv.Value) max = kv;
                }
                return max;
            }

        }

        public void Test()
        {
            var b = new int[,]
            {
                {2, 9, 10},
                {3, 7, 15},
                {5, 12, 12},
                {15, 20, 10},
                {19, 24, 8}
            };
            var exp = new List<int[]>
            {
                new int[] { 2, 10 },
                new int[] { 3, 15 },
                new int[] { 7, 12 },
                new int[] { 12, 0 },
                new int[] { 15, 10 },
                new int[] { 20, 8 },
                new int[] { 24, 0 }
            };
            Console.WriteLine(exp.SameSet2(GetSkyline(b)));

            b = new int[,]
            {
                {0, 2147483647, 2147483647}
            };
            exp = new List<int[]> {
                new int[] { 0, 2147483647 },
                new int[] { 2147483647, 0 } };
            Console.WriteLine(exp.SameSet2(GetSkyline(b)));
        }
    }
}

