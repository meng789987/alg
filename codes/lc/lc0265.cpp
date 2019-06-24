#include "pch.h"

/*
 * tags: dp
 * Time(nk), Space(1)
 * dp[h, c] is the min cost to paint house[0..h] and house[h] is painted with color[c]
 * dp[h, c] = min(dp[h-1, i], i!=c) + costs[h][c]
 * base case: dp[0, c] = costs[0][c]
 */

class lc0265 {
public:
	int minCostII(vector<vector<int>>& costs) {
		int min = 0, sec = 0, minIdx = -1;
		for (auto& cost : costs) {
			int preMin = min, preSec = sec, preIdx = minIdx;
			min = INT_MAX; sec = 0; minIdx = -1;

			for (int c = 0; c < (int)cost.size(); c++) {
				int cur = cost[c] + (c != preIdx ? preMin : preSec);
				if (cur <= min) {
					sec = min;
					min = cur;
					minIdx = c;
				}
				else if (cur < sec) {
					sec = cur;
				}
			}
		}

		return min;
	}

	void test()
	{
		vector<vector<int>> costs{ {1,5,3},{2,9,4} };
		cout << (5 == minCostII(costs)) << endl;
	}
};

