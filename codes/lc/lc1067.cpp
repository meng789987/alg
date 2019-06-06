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
 */

class lc1067 {
public:
	int digitsCount(int d, int low, int high) {
		return max(0, count(high, d) - count(low - 1, d));
	}

	int count(int n, int d) {
		if (n < 10) return n >= d ? 1 : 0;
		int res = 0;
		for (int b = 1; b <= n; b *= 10) {
			int divider = b * 10;
			res += n / divider * b + max(0, min(b, n%divider - b * d + 1));
			if (d == 0 && b > 1) res -= b;
		}

		return res;
	}

	void test()
	{
		cout << (1 == digitsCount(0, 1, 10)) << endl;
		cout << (22 == digitsCount(0, 1, 123)) << endl;
		cout << (35 == digitsCount(3, 100, 250)) << endl;
	}
};

