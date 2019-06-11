#include "pch.h"

/*
 * tags: tree, design
 * Time(n), Space(n)
 * first child is encoded to its left, the other children are encoded to its left's right, and right, and so on.
 */

class lc0431_Codec {

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

public:

	// Encodes an n-ary tree to a binary tree.
	TreeNode* encode(Node* root) {
		if (!root) return NULL;
		auto tree = new TreeNode(root->val);
		if (!root->children.empty())
			tree->left = encode(root->children[0]);
		auto tnode = tree->left;
		for (size_t i = 1; i < root->children.size(); i++) {
			tnode->right = encode(root->children[i]);
			tnode = tnode->right;
		}

		return tree;
	}

	// Decodes your binary tree to an n-ary tree.
	Node* decode(TreeNode* root) {
		if (!root) return NULL;
		auto nroot = new Node();
		nroot->val = root->val;
		if (root->left) {
			nroot->children.push_back(decode(root->left));
			auto node = root->left;
			while (node->right) {
				nroot->children.push_back(decode(node->right));
				node = node->right;
			}
		}

		return nroot;
	}
};

// Your Codec object will be instantiated and called as such:
// Codec codec;
// codec.decode(codec.encode(root));
