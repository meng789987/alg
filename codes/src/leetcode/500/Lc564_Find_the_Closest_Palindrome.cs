
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using alg;

/*
 * tags: array
 * Time(n), Space(n)
 */
namespace leetcode
{
    public class Lc564_Find_the_Closest_Palindrome
    {
        public string NearestPalindromic(string n)
        {
            var pcs = n.ToCharArray();
            for (int i = 0, j = pcs.Length - 1; i < j; i++, j--)
                pcs[j] = pcs[i];
            var ps = new string(pcs);
            if (ps == n)
            {
                if (pcs.Length % 2 == 1)
                {
                    char c = pcs[pcs.Length / 2];
                    pcs[pcs.Length / 2] = c == '0' ? '1' : (char)(c - 1);
                }
                else
                {
                    char c = pcs[pcs.Length / 2];
                    pcs[pcs.Length / 2] = pcs[pcs.Length / 2 - 1] = c == '0' ? '1' : (char)(c - 1);
                }
                ps = new string(pcs);
            }

            char[] pcs2;
            if (n[0] == '9')
            {
                pcs2 = new char[n.Length + 1];
                Array.Fill(pcs2, '0');
                pcs2[0] = pcs2[pcs2.Length - 1] = '1';
            }
            else
            {
                pcs2 = new char[n.Length];
                Array.Fill(pcs2, '0');
                pcs2[0] = pcs2[pcs2.Length - 1] = (char)(n[0] + 1);
            }
            var ps2 = new string(pcs2);

            var pcs3 = new char[n.Length - 1];
            Array.Fill(pcs3, '9');
            var ps3 = new string(pcs3);
            if (ps3 == "") ps3 = "99";

            //var bn = BigInteger.Parse(n);
            //var b = BigInteger.Parse(ps);
            //var b2 = BigInteger.Parse(ps2);
            //return BigInteger.Abs(b - bn) > BigInteger.Abs(b2 - bn) ? ps2 : ps;
            string smallerps, smallerdiff;
            var diff = Diff(ps, n);
            var diff2 = Diff(ps2, n);
            int c12 = CompareTo(diff, diff2);
            if (c12 < 0 || (c12 == 0 && CompareTo(ps, ps2) < 0))
            {
                smallerdiff = diff;
                smallerps = ps;
            }
            else
            {
                smallerdiff = diff2;
                smallerps = ps2;
            }


            var diff3 = Diff(ps3, n);
            int c3 = CompareTo(smallerdiff, diff3);
            return c3 < 0 || (c3 == 0 && CompareTo(smallerps, ps3) < 0) ? smallerps : ps3;
        }

        string Diff(string a, string b)
        {
            string big = "", small = "";
            if (a.Length != b.Length)
            {
                if (a.Length > b.Length)
                {
                    big = a;
                    small = b;
                }
                else
                {
                    big = b;
                    small = a;
                }
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        if (a[i] > b[i])
                        {
                            big = a;
                            small = b;
                        }
                        else
                        {
                            big = b;
                            small = a;
                        }
                        break;
                    }
                }
            }

            var diff = new char[big.Length];
            int idx = diff.Length;
            for (int carry = 0, i = big.Length - 1, j = small.Length - 1; i >= 0; i--, j--)
            {
                int val = big[i] - (j >= 0 ? small[j] : '0') - carry;
                if (val < 0)
                {
                    val += 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                diff[--idx] = (char)(val + '0');
            }

            while (idx < diff.Length && diff[idx] == '0') idx++;

            return new string(diff, idx, diff.Length - idx);
        }

        int CompareTo(string a, string b)
        {
            if (a.Length != b.Length) return a.Length - b.Length;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) return a[i] - b[i];
            return 0;
        }


        public void Test()
        {
            Console.WriteLine(NearestPalindromic("1") == "0");
            Console.WriteLine(NearestPalindromic("9") == "8");
            Console.WriteLine(NearestPalindromic("10") == "9");
            Console.WriteLine(NearestPalindromic("123") == "121");
            Console.WriteLine(NearestPalindromic("1283") == "1331");
            Console.WriteLine(NearestPalindromic("19999") == "20002");
        }
    }
}

