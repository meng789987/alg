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
		for (size_t i = 0; i < nums.size(); i++) {
			while (!stack.empty() && nums[stack.top()] > nums[i]) stack.pop();
			stack.push(i);
			res += stack.size();
		}

		return res;
	}

	void test()
	{
		vector<int> nums{ 1,4,2,5,3 };
		cout << (11 == validSubarrays(nums)) << endl;
		nums = vector<int>{ 3,2,1 };
		cout << (3 == validSubarrays(nums)) << endl;
		nums = vector<int>{ 2,2,2 };
		cout << (6 == validSubarrays(nums)) << endl;
	}
};

