using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: segment tree
 * Space(n); Build: Time(n), Query/Update: Time(logn)
 * segment tree is a perfect binary tree. Each node has a sum and a data range.
 * root is the sum of data range[0..n-1], its left is sum of data range[0..n/2], and its right is sum of data range[n/2+1..n-1].
 * e.g. for data [10, 13, 12, 15, 16, 18], it would be:
 *                        84(0-7)
 *           50(0-3)                   34(4-7)
 *    23(0-1)       27(2-3)      34(4-5)      0(6-7)
 * 10(0)  13(1)  12(2)  15(3)  16(4)  18(5)  0(6)  0(7)
 */
namespace alg.tree
{
    public class SegmentTreeSum
    {
        public SegmentTreeSum() { }
        public SegmentTreeSum(int[] data)
        {
            _data = data;

            int n = 1;
            while (n < _data.Length) n <<= 1;
            n = n * 2 - 1;
            _segtree = new int[n];
            BuildTree(0, 0, _data.Length - 1);
        }

        // sum of data[i..j]
        public int Sum(int i, int j)
        {
            return Sum(0, 0, _data.Length - 1, i, j);
        }

        // set data[pos] = newValue
        public void Update(int pos, int newValue)
        {
            Update(0, 0, _data.Length - 1, pos, newValue - _data[pos]);
        }


        private int BuildTree(int node, int lo, int hi)
        {
            if (lo == hi) return _segtree[node] = _data[lo];
            int m = (lo + hi) / 2;
            return _segtree[node] = BuildTree(node * 2 + 1, lo, m) + BuildTree(node * 2 + 2, m + 1, hi);
        }

        // calculate the sum of data[i..j] from node whose range is [lo..hi]
        private int Sum(int node, int lo, int hi, int i, int j)
        {
            if (hi < i || j < lo) return 0; // no overlap
            if (i <= lo && hi <= j) return _segtree[node]; // the node is fully covered
            int m = (lo + hi) / 2;
            return Sum(node * 2 + 1, lo, m, i, j) + Sum(node * 2 + 2, m + 1, hi, i, j);
        }

        private void Update(int node, int lo, int hi, int pos, int diff)
        {
            if (pos < lo || hi < pos) return;
            _segtree[node] += diff;
            if (lo < hi) // not leaf node
            {
                int m = (lo + hi) / 2;
                Update(node * 2 + 1, lo, m, pos, diff);
                Update(node * 2 + 2, m + 1, hi, pos, diff);
            }
        }

        private int[] _data;
        private int[] _segtree;

        public void Test()
        {
            var nums = new int[] { 10, 13, 12, 15, 16, 18 };
            var tree = new SegmentTreeSum(nums);
            Console.WriteLine(tree.Sum(1, 3) == 40);
            Console.WriteLine(tree.Sum(2, 5) == 61);
            tree.Update(2, 20);
            Console.WriteLine(tree.Sum(1, 3) == 48);
        }
    }
}

