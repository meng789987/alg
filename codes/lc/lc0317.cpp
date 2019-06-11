#include "pch.h"

/*
 * tags: bfs
 * Time(mmnn), Space(mn)
 * for each building, run bfs to mark all empty cell with the bulding number and the min distance, 
 * then for next building we can check only the marked cells to skip unreachable cells.
 */

class lc0317 {
public:
	int shortestDistance(vector<vector<int>> grid) {
		int res = -1, label = 0;
		auto dist = grid;
		for (size_t i = 0; i < grid.size(); i++) {
			for (size_t j = 0; j < grid[0].size(); j++) {
				if (grid[i][j] == 1) {
					int cur = minDist(grid, i, j, label--, dist);
					if (cur == -1) return -1;
					res = cur;
				}
			}
		}

		return res;
	}

	int minDist(vector<vector<int>>& grid, int row, int col, int label, vector<vector<int>>& dist) {
		int res = INT_MAX, m = grid.size(), n = grid[0].size();
		int dirs[] = { 1, 0, -1, 0, 1 };
		queue<pair<int, int>> q;
		q.emplace(row, col);

		for (int step = 1; q.size(); step++) {
			for (int cnt = q.size(); cnt > 0; cnt--) {
				auto node = q.front(); q.pop();
				int r = node.first, c = node.second;
				for (int d = 0; d < 4; ++d) {
					int i = r + dirs[d], j = c + dirs[d + 1];
					if (0 <= i && i < m && 0 <= j && j < n && grid[i][j] == label) {
						grid[i][j]--;
						dist[i][j] += step;
						q.emplace(i, j);
						res = min(res, dist[i][j]);
					}
				}
			}
		}

		return res == INT_MAX ? -1 : res;
	}

	void test()
	{
		vector<vector<int>> grid{ {1,0,2,0,1},{0,0,0,0,0},{0,0,1,0,0} };
		cout << (7 == shortestDistance(grid)) << endl;
	}
};

