#include "pch.h"

/*
 * tags: binary search
 * Time(nlogn), Space(1)
 * same with lc0644, when looking for a value in an error epsilon range, we can think about binary search.
 */

class lc0774 {
public:
	double minmaxGasDist(vector<int>& stations, int K) {
		double lo = 0, hi = stations.back(), e = 1e-6;
		while (lo + e < hi) {
			double mi = (lo + hi) / 2;
			if (possible(stations, K, mi)) hi = mi;
			else lo = mi;
		}

		return lo;
	}

	bool possible(vector<int>& stations, int K, double gap) {
		int cnt = 0;
		for (int i = 1; i < stations.size(); i++)
			cnt += ceil((stations[i] - stations[i - 1]) / gap) - 1;
		return cnt <= K;
	}

	void test()
	{
		vector<int> nums{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		cout << (abs(0.5 - minmaxGasDist(nums, 9)) < 1e-6) << endl;
	}
};

