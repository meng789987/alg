#include "..\pch.h"

/*
 * tags: binary index tree
 * Space(n); Build: Time(n), Query/Update: Time(logn)
 * BIT is an updatable prefix sum, but every element stores partial prefix sum. bit[0] is unused.
 * bit[i] = sum[i-leastbit(i)..i-1], that is sum of data ending at i-1 with length leastbit(i), where leastbit(i) = i&(-i)
 * e.g. leastbit(6) = leastbit(110b) = 2; bit[8] = sum[0..7]; bit[6] = sum[4..5]
 * when update i (i++ first as bit has offset 1), loop forward to add diff to bit[i] then add i by its least bit, until i is out of range;
 * when query prefix sum[0..i], loop backward to sum of bit[i] then decrease i by its least bit, until i is 0.
 */


class _Binary_Index_Tree {
public:
	_Binary_Index_Tree(vector<int> &data) 
	{
		int n = data.size();
		this->data = data;
		tree = vector<int>(n + 1, 0);

		// this also works and simple, but time is O(nlogn)
		//for (int i = 0; i < n; i++)
		//	Update(i, data[i]);

		for (int i = 1; i <= n; i++) {
			tree[i] += data[i - 1];
			if (i + LowBit(i) < tree.size())
				tree[i + LowBit(i)] += tree[i]; // accumulate partial sum
		}
	}

	// set data[i] = value
	void Update(int i, int value)
	{
		int diff = value - data[i];
		data[i] = value;
		for (i++; i < tree.size(); i += LowBit(i))
			tree[i] += diff;
	}

	int Sum(int i, int j)
	{
		return SumPrefix(j) - SumPrefix(i - 1);
	}

	int SumPrefix(int i)
	{
		int res = 0;
		for (i++; i > 0; i -= LowBit(i))
			res += tree[i];

		return res;
	}

	inline int LowBit(int n)
	{
		return n & (-n);
	}

	vector<int> data;
	vector<int> tree;
};

class _Binary_Index_Tree_2D {
public:
	_Binary_Index_Tree_2D(vector<vector<int>> &data)
	{
		int m = data.size(), n = data[0].size();
		this->data = data;
		tree = vector<vector<int>>(m + 1, vector<int>(n + 1, 0));

		for (int i = 1; i <= m; i++) {
			vector<int> row(n + 1, 0); // BIT tree of this row
			for (int j = 1; j <= n; j++) {
				row[j] += data[i - 1][j - 1];
				if (j + LowBit(j) < row.size())
					row[j + LowBit(j)] += row[j]; // accumulate partial sum

				tree[i][j] += row[j];
				if (i + LowBit(i) < tree.size())
					tree[i + LowBit(i)][j] += tree[i][j];
			}
		}
	}

	// set data[i][j] = value
	void Update(int row, int col, int value)
	{
		int diff = value - data[row][col];
		data[row][col] = value;
		for (int i = row + 1; i < tree.size(); i += LowBit(i)) {
			for (int j = col + 1; j < tree[0].size(); j += LowBit(j))
				tree[i][j] += diff;
		}
	}

	int Sum(int row1, int col1, int row2, int col2)
	{
		return Sum(row2, col2) - Sum(row2, col1 - 1) - Sum(row1 - 1, col2) + Sum(row1 - 1, col1 - 1);
	}

	int Sum(int row, int col)
	{
		int res = 0;
		for (int i = row + 1; i > 0; i -= LowBit(i)) {
			for (int j = col + 1; j > 0; j -= LowBit(j))
				res += tree[i][j];
		}

		return res;
	}

	inline int LowBit(int n)
	{
		return n & (-n);
	}

	vector<vector<int>> data;
	vector<vector<int>> tree;
};

class _Binary_Index_Tree_Test {
public:

	static void Test()
	{
		vector<int> nums({ 10, 13, 12, 15, 16, 18 });
		_Binary_Index_Tree tree(nums);
		cout << (tree.Sum(1, 3) == 40) << endl;
		cout << (tree.Sum(2, 5) == 61) << endl;
		tree.Update(2, 20);
		cout << (tree.Sum(1, 3) == 48) << endl;

		vector<vector<int>> matrix({
			  {3, 0, 1, 4, 2},
			  {5, 6, 3, 2, 1},
			  {1, 2, 0, 1, 5},
			  {4, 1, 0, 1, 7},
			  {1, 0, 3, 0, 5} });
		_Binary_Index_Tree_2D tree2d(matrix);
		cout << (tree2d.Sum(2, 1, 4, 3) == 8) << endl;
		tree2d.Update(3, 2, 2);
		cout << (tree2d.Sum(2, 1, 4, 3) == 10) << endl;
	}
};