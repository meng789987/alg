#include "pch.h"

/*
 * tags: design, trie
 * Time(n), Space(n)
 * trie
 */

class lc0568_FileSystem {
	struct Node {
		string name;
		string content;
		int type; // 0: directory; 1: file;
		map<string, Node*> children;
		Node(string name, int type = 0) : name(name), type(type) {}
		~Node() { for (auto& p : children) delete p.second; }
	};

	Node *root;

public:
	lc0568_FileSystem() {
		root = new Node("/");
	}

	Node* resolve(string path) {
		Node *node = root;
		for (size_t i = 1, j = 1; j < path.size(); i = j + 1) {
			j = path.find('/', i);
			string name = path.substr(i, j - i);
			if (node->children.find(name) == node->children.end())
				node->children[name] = new Node(name);
			node = node->children[name];
		}
		return node;
	}

	vector<string> ls(string path) {
		Node *node = resolve(path);
		vector<string> res;
		if (node->type == 1)
			res.push_back(node->name);
		else for (auto& p : node->children)
			res.push_back(p.first);
		return res;
	}

	void mkdir(string path) {
		resolve(path);
	}

	void addContentToFile(string filePath, string content) {
		Node *node = resolve(filePath);
		node->type = 1;
		node->content += content;
	}

	string readContentFromFile(string filePath) {
		Node *node = resolve(filePath);
		return node->content;
	}
};


/**
 * Your FileSystem object will be instantiated and called as such:
 * FileSystem* obj = new FileSystem();
 * vector<string> param_1 = obj->ls(path);
 * obj->mkdir(path);
 * obj->addContentToFile(filePath,content);
 * string param_4 = obj->readContentFromFile(filePath);
 */
