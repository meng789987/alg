using System;
using System.Collections.Generic;
using System.Linq;

namespace alg
{
    public static class Extensions
    {
        public static bool SameSet<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;

            var al = a.ToList();
            var bl = b.ToList();
            al.Sort();
            bl.Sort();

            return al.SequenceEqual(bl);
        }

        public static bool SameSet<T>(this IList<IList<T>> a, IList<IList<T>> b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;

            var al = a.ToList();
            var bl = b.ToList();
            al.Sort((i, j) => i.Compare(j));
            bl.Sort((i, j) => i.Compare(j));
            var comp = Comparer<IList<T>>.Create((i, j) => i.Compare(j, Comparer<T>.Default));

            return al.Compare(bl, comp) == 0;
        }

        public static bool SameWith<T>(this T[,] a, T[,] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1)) return false;

            var comparer = Comparer<T>.Default;
            var ia = a.GetEnumerator();
            var ib = b.GetEnumerator();
            ia.Reset();
            ib.Reset();
            while (ia.MoveNext())
            {
                if (!ib.MoveNext()) return false;
                int c = comparer.Compare((T)ia.Current, (T)ib.Current);
                if (c != 0) return false;
            }
            return ib.MoveNext() ? false : true;
        }

        public static int Compare<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return Compare(a, b, Comparer<T>.Default);
        }

        public static int Compare<T>(this IEnumerable<T> a, IEnumerable<T> b, IComparer<T> comparer)
        {
            if (a == null && b == null) return 0;
            if (a == null) return -1;
            if (b == null) return 1;

            using (var ia = a.GetEnumerator())
            using (var ib = b.GetEnumerator())
            {
                while (ia.MoveNext())
                {
                    if (!ib.MoveNext()) return 1;
                    int c = comparer.Compare(ia.Current, ib.Current);
                    if (c != 0) return c;
                }
                return ib.MoveNext() ? -1 : 0;
            }
        }
    }
}
