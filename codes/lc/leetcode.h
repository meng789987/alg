#ifndef LEETCODE_H
#define LEETCODE_H

#include "lc0158.cpp"
#include "lc0159.cpp"
#include "lc0248.cpp"
#include "lc0265.cpp"
#include "lc0269.cpp"
#include "lc0272.cpp"
#include "lc0291.cpp"
#include "lc0296.cpp"
#include "lc0302.cpp"
#include "lc0308.cpp"
#include "lc0317.cpp"
#include "lc0340.cpp"
#include "lc0358.cpp"
#include "lc0411.cpp"
#include "lc0425.cpp"
#include "lc0428.cpp"
#include "lc0431.cpp"
#include "lc0465.cpp"
#include "lc0471.cpp"
#include "lc0499.cpp"
#include "lc0527.cpp"
#include "lc0568.cpp"
#include "lc0644.cpp"
#include "lc0656.cpp"
#include "lc0660.cpp"
#include "lc0683.cpp"
#include "lc0711.cpp"
#include "lc0727.cpp"
#include "lc0772.cpp"
#include "lc1063.cpp"
#include "lc1067.cpp"

void LocalTest();
void RunLeetCodeTest()
{
	cout << "lc1063" << endl;
	lc1063().test();
	//LocalTest();
}

void LocalTest()
{
	cout << ((-3) & 1) << endl;
}

/*
// trick: putting this in the head of the codes in leetcode will make it much faster.

#pragma GCC optimize("O3")
static const auto __ = [](){
	std::ios::sync_with_stdio(false);
	std::cin.tie(nullptr);
	std::cout.tie(nullptr);
	return nullptr;
}();

*/

#endif //LEETCODE_H
