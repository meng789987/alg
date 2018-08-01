using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using alg;

/*
 * tags: dfs, backtracking
 * Time(2^n), Space(n)
 * try insert any hand ball into each position. incorrect pruning: only try to insert the same hand ball
 */
namespace leetcode
{
    public class Lc488_Zuma_Game
    {
        public int FindMinStep(string board, string hand)
        {
            var handcs = hand.ToCharArray();
            Array.Sort(handcs);
            hand = new string(handcs);
            return Dfs(board, hand, new Dictionary<string, int>());
        }

        int Dfs(string board, string hand, Dictionary<string, int> memo)
        {
            if (string.IsNullOrEmpty(board)) return 0;
            var key = board + "=" + hand;
            if (memo.ContainsKey(key)) return memo[key];

            int min = int.MaxValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (i > 0 && board[i - 1] == board[i]) continue;
                int idx = hand.IndexOf(board[i]);
                if (idx < 0) continue;
                var newHand = hand.Substring(0, idx) + hand.Substring(idx + 1);
                var newBoard = MergeBoard(board.Insert(i, board[i].ToString()), i);
                int r = Dfs(newBoard, newHand, memo);
                if (0 <= r && r < min) min = r;
            }

            return memo[key] = min == int.MaxValue ? -1 : 1 + min;
        }

        string MergeBoard(string board, int pos)
        {
            int n = board.Length;
            if (pos >= n) return board;
            int i = pos - 1, j = pos + 1;
            while (i >= 0 && board[i] == board[pos]) i--;
            while (j < n && board[j] == board[pos]) j++;
            if (j - i <= 3) return board;
            return MergeBoard(board.Substring(0, i + 1) + board.Substring(j), i + 1);
        }

        public void Test()
        {
            Console.WriteLine(FindMinStep("WRRBBW", "RB") == -1);
            Console.WriteLine(FindMinStep("WWRRBBWW", "WRBRW") == 2);
            Console.WriteLine(FindMinStep("G", "GGGGG") == 2);
            Console.WriteLine(FindMinStep("RBYYBBRRB", "YRBGB") == 3);
        }
    }
}

