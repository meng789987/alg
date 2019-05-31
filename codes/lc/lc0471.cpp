#include "pch.h"

/*
 * tags: dp
 * Time(n^3), Space(n^2)
 * dp[i, j] is the shortest encoding of s[i..j], and its base is bases[i, j], count is counts[i, j]
 * construct dp[i, j] from dp[i, k] and dp[k+1, j], where k=[i..j-1], if their bases are same they can be merged
 */

class lc0471 {
public:
	string encode(string s) {
		int n = s.size();
		vector<vector<string>> dp(n, vector<string>(n)), bases(n, vector<string>(n));
		vector<vector<int>> counts(n, vector<int>(n));

		for (int len = 1; len <= n; len++) {
			for (int i = 0; i <= n - len; i++) {
				int j = i + len - 1;
				dp[i][j] = bases[i][j] = s.substr(i, len);
				counts[i][j] = 1;

				for (int k = i; k < j; k++) {
					if (dp[i][j].size() > dp[i][k].size() + dp[k + 1][j].size())
						dp[i][j] = dp[i][k] + dp[k + 1][j];
					if (bases[i][k] == bases[k + 1][j]) {
						bases[i][j] = bases[i][k];
						counts[i][j] = counts[i][k] + counts[k + 1][j];
					}
				}

				if (counts[i][j] == 1) {
					bases[i][j] = dp[i][j];
				}
				else {
					string tmp = to_string(counts[i][j]) + '[' + bases[i][j] + ']';
					if (dp[i][j].size() > tmp.size())
						dp[i][j] = tmp;
				}
			}
		}

		return dp[0][n - 1];
	}

	void test()
	{
		cout << ("aaa" == encode("aaa")) << endl;
		cout << ("5[a]" == encode("aaaaa")) << endl;
		cout << ("a9[a]" == encode("aaaaaaaaaa")) << endl;
		cout << ("2[aabc]d" == encode("aabcaabcd")) << endl;
		cout << ("2[2[abbb]c]" == encode("abbbabbbcabbbabbbc")) << endl;
	}
};

