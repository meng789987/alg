#ifndef COMMONSTD_H
#define COMMONSTD_H

#include <iterator>
#include <ios>
#include <istream>
#include <ostream>
#include <iostream>

#include <string>
#include <stack>
#include <vector>
#include <list>
#include <queue>
#include <deque>
#include <map>
#include <set>
#include <unordered_map>
#include <unordered_set>

#include <algorithm>
#include <new>
#include <tuple>
#include <utility>
#include <functional>

using namespace std;

struct TreeNode {
	int val;
	TreeNode *left;
	TreeNode *right;
	TreeNode(int x) : val(x), left(NULL), right(NULL) {}
};

#endif //COMMONSTD_H
