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
        }
    }
}
