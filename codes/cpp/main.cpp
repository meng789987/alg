#include "pch.h"

#include "tree\_Binary_Index_Tree.cpp"


void test();

int main()
{
	cout << boolalpha;
	cout << "_Binary_Index_Tree" << endl;
	_Binary_Index_Tree_Test::Test();
	//test();
	return 0;
}

void test()
{
	size_t i = 7;
	cout << (i&(i)) << endl;
}

/*
// trick: putting this in the head of the codes in leetcode will make it much faster.

#pragma GCC optimize("O3")
static const auto _leetcode_speedup_ = [](){
	std::ios::sync_with_stdio(false);
	std::cin.tie(nullptr);
	std::cout.tie(nullptr);
	return nullptr;
}();

*/