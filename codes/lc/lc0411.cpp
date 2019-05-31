#include "pch.h"

/*
 * tags: heap
 * Time(n), Space(1)
 * count all chars, each time select an available char with max count, mark its available position as current plus k.
 */

class lc0411 {
public:
	string minAbbreviation(string target, vector<string>& dictionary) {
		size_t n = target.size();
		vector<string> dict;
		for (string& s : dictionary) {
			if (s.size() == n) dict.push_back(s);
		}

		if (dict.size() == 0) return to_string(n);

		string res = target;
		int reslen = n;

		for (int totalcnt = 1; totalcnt < n; totalcnt++) {
			vector<bool> sel(n, false);
			if (bt(target, dict, 0, totalcnt, 0, sel)) {
				string r;
				int rlen = 0, num = 0;
				for (int i = 0; i < n; i++) {
					if (sel[i]) {
						if (num > 0) {
							r += num;
							rlen++;
						}
						r += target[i];
						rlen++;
						num = 0;
					}
					else {
						num++;
					}
				}

				if (num > 0) {
					r += num;
					rlen++;
				}

				if (reslen > rlen) {
					reslen = rlen;
					res = r;
				}
			}
		}

		return res;
	}

	void test()
	{
		vector<string> dict{ "blade" };
		cout << ("a4" == minAbbreviation("apple", dict)) << endl;
		dict = vector<string>{ "plain", "amber", "blade" };
		cout << ("ap3" == minAbbreviation("apple", dict)) << endl;
	}
};

