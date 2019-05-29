using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: binary index tree
 * Space(n); Build: Time(n), Query/Update: Time(logn)
 * use an array, tree[i] = sum(data[i-(i&-i)..i-1]); 
 * tree index 0 is not used, tree index is 1 more than data index;
 * e.g. for data [10, 13, 12, 15, 16, 18], it would be [0, 10, 23, 12, 50, 16, 34]
 */
namespace alg.tree
{
    public class BinaryIndexTree
    {
        public BinaryIndexTree() { }
        public BinaryIndexTree(int[] data)
        {
            _data = new int[data.Length];
            _bitree = new int[data.Length + 1];

            // this also works and is simple, but time takes O(nlogn)
            //for (int i = 0; i < data.Length; i++)
            //    Update(i, data[i]);

            for (int i = 1; i <= data.Length; i++)
            {
                _data[i - 1] = data[i - 1];
                _bitree[i] += data[i - 1];
                if (i + (i & (-i)) < _bitree.Length)
                    _bitree[i + (i & (-i))] += _bitree[i];
            }
        }

        // sum of data[i..j]
        public int Sum(int i, int j)
        {
            return SumBit(j + 1) - SumBit(i);
        }

        // set data[pos] = newValue
        public void Update(int pos, int newValue)
        {
            var diff = newValue - _data[pos];
            _data[pos] = newValue;

            for (int i = pos + 1; i < _bitree.Length;)
            {
                _bitree[i] += diff;
                i += i & (-i); // double the last set bit
            }
        }

        private int[] _data;
        private int[] _bitree;

        private int SumBit(int pos)
        {
            int sum = 0;
            while (pos > 0)
            {
                sum += _bitree[pos];
                pos -= pos & (-pos); // clear the last set bit
            }
            return sum;
        }

        public void Test()
        {
            var nums = new int[] { 10, 13, 12, 15, 16, 18 };
            var tree = new BinaryIndexTree(nums);
            Console.WriteLine(tree.Sum(1, 3) == 40);
            Console.WriteLine(tree.Sum(2, 5) == 61);
            tree.Update(2, 20);
            Console.WriteLine(tree.Sum(1, 3) == 48);

            var matrix = new int[,] {
                  {3, 0, 1, 4, 2},
                  {5, 6, 3, 2, 1},
                  {1, 2, 0, 1, 5},
                  {4, 1, 0, 1, 7},
                  {1, 0, 3, 0, 5} };
            var tree2 = new BinaryIndexTree2D(matrix);
            Console.WriteLine(tree2.Sum(2, 1, 4, 3) == 8);
            tree2.Update(3, 2, 2);
            Console.WriteLine(tree2.Sum(2, 1, 4, 3) == 10);
        }
    }

    class BinaryIndexTree2D
    {
        public BinaryIndexTree2D(int[,] data)
        {
            _m = data.GetLength(0);
            _n = data.GetLength(1);
            _data = new int[_m, _n];
            _tree = new int[_m + 1, _n + 1];

            // this also works and is simple, but time takes O(mnlogmlogn)
            //for (int i = 0; i < _m; i++)
            //{
            //    for (int j = 0; j < _n; j++)
            //        Update(i, j, data[i, j]);
            //}

            for (int i = 1; i <= _m; i++)
            {
                var row = new int[_n + 1];
                for (int j = 1; j <= _n; j++)
                {
                    _data[i - 1, j - 1] = data[i - 1, j - 1];
                    row[j] += data[i - 1, j - 1];
                    if (j + (j & -j) <= _n)
                        row[j + (j & -j)] += row[j]; // accumulate this row
                    _tree[i, j] += row[j];
                    if (i + (i & -i) <= _m)
                        _tree[i + (i & -i), j] += _tree[i, j];
                }
            }
        }

        public void Update(int row, int col, int value)
        {
            int diff = value - _data[row, col];
            _data[row, col] = value;
            // go forward to update
            for (int i = row + 1; i <= _m; i += i & (-i))
            {
                for (int j = col + 1; j <= _n; j += j & (-j))
                    _tree[i, j] += diff;
            }
        }

        public int Sum(int row1, int col1, int row2, int col2)
        {
            return Sum(row2 + 1, col2 + 1) + Sum(row1, col1) - Sum(row2 + 1, col1) - Sum(row1, col2 + 1);
        }

        private int Sum(int row, int col)
        {
            int sum = 0;
            // go back to sum
            for (int i = row; i > 0; i -= i & (-i))
            {
                for (int j = col; j > 0; j -= j & (-j))
                    sum += _tree[i, j];
            }

            return sum;
        }

        int _m, _n;
        int[,] _data, _tree;
    }
}
