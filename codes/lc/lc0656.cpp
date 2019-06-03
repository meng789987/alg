#include "pch.h"

/*
 * tags: dp
 * Time(n), Space(n)
 * dp[i] = min(dp[i-B..i-1]) + A[i]
 *
 * 1. in order to get the lexicographically smallest path, we collect the coins backward. so
 * dp[i] = min(dp[i+1..i+B]) + A[i] // backward
 * if dp[i+j] == dp[i+k], j<k, then select dp[i+j] because this path is smaller than the other
 *
 * 2. this is the typical case to use stack/deque to get previous min in O(1)
 */

class lc0656 {
public:
	vector<int> cheapestJump(vector<int>& A, int B) {
		int n = A.size();
		if (n == 0 || A[n - 1] == -1) return vector<int>();
		vector<int> costs(n), nexts(n);
		deque<int> win; // index within sliding window
		nexts[n - 1] = n;
		costs[n - 1] = A[n - 1];
		win.push_front(n - 1);

		// going backward to get the lexicographically smallest path
		for (int i = n - 2, b = 0; i >= 0; i--) {
			if (i + B < win.back()) win.pop_back(); // slide window
			if (win.empty()) return vector<int>(); // unreachable
			if (A[i] == -1) continue;
			nexts[i] = win.back();
			costs[i] = costs[win.back()] + A[i];
			while (!win.empty() && costs[win.front()] >= costs[i]) win.pop_front();
			win.push_front(i);
		}

		vector<int> res;
		for (int i = 0; i < n; i = nexts[i])
			res.push_back(i + 1);

		return res;
	}

	vector<int> cheapestJump2(vector<int>& A, int B) {
		vector<vector<int>> dp; // item: [index, cost, next_index], order by cost
		int n = A.size();
		if (n == 0 || A[n - 1] == -1) return vector<int>();
		dp.push_back({ n - 1, A.back(), n });

		// going backward to get the lexicographically smallest path
		// b is the left index of the sliding window, so dp[b] is the min cost in the window
		for (int i = n - 2, b = 0; i >= 0; i--) {
			if (i + B < dp[b][0]) b++; // slide window
			if (b == dp.size()) return vector<int>(); // unreachable
			if (A[i] == -1) continue;
			int cost = dp[b][1] + A[i];
			while (dp.size() > b + 1 && dp.back()[1] >= cost) dp.pop_back();
			dp.push_back({ i, cost, dp[b][0] });
			if (A[i] == 0) b++;
		}

		vector<int> res;
		for (int i = 0; dp.size(); dp.pop_back()) {
			if (i == dp.back()[0]) {
				res.push_back(i + 1);
				i = dp.back()[2];
			}
		}

		return res;
	}

	vector<int> cheapestJump1(vector<int>& A, int B) {
		int n = A.size();
		if (n == 0 || A[n - 1] == -1) return vector<int>();
		vector<int> dp(n, INT_MAX), next(n, -1);
		dp[n - 1] = A[n - 1];

		// going backward to get the lexicographically smallest path
		for (int i = n - 2; i >= 0; i--) {
			if (A[i] == -1) continue;
			for (int j = i + 1; j <= i + B && j < n; j++) {
				if (dp[j] == INT_MAX) continue;
				if (dp[i] > dp[j] + A[i]) { // no override when equal
					dp[i] = dp[j] + A[i];
					next[i] = j;
				}
			}
		}

		vector<int> res;
		if (dp[0] == INT_MAX) return res;
		for (int i = 0; i != -1; i = next[i])
			res.push_back(i + 1);

		return res;
	}

	void test()
	{
		vector<int> nums = { 1,2,4,-1,2 };
		vector<int> exp = { 1,3,5 };
		cout << (exp == cheapestJump(nums, 2)) << endl;

		nums = vector<int>{ 1,2,4,-1,2 };
		exp = vector<int>{  };
		cout << (exp == cheapestJump(nums, 1)) << endl;

		nums = vector<int>{ 0,-1,-1,-1,-1,-1 };
		exp = vector<int>{  };
		cout << (exp == cheapestJump(nums, 5)) << endl;

		nums = vector<int>{ 1 };
		exp = vector<int>{ 1 };
		cout << (exp == cheapestJump(nums, 1)) << endl;

		nums = vector<int>{ 33,90,57,39,42,45,29,90,81,87,88,37,58,97,80,2,77,64,82,41,2,33,34,95,85,
			77,92,3,8,15,71,84,58,65,46,48,3,74,4,83,23,12,15,77,33,65,17,86,21,62,71,55,80,63,75,55,
			11,34,-1,64,81,18,77,82,8,12,14,6,46,39,71,14,6,46,89,37,88,70,88,33,89,92,60,0,78,10,88,99,20 };
		exp = vector<int>{ 1,16,84,89 };
		cout << (exp == cheapestJump(nums, 74)) << endl;

		nums = vector<int>{ 21,7,96,68,73,99,19,89,0,62,86,8,6,62,49,77,47,12,86,5,46,29,3,41,68,50,
			83,41,77,29,10,91,75,23,59,36,8,26,26,88,-1,41,45,32,3,51,83,75,12,48,99,38,21,98,83,46,
			42,48,64,92,70,6,96,30,65,7,90,95,5,97,25,7,99,2,26,42,38,95,26,11,86,24,16,87,77,58,30,31 };
		exp = vector<int>{ };
		cout << (exp == cheapestJump(nums, 1)) << endl;
	}
};

