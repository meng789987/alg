
/*
 * tags: topology sort, bfs/dfs
 * Time(N+E), Space(N)
 */

class Solution {
public:
    string alienOrder(vector<string>& words) {
        unordered_map<char, int> degrees;
        for (auto& word : words) {
            for (char c : word)
                degrees[c] = 0;
        }
        
        unordered_map<char, unordered_set<char>> dag;
        for (int i = 1; i < words.size(); i++) {
            string& wa = words[i-1], wb = words[i];
            for (int j = 0; j < wa.size() && j < wb.size(); j++) {
                if (wa[j] != wb[j]) {
                    if (dag[wa[j]].insert(wb[j]).second) degrees[wb[j]]++;
                    break;
                }
            }
        }
        
        queue<char> q;
        for (auto& p : degrees) {
            if (p.second == 0) q.push(p.first);
        }
        
        vector<char> res;
        while (q.size() > 0) {
            char c = q.front(); q.pop();
            res.push_back(c);
            for (char b : dag[c]) {
                if (--degrees[b] == 0) q.push(b);
            }
        }
        
        if (res.size() != degrees.size()) return "";
        return string(res.begin(), res.end());
    }
};

