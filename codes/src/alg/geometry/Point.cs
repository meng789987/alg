using System;
using System.Collections.Generic;
using System.Text;

namespace alg.geometry
{
    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;

            var that = obj as Point;
            return this.x == that.x && this.y == that.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static Point[] FromMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            var res = new Point[n];
            for (int i = 0; i < n; i++)
                res[i] = new Point(matrix[i, 0], matrix[i, 1]);
            return res;
        }
    }
}
