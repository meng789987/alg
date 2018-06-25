using System;

/*
 * tags: bit
 * see: http://graphics.stanford.edu/~seander/bithacks.html
 */
namespace alg.math
{
    public class BitManipulation
    {
        /*
         * n&(n-1) will clear the least signaficant 1-bit of n
         */
        public int BitCount(int n)
        {
            int ret = 0;
            while (n != 0)
            {
                n &= n - 1;
                ret++;
            }
            return ret;

            //Counting bits set by lookup table
            //static const unsigned char BitsSetTable256[256] =
            //{
            //#   define B2(n) n,     n+1,     n+1,     n+2
            //#   define B4(n) B2(n), B2(n+1), B2(n+1), B2(n+2)
            //#   define B6(n) B4(n), B4(n+1), B4(n+1), B4(n+2)
            //    B6(0), B6(1), B6(1), B6(2)
            //};

            //unsigned int v; // count the number of bits set in 32-bit value v
            //unsigned int c; // c is the total bits set in v

            //// Option 1:
            //c = BitsSetTable256[v & 0xff] +
            //    BitsSetTable256[(v >> 8) & 0xff] +
            //    BitsSetTable256[(v >> 16) & 0xff] +
            //    BitsSetTable256[v >> 24];

            //// Option 2:
            //unsigned char* p = (unsigned char*) &v;
            //c = BitsSetTable256[p[0]] +
            //    BitsSetTable256[p[1]] +
            //    BitsSetTable256[p[2]] +
            //    BitsSetTable256[p[3]];


            //// To initially generate the table algorithmically:
            //BitsSetTable256[0] = 0;
            //for (int i = 0; i < 256; i++)
            //{
            //    BitsSetTable256[i] = (i & 1) + BitsSetTable256[i / 2];
            //}
            //On July 14, 2009 Hallvard Furuseth suggested the macro compacted table.
        }

        /*
         * clear all bits except the least one
         */
        public int LeastSignaficantBit(int n)
        {
            return n & (-n);
        }

        public void Test()
        {
            Console.WriteLine(BitCount(0b101101) == 4);
            Console.WriteLine(BitCount(0b101011011101) == 8);
            Console.WriteLine(LeastSignaficantBit(0b101011011100) == 0b100);
        }
    }
}
