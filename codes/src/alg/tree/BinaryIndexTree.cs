using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: binary index tree
 * Space(n); Build: Time(nlogn), Query/Update: Time(logn)
 * use an array, the i-th element holding the sum of some data is based on the 1-bit of i.
 * e.g. for data [10, 13, 12, 15, 16, 18], it would be:
 *                84[0..5]
 *         35[0..2]            49[3..5]
 *   23[0..1]             31[3..4]    
 * 10[0]  13[1]  12[2]  15[3]  16[4]  18[5]
 * 
 * use an array, for node i its left is 2*i and right is 2*i+1. root is a[1]
 *                84[0..7]
 *          50[0..3]
 *   23[0..1]      27[2..3]      34[4..5]
 * 10[0]  13[1]  12[2]  15[3]  16[4]  18[5]  0[6]  0[7]
 */
namespace alg.tree
{
    public class BinaryIndexTree
    {
        public BinaryIndexTree() { }
        public BinaryIndexTree(int[] data)
        {
            _data = data.ToArray();
            _bitree = new int[data.Length + 1];

            for (int i = 0; i < data.Length; i++)
                AddBit(i + 1, data[i]);
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
            AddBit(pos + 1, diff);
        }

        private int[] _data;
        private int[] _bitree;

        private void AddBit(int pos, int diff)
        {
            while (pos < _bitree.Length)
            {
                _bitree[pos] += diff;
                pos += pos & (-pos); // double the last set bit
            }
        }

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
            for (int i = 0; i < _m; i++)
            {
                for (int j = 0; j < _n; j++)
                    Update(i, j, data[i, j]);
            }
        }

        public void Update(int row, int col, int value)
        {
            int diff = value - _data[row, col];
            _data[row, col] = value;
            // go up to update sum
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
            // go down to sum
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
