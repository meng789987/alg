#include "pch.h"

/*
 * tags: bst, sliding window
 * Time(n), Space(n)
 * 1. bst, put the bulbs into bst one by one, and check if adjacent bulbs meet the anwser.
 * 2. sliding window
 *    convert bulbs to days, which means ith bulbs is turned on on (days[i])th day.
 *    so if a window of bulbs left and right[=left+K+1] is valid, then all bulb i between them are turned on later than bulb left and right, 
 *    that is, days[i] > days[left] and days[i] > days[right]
 */

class lc0683 {
public:
	int kEmptySlots2(vector<int>& bulbs, int K) {
		int n = bulbs.size();
		vector<int> days(n);
		for (int d = 0; d < n; d++) days[bulbs[d] - 1] = d + 1;

		int res = INT_MAX;
		size_t left = 0, right = K + 1;
		for (size_t b = 1; right < bulbs.size(); b++) {
			// bulb b is good, continue scanning
			if (days[b] > days[left] && days[b] > days[right]) continue;

			// got a valid window, record the result
			if (b == right) res = min(res, max(days[left], days[right]));

			// slide the window
			left = b;
			right = b + K + 1;
		}

		return res == INT_MAX ? -1 : res;
	}

	int kEmptySlots(vector<int>& bulbs, int K) {
		size_t n = bulbs.size();
		vector<int> days(n); // ith bulbs is turned on on (days[i])th day.
		for (size_t i = 0; i < n; i++)
			days[bulbs[i] - 1] = i + 1;

		int res = INT_MAX;
		deque<size_t> win;
		win.push_front(0);

		for (size_t b = 1; b < n; b++) {
			if (win.front() + K + 1 < b) win.pop_front();
			int f = win.front();
			while (win.size() && days[b] < days[win.back()]) win.pop_back();
			if (f + K + 1 == b && win.size() <= 1)
				res = min(res, max(days[f], days[b]));
			win.push_back(b);
		}

		return res == INT_MAX ? -1 : res;
	}

	int kEmptySlots1(vector<int>& bulbs, int K) {
		set<int> days;
		for (size_t i = 0; i < bulbs.size(); i++) {
			auto it = days.insert(bulbs[i]).first;
			if (it != days.begin()) {
				--it;
				if (abs(*it - bulbs[i]) == K + 1) return i + 1;
				++it;
			}
			if (++it != days.end() && abs(*it - bulbs[i]) == K + 1)
				return i + 1;
		}

		return -1;
	}

	void test()
	{
		vector<int> nums{ 1,3,2 };
		cout << (2 == kEmptySlots(nums, 1)) << endl;

		nums = vector<int>{ 1,2,3 };
		cout << (-1 == kEmptySlots(nums, 1)) << endl;
	}
};

