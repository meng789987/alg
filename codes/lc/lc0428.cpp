#include "pch.h"

/*
 * tags: tree
 * Time(n), Space(n)
 * 1. serialize each node as "val;children count;"
 * 2. serialize each node as "level;val;"
 */


 // Definition for a Node.
 class Node {
 public:
	 int val = NULL;
	 vector<Node*> children;

	 Node() {}

	 Node(int _val, vector<Node*> _children) {
		 val = _val;
		 children = _children;
	 }
 };

class lc0428_Codec {
public:

	// Encodes a tree to a single string.
	string serialize(Node* root) {
		if (!root) return "";
		string res = to_string(root->val) + ';' + to_string(root->children.size()) + ';';
		for (Node* node : root->children)
			res += serialize(node);
		return res;
	}

	// Decodes your encoded data to tree.
	Node* deserialize(string data) {
		if (data.empty()) return NULL;
		vector<int> values;
		for (char *p = (char*)data.c_str(); *p; ++p)
			values.push_back(strtol(p, &p, 10));
		int idx = 0;
		return deserialize(values, idx);
	}

	Node* deserialize(vector<int>& values, int& idx) {
		int val = values[idx++];
		vector<Node*> children(values[idx++]);
		for (int i = 0; i < children.size(); i++)
			children[i] = deserialize(values, idx);
		return new Node(val, children);
	}
};

// Your Codec object will be instantiated and called as such:
// Codec codec;
// codec.deserialize(codec.serialize(root));

