﻿using System;

namespace SlotMachine
{
    public static class LogicMethods
    {
        /// <summary>
        /// generates SlotMachine array values based on amount of lines played
        /// </summary>
        /// <param name="slotMachine"></param>
        /// <param name="amountRows"></param>
        /// <param name="MAX_NUMBER"></param>
        /// <returns></returns>
        public static int[,] GenerateRndGridAny(int[,] slotMachine, int amountRows, int MAX_NUMBER)
        {
            var rnd = new Random();
            int randomNumber;
            int cols = slotMachine.GetLength(1);

            for (int i = 0; i < amountRows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    randomNumber = rnd.Next(MAX_NUMBER);
                    slotMachine[i, j] = randomNumber;
                }
            }
            return slotMachine;
        }
        //public static int[,] GenerateSlotMachine3x3( int MAX_NUMBER)
        //{
        //    var rnd = new Random();
        //    int randomNumber;
        //    int[,] grid = new int[3, 3];

        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            randomNumber = rnd.Next(MAX_NUMBER);
        //            grid[i, j] = randomNumber;
        //        }
        //    }
        //    return grid;
        //}
        //public static int[,] GenerateSlotMachineAny(int rowCnt, int colCnt, int MAX_NUMBER)
        //{
        //    var rnd = new Random();
        //    int randomNumber;
        //    int[,] grid = new int[rowCnt, colCnt];

        //    for (int i = 0; i < rowCnt; i++)
        //    {
        //        for (int j = 0; j < colCnt; j++)
        //        {
        //            randomNumber = rnd.Next(MAX_NUMBER);
        //            grid[i, j] = randomNumber;
        //        }
        //    }
        //    return grid;
        //}


        /// <summary>
        /// checks for matches on all rows
        /// </summary>
        /// <param name="amountRows"></param>
        /// <param name="slotMachine"></param>
        /// <param name="matchingRows"></param>
        /// <returns></returns>
        public static int CheckWinningRows(int amountRows, int[,] slotMachine)
        {
            int winningRows = 0;

            for (int i = 0; i < amountRows; i++)
            {
                if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                {
                    winningRows++;
                }
            }
            return winningRows;
        }

        /// <summary>
        /// checks for matches on all columns
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="slotMachine"></param>
        /// <param name="winningCols"></param>
        public static int CheckWinningColumns(int amountRows, int[,] slotMachine)
        {
            int winningCols = 0;

            for (int i = 0; i < amountRows; i++)
            {
                if (slotMachine[0, i] == slotMachine[1, i] && slotMachine[1, i] == slotMachine[2, i])
                {
                    winningCols++;
                }
            }
            return winningCols;
        }

        /// <summary>
        /// checks for matches on all diagonals
        /// </summary>
        /// <param name="diags"></param>
        /// <param name="slotMachine"></param>
        /// <param name="winningDiags"></param>
        public static int CheckWinningDiags(int amountRows, int[,] slotMachine)
        {
            int winningDiags = 0;

            for (int i = 0; i < amountRows; i++)
            {
                if (i == 0 && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    winningDiags++;
                }
                else if (i == 1 && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    winningDiags++;
                }
            }
            return winningDiags;
        }

        /// <summary>
        /// checks for jackpot match or big win match
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="rows"></param>
        /// <param name="matchingColumns"></param>
        /// <param name="cols"></param>
        /// <param name="LARGE_WIN"></param>
        /// <param name="MEDIUM_WIN"></param>
        /// <param name="balance"></param>
        public static int GrantWins(int matchingRows, int rows, int matchingColumns, int cols, int matchingDiagonals, int LARGE_WIN, int MEDIUM_WIN, int SMALL_WIN, int balance)
        {
            int winnings = 0;

            if (matchingRows == rows && matchingColumns == cols)
            {
                UIMethods.DisplayJackpotWin(LARGE_WIN);
                winnings += LARGE_WIN;
            }
            else if (matchingRows == rows || matchingColumns == cols)
            {
                UIMethods.DisplayBigWin(MEDIUM_WIN);
                winnings += MEDIUM_WIN;
            }
            else if (matchingRows > 0)
            {
                UIMethods.DisplayRowsWin(matchingRows, SMALL_WIN);
                winnings += SMALL_WIN;
            }
            else if (matchingColumns > 0)
            {
                UIMethods.DisplayColumnsWin(matchingColumns, SMALL_WIN);
                winnings += SMALL_WIN;
            }
            else if (matchingDiagonals > 0)
            {
                UIMethods.DisplayDiagonalsWin(matchingDiagonals, SMALL_WIN);
                winnings += SMALL_WIN;
            }

            balance = balance + winnings;
            return balance;
        }
    }
}