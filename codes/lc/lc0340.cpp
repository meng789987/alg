#include "pch.h"

/*
 * tags: two pointers, sliding windows
 * Time(n), Space(1)
 * count the entering and leaving char, if the count is 0 then the total number of distinct chars increment.
 * ref: lc159
 */

class lc0340 {
public:
	int lengthOfLongestSubstringKDistinct(string s, int k) {
		int res = 0;
		int counts[256] = { 0 };
		for (int i = 0, j = 0, cnt = 0; j < s.size(); j++) {
			if (++counts[s[j]] == 1) cnt++;
			if (cnt > k && --counts[s[i++]] == 0) cnt--;
			res = max(res, j - i + 1);
		}

		return res;
	}

	void test()
	{
		cout << (3 == lengthOfLongestSubstringKDistinct("eceba", 2)) << endl;
		cout << (2 == lengthOfLongestSubstringKDistinct("aa", 1)) << endl;
	}
};

