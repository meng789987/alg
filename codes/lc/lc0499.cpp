#include "pch.h"

/*
 * tags: bfs, dfs
 * Time(mn), Space(mn)
 * dp[h, c] is the min cost to paint house[0..h] and house[h] is painted with color[c]
 * dp[h, c] = min(dp[h-1, i], i!=c) + costs[h][c]
 * base case: dp[0, c] = costs[0][c]
 */

class lc0499 {
public:
	string findShortestWay(vector<vector<int>>& maze, vector<int>& ball, vector<int>& hole) {
		int min = 0, sec = 0, minIdx = -1;
		for (auto& cost : costs) {
			int preMin = min, preSec = sec, preIdx = minIdx;
			min = INT_MAX; sec = 0; minIdx = -1;

			for (int c = 0; c < cost.size(); c++) {
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
		vector<vector<int>> maze{ {0,0,0,0,0}, {1,1,0,0,1}, {0,0,0,0,0}, {0,1,0,0,1}, {0,1,0,0,0} };
		vector<int> ball{ 4, 3 };
		vector<int> hole{ 0, 1 };
		cout << ("lul" == findShortestWay(maze, ball, hole)) << endl;

		maze = vector<vector<int>> { {0,0,0,0,0}, {1,1,0,0,1}, {0,0,0,0,0}, {0,1,0,0,1}, {0,1,0,0,0} };
		ball = vector<int> { 4, 3 };
		hole = vector<int> { 3, 0 };
		cout << ("impossible" == findShortestWay(maze, ball, hole)) << endl;
	}
};

