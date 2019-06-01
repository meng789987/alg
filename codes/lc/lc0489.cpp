#include "pch.h"

/*
 * tags: dfs
 * Time(mn), Space(mn)
 * dfs traversal
 */

class lc0489 {
	
	// This is the robot's control interface.
	// You should not implement it, or speculate about its implementation
	class Robot {
	  public:
	    // Returns true if the cell in front is open and robot moves into the cell.
	    // Returns false if the cell in front is blocked and robot stays in the current cell.
	    bool move();
	
	    // Robot will stay in the same cell after calling turnLeft/turnRight.
	    // Each turn will be 90 degrees.
	    void turnLeft();
	    void turnRight();
	
	    // Clean the current cell.
	    void clean();
	};
	
public:
	vector<int> DIRS{ 0, 1, 0, -1, 0 };
	const long N = 100000000;

	void cleanRoom(Robot& robot) {
		unordered_set<long> visited;
		dfs(robot, 0, 0, visited);
	}

	void dfs(Robot& robot, long pos, int dir, unordered_set<long>& visited) {
		if (!visited.insert(pos).second) return;
		robot.clean();

		for (int d = 0; d < 4; d++) {
			int nd = (dir + d) % 4;
			long next = (pos / N + DIRS[nd]) * N + (pos%N + DIRS[nd + 1]);
			if (robot.move()) {
				dfs(robot, next, nd, visited);
				robot.turnRight(); robot.turnRight();
				robot.move(); // reset
				robot.turnRight(); robot.turnRight();
			}
			robot.turnRight();
		}
	}
};

