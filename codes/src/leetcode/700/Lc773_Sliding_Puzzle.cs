using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: bfs
 * Time(n!), Space(n!)
 */
namespace leetcode
{
    public class Lc773_Sliding_Puzzle
    {
        public int SlidingPuzzle(int[,] board)
        {
            var state = "" + board[0, 0] + board[0, 1] + board[0, 2] + board[1, 0] + board[1, 1] + board[1, 2];
            if (_stepCache.ContainsKey(state)) return _stepCache[state];
            return -1;
        }

        static Lc773_Sliding_Puzzle()
        {
            var okState = "123450";
            var seen = new HashSet<string>();
            seen.Add(okState);
            var q = new Queue<string>();
            q.Enqueue(okState);

            int[] dirs = { 1, -1, 3, -3 };
            for (int step = 0; q.Count > 0; step++)
            {
                for (int cnt = q.Count; cnt > 0; cnt--)
                {
                    var state = q.Dequeue();
                    _stepCache[state] = step;

                    int i = state.IndexOf('0');
                    foreach (int d in dirs)
                    {
                        int j = i + d;
                        if (j < 0 || j > 5 || i == 2 && j == 3 || i == 3 && j == 2) { continue; }
                        var cs = state.ToArray();
                        char tmp = cs[i];
                        cs[i] = cs[j];
                        cs[j] = tmp;
                        var newState = new string(cs);
                        if (seen.Add(newState)) q.Enqueue(newState);
                    }
                }
            }

        }

        static Dictionary<string, int> _stepCache = new Dictionary<string, int>();

        public void Test()
        {
            var board = new int[,] { { 1, 2, 3 }, { 4, 0, 5 } };
            Console.WriteLine(SlidingPuzzle(board) == 1);

            board = new int[,] { { 1, 2, 3 }, { 5, 4, 0 } };
            Console.WriteLine(SlidingPuzzle(board) == -1);

            board = new int[,] { { 4, 1, 2 }, { 5, 0, 3 } };
            Console.WriteLine(SlidingPuzzle(board) == 5);

            board = new int[,] { { 3, 2, 4 }, { 1, 5, 0 } };
            Console.WriteLine(SlidingPuzzle(board) == 14);
        }
    }
}
