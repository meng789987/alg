#include "pch.h"

/*
 * tags: sort
 * Time(nlogn), Space(n)
 * add them into one array and sort, or merge sort directly
 */

class lc0759 {
public:
	vector<vector<int>> employeeFreeTime(vector<vector<vector<int>>>& schedule) {
		vector<vector<int>> times;
		for (auto& s : schedule)
			times.insert(times.end(), s.begin(), s.end());
		sort(times.begin(), times.end());

		vector<vector<int>> res;
		for (int i = 1, pre = times[0][1]; i < times.size(); i++) {
			if (pre < times[i][0]) res.push_back({ pre, times[i][0] });
			pre = max(pre, times[i][1]);
		}

		return res;
	}

	vector<vector<int>> employeeFreeTime1(vector<vector<vector<int>>>& schedule) {
		merge(schedule, 0, schedule.size() - 1);
		auto& time = schedule[0];
		vector<vector<int>> res;
		for (int i = 1; i < time.size(); i++)
			res.push_back({ time[i - 1][1], time[i][0] });
		return res;
	}

	// merge times[lo..hi] into times[lo]
	void merge(vector<vector<vector<int>>>& times, int lo, int hi) {
		if (hi == lo) return;

		int mi = (lo + hi) / 2;
		merge(times, lo, mi);
		merge(times, mi + 1, hi);

		auto& vlo = times[lo];
		auto& vhi = times[mi + 1];
		vector<vector<int>> res;
		for (int i = 0, j = 0; i < vlo.size() || j < vhi.size(); ) {
			vector<int> min;
			if (j == vhi.size() || (i < vlo.size() && vlo[i][0] < vhi[j][0]))
				min = vlo[i++];
			else min = vhi[j++];
			if (res.empty() || min[0] > res.back()[1]) res.push_back(min);
			else res.back()[1] = max(res.back()[1], min[1]);
		}

		times[lo] = res;
	}

	void test()
	{
		vector<vector<vector<int>>> times{ {{1,2},{5,6}},{{1,3}},{{4,10}} };
		vector<vector<int>> exp{ {3,4} };
		cout << (exp == employeeFreeTime(times)) << endl;

		times = vector<vector<vector<int>>>{ {{1,2},{5,6}},{{1,3}},{{4,10}} };
		exp = vector<vector<int>>{ {3,4} };
		cout << (exp == employeeFreeTime(times)) << endl;
	}
};

