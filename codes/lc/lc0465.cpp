#include "pch.h"

/*
 * tags: subset, bt, dfs
 * Time(2^n), Space(n)
 * when a minimized group has 0 balance, then the min number of transactions is the group count minus 1.
 *
 * The question can be transferred to a 3-partition problem, which is NP-Complete.
 * ref: Settling Multiple Debts Efficiently: An Invitation to Computing Science by T. Verhoeff, June 2003.
 * link: http://www.mathmeth.com/tom/files/settling-debts.pdf
 */

class lc0465 {
public:
	int minTransfers(vector<vector<int>>& transactions) {
		unordered_map<int, int> balances;
		for (auto& v : transactions) {
			balances[v[0]] += v[2];
			balances[v[1]] -= v[2];
		}

		vector<int> debts;
		for (auto& p : balances) {
			if (p.second) debts.push_back(p.second);
		}

		// method #1
		// if (true) return dfs(debts, 0);

		// method #2
		sort(debts.begin(), debts.end());

		int res = debts.size();
		for (int setcnt = 2; debts.size(); setcnt++) {
			while (debts.size()) {
				vector<bool> sel(debts.size(), false);
				if (!hasSubset(debts, 0, setcnt, 0, 0, sel)) break;
				cout << endl;
				copy(debts.begin(), debts.end(), ostream_iterator<int>(cout, ","));
				cout << endl;
				copy(sel.begin(), sel.end(), ostream_iterator<int>(cout, ", "));
				cout << endl;
				for (int i = 0, j = 0; j < debts.size(); j++) {
					if (!sel[j]) debts[i++] = debts[j];
				}
				debts.erase(debts.end() - setcnt, debts.end());
				res--;
			}
		}

		return res;
	}

	bool hasSubset(vector<int>& debts, int start, int setcnt, int selcnt, int selsum, vector<bool>& sel) {
		if (selcnt == setcnt) return selsum == 0;
		for (int i = start; i < debts.size(); i++) {
			if (i > start && debts[i - 1] == debts[i]) continue; // dedup
			sel[i] = true;
			if (hasSubset(debts, i + 1, setcnt, selcnt + 1, selsum + debts[i], sel)) return true;
			sel[i] = false;
		}
		return false;
	}

	int dfs(vector<int>& debts, int start) {
		while (start < debts.size() && !debts[start]) ++start; // get next non-zero debt
		int res = INT_MAX;
		for (int i = start + 1, prev = 0; i < debts.size(); ++i) {
			if (debts[i] == prev || (long)debts[i] * debts[start] >= 0) continue; // skip already tested or same sign debt
			debts[i] += debts[start];
			res = min(res, 1 + dfs(debts, start + 1));
			debts[i] -= debts[start];
			prev = debts[i];
		}
		return res < INT_MAX ? res : 0;
	}

	void test()
	{
		vector<vector<int>> trans{ {0,1,10}, {2,0,5} };
		cout << (2 == minTransfers(trans)) << endl;

		trans = vector<vector<int>>{ {0,1,10}, {1,0,1}, {1,2,5}, {2,0,5} };
		cout << (1 == minTransfers(trans)) << endl;

		trans = vector<vector<int>>{ {0,1,1}, {2,3,2}, {4,5,3}, {6,7,4}, {8,9,5}, {10,11,6}, {12,13,7}, {14,15,2}, {14,16,2}, {14,17,2}, {14,18,2} };
		cout << ( minTransfers(trans)) << endl;

		trans = vector<vector<int>>{ {8,23,20},{3,24,78},{4,20,37},{0,29,66},{2,29,2},{0,20,23},{0,22,65},{5,24,34},{0,27,6},{6,21,16},{1,26,2},
			{4,21,73},{8,27,64},{6,27,39},{5,25,15},{5,23,28},{8,25,53},{6,27,98},{0,25,92},{5,28,91},{8,21,75},{1,25,39},{1,22,55},{1,25,14},
			{4,26,70},{6,29,30},{6,26,11},{1,28,68},{1,26,13},{7,21,4},{3,29,77},{0,26,93},{7,20,39},{5,22,91},{9,27,80},{1,23,71},{6,29,27},
			{8,26,95},{8,29,24},{7,25,70},{1,29,17},{9,29,98},{6,22,26},{1,24,74},{0,25,33},{0,24,68},{8,25,91},{8,23,36},{1,29,25},{8,27,82},{4,24,14} };
		cout << (minTransfers(trans)) << endl;
	}
};

