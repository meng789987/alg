#include "pch.h"

/*
 * tags: math, sort
 * Time(m+n), Space(m+n)
 * median is the best point to meet
 */

class lc0296 {
public:
	int minTotalDistance(vector<vector<int>>& grid) {
		vector<int> rows;
		for (int i = 0; i < (int)grid.size(); ++i) {
			for (int j = 0; j < (int)grid[i].size(); ++j) {
				if (grid[i][j]) rows.push_back(i);
			}
		}

		vector<int> cols;
		for (int j = 0; j < (int)grid[0].size(); ++j) {
			for (int i = 0; i < (int)grid.size(); ++i) {
				if (grid[i][j]) cols.push_back(j);
			}
		}

		// median is the best point to meet
		int mx = rows[rows.size() / 2];
		int my = cols[cols.size() / 2];

		int d = 0;
		for (int i : rows) d += abs(mx - i);
		for (int i : cols) d += abs(my - i);

		return d;
	}

	void test()
	{
		vector<vector<int>> grid{ {1,0,0,0,1}, {0,0,0,0,0}, {0,0,1,0,0} };
		cout << (6 == minTotalDistance(grid)) << endl;
	}
};

