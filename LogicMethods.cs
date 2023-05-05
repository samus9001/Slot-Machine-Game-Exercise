using System;

namespace SlotMachine
{
    public static class LogicMethods
    {
        /// <summary>
        /// generates either a 3x1 or 3x3 array with random values between 0-9 based on amount of lines played
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int[,] GenerateSlotMachineAny(int[,] grid)
        {
            var rnd = new Random();
            int randomNumber;

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    randomNumber = rnd.Next(Program.MAX_NUMBER);
                    grid[row, col] = randomNumber;
                }
            }
            return grid;
        }

        /// <summary>
        /// checks for matches on each row and stores each row number in a list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<int> CheckWinningRows(int[,] grid)
        {
            List<int> winningRows = new List<int>();

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                int first = grid[row, 0];
                bool match = true;

                for (int col = 1; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] != first)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    winningRows.Add(row + 1);
                }
            }
            return winningRows;
        }

        /// <summary>
        /// checks for matches on each column and stores each column number in a list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<int> CheckWinningCols(int[,] grid)
        {
            List<int> winningCols = new List<int>();

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int first = grid[0, col];

                bool match = true;

                for (int row = 1; row < grid.GetLength(0); row++)
                {
                    if (grid[row, col] != first)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    winningCols.Add(col + 1);
                }
            }
            return winningCols;
        }

        /// <summary>
        /// checks for matches on each diagonal and stores each diagonal number in a list
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<int> CheckWinningDiags(int[,] grid)
        {
            List<int> winningDiags = new List<int>();

            if (grid.GetLength(0) == 3)
            {
                if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
                {
                    winningDiags.Add(1);
                }
                if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
                {
                    winningDiags.Add(2);
                }
            }
            return winningDiags;
        }

        /// <summary>
        /// checks for all matches and displays them if present then updates the balance
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="LARGE_WIN"></param>
        /// <param name="MEDIUM_WIN"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static int GrantWins(int[,] grid, int LARGE_WIN, int MEDIUM_WIN, int SMALL_WIN, int balance)
        {
            int winnings = 0;
            bool jackpotWin = false;
            bool bigWin = false;

            List<int> matchingRows = LogicMethods.CheckWinningRows(grid);
            List<int> matchingColumns = LogicMethods.CheckWinningCols(grid);
            List<int> matchingDiagonals = LogicMethods.CheckWinningDiags(grid);

            if (matchingRows.Count == Program.ROWS && matchingColumns.Count == Program.COLS)
            {
                UIMethods.DisplayJackpotWin(LARGE_WIN);
                winnings += LARGE_WIN;
                jackpotWin = true;
            }

            if (!jackpotWin && (matchingRows.Count == Program.ROWS || matchingColumns.Count == Program.COLS))
            {
                UIMethods.DisplayBigWin(MEDIUM_WIN);
                winnings += MEDIUM_WIN;
                bigWin = true;
            }

            if (!jackpotWin && !bigWin)
            {
                if (matchingRows.Count > 0)
                {
                    winnings += matchingRows.Count * SMALL_WIN;
                    UIMethods.DisplayRowsWin(matchingRows);
                }
                if (matchingColumns.Count > 0)
                {
                    winnings += matchingColumns.Count * SMALL_WIN;
                    UIMethods.DisplayColumnsWin(matchingColumns);
                }
                if (matchingDiagonals.Count > 0)
                {
                    winnings += matchingDiagonals.Count * SMALL_WIN;
                    UIMethods.DisplayDiagonalsWin(matchingDiagonals);
                }
            }

            if (winnings > 0)
            {
                UIMethods.DisplayTotalWin(winnings);
            }

            balance = balance + winnings;
            return balance;
        }
    }
}