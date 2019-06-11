#include "pch.h"

/*
 * tags: two pointers
 * Time(n), Space(1)
 * count the entering and leaving char, if the count is 0 then the total number of distinct chars increment.
 * ref: lc340
 */

class lc0159 {
public:
	int lengthOfLongestSubstringTwoDistinct(string s) {
		int res = 0, counts[256] = { 0 }, cnt = 0;
		for (size_t i = 0, j = 0; i < s.size(); i++) {
			if (++counts[s[i]] == 1) cnt++;
			if (cnt > 2 && --counts[s[j++]] == 0) cnt--;
			res = max(res, (int)(i - j + 1));
		}

		return res;
	}

	void test()
	{
		cout << (3 == lengthOfLongestSubstringTwoDistinct("eceba")) << endl;
		cout << (5 == lengthOfLongestSubstringTwoDistinct("ccaabbb")) << endl;
	}
};

