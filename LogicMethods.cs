using System;

namespace SlotMachine
{
    public static class LogicMethods
    {
        /// <summary>
        /// generates a 3x3 array with random values between 0-9
        /// </summary>
        /// <returns></returns>
        public static int[,] GenerateSlotMachineArray(char userInput)
        {
            int[,] grid = new int[Program.ROWS, Program.COLS];
            var rnd = new Random();
            int randomNumber;

            if (userInput == '1')
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (row == 0 || row == 2)
                        {
                            grid[row, col] = 0;
                        }
                        else if (row == 1)
                        {
                            randomNumber = rnd.Next(Program.MAX_NUMBER);
                            grid[row, col] = randomNumber;
                        }
                    }
                }
            }

            if (userInput == '3')
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        randomNumber = rnd.Next(Program.MAX_NUMBER);
                        grid[row, col] = randomNumber;
                    }
                }
            }
            return grid;
        }

        /// <summary>
        /// checks for matches on the single row for playing one line
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool CheckSingleWinningRow(int[,] grid)
        {
            int first = grid[1, 0];

            return grid[1, 1] == first && grid[1, 2] == first;
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

            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
            {
                winningDiags.Add(1);
            }
            if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
            {
                winningDiags.Add(2);
            }
            return winningDiags;
        }

        /// <summary>
        /// checks for a jackpot match
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="matchingCols"></param>
        /// <returns></returns>
        public static bool CheckJackpotMatch(List<int> matchingRows, List<int> matchingCols)
        {
            bool jackpotMatch = false;

            if (matchingRows.Count == Program.ROWS && matchingCols.Count == Program.COLS)
            {
                jackpotMatch = true;
            }
            return jackpotMatch;
        }

        /// <summary>
        /// checks if there is a big match and updates a bool to store it
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="matchingCols"></param>
        /// <returns></returns>
        public static bool CheckBigMatch(List<int> matchingRows, List<int> matchingCols)
        {
            bool bigMatch = false;

            if ((matchingRows.Count == Program.ROWS || matchingCols.Count == Program.COLS))
            {
                bigMatch = true;
            }
            return bigMatch;
        }

        /// <summary>
        /// checks if there is a single row match and updates a bool to store it
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool CheckSingleRowMatch(int[,] grid)
        {
            bool singleRowMatch = false;

            if (CheckSingleWinningRow(grid))
            {
                singleRowMatch = true;
            }
            return singleRowMatch;
        }

        /// <summary>
        /// checks if there are matching rows and updates a bool to store it
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <returns></returns>
        public static bool CheckRowsMatch(List<int> matchingRows)
        {
            bool rowsMatch = false;

            if (matchingRows.Count > 0)
            {
                rowsMatch = true;
            }
            return rowsMatch;
        }

        /// <summary>
        /// checks if there are matching columns and updates a bool to store it
        /// </summary>
        /// <param name="matchingCols"></param>
        /// <returns></returns>
        public static bool CheckColsMatch(List<int> matchingCols)
        {
            bool colsMatch = false;

            if (matchingCols.Count > 0)
            {
                colsMatch = true;
            }
            return colsMatch;
        }

        /// <summary>
        /// checks if there are matching diagonals and updates a bool to store it
        /// </summary>
        /// <param name="matchingDiags"></param>
        /// <returns></returns>
        public static bool CheckDiagsMatch(List<int> matchingDiags)
        {
            bool diagsMatch = false;

            if (matchingDiags.Count > 0)
            {
                diagsMatch = true;
            }
            return diagsMatch;
        }

        /// <summary>
        /// sets the win amount when a match is found
        /// </summary>
        /// <param name="jackpotMatch"></param>
        /// <param name="bigMatch"></param>
        /// <param name="singleRowMatch"></param>
        /// <param name="rowsMatch"></param>
        /// <param name="colsMatch"></param>
        /// <param name="diagsMatch"></param>
        /// <param name="matchingRows"></param>
        /// <param name="matchingCols"></param>
        /// <param name="matchingDiags"></param>
        /// <returns></returns>
        public static int GrantWins(bool jackpotMatch, bool bigMatch, bool singleRowMatch, bool rowsMatch, bool colsMatch, bool diagsMatch, List<int> matchingRows, List<int> matchingCols, List<int> matchingDiags)
        {
            int winnings = 0;

            if (jackpotMatch)
            {
                winnings += Program.LARGE_WIN;
            }

            if (!jackpotMatch && bigMatch)
            {
                winnings += Program.MEDIUM_WIN;
            }

            if (!jackpotMatch && !bigMatch && singleRowMatch)
            {
                winnings += Program.SMALL_WIN;
            }

            if (!jackpotMatch && !bigMatch && rowsMatch)
            {
                winnings += matchingRows.Count * Program.SMALL_WIN;
            }

            if (!jackpotMatch && !bigMatch && colsMatch)
            {
                winnings += matchingCols.Count * Program.SMALL_WIN;
            }

            if (!jackpotMatch && !bigMatch && diagsMatch)
            {
                winnings += matchingDiags.Count * Program.SMALL_WIN;
            }
            return winnings;
        }

        /// <summary>
        /// checks if the winnings has increased and updates a bool to store it
        /// </summary>
        /// <param name="winnings"></param>
        public static bool WinTotal(int winnings)
        {
            bool winAmount = false;

            if (winnings > 0)
            {
                winAmount = true;
            }
            return winAmount;
        }
    }
}