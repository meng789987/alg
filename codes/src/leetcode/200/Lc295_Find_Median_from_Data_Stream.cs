
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: bst
 * use two heaps to mimic self balanced bst
 */
namespace leetcode
{
    public class Lc295_Find_Median_from_Data_Stream
    {
        public void AddNum(int num)
        {
            // add the num to lo first
            if (_lo.ContainsKey(num))
                _lo[num]++;
            else _lo.Add(num, 1);

            // move the max item in lo to hi
            {
                int loMax = _lo.Keys.First();
                if (_hi.ContainsKey(loMax))
                    _hi[loMax]++;
                else _hi.Add(loMax, 1);
                if (_lo[loMax] > 1)
                    _lo[loMax]--;
                else _lo.Remove(loMax);
                _hiCnt++;
            }

            // balance lo and hi by moving the min item in hi to lo
            if (_loCnt < _hiCnt)
            {
                int hiMin = _hi.Keys.First();
                if (_lo.ContainsKey(hiMin))
                    _lo[hiMin]++;
                else _lo.Add(hiMin, 1);
                if (_hi[hiMin] > 1)
                    _hi[hiMin]--;
                else _hi.Remove(hiMin);
                _loCnt++;
                _hiCnt--;
            }

        }

        public double FindMedian()
        {
            if (_loCnt > _hiCnt)
                return _lo.Keys.First();
            return ((double)_lo.Keys.First() + _hi.Keys.First()) / 2.0;
        }

        int _loCnt, _hiCnt;
        SortedDictionary<int, int> _lo = new SortedDictionary<int, int>((Comparer<int>.Create((a, b) => b - a)));
        SortedDictionary<int, int> _hi = new SortedDictionary<int, int>();

        public void Test()
        {
            AddNum(6);
            Console.WriteLine(FindMedian() == 6);
            AddNum(10);
            Console.WriteLine(FindMedian() == 8);
            AddNum(2);
            Console.WriteLine(FindMedian() == 6);
            AddNum(6);
            Console.WriteLine(FindMedian() == 6);
            AddNum(5);
            Console.WriteLine(FindMedian() == 6);
            AddNum(0);
            Console.WriteLine(FindMedian() == 5.5);
        }
    }
}

