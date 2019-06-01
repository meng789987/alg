#include "pch.h"

/*
 * tags: dfs
 * Time(n), Space(1)
 * count all strobogrammatic numbers one by one between the two numbers
 */

class lc0248 {
public:
	vector<vector<char>> pairs{ {'0','0'}, {'1','1'}, {'8','8'}, {'6','9'}, {'9','6'} };
	int strobogrammaticInRange(string low, string high) {
		int res = 0;
		for (int len = low.size(); len <= high.size(); len++) {
			string path(len, ' ');
			res += dfs(low, high, 0, path);
		}

		return res;
	}

	int dfs(string& low, string& high, int pos, string& path) {
		int rpos = path.size() - pos - 1;
		if (pos > rpos) {
			if (low.size() == path.size() && path < low || high.size() == path.size() && path > high)
				return 0;
			return 1;
		}

		int res = 0;
		for (auto& p : pairs) {
			if (pos == 0 && path.size() > 1 && p[0] == '0') continue;
			if (pos == rpos && p[0] != p[1]) continue;
			path[pos] = p[0];
			path[rpos] = p[1];
			res += dfs(low, high, pos + 1, path);
		}

		return res;
	}

	void test()
	{
		cout << (3 == strobogrammaticInRange("50", "100")) << endl;
	}
};

