#include "pch.h"

/*
 * tags: dfs, Dihedral groups
 * Time(mn), Space(mn)
 * it's easy to use dfs to find all islands, it's hard to distinct them.
 * Dihedral groups: https://en.wikipedia.org/wiki/Dihedral_group
 */

class lc0711 {
public:
	int numDistinctIslands2(vector<vector<int>>& grid) {
		set<vector<int>> lands;
		for (int i = 0; i < grid.size(); i++) {
			for (int j = 0; j < grid[0].size(); j++) {
				if (grid[i][j] == 1) {
					vector<pair<int, int>> land;
					dfs(grid, i, j, land);
					lands.insert(normalize(land));
				}
			}
		}

		return lands.size();
	}

	void dfs(vector<vector<int>>& grid, int row, int col, vector<pair<int, int>>& land) {
		land.push_back({ row, col });
		grid[row][col] = -1;

		int dirs[] = { 1,0,-1,0,1 };
		for (int d = 0; d < 4; d++) {
			int r = row + dirs[d], c = col + dirs[d + 1];
			if (0 <= r && r < grid.size() && 0 <= c && c < grid[0].size() && grid[r][c] == 1)
				dfs(grid, r, c, land);
		}
	}

	// Dihedral groups: https://en.wikipedia.org/wiki/Dihedral_group
	vector<int> normalize(vector<pair<int, int>>& shape) {
		vector<vector<int>> all(8);
		for (auto& p : shape) {
			all[0].push_back(p.first * 100 + p.second);
			all[1].push_back(-p.first * 100 + p.second);
			all[2].push_back(p.first * 100 - p.second);
			all[3].push_back(-p.first * 100 - p.second);
			all[4].push_back(p.second * 100 + p.first);
			all[5].push_back(-p.second * 100 + p.first);
			all[6].push_back(p.second * 100 - p.first);
			all[7].push_back(-p.second * 100 - p.first);
		}

		for (auto& v : all) {
			sort(v.begin(), v.end());
			for (int i = 1; i < v.size(); i++) v[i] -= v[0];
			v[0] = 0;
		}

		sort(all.begin(), all.end());
		return all[0];
	}

	void test()
	{
		vector<vector<int>> grid{ {1,1,0,0,0}, {1,0,0,0,0}, {0,0,0,0,1}, {0,0,0,1,1} };
		cout << (1 == numDistinctIslands2(grid)) << endl;

		grid = vector<vector<int>>{ {1,1,1,0,0}, {1,0,0,0,1}, {0,1,0,0,1}, {0,1,1,1,0} };
		cout << (2 == numDistinctIslands2(grid)) << endl;
	}
};

