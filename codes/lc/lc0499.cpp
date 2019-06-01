#include "pch.h"

/*
 * tags: bfs, dfs
 * Time(mn), Space(mn)
 * just search using bfs by lexicographical order "dlru"
 */

class lc0499 {
	struct cell {
		int x, y, d;
		string path;
		cell(int x, int y, int d, string path)
			: x(x), y(y), d(d), path(path) {}
	};

public:
	string findShortestWay(vector<vector<int>>& maze, vector<int>& ball, vector<int>& hole) {
		const string DIR = "dlru";
		int DIRX[4] = { 1, 0, 0, -1 }; // order by dlru
		int DIRY[4] = { 0, -1, 1, 0 };
		int MASK[4] = { 2, 4, 8, 16 };

		size_t m = maze.size(), n = maze[0].size();
		queue<cell> q;
		for (int d = 0; d < 4; d++) {
			maze[ball[0]][ball[1]] |= MASK[d];
			q.emplace(ball[0], ball[1], d, "");
		}

		while (!q.empty()) {
			cell node = q.front(); q.pop();
			if (node.x == hole[0] && node.y == hole[1]) return node.path;

			for (int i = 0; i < 4; i++) {
				int d = (node.d + i) % 4;
				cell next(node.x + DIRX[d], node.y + DIRY[d], d, node.path);
				if (node.d != d || next.path.empty()) next.path += DIR[d];
				if (0 <= next.x && next.x < m && 0 <= next.y && next.y < n && maze[next.x][next.y] != 1) {
					if ((maze[next.x][next.y] & MASK[d]) == 0) { // not visited
						maze[next.x][next.y] |= MASK[d];
						q.push(next);
					}
					if (node.d == d) break;
				}
			}
		}

		return "impossible";
	}

	void test()
	{
		vector<vector<int>> maze{ {0,0,0,0,0}, {1,1,0,0,1}, {0,0,0,0,0}, {0,1,0,0,1}, {0,1,0,0,0} };
		vector<int> ball{ 4, 3 };
		vector<int> hole{ 0, 1 };
		cout << ("lul" == findShortestWay(maze, ball, hole)) << endl;

		maze = vector<vector<int>> { {0,0,0,0,0}, {1,1,0,0,1}, {0,0,0,0,0}, {0,1,0,0,1}, {0,1,0,0,0} };
		ball = vector<int> { 4, 3 };
		hole = vector<int> { 3, 0 };
		cout << ("impossible" == findShortestWay(maze, ball, hole)) << endl;
	}
};

