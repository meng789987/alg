#include "pch.h"

/*
 * tags: binary index tree, segment tree
 * Build: Time(mn); Query/Update: Time(logmlogn), Space(mn)
 * 2D binary index tree
 */

class lc0308 {
public:
	void init(vector<vector<int>>& matrix) {
		if (matrix.size() == 0) return;
		int m = matrix.size();
		int n = matrix[0].size();
		data = matrix;
		tree.resize(m + 1, vector<int>(n + 1, 0));

		for (int i = 1; i <= m; i++) {
			vector<int> row(n + 1, 0);
			for (int j = 1; j <= n; j++) {
				row[j] += matrix[i - 1][j - 1];
				if (j + (j&-j) <= n)
					row[j + (j&-j)] += row[j];
				tree[i][j] += row[j];
				if (i + (i&-i) <= m)
					tree[i + (i&-i)][j] += tree[i][j];
			}
		}
	}

	void update(int row, int col, int val) {
		int diff = val - data[row][col];
		data[row][col] = val;
		// go forward to update
		for (int i = row + 1; i < tree.size(); i += i & (-i)) {
			for (int j = col + 1; j < tree[0].size(); j += j & (-j))
				tree[i][j] += diff;
		}
	}

	int sumRegion(int row1, int col1, int row2, int col2) {
		return sum(row2 + 1, col2 + 1) - sum(row2 + 1, col1) - sum(row1, col2 + 1) + sum(row1, col1);
	}

	int sum(int row, int col) {
		int s = 0;
		// go back to sum
		for (int i = row; i > 0; i -= i & (-i)) {
			for (int j = col; j > 0; j -= j & (-j))
				s += tree[i][j];
		}

		return s;
	}

	vector<vector<int>> data;
	vector<vector<int>> tree;


	void test()
	{
		vector<vector<int>> matrix{
			{3, 0, 1, 4, 2},
			{5, 6, 3, 2, 1},
			{1, 2, 0, 1, 5},
			{4, 1, 0, 1, 7},
			{1, 0, 3, 0, 5} };
		init(matrix);
		cout << (8 == sumRegion(2, 1, 4, 3)) << endl;
		update(3, 2, 2);
		cout << (10 == sumRegion(2, 1, 4, 3)) << endl;
	}
};

