#include "pch.h"

/*
 * tags: design, trie, sort
 * Time(nlogn), Space(n)
 */

class lc0642_AutocompleteSystem {
	template<class Comp> struct Node {
		unordered_map<char, Node*> children;
		set<int, Comp> texts;
		int textidx = -1;

		Node(const Comp& comp) : texts(comp) {}
		~Node() { for (Node* n : children) delete n; }
	};

	function<bool(int, int)> comp = [this](int i, int j) {
		return times[i] != times[j] ? times[j] < times[i] : texts[i] < texts[j];
	};

	Node<decltype(comp)> *root, *curr;
	string currtext;
	vector<string> texts;
	vector<int> times;

	void insertText(int i) {
		auto node = root;
		for (char c : texts[i]) {
			if (!node->children[c])
				node->children[c] = new Node<decltype(comp)>(comp);
			node = node->children[c];
			node->texts.insert(i);
		}
		node->textidx = i;
	}

	void eraseText(int i) {
		auto node = root;
		for (char c : texts[i]) {
			node = node->children[c];
			node->texts.erase(i);
		}
		node->textidx = -1;
	}

public:
	lc0642_AutocompleteSystem(vector<string>& sentences, vector<int>& times) {
		texts = sentences;
		this->times = times;
		root = curr = new Node<decltype(comp)>(comp);
		for (int i = 0; i < (int)texts.size(); i++) {
			insertText(i);
		}
	}

	vector<string> input(char c) {
		if (c == '#') {
			if (!curr || curr->textidx == -1) {
				texts.push_back(currtext);
				times.push_back(1);
				insertText(texts.size() - 1);
			}
			else {
				int i = curr->textidx;
				eraseText(i);
				times[i]++;
				insertText(i);
			}
			curr = root;
			currtext = "";
			return vector<string>();
		}

		vector<string> res;
		currtext += c;
		curr = curr ? curr->children[c] : NULL;
		if (!curr) return res;

		// or we can sort here, then no need for customized comparison in the set/vector
		for (int i : curr->texts) {
			res.push_back(texts[i]);
			if (res.size() == 3) break;
		}

		return res;
	}
};

/**
 * Your AutocompleteSystem object will be instantiated and called as such:
 * AutocompleteSystem* obj = new AutocompleteSystem(sentences, times);
 * vector<string> param_1 = obj->input(c);
 */
