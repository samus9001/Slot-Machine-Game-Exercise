using System;

namespace SlotMachine
{
    public static class LogicMethods
    {
        /// <summary>
        /// generates either a 3x1 or 3x3 array with random values between 0-9 based on amount of lines played
        /// </summary>
        /// <param name="rowCnt"></param>
        /// <param name="colCnt"></param>
        /// <param name="MAX_NUMBER"></param>
        /// <returns></returns>
        public static int[,] GenerateSlotMachineAny(int rowCnt, int colCnt, int MAX_NUMBER)
        {
            var rnd = new Random();
            int randomNumber;
            int[,] grid = new int[rowCnt, colCnt];

            for (int row = 0; row < rowCnt; row++)
            {
                for (int col = 0; col < colCnt; col++)
                {
                    randomNumber = rnd.Next(MAX_NUMBER);
                    grid[row, col] = randomNumber;
                }
            }
            return grid;
        }

        /// <summary>
        /// checks for matches on each row and stores each row number in a list
        /// </summary>
        /// <param name="rowCnt"></param>
        /// <param name="slotMachine"></param>
        /// <returns></returns>
        public static List<int> CheckWinningRows(int rowCnt, int[,] slotMachine)
        {
            List<int> winningRows = new List<int>();

            for (int row = 0; row < rowCnt; row++)
            {
                int first = slotMachine[row, 0];

                bool match = true;

                for (int col = 1; col < slotMachine.GetLength(1); col++)
                {
                    if (slotMachine[row, col] != first)
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
        /// <param name="rowCnt"></param>
        /// <param name="slotMachine"></param>
        /// <returns></returns>
        public static List<int> CheckWinningCols(int rowCnt, int[,] slotMachine)
        {
            List<int> winningCols = new List<int>();

            for (int col = 0; col < slotMachine.GetLength(1); col++)
            {
                int first = slotMachine[0, col];

                bool match = true;

                for (int row = 1; row < rowCnt; row++)
                {
                    if (slotMachine[row, col] != first)
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
        /// <param name="rowCnt"></param>
        /// <param name="grid3x3"></param>
        /// <param name="slotMachine"></param>
        /// <returns></returns>
        public static List<int> CheckWinningDiags(int rowCnt, int grid3x3, int[,] slotMachine)
        {
            List<int> winningDiags = new List<int>();

            if (rowCnt == grid3x3)
            {
                if (slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    winningDiags.Add(1);
                }
                if (slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    winningDiags.Add(2);
                }
            }
            return winningDiags;
        }

        /// <summary>
        /// checks for all matches and displays them if present then updates the balance
        /// </summary>
        /// <param name="rowCnt"></param>
        /// <param name="slotMachine"></param>
        /// <param name="grid3x3"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="LARGE_WIN"></param>
        /// <param name="MEDIUM_WIN"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static int GrantWins(int rowCnt, int[,] slotMachine, int grid3x3, int rows, int cols, int LARGE_WIN, int MEDIUM_WIN, int SMALL_WIN, int balance)
        {
            int winnings = 0;
            bool jackpotWin = false;
            bool bigWin = false;

            List<int> matchingRows = LogicMethods.CheckWinningRows(rowCnt, slotMachine);
            List<int> matchingColumns = LogicMethods.CheckWinningCols(rowCnt, slotMachine);
            List<int> matchingDiagonals = LogicMethods.CheckWinningDiags(rowCnt, grid3x3, slotMachine);

            if (matchingRows.Count == rows && matchingColumns.Count == cols)
            {
                UIMethods.DisplayJackpotWin(LARGE_WIN);
                winnings += LARGE_WIN;
                jackpotWin = true;
            }

            if (!jackpotWin && (matchingRows.Count == rows || matchingColumns.Count == cols))
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