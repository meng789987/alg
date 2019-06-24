#include "pch.h"

/*
 * tags: dp
 * Time(NNK), Space(N)
 * dp[c, w] is the max vacations after w-th week and staying on city in last week.
 * dp[c, w] = max(dp[i, w-1]) + days[c, w], if i==w or has flights[i, w]
 * base case: dp[i, 0] = days[i, 0] if i==0 or has flights[i, 0]
 */

class lc0568 {
public:
	int maxVacationDays(vector<vector<int>>& flights, vector<vector<int>>& days) {
		size_t C = days.size(), W = days[0].size();
		vector<vector<int>> dp(C, vector<int>(W + 1, -1));
		dp[0][0] = 0;
		for (size_t w = 1; w <= W; w++) {
			for (size_t from = 0; from < C; from++) {
				if (dp[from][w - 1] == -1) continue;
				for (size_t to = 0; to < C; to++) {
					if (from == to || flights[from][to])
						dp[to][w] = max(dp[to][w], dp[from][w - 1] + days[to][w - 1]);
				}
			}
		}

		int res = dp[0][W];
		for (size_t c = 0; c < C; c++) res = max(res, dp[c][W]);
		return res;
	}

	void test()
	{
		vector<vector<int>> flights{ {0,1,1},{1,0,1},{1,1,0} };
		vector<vector<int>> days{ {1,3,1},{6,0,3},{3,3,3} };
		cout << (12 == maxVacationDays(flights, days)) << endl;

		flights = vector<vector<int>>{ {0,0,0},{0,0,0},{0,0,0} };
		days = vector<vector<int>>{ {1,1,1},{7,7,7},{7,7,7} };
		cout << (3 == maxVacationDays(flights, days)) << endl;

		flights = vector<vector<int>>{ {0,1,1},{1,0,1},{1,1,0} };
		days = vector<vector<int>>{ {7,0,0},{0,7,0},{0,0,7} };
		cout << (21 == maxVacationDays(flights, days)) << endl;
	}
};

