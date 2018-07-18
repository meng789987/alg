using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: bit, merge sort
 * Time(nlogn), Space(n)
 * merge sort: result[i] is the the count of numbers moved from the right of i-th element to its left,
 *         so we track the moving count when merge.
 * bit: bitree[i] is the count of numbers whose value is sitting at i-th in the sorted array.
 */
namespace leetcode
{
    public class Lc315_Count_of_Smaller_Numbers_After_Self
    {
        public IList<int> CountSmallerInsertSort(int[] nums)
        {
            int n = nums.Length;
            var counts = new int[n];
            var sortedNums = new int[n];
            for (int i = n - 1; i >= 0; i--)
            {
                int j = n - i - 1;
                while (j > 0 && sortedNums[j - 1] >= nums[i])
                {
                    sortedNums[j] = sortedNums[j - 1];
                    j--;
                }
                sortedNums[j] = nums[i];
                counts[i] = j;
            }
            return counts.ToList();
        }

        public IList<int> CountSmallerMergeSort(int[] nums)
        {
            int n = nums.Length;
            var ret = new int[n];
            var indice = new int[n];
            for (int i = 0; i < n; i++)
                indice[i] = i;
            MergeSortRc(nums, indice, 0, n - 1, new int[n], ret);
            return ret.ToList();
        }

        void MergeSortRc(int[] nums, int[] indice, int lo, int hi, int[] aux, int[] res)
        {
            if (lo >= hi) return;
            int m = (lo + hi) / 2;
            MergeSortRc(nums, indice, lo, m, aux, res);
            MergeSortRc(nums, indice, m + 1, hi, aux, res);
            Merge(nums, indice, lo, m, hi, aux, res);
        }

        void Merge(int[] nums, int[] indice, int lo, int m, int hi, int[] aux, int[] res)
        {
            int movedCount = 0;
            Array.Copy(indice, lo, aux, lo, hi - lo + 1);
            for (int i = lo, j = m + 1, k = lo; k <= hi; k++)
            {
                if ((i <= m && (j > hi || nums[aux[i]] <= nums[aux[j]])))
                {
                    res[aux[i]] += movedCount;
                    indice[k] = aux[i++];
                }
                else
                {
                    movedCount++;
                    indice[k] = aux[j++];
                }
            }
        }

        public IList<int> CountSmallerBst(int[] nums)
        {
            int n = nums.Length;
            if (n == 0) return new List<int>();
            var counts = new int[n];
            var root = new Node(nums[n - 1]);
            for (int i = n - 2; i >= 0; i--)
            {
                counts[i] = Add(root, nums[i]);
            }
            return counts.ToList();
        }

        int Add(Node root, int value)
        {
            int count = 0;
            while (true)
            {
                if (root.Value < value)
                {
                    count += root.Count;
                    if (root.Right == null)
                    {
                        root.Right = new Node(value);
                        return count;
                    }
                    root = root.Right;
                }
                else
                {
                    root.Count++;
                    if (root.Left == null)
                    {
                        root.Left = new Node(value);
                        return count;
                    }
                    root = root.Left;
                }
            }
        }

        class Node
        {
            public int Value;
            public Node Left, Right;
            public int Count;

            public Node(int value)
            {
                Value = value;
                Count = 1;
            }
        }

        public IList<int> CountSmallerBit(int[] nums)
        {
            int n = nums.Length;
            var ret = new int[n];

            var clone = nums.ToArray();
            Array.Sort(clone);
            var map = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
                map[clone[i]] = i;

            var bitree = new int[n + 1];
            for (int i = n - 1; i >= 0; i--)
            {
                ret[i] = QueryBit(bitree, map[nums[i]]);
                AddBit(bitree, map[nums[i]] + 1, 1);
            }

            return ret;
        }

        int LowBit(int n)
        {
            return n & (-n);
        }

        void AddBit(int[] bitree, int pos, int diff)
        {
            while (pos < bitree.Length)
            {
                bitree[pos] += diff;
                pos += LowBit(pos);
            }
        }

        int QueryBit(int[] bitree, int pos)
        {
            int r = 0;
            while (pos > 0)
            {
                r += bitree[pos];
                pos -= LowBit(pos);
            }
            return r;
        }

        public IList<int> CountSmallerSegtree(int[] nums)
        {
            var tree = new SegmentCountTree(new SortedSet<int>(nums).ToArray());
            var ret = new int[nums.Length];
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                ret[i] = tree.Query(nums[i]);
                tree.Update(nums[i]);
            }
            return ret;
        }

        class SegmentCountTree
        {
            public SegmentCountTree(int[] nums)
            {
                _root = Build(nums, 0, nums.Length - 1);
            }

            SegmentTreeNode Build(int[] nums, int lo, int hi)
            {
                if (lo > hi) return null;
                var node = new SegmentTreeNode(nums[lo], nums[hi]);
                if (lo == hi) return node;

                int m = (lo + hi) / 2;
                node.Left = Build(nums, lo, m);
                node.Right = Build(nums, m + 1, hi);
                return node;
            }

            public void Update(int value)
            {
                Update(_root, value);
            }

            void Update(SegmentTreeNode node, int value)
            {
                if (node == null || value < node.Min || node.Max < value) return;
                node.Count++;
                Update(node.Left, value);
                Update(node.Right, value);
            }

            // return sum of Count of nodes whose range is less than given value
            public int Query(int value)
            {
                return Query(_root, value);
            }

            int Query(SegmentTreeNode node, int value)
            {
                if (node == null || value <= node.Min) return 0;
                if (node.Max < value) return node.Count;
                return Query(node.Left, value) + Query(node.Right, value);
            }

            SegmentTreeNode _root;

            class SegmentTreeNode
            {
                public int Min, Max;
                public int Count;
                public SegmentTreeNode Left, Right;
                public SegmentTreeNode(int min, int max)
                {
                    Min = min;
                    Max = max;
                }
            }
        }

        public void Test()
        {
            var nums = new int[] { 5, 2, 6, 1 };
            var exp = new int[] { 2, 1, 1, 0 };
            Console.WriteLine(exp.SequenceEqual(CountSmallerInsertSort(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerMergeSort(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerBst(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerBit(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerSegtree(nums)));

            nums = new int[] { 26, 78, 27, 100, 33, 67, 90, 23, 66, 5, 38, 7, 35, 23, 52, 22, 83, 51, 98, 69, 81, 32, 78, 28, 94, 13, 2, 97, 3, 76, 99, 51, 9, 21, 84, 66, 65, 36, 100, 41 };
            exp = new int[] { 10, 27, 10, 35, 12, 22, 28, 8, 19, 2, 12, 2, 9, 6, 12, 5, 17, 9, 19, 12, 14, 6, 12, 5, 12, 3, 0, 10, 0, 7, 8, 4, 0, 0, 4, 3, 2, 0, 1, 0 };
            Console.WriteLine(exp.SequenceEqual(CountSmallerInsertSort(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerMergeSort(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerBst(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerBit(nums)));
            Console.WriteLine(exp.SequenceEqual(CountSmallerSegtree(nums)));
        }
    }
}

