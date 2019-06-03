#include "pch.h"

/*
 * tags: math
 * Time(logn), Space(1)
 * convert 10-based to 9-based number
 *  sum(a[i]*10^i) = sum(b[j]*9^j)
 * it also works for removing 8, the extra step is to replace '8' with '9' literally.
 * e.g. if the 9-based (without 9) number is 75824, then 9-based (without 8) is 75924
 */

class lc0660 {
public:
	int newInteger(int n) {
		int res = 0;
		for (long b = 1; n > 0; b *= 10) {
			res += (n % 9) * b;
			n /= 9;
		}

		return res;
	}

	void test()
	{
		cout << (11 == newInteger(10)) << endl;
		cout << (1204602 == newInteger(652943)) << endl;
		cout << (2052305618 == newInteger(800000000)) << endl;
	}
};

