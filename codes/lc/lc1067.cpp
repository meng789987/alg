#include "pch.h"

/*
 * tags: math
 * Time(logn), Space(1)
 * ref lc0233, 
 * < 10:   1
 * < 100:  x1 and 1x, x=[0..9]
 * < 1000: xx1 and x1x and 1xx
 * For n,
 * No. of 1s in ones place: n/10 + min(1, n%10)
 * No. of 1s in tens place: n/100*10 + min(10, n%100 - 10 + 1)
 * No. of 1s in hundrens place: n/1000*100 + min(100, n%1000 - 100 + 1)
 * 
 * extend to digit d (see lc1067. Digit Count in Range):
 * No. of d in ones place: n/10 + min(1, n%10 - d + 1)
 * No. of d in tens place: n/100*10 + min(10, n%100 - 10*d + 1)
 * No. of d in hundrens place: n/1000*100 + min(100, n%1000 - 100*d + 1)
 * 
 * for digit zero, skip those numbers leading zero, except for 0 itself:
 * No. of d in ones place: n/10 + min(1, n%10 - d + 1)
 * No. of d in tens place: (n/100-1)*10 + min(10, n%100 - 10*d + 1)
 * No. of d in hundrens place: (n/1000-1)*100 + min(100, n%1000 - 100*d + 1)
 */

class lc1067 {
public:
	int digitsCount(int d, int low, int high) {
		return max(0, count(high, d) - count(low - 1, d));
	}

	int count(int n, int d) {
		if (n < 10) return n >= d ? 1 : 0;
		long ret = 0;
		for (long b = 1; b <= n; b *= 10)
		{
			long divider = b * 10;
			ret += n / divider * b + max(0L, min(b, n % divider - b * d + 1));
			if (d == 0 && b > 1) ret -= b;
		}

		return (int)ret;
	}

	void test()
	{
		cout << (1 == digitsCount(0, 1, 10)) << endl;
		cout << (22 == digitsCount(0, 1, 123)) << endl;
		cout << (35 == digitsCount(3, 100, 250)) << endl;
	}
};

