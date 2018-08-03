
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: bs
 * math: According to geometric sequence, b^0 + b^1 + ... + b^(w-1) = (b^w - 1) / (b - 1), that is the num n=11111 with width w and base b.
 * b^(w-1) < n < b^w, so b should be [n^(1/w), n^(1/(w-1)], then binary search; or
 * b^(w-1) < n < (b+1)^(w-1), so b=n^(1/(w-1)) is what we want to verify.
 */
namespace leetcode
{
    public class Lc483_Smallest_Good_Base
    {
        public string SmallestGoodBaseBs(string n)
        {
            long target = long.Parse(n);

            for (int width = (int)(Math.Log(target + 1) / Math.Log(2)); width > 0; width--)
            {
                long lo = (long)(Math.Pow(target, 1.0 / width));
                long hi = (long)(Math.Pow(target, 1.0 / (width - 1)));
                var r = Test(target, lo, hi, width);
                if (r != null) return r;
            }

            return (target - 1).ToString();
        }

        string Test(long target, long lo, long hi, int width)
        {
            while (lo <= hi)
            {
                var m = lo + (hi - lo) / 2;
                long sum = 0;
                for (int i = 0; i < width; i++) sum = sum * m + 1;

                if (sum == target) return m.ToString();
                if (sum < target) lo = m + 1;
                else hi = m - 1;
            }

            return null;
        }

        public string SmallestGoodBase(string n)
        {
            long target = long.Parse(n);
            var bigTarget = new BigInteger(target);

            for (int width = (int)Math.Log(target + 1, 2); width > 0; width--)
            {
                long b = (long)(Math.Pow(target, 1.0 / (width - 1)));
                if (BigInteger.Pow(b, width) - 1 == bigTarget * (b - 1))
                    return b.ToString();
            }

            return (target - 1).ToString();
        }


        public void Test()
        {
            Console.WriteLine(SmallestGoodBaseBs("13") == "3");
            Console.WriteLine(SmallestGoodBase("13") == "3");
            Console.WriteLine(SmallestGoodBaseBs("4681") == "8");
            Console.WriteLine(SmallestGoodBase("4681") == "8");
            Console.WriteLine(SmallestGoodBaseBs("15") == "2");
            Console.WriteLine(SmallestGoodBase("15") == "2");
            Console.WriteLine(SmallestGoodBaseBs("1000000000000000000") == "999999999999999999");
            Console.WriteLine(SmallestGoodBase("1000000000000000000") == "999999999999999999");
            Console.WriteLine(SmallestGoodBaseBs("625318600331879360") == "625318600331879359");
            Console.WriteLine(SmallestGoodBase("625318600331879360") == "625318600331879359");
            Console.WriteLine(SmallestGoodBaseBs("470988884881403701") == "686286299");
            Console.WriteLine(SmallestGoodBase("470988884881403701") == "686286299");
        }
    }
}

