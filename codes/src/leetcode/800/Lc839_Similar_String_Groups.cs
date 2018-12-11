using alg;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: disjoint set, dfs
 * Time(n^2L), Space(n), L is length(A[i])
 */
namespace leetcode
{
    public class Lc839_Similar_String_Groups
    {
        public int NumSimilarGroups(string[] A)
        {
            int n = A.Length;
            var simlists = new List<int>[n];
            for (int i = 0; i < n; i++)
                simlists[i] = new List<int>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                    if (IsSimilar(A[i], A[j]))
                    {
                        simlists[i].Add(j);
                        simlists[j].Add(i);
                    }
            }

            int ret = 0;
            var visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (visited[i]) continue;
                Dfs(simlists, i, visited);
                ret++;
            }

            return ret;
        }

        // they are anagrams
        bool IsSimilar(string a, string b)
        {
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) diff++;

            return diff <= 2;
        }

        void Dfs(List<int>[] lists, int pos, bool[] visited)
        {
            visited[pos] = true;
            foreach (var i in lists[pos])
                if (!visited[i]) Dfs(lists, i, visited);
        }

        public int NumSimilarGroups2(string[] A)
        {
            if (A.Length == 0) return 0;
            if (A.Length < A[0].Length * A[0].Length)
                return NumSimilarGroupsDirect(A);
            return NumSimilarGroupsEnum(A);
        }

        // Time(n^2L), Space(n), L is length(A[i])
        public int NumSimilarGroupsDirect(string[] A)
        {
            int n = A.Length;
            int ret = n;
            var ds = new DisjointSet(n);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                    if (IsSimilar(A[i], A[j]) && ds.Union(i, j))
                        ret--;
            }

            return ret;
        }

        // Time(nL^3), Space(n+L^2), L is length(A[i])
        public int NumSimilarGroupsEnum(string[] A)
        {
            int n = A.Length;
            var ds = new DisjointSet(n);
            var dict = new Dictionary<string, List<int>>();

            // enumerate all neigbors to generate the connected graph
            for (int i = 0; i < n; i++)
            {
                for (int p = 0; p < A[i].Length; p++)
                    for (int q = p; q < A[i].Length; q++)
                    {
                        if (p < q && A[i][p] == A[i][q]) continue;
                        var cs = A[i].ToCharArray();
                        var c = cs[p]; cs[p] = cs[q]; cs[q] = c;
                        var str = new string(cs);
                        if (!dict.ContainsKey(str))
                            dict.Add(str, new List<int>());
                        dict[str].Add(i);
                    }
            }

            int ret = n;
            for (int i = 0; i < n; i++)
            {
                foreach (int j in dict[A[i]])
                    if (ds.Union(i, j)) ret--;
                dict[A[i]].Clear();
            }

            return ret;
        }

        public void Test()
        {
            var a = new string[] { "tars", "rats", "arts", "star" };
            Console.WriteLine(NumSimilarGroups(a) == 2);
            Console.WriteLine(NumSimilarGroups2(a) == 2);
            Console.WriteLine(NumSimilarGroupsDirect(a) == 2);
            Console.WriteLine(NumSimilarGroupsEnum(a) == 2);

            a = new string[] { "kccomwcgcs", "socgcmcwkc", "sgckwcmcoc", "coswcmcgkc", "cowkccmsgc", "cosgmccwkc", "sgmkwcccoc", "coswmccgkc", "kowcccmsgc", "kgcomwcccs" };
            Console.WriteLine(NumSimilarGroups(a) == 5);
            Console.WriteLine(NumSimilarGroups2(a) == 5);
            Console.WriteLine(NumSimilarGroupsDirect(a) == 5);
            Console.WriteLine(NumSimilarGroupsEnum(a) == 5);
        }
    }
}
