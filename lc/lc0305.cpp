#include "pch.h"

/*
 * tags: disjoint set
 * Time(klogk), Space(k)
 */

class lc0305 {
public:
    vector<int> numIslands2(int m, int n, vector<vector<int>>& positions) {
        parents.clear();
        vector<int> res;
        int cnt = 0;
        int dirs[] = { 1, 0, -1, 0, 1 };
        for (auto& v : positions) {
            int cur = v[0]*n + v[1];
            if (parents.find(cur) == parents.end()) {
                parents[cur] = cur;
                cnt++;
            }
            for (int d = 0; d < 4; d++) {
                int x = v[0] + dirs[d], y = v[1] + dirs[d+1];
                int adj = x*n + y;
                if (0 <= x && x < m && 0 <= y && y < n
                        && parents.find(adj) != parents.end() && merge(cur, adj))
                    cnt--;
            }
            res.push_back(cnt);
        }
        
        return res;
    }
    
    unordered_map<int, int> parents;
    
    int find(int i) {
        if (parents[i] != i)
            parents[i] = find(parents[i]);
        return parents[i];
    }
    
    bool merge(int i, int j) {
        int pi = find(i);
        int pj = find(j);
        if (pi == pj) return false;
        parents[pi] = pj;
        return true;
    }

	void test() {
		vector<vector<int>> lands = { {0,0}, {0,1}, {1,2}, {2,1} };
		cout << (vector<int>{1,1,2,3} == numIslands2(3, 3, lands)) << endl;
	}
};
