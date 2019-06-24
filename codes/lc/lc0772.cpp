#include "pch.h"

/*
 * tags: stack
 * Time(n), Space(n)
 * calculator: lc0224, lc0227
 */

class lc0772 {
public:
	int calculate(string s) {
		stack<pair<long, long>> res; // <operand, operator>
		long num = 0;
		s += '+'; // make things easy

		for (int i = 0; i < s.size(); i++) {
			char c = s[i];
			if (c == ' ') continue;
			if (c == '(') res.push({ num, c });
			else if(c == '*' || c == '/') {
				if (!res.empty() && (res.top().second == '*' || res.top().second == '/')) {
					long a = res.top().first, op = res.top().second;
					res.pop();
					num = EvalExp(a, op, num);
				}
				res.push({ num, c });
			}
			else if (c == ')' || c == '+' || c == '-') {
				while (!res.empty() && res.top().second != '(') {
					long a = res.top().first, op = res.top().second;
					res.pop();
					num = EvalExp(a, op, num);
				}
				if (c == ')') res.pop();
				else res.push({ num, c });
			}
			else { // digit
				num = s[i] - '0';
				for (; '0' <= s[i + 1] && s[i + 1] <= '9'; i++)
					num = num * 10 + (s[i + 1] - '0');
			}
		}

		return num;
	}

	long EvalExp(long a, long op, long b) {
		switch (op) {
			case '*': a *= b; break;
			case '/': a /= b; break;
			case '+': a += b; break;
			case '-': a -= b; break;
		}

		return a;
	}


	void test()
	{
		cout << (2 == calculate("1 + 1")) << endl;
		cout << (4 == calculate(" 6-4 / 2 ")) << endl;
		cout << (21 == calculate("2*(5+5*2)/3+(6/2+8)")) << endl;
		cout << (-12 == calculate("(2+6* 3+5- (3*14/7+2)*5)+3")) << endl;
		cout << (-2147483648LL == calculate("0-2147483648")) << endl;
	}
};

