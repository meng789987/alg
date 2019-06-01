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

void LocalTest();
void RunLeetCodeTest()
{
	cout << "lc0499" << endl;
	lc0499().test();
	//LocalTest();
}

void LocalTest()
{
	cout << ((-3) & 1) << endl;
}

#endif //LEETCODE_H
