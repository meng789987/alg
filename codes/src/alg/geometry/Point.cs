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

    }
}
