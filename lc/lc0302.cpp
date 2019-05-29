#include "pch.h"

/*
 * tags: binary search
 * Time(mlogn+nlogm), Space(1)
 * the simple solution is to use dfs to find the 4 corners;
 * a better solution is to binary search to test if the row/col has 1 to find the 4 corners.
 */

class lc0302 {
public:
	int minArea(vector<vector<char>>& image, int x, int y) {
		int m = image.size(), n = image[0].size(), lo, hi;

		for (lo = 0, hi = x; lo <= hi; ) {
			int mi = (lo + hi) / 2;
			if (hasOneInRow(image, mi)) hi = mi - 1;
			else lo = mi + 1;
		}
		int top = lo;

		for (lo = x, hi = m-1; lo <= hi; ) {
			int mi = (lo + hi) / 2;
			if (hasOneInRow(image, mi)) lo = mi + 1;
			else hi = mi - 1;
		}
		int bottom = lo; // offset 1 more

		for (lo = 0, hi = y; lo <= hi; ) {
			int mi = (lo + hi) / 2;
			if (hasOneInCol(image, mi)) hi = mi - 1;
			else lo = mi + 1;
		}
		int left = lo;

		for (lo = y, hi = n - 1; lo <= hi; ) {
			int mi = (lo + hi) / 2;
			if (hasOneInCol(image, mi)) lo = mi + 1;
			else hi = mi - 1;
		}
		int right = lo; // offset 1 more

		return (bottom - top) * (right - left);
	}

	bool hasOneInRow(vector<vector<char>>& image, int r) {
		for (char c : image[r]) {
			if (c == '1') return true;
		}
		return false;
	}

	bool hasOneInCol(vector<vector<char>>& image, int c) {
		for (auto& v : image) {
			if (v[c] == '1') return true;
		}
		return false;
	}

	void test()
	{
		vector<vector<char>> grid{ {'0','0','1','0'}, {'0','1','1','0'}, {'0','1','0','0'} };
		cout << (6 == minArea(grid, 0, 2)) << endl;
	}
};

