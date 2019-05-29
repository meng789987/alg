#include "pch.h"

/*
 * tags: stack, tree
 * Time(n), Space(n)
 * inorder traversal
 */

class lc0272 {
public:
	vector<int> closestKValues(TreeNode* root, double target, int k) {
		vector<int> res, path;
		inorder(root, target, k, path, res);
		while (res.size() < k) {
			res.push_back(path.back());
			path.pop_back();
		}

		return res;
	}

	void inorder(TreeNode* root, double target, int k, vector<int>& path, vector<int>& res) {
		if (!root || res.size() >= k) return;
		inorder(root->left, target, k, path, res);

		if (root->val < target) {
			path.push_back(root->val);
		}
		else {
			while (res.size() < k && path.size() > 0 && abs(path.back() - target) < abs(root->val - target)) {
				res.push_back(path.back());
				path.pop_back();
			}
			if (res.size() < k) res.push_back(root->val);
		}

		inorder(root->right, target, k, path, res);
	}

	void test() {
		auto root = new TreeNode(4);
		root->left = new TreeNode(2);
		root->left->left = new TreeNode(1);
		root->left->right = new TreeNode(3);
		root->right = new TreeNode(5);
		cout << (vector<int>{4, 3} == closestKValues(root, 3.714286, 2)) << endl;
	}
};
