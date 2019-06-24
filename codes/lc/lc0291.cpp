#include "pch.h"

/*
 * tags: backtracking
 * Time(m^n), Space(N)
 * try to map a letter to any substring
 */

class lc0291 {
public:
	bool wordPatternMatch(string pattern, string str) {
		string map[26];
		unordered_set<string> used;
		int counts[26] = { 0 };
		for (char c : pattern) counts[c - 'a']++;
		return bt(pattern, 0, str, 0, map, used, counts);
	}

	bool bt(string pattern, int pi, string str, int si, string* map, unordered_set<string>& used, int* counts) {
		if (pi == pattern.size()) return si == str.size();
		char pci = pattern[pi] - 'a';

		if (!map[pci].empty()) {
			auto& mp = map[pci];
			return mismatch(mp.begin(), mp.end(), str.begin() + si).first == mp.end()
				&& bt(pattern, pi + 1, str, si + mp.size(), map, used, counts);
		}

		for (size_t i = si; i < str.size(); i++) {
			string sub = str.substr(si, i - si + 1);
			if (used.find(sub) != used.end()) continue;
			if (si + counts[pci] * sub.size() > str.size()) continue; // prune

			auto usedIt = used.insert(sub).first;
			map[pci] = sub;
			if (bt(pattern, pi + 1, str, i + 1, map, used, counts)) return true;
			map[pci] = "";
			used.erase(usedIt);
		}

		return false;
	}

	void test()
	{
		cout << (wordPatternMatch("abab", "redblueredblue")) << endl;
		cout << (wordPatternMatch("aaaa", "asdasdasdasd")) << endl;
		cout << (!wordPatternMatch("aabb", "xyzabcxzyabc")) << endl;
	}
};

