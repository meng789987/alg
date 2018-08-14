
using System;
using System.Collections.Generic;
using System.Linq;
using alg;

/*
 * tags: bst, heap
 * use two heaps to mimic self balanced bst
 */
namespace leetcode
{
    public class Lc295_Find_Median_from_Data_Stream
    {
        public void AddNum(int num)
        {
            // add the num to lo first
            _lo.Add(num);

            // move the max item in lo to hi
            _hi.Add(_lo.Poll());

            // balance lo and hi by moving the min item in hi to lo
            if (_lo.Count < _hi.Count)
                _lo.Add(_hi.Poll());
        }

        public void AddNum2(int num)
        {
            if (_lo.Count == 0 || num <= _lo.Peek())
            {
                _lo.Add(num);
                if (_lo.Count > _hi.Count + 1)
                    _hi.Add(_lo.Poll());
            }
            else
            {
                _hi.Add(num);
                if (_lo.Count < _hi.Count)
                    _lo.Add(_hi.Poll());
            }
        }

        public double FindMedian()
        {
            if (_lo.Count > _hi.Count)
                return _lo.Peek();
            return ((double)_lo.Peek() + _hi.Peek()) / 2.0;
        }

        Heap _lo = new Heap(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        Heap _hi = new Heap(Comparer<int>.Default);


        class Heap
        {
            public Heap(IComparer<int> comp)
            {
                this.Map = new SortedDictionary<int, int>(comp);
            }

            public void Add(int num)
            {
                if (Map.ContainsKey(num)) Map[num]++;
                else Map[num] = 1;
                _Count++;
            }

            public int Poll()
            {
                var pair = Map.First();
                if (pair.Value == 1) Map.Remove(pair.Key);
                else Map[pair.Key]--;
                _Count--;
                return pair.Key;
            }

            public int Peek()
            {
                return Map.First().Key;
            }

            public int Count { get { return _Count; } }

            private SortedDictionary<int, int> Map;
            private int _Count;
        }

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

