using System;
using System.Collections.Generic;
using System.Text;

namespace alg
{
    public class Heap
    {
        public Heap(int[] values)
        {
            Values = values;
            Nodes = new SortedSet<int>(Comparer<int>.Create((i, j) => Values[i] != Values[j] ? Values[i] - Values[j] : i - j));
            for (int i = 0; i < values.Length; i++)
                Nodes.Add(i);
        }

        public int ExtractMin()
        {
            var min = Nodes.Min;
            Nodes.Remove(Nodes.Min);
            return min;
        }

        public void Update(int i, int value)
        {
            Nodes.Remove(i);
            Values[i] = value;
            Nodes.Add(i);
        }

        public int Count
        {
            get { return Nodes.Count; }
        }

        private int[] Values;
        private SortedSet<int> Nodes;
    }

    public class HeapArray
    {
        public HeapArray(int[] values)
        {
            Count = values.Length;
            Values = values;
            Nodes = new int[Count];
            Mapping = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                Nodes[i] = i;
                Mapping[i] = i;
            }

            // build the heap
            for (int i = Count - 1; i > 0; i--)
            {
                int p = (i - 1) / 2;
                int c = 2 * p + 1, rc = 2 * p + 2; // children
                if (rc < Count && Values[Nodes[c]] > Values[Nodes[rc]])
                {
                    c = rc;
                }

                if (c < Count && Values[Nodes[p]] > Values[Nodes[c]])
                    SwapNodes(p, c);
            }
        }

        public int ExtractMin()
        {
            SwapNodes(0, Count - 1);
            Count--;

            HeapifyDown(0);

            return Nodes[Count];
        }

        public void Update(int i, int value)
        {
            bool isDown = value > Values[i];
            Values[i] = value;

            int idxInNodes = Mapping[i];
            if (isDown) HeapifyDown(idxInNodes);
            else HeapifyUp(idxInNodes);
        }

        void SwapNodes(int i, int j)
        {
            int temp = Nodes[i];
            Nodes[i] = Nodes[j];
            Nodes[j] = temp;

            temp = Mapping[Nodes[i]];
            Mapping[Nodes[i]] = Mapping[Nodes[j]];
            Mapping[Nodes[j]] = temp;
        }

        void HeapifyUp(int i)
        {
            for (int c = i; c > 0;)
            {
                int p = (c - 1) / 2;
                if (Values[Nodes[p]] > Values[Nodes[c]])
                {
                    SwapNodes(p, c);
                    c = p;
                }
                else
                {
                    break;
                }
            }
        }

        void HeapifyDown(int i)
        {
            for (int p = i; p < Count;)
            {
                int c = 2 * p + 1, rc = 2 * p + 2; // children
                if (rc < Count && Values[Nodes[c]] > Values[Nodes[rc]])
                {
                    c = rc;
                }

                if (c < Count && Values[Nodes[p]] > Values[Nodes[c]])
                {
                    SwapNodes(p, c);
                    p = c;
                }
                else
                {
                    break;
                }
            }
        }

        public int Count;

        private int[] Values;
        private int[] Nodes;
        private int[] Mapping; // mapping from index in Values to index in Nodes
    }
}
