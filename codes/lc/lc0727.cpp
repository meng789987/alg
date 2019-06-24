#include "pch.h"

/*
 * tags: dp, sliding window
 * Time(mn), Space(1)
 * 1. sliding window: find a match then tighten its left bound of the window, and restart from next of its left bound.
 * 2. dp: dp[i, j] is -1(invalid) if s[0..i] can't cover t[0..j], otherwise it is the max value(index) so s[dp[i, j]..i] covers t[0..j].
      so the answer is min of s[dp[i, n-1]..i], i=[0..n-1], the recursive formula is
 *    dp[i, j] = dp[i-1, j-1] if s[i] == t[j], or
 *               dp[i-1, j]
 *    base case: dp[0][0] = s[0] == t[0] ? 0 : -1 (invalid);
 *               dp[i, 0] = s[i]==t[0] ? i : dp[i-1][0], i > 0
 *               dp[0, j] = -1, j > 0.
 */

class lc0727 {
public:
	string minWindow(string s, string t) {
		int minidx, minlen = INT_MAX;
		for (int i = 0, j = 0; i < (int)s.size(); i++) {
			if (s[i] == t[j]) j++;
			if (j < (int)t.size()) continue;

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

	string minWindow1(string s, string t) {
		size_t m = s.size(), n = t.size();
		vector<vector<int>> dp(m, vector<int>(n, -1));
		dp[0][0] = s[0] == t[0] ? 0 : -1;
		for (size_t i = 1; i < m; i++)
			dp[i][0] = s[i] == t[0] ? i : dp[i - 1][0];

		for (size_t i = 1; i < m; i++) {
			for (size_t j = 1; j < n; j++)
				dp[i][j] = s[i] == t[j] ? dp[i - 1][j - 1] : dp[i - 1][j];
		}

		size_t minidx, minlen = UINT_MAX;
		for (size_t i = 0; i < m; i++) {
			if (dp[i][n - 1] != -1 && minlen > i - dp[i][n - 1] + 1) {
				minlen = i - dp[i][n - 1] + 1;
				minidx = dp[i][n - 1];
			}
		}

		return minlen == UINT_MAX ? "" : s.substr(minidx, minlen);
	}

	void test()
	{
		cout << ("bcde" == minWindow("abcdebdde", "bde")) << endl;
		cout << ("mccqouqadqtm" == minWindow("cnhczmccqouqadqtmjjzl", "mm")) << endl;
	}
};

