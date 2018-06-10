using System;
using System.Collections.Generic;
using System.Text;

namespace alg
{
    public class DisjoinSet
    {
        public DisjoinSet(int n)
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

        public void Union(int i, int j)
        {
            var ip = _parents[i];
            var jp = _parents[j];
            _parents[ip] = jp; // can also set _parents[jp] = ip, depends on what it needs

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
