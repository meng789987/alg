#include "pch.h"

/*
 * tags: design
 * Time(mn), Space(mn)
 * recursive calculate the sum when request.
 */

class lc0499_Excel {
	struct Cell {
		int val = 0;
		vector<Cell*> refs;
		int get() {
			int sum = val;
			for (Cell* c : refs) sum += c->get();
			return sum;
		}
	};

	vector<vector<Cell>> cells;

public:
	lc0499_Excel(int H, char W) {
		cells.resize(H, vector<Cell>(W - 'A' + 1));
	}

	void set(int r, char c, int v) {
		Cell& cell = cells[r - 1][c - 'A'];
		cell.val = v;
		cell.refs.clear();
	}

	int get(int r, char c) {
		return cells[r - 1][c - 'A'].get();
	}

	int sum(int r, char c, vector<string> strs) {
		Cell& cell = cells[r - 1][c - 'A'];
		cell.val = 0;
		cell.refs.clear();

		for (string& s : strs) {
			if (s.find(':') == string::npos) {
				int i = stoi(s.substr(1)) - 1, j = s[0] - 'A';
				cell.refs.push_back(&cells[i][j]);
			}
			else {
				int idx = s.find(':');
				int row1 = stoi(s.substr(1, idx - 1)) - 1, col1 = s[0] - 'A';
				int row2 = stoi(s.substr(idx + 2)) - 1, col2 = s[idx + 1] - 'A';
				for (int i = row1; i <= row2; i++) {
					for (int j = col1; j <= col2; j++)
						cell.refs.push_back(&cells[i][j]);
				}
			}
		}

		return cell.get();
	}
};

/**
 * Your Excel object will be instantiated and called as such:
 * Excel* obj = new Excel(H, W);
 * obj->set(r,c,v);
 * int param_2 = obj->get(r,c);
 * int param_3 = obj->sum(r,c,strs);
 */