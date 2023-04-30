using System;

namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10; // sets the range of values that can be generated to fill the array
        const int MAX_LOSS = 5; // largest amount of balance that is lost
        const int SMALL_WIN = 50;
        const int MEDIUM_WIN = 500;
        const int LARGE_WIN = 1000;
        const int STARTING_BALANCE = 100;
        const int LOW_BALANCE = 5; // sets the limit on available lines depending on balance

        static void Main(string[] args)
        {
            int balance = STARTING_BALANCE;
            char userInput;

            // creates a 3x3 array
            int[,] slotMachine = new int[3, 3];

            // checks the length of each dimension of the array
            int rows = slotMachine.GetLength(0);
            int cols = slotMachine.GetLength(1);

            UIMethods.DisplayWelcome();

            while (true)
            {
                // used for setting the array length to one line by default
                int rowCnt = 1;
                int colCnt = 3;

                // used for setting the array length to three lines
                int grid3x3 = 3;

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
                    rowCnt = grid3x3;
                    decreaseBalanceForGameRound = MAX_LOSS;
                }

                slotMachine = LogicMethods.GenerateSlotMachineAny(rowCnt, colCnt, MAX_NUMBER);
                balance -= decreaseBalanceForGameRound;
                UIMethods.DisplaySlotMachineArray(slotMachine, rowCnt);

                // counter variables used for checking for matches
                int matchingRows = LogicMethods.CheckWinningRows(rowCnt, slotMachine);
                int matchingColumns = LogicMethods.CheckWinningColumns(rowCnt, grid3x3, slotMachine);
                int matchingDiagonals = LogicMethods.CheckWinningDiags(rowCnt, grid3x3, slotMachine);

                matchingRows = LogicMethods.CheckWinningRows(rowCnt, slotMachine);

                if (rowCnt == 3)
                {
                    matchingColumns = LogicMethods.CheckWinningColumns(rowCnt, grid3x3, slotMachine);

                    matchingDiagonals = LogicMethods.CheckWinningDiags(rowCnt, grid3x3, slotMachine);
                }
                
                balance = LogicMethods.GrantWins(matchingRows, rows, matchingColumns, cols, matchingDiagonals, LARGE_WIN, MEDIUM_WIN, SMALL_WIN, balance);

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