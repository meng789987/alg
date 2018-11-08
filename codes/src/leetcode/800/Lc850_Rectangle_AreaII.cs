using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: topology sort
 * Time(n^2logn), Space(n)
 */
namespace leetcode
{
    public class Lc850_Rectangle_AreaII
    {
        public int RectangleArea(int[][] rectangles)
        {
            // key: x, value: {key: y, value:{xcount, ycount}}, count:{1:enter, -1:leave}
            var data = new SortedDictionary<int, SortedDictionary<int, int[]>>();
            foreach (var r in rectangles)
            {
                if (!data.ContainsKey(r[0]))
                    data[r[0]] = new SortedDictionary<int, int[]>();
                if (!data.ContainsKey(r[2]))
                    data[r[2]] = new SortedDictionary<int, int[]>();

                Inc(data[r[0]], r[1], 1, 1);   // [x1, y1]
                Inc(data[r[0]], r[3], 1, -1);  // [x1, y2]
                Inc(data[r[2]], r[1], -1, -1); // [x2, y1]
                Inc(data[r[2]], r[3], -1, 1);  // [x2, y2]
            }

            long ret = 0, len = 0, prevx = 0;
            var scan = new SortedDictionary<int, int[]>();

            foreach (var item in data)
            {
                int x = item.Key;
                ret += len * (x - prevx);
                prevx = x;
                len = 0;

                foreach (var kv in item.Value)
                {
                    int y = kv.Key;
                    if (!scan.ContainsKey(y))
                        scan.Add(y, new int[2]);
                    scan[y][0] += kv.Value[0];
                    scan[y][1] += kv.Value[1];

                    if (scan[y][0] == 0)
                        scan.Remove(y);
                }

                int prevy = 0, prevbal = 0;
                foreach (var kv in scan)
                {
                    int bal = prevbal + kv.Value[1];
                    if (prevbal == 0 && bal > 0) // enter
                        prevy = kv.Key;
                    if (prevbal > 0 && bal == 0) // leave
                        len += kv.Key - prevy;
                    prevbal = bal;
                }
            }

            return (int)(ret % 1000000007);
        }

        void Inc(SortedDictionary<int, int[]> map, int key, int xinc, int yinc)
        {
            if (!map.ContainsKey(key))
                map[key] = new int[2];
            map[key][0] += xinc;
            map[key][1] += yinc;
        }

        public void Test()
        {
            // {x1, y1, x2, y2}
            var rect = new int[][] { new int[] { 0, 0, 2, 2 }, new int[] { 1, 0, 2, 3 }, new int[] { 1, 0, 3, 1 } };
            Console.WriteLine(RectangleArea(rect) == 6);

            rect = new int[][] { new int[] { 0, 0, 1000000000, 1000000000 } };
            Console.WriteLine(RectangleArea(rect) == 49);

            rect = new int[][] { new int[] { 0, 0, 1, 1 }, new int[] { 2, 2, 3, 3 } };
            Console.WriteLine(RectangleArea(rect) == 2);
        }
    }
}
