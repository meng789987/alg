#ifndef LEETCODE_H
#define LEETCODE_H

#include "lc0158.cpp"
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

void LocalTest();
void RunLeetCodeTest()
{
	cout << "lc0411" << endl;
	lc0411().test();
	//LocalTest();
}

void LocalTest()
{
	if (true) {
		vector<int> v(10, 5);
		v[4] = 300;
	}
	vector<int> v(10);
	v[20] = 4;
	cout << v.size() << endl;
}

#endif //LEETCODE_H
