#include "pch.h"

/*
 * tags: dp, sliding window
 * Time(mn), Space(1)
 * find a match then tighten its left bound of the window, and restart from next of its left bound.
 */

class lc0727 {
public:
	string minWindow(string s, string t) {
		int minidx, minlen = INT_MAX;
		for (int i = 0, j = 0; i < s.size(); i++) {
			if (s[i] == t[j]) j++;
			if (j < t.size()) continue;

			// found a match, tighten the left bound
			int end = i;
			for (j--; j >= 0; i--)
				if (s[i] == t[j]) j--;
			if (minlen > end - i) {
				minlen = end - i;
				minidx = i + 1;
			}

			i++; // restart from the next
			j = 0;
		}

		return minlen == INT_MAX ? "" : s.substr(minidx, minlen);
	}

	void test()
	{
		cout << ("bcde" == minWindow("abcdebdde", "bde")) << endl;
		cout << ("mccqouqadqtm" == minWindow("cnhczmccqouqadqtmjjzl", "mm")) << endl;
	}
};

