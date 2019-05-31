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
#include "lc0425.cpp"
#include "lc0428.cpp"

void LocalTest();
void RunLeetCodeTest()
{
	cout << "lc0411" << endl;
	lc0411().test();
	//LocalTest();
}

void LocalTest()
{
	string a = "12;234;2;4;";
	vector<int> values;
	for (char *p = (char*)a.c_str(); *p; ++p)
		values.push_back(strtol(p, &p, 10));
	for (size_t last = 0, next; (next = a.find(';', last)) != string::npos; last = next + 1)
		values.push_back(strtol(a.c_str() + last, NULL, 10));
}

#endif //LEETCODE_H
