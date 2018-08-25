using System;
using System.Collections.Generic;
using System.Text;

namespace alg
{
    public class DisjointSet
    {
        public DisjointSet(int n)
        {
            _parents = new int[n];
            for (int i = 0; i < n; i++)
                _parents[i] = i;

            //_ranks = new int[n];
            //Array.Fill(_ranks, 1);
        }

        public int Find(int i)
        {
            if (_parents[i] != i)
                _parents[i] = Find(_parents[i]); // compress path
            return _parents[i];
        }

        public bool Union(int i, int j)
        {
            var pi = Find(i);
            var pj = Find(j);
            if (pi == pj) return false;
            _parents[pi] = pj; // can also set _parents[jp] = ip, depends on what it needs
            return true;

            // use ranks to improve efficiency
            //if (ip == jp) return;
            //if (_ranks[ip] < _ranks[jp]) _parents[ip] = jp;
            //else if (_ranks[ip] > _ranks[jp]) _parents[jp] = ip;
            //else
            //{
            //    _parents[ip] = jp;
            //    _ranks[jp]++;
            //}
        }

        private int[] _parents;
        //private int[] _ranks;
    }
}
