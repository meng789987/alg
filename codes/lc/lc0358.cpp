#include "pch.h"

/*
 * tags: heap
 * Time(n), Space(1)
 * count all chars, each time select an available char with max count, mark its available position as current plus k.
 */

class lc0358 {
public:
	string rearrangeString(string s, int k) {
		vector<char> res;
		int counts[26] = { 0 };
		size_t positions[26] = { 0 };
		for (char c : s) counts[c - 'a']++;
		for (size_t i = 0; i < s.size(); i++) {
			int maxi = -1, max = 0;
			for (int ci = 0; ci < 26; ci++) {
				if (positions[ci] <= i && max < counts[ci])
					max = counts[ci], maxi = ci;
			}

			if (maxi == -1) return "";
			res.push_back('a' + maxi);
			counts[maxi]--;
			positions[maxi] = i + k; // available after k
		}

		return string(res.begin(), res.end());
	}

	void test()
	{
		cout << ("abcabc" == rearrangeString("aabbcc", 3)) << endl;
		cout << ("" == rearrangeString("aaabc", 3)) << endl;
		cout << ("abacabcd" == rearrangeString("aaadbbcc", 2)) << endl;
	}
};

