#include "pch.h"

/*
 * tags: bs
 * Time(nlogn), Space(1)
 * ref lc0644, when looking for a value in an error epsilon range, we can think about binary search.
 * here we try distance from the min to max step by epsilon.
 * to check if given distance is possible, we add stations to each gap and check if it needs more stations thank K.
 */

class lc0774 {
public:
	double minmaxGasDist(vector<int>& stations, int K) {
		double lo = 0, hi = *max_element(stations.begin(), stations.end()), e = 1e-6;
		while (lo + e <= hi) {
			double mi = (lo + hi) / 2;
			if (isPossible(stations, K, mi)) hi = mi;
			else lo = mi;
		}

		return lo;
	}

	bool isPossible(vector<int>& stations, int K, double dist) {
		int cnt = 0;
		for (size_t i = 1; i < stations.size(); i++)
			cnt += (int)ceil((stations[i] - stations[i - 1]) / dist) - 1;
		return cnt <= K;
	}

	void test()
	{
		vector<int> stations{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		cout << (abs(0.5 - minmaxGasDist(stations, 9)) < 1e-6) << endl;
	}
};

