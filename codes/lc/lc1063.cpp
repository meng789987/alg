#include "pch.h"

/*
 * tags: stack
 * Time(n), Space(n)
 * this is the typical case to use stack/deque to get previous min in O(1)
 */

class lc1063 {
public:
	int validSubarrays(vector<int>& nums) {
		int res = 0;
		stack<int> stack;
		for (int i = 0; i < nums.size(); i++) {
			while (!stack.empty() && nums[stack.top()] > nums[i]) stack.pop();
			stack.push(i);
			res += stack.size();
		}

		return res;
	}

	void test()
	{
		vector<int> nums
		cout << (1 == validSubarrays(0, 1, 10)) << endl;
		cout << (22 == digitsCount(0, 1, 123)) << endl;
		cout << (35 == digitsCount(3, 100, 250)) << endl;
	}
};

