#include "pch.h"

/*
 * tags: backtracking
 * Time(n!), Space(n)
 * try every possible
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

		for (int totalcnt = 1, maxcnt = n; totalcnt < maxcnt; totalcnt++) {
			vector<bool> sel(n, false);
			bt(target, dict, 0, totalcnt, 0, sel, reslen, res);
			maxcnt = min(maxcnt, 2 * reslen);
		}

		return res;
	}

	void bt(string target, vector<string>& dict, int start, int totalcnt, int selcnt, vector<bool>& sel, int& reslen, string& res) {
		if (selcnt == totalcnt) {
			if (isValid(target, dict, sel)) {
				string r;
				int rlen = createAbbr(target, sel, r);
				if (reslen > rlen) {
					reslen = rlen;
					res = r;
				}
			}
			return;
		}

		for (size_t i = start; i < target.size(); i++) {
			sel[i] = true;
			bt(target, dict, i + 1, totalcnt, selcnt + 1, sel, reslen, res);
			sel[i] = false;
		}
	}

	bool isValid(string target, vector<string>& dict, vector<bool>& sel) {
		for (string word : dict) {
			size_t i = 0;
			for (; i < sel.size(); i++) {
				if (sel[i] && target[i] != word[i]) break;
			}
			if (i == sel.size()) return false;
		}

		return true;
	}

	int createAbbr(string target, vector<bool>& sel, string& res) {
		int rlen = 0;
		for (size_t i = 0; i < sel.size(); rlen++) {
			if (sel[i]) res += target[i++];
			else {
				int num = 0;
				for (; i < sel.size() && !sel[i]; i++) num++;
				res += to_string(num);
			}
		}

		return rlen;
	}

	void test()
	{
		vector<string> dict{ "blade" };
		cout << ("a4" == minAbbreviation("apple", dict)) << endl;
		dict = vector<string>{ "plain", "amber", "blade" };
		cout << ("1p3" == minAbbreviation("apple", dict)) << endl;
	}
};

