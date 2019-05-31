#include "pch.h"

/*
 * tags: bt, trie
 * Time(m^n), Space(mn), m is the length of word
 * build a trie to find the next qualified words effiently in O(m).
 */

class lc0425 {
public:
	vector<vector<string>> wordSquares(vector<string>& words) {
		size_t n = words.size();
		for (int i = 0; i < n; i++) {
			Node* node = &root;
			node->words.push_back(i);
			for (char c : words[i]) {
				int ci = c - 'a';
				if (!node->children[ci]) 
					node->children[ci] = new Node();
				node = node->children[ci];
				node->words.push_back(i);
			}
		}

		vector<vector<string>> res;
		vector<string> path;
		bt(words, path, res);

		return res;
	}

	void bt(vector<string>& words, vector<string>& path, vector<vector<string>>& res) {
		if (path.size() == words[0].size()) {
			res.push_back(path);
			return;
		}

		Node* node = &root;
		int col = path.size();
		for (int i = 0; i < path.size() && node; i++) {
			int ci = path[i][col] - 'a';
			node = node->children[ci];
		}

		if (!node) return;
		for (int i : node->words) {
			path.push_back(words[i]);
			bt(words, path, res);
			path.erase(path.end() - 1);
		}
	}

	struct Node {
		vector<Node*> children;
		vector<int> words;
		Node() : children(26, NULL) {}
		~Node() {
			for (int i = 0; i < 26; i++)
				delete children[i];
		}
	};

	Node root;


	void test()
	{
		vector<string> words{ "area", "lead", "wall", "lady", "ball" };
		vector<vector<string>> exp{ {"wall", "area", "lead", "lady"}, {"ball", "area", "lead", "lady"} };
		cout << (exp == wordSquares(words)) << endl;
	}
};

