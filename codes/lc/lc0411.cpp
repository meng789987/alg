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

