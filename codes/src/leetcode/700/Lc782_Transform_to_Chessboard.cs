using System;
using System.Collections.Generic;
using System.Linq;

/*
 * tags: adhoc
 * Time(n^2), Space(1)
 */
namespace leetcode
{
    public class Lc782_Transform_to_Chessboard
    {
        /// <summary>
        /// 1. The 4 corners of any rectangle inside a chessboard must be 4 ones, or 4 zeroes, or 2 ones and 2 zeroes, that is NW^NE^SW^SE==0
        /// 2. Given a row, any other row must be identical to it or its mirror.
        /// so we only need to count the total swap to make first row and first column to 01010... or 10101..., which one should we make?
        /// In case of n even, take the minimum one, because both are possible.
        /// In case of n odd, take the even swaps. Because when we make a swap, we move two rows or two columns at the same time.
        /// </summary>
        public int MovesToChessboard(int[,] board)
        {
            int n = board.GetLength(0);
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                    if ((board[0, 0] ^ board[0, c] ^ board[r, 0] ^ board[r, c]) == 1)
                        return -1;
            }


            int rowSum = 0, colSum = 0, rowSwap = 0, colSwap = 0;
            for (int i = 0; i < n; i++)
            {
                rowSum += board[i, 0];
                colSum += board[0, i];
                if (board[i, 0] == i % 2) rowSwap++;
                if (board[0, i] == i % 2) colSwap++;
            }

            if (rowSum != n / 2 && rowSum != (n + 1) / 2) return -1;
            if (colSum != n / 2 && colSum != (n + 1) / 2) return -1;

            if (n % 2 == 1)
            {
                if (rowSwap % 2 == 1) rowSwap = n - rowSwap;
                if (colSwap % 2 == 1) colSwap = n - colSwap;
            }
            else
            {
                rowSwap = Math.Min(rowSwap, n - rowSwap);
                colSwap = Math.Min(colSwap, n - colSwap);
            }

            return (rowSwap + colSwap) / 2;
        }

        public void Test()
        {
            var board = new int[,] { { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 1, 0, 0, 1 }, { 1, 0, 0, 1 } };
            Console.WriteLine(MovesToChessboard(board) == 2);

            board = new int[,] { { 0, 1 }, { 1, 0 } };
            Console.WriteLine(MovesToChessboard(board) == 0);

            board = new int[,] { { 1, 0 }, { 0, 1 } };
            Console.WriteLine(MovesToChessboard(board) == 0);

            board = new int[,] { { 1, 0 }, { 1, 0 } };
            Console.WriteLine(MovesToChessboard(board) == -1);

            board = new int[,] { { 1, 1, 0 }, { 0, 0, 1 }, { 0, 0, 1 } };
            Console.WriteLine(MovesToChessboard(board) == 2);

            board = new int[,] { { 1, 0, 0 }, { 0, 1, 1 }, { 1, 0, 0 } };
            Console.WriteLine(MovesToChessboard(board) == 1);

            board = new int[,] { { 0, 0, 1, 0, 1, 1 }, { 1, 1, 0, 1, 0, 0 }, { 1, 1, 0, 1, 0, 0 }, { 0, 0, 1, 0, 1, 1 }, { 1, 1, 0, 1, 0, 0 }, { 0, 0, 1, 0, 1, 1 } };
            Console.WriteLine(MovesToChessboard(board) == 2);
        }
    }
}
