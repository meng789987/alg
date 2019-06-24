#include "pch.h"

/*
 * tags: binary search
 * Time(nlogn), Space(1)
 * when looking for a value in an error epsilon range, we can think about binary search.
 * here we try average from the min to max step by epsilon.
 * to check if given average is possible, we subtract the average from all nums, then check if there is a subarray sum >= 0;
 */

class lc0644 {
public:
	double findMaxAverage(vector<int>& nums, int k) {
		double lo = -10000, hi = 10000, e = 1e-5;
		while (lo + e <= hi) {
			double mi = (lo + hi) / 2;
			if (possible(nums, k, mi)) lo = mi;
			else hi = mi;
		}
		return lo;
	}

	bool possible(vector<int>& nums, int k, double average) {
		double sum = 0, sumBeforeK = 0;
		for (int i = 0; i < nums.size(); i++) {
			if (i >= k) {
				sumBeforeK += nums[i - k] - average;
				if (sumBeforeK <= 0) { // drop the useless part
					sum -= sumBeforeK;
					sumBeforeK = 0;
				}
			}
			sum += nums[i] - average;
			if (i >= k - 1 && sum >= 0) return true;
		}

		return false;
	}

	void test()
	{
		vector<int> nums = { 1,12,-5,-6,50,3 };
		cout << (abs(12.75 - findMaxAverage(nums, 4)) <= 1e5) << endl;
	}
};

