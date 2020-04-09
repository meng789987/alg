using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: segment tree
 * Space(n); Build: Time(n), Query/Update: Time(logn)
 * Depending on its purpose, tree node stores the sum/max/gcd/lcd of its data range. If a node is the sum of data range[i..j], 
 * then its left is sum of data range[i..m], where m=(i+j)/2, and its right is sum of data range[m+1..j].
 * e.g. for data [10, 13, 12, 15, 16, 18], it would be:
 *                84[0..5]
 *         35[0..2]            49[3..5]
 *   23[0..1]             31[3..4]    
 * 10[0]  13[1]  12[2]  15[3]  16[4]  18[5]
 * 
 * use an array with double length, for node i its left is 2*i and right is 2*i+1. 
 * leaf node[n+i]=data[i]. internal node[i] = node[2*i] + node[2*i+1]. node[0] is unused.
 *                84[0..5]
 *                       50[2..5]
 *   23[0..1]      27[2..3]      34[4..5]
 * 10[0]  13[1]  12[2]  15[3]  16[4]  18[5]
 * when update data[i], update from leaf node[i+n] to root;
 * when query sum[i..j], sum node[i++] if node[i] is right child, and sum node[j--] if node[j] is left child, until i>j or i==0.
 */
namespace alg.tree
{
    public class SegmentTree
    {
        class SegmentSumTree
        {
            public SegmentSumTree(int[] data)
            {
                _data = data;
                _root = Build(0, data.Length - 1);
            }

            Node Build(int lo, int hi)
            {
                if (lo > hi) return null;
                var node = new Node(lo, hi);
                if (lo == hi)
                {
                    node.Sum = _data[lo];
                    return node;
                }

                int m = (lo + hi) / 2;
                node.Left = Build(lo, m);
                node.Right = Build(m + 1, hi);
                node.Sum = node.Left.Sum + node.Right.Sum;
                return node;
            }

            // set data[pos] = newValue
            public void Update(int pos, int newValue)
            {
                Update(_root, pos, newValue - _data[pos]);
                _data[pos] = newValue;
            }

            void Update(Node node, int pos, int diff)
            {
                if (node == null || pos < node.Lo || node.Hi < pos) return;
                node.Sum += diff;
                Update(node.Left, pos, diff);
                Update(node.Right, pos, diff);
            }

            public int Query(int lo, int hi)
            {
                return Query(_root, lo, hi);
            }

            // sum of data[lo..hi]
            int Query(Node node, int lo, int hi)
            {
                if (node == null || hi < node.Lo || node.Hi < lo) return 0; // no overlap
                if (lo <= node.Lo && node.Hi <= hi) return node.Sum;        // node is fully covered
                return Query(node.Left, lo, hi) + Query(node.Right, lo, hi);  // node is partially covered
            }

            int[] _data;
            Node _root;

            class Node
            {
                public int Lo, Hi;
                public int Sum;
                public Node Left, Right;
                public Node(int lo, int hi)
                {
                    Lo = lo;
                    Hi = hi;
                }
            }
        }

        class SegmentSumTreeArray
        {
            public SegmentSumTreeArray(int[] data)
            {
                n = data.Length;
                _segtree = new int[2 * n];
                Array.Copy(data, 0, _segtree, n, n);
                for (int i = n - 1; i > 0; i--)  // bottom up
                    _segtree[i] = _segtree[2 * i] + _segtree[2 * i + 1];
            }

            // set data[i] = newValue
            public void Update(int i, int newValue)
            {
                i += n;
                _segtree[i] = newValue;
                while (i > 0)
                {
                    i /= 2;
                    _segtree[i] = _segtree[2 * i] + _segtree[2 * i + 1];
                }
            }

            // sum of data[i..j]
            public int Query(int i, int j)
            {
                int sum = 0;
                for (i += n, j += n; 0 < i && i <= j; i /= 2, j/= 2)
                {
                    if (i % 2 == 1) // node i is right child, add itself instead of its parent
                        sum += _segtree[i++]; // move to parent sibling
                    if (j % 2 == 0) // node j is left child, add itself instead of its parent
                        sum += _segtree[j--]; // move to parent sibling
                }
                return sum;
            }

            private int n;
            private int[] _segtree;
        }

        public void Test()
        {
            var nums = new int[] { 10, 13, 12, 15, 16, 18 };
            var treea = new SegmentSumTreeArray(nums);
            Console.WriteLine(treea.Query(1, 3) == 40);
            Console.WriteLine(treea.Query(2, 5) == 61);
            treea.Update(2, 20);
            Console.WriteLine(treea.Query(1, 3) == 48);

            nums = new int[] { 10, 13, 12, 15, 16, 18 };
            var tree = new SegmentSumTree(nums);
            Console.WriteLine(tree.Query(1, 3) == 40);
            Console.WriteLine(tree.Query(2, 5) == 61);
            tree.Update(2, 20);
            Console.WriteLine(tree.Query(1, 3) == 48);
        }
    }
}

