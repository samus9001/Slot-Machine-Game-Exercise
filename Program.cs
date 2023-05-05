﻿using System;

namespace SlotMachine
{
    internal class Program
    {
        const int ROW = 1;
        public const int ROWS = 3;
        public const int COLS = 3;
        const int STARTING_BALANCE = 100; //sets the balance upon starting a new game
        const int LOW_BALANCE = 5; // sets the limit on available lines depending on balance
        public const int MAX_NUMBER = 10; // sets the range of values that can be generated to fill the array
        const int MAX_LOSS = 5; // sets the balance amount lost when the array size is 3x3
        const int SMALL_WIN = 50;
        const int MEDIUM_WIN = 500;
        const int LARGE_WIN = 1000;

        static void Main(string[] args)
        {
            int balance = STARTING_BALANCE;
            char userInput;

            // creates a 1x3 array for playing a single line
            int[,] grid = new int[ROW, COLS];

            UIMethods.DisplayWelcome();

            while (true)
            {
                UIMethods.DisplayBalance(balance);

                if (balance >= LOW_BALANCE)
                {
                    UIMethods.DisplaySufficientBalance();
                }
                else
                {
                    UIMethods.DisplayLowBalance();
                }

                UIMethods.DisplayInstructions();

                userInput = UIMethods.Input();

                UIMethods.ClearScreen();

                // checks if input is out of bounds
                bool validOption = (userInput == '1' || userInput == '3' || userInput == 'L');

                if (!validOption)
                {
                    UIMethods.DisplayInvalidOption();
                    continue;
                }

                if (userInput == '3' && balance < LOW_BALANCE)
                {
                    UIMethods.DisplayInsufficientBalance();
                    continue;
                }

                if (userInput == 'L')
                {
                    UIMethods.DisplayFinalBalance(balance);
                    return;
                }

                int decreaseBalanceForGameRound = 1;

                if (userInput == '3')
                {
                    grid = new int[ROWS, COLS];
                    decreaseBalanceForGameRound = MAX_LOSS;
                    LogicMethods.CheckWinningCols(grid);
                    LogicMethods.CheckWinningDiags(grid);
                }

                grid = LogicMethods.GenerateSlotMachineAny(grid);
                balance -= decreaseBalanceForGameRound;
                UIMethods.DisplaySlotMachineArray(grid);

                LogicMethods.CheckWinningRows(grid);

                int winnings = LogicMethods.GrantWins(grid, LARGE_WIN, MEDIUM_WIN, SMALL_WIN, balance);
                balance += winnings;

                // checks if the game is over
                if (balance <= 0)
                {
                    UIMethods.DisplayGameOver();

                    bool restartGame = UIMethods.RestartGame();

                    if (restartGame)
                    {
                        balance = STARTING_BALANCE;
                        UIMethods.ClearScreen();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}