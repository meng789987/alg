#include "pch.h"

/*
 * tags: trie
 * Time(nm), Space(nm)
 * 1. in each bucket (same length and same ending letter), build trie
 * 2. for each word, try as short as it can be.
 */

class lc0527 {
	struct Node {
		Node* children[26] = { NULL };
		vector<int> words;
	};
public:
	vector<string> wordsAbbreviation1(vector<string>& dict) {
		unordered_map<string, Node*> nodes;
		for (size_t i = 0; i < dict.size(); i++) {
			string word = dict[i];
			string key = to_string(word.size()) + word.back();
			if (!nodes[key]) nodes[key] = new Node();
			Node* node = nodes[key];
			for (char c : word) {
				int ci = c - 'a';
				if (!node->children[ci]) node->children[ci] = new Node();
				node = node->children[ci];
				node->words.push_back(i);
			}
		}

		vector<string> res;
		for (size_t i = 0; i < dict.size(); i++) {
			string word = dict[i];
			string key = to_string(word.size()) + word[word.size() - 1];
			Node* node = nodes[key];
			size_t j = 0;
			for (; j < word.size(); j++) {
				int ci = word[j] - 'a';
				node = node->children[ci];
				if (node->words.size() == 1) break;
			}
			if (word.size() <= j + 3)
				res.push_back(word);
			else
				res.push_back(word.substr(0, j+1) + to_string(word.size() - j - 2) + word.back());
		}

		return res;
	}

	string makeAbbr(string& s, size_t prefixlen) {
		if (prefixlen + 2 >= s.size()) return s;
		return s.substr(0, prefixlen) + to_string(s.size() - prefixlen - 1) + s.back();
	}

	vector<string> wordsAbbreviation(vector<string>& dict) {
		size_t n = dict.size();
		vector<string> res(n);
		vector<bool> done(n);
		for (size_t prefixlen = 1, cnt = 0; cnt < n; prefixlen++) {
			unordered_map<string, vector<int>> dup;
			for (size_t i = 0; i < n; i++) {
				if (done[i]) continue;
				string abbr = makeAbbr(dict[i], prefixlen);
				dup[abbr].push_back(i);
			}
			for (auto& p : dup) {
				if (p.second.size() == 1) {
					cnt++;
					done[p.second[0]] = true;
					res[p.second[0]] = p.first;
				}
			}
		}

		return res;
	}

	void test()
	{
		vector<string> dict{ "like", "god", "internal", "me", "internet", "interval", "intension", "face", "intrusion" };
		vector<string> exp{ "l2e","god","internal","me","i6t","interval","inte4n","f2e","intr4n" };
		cout << (exp == wordsAbbreviation(dict)) << endl;
	}
};

