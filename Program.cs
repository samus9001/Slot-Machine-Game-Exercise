using System;

namespace SlotMachine
{
    internal class Program
    {
        public const int ROWS = 3; // sets the row length for the grid
        public const int COLS = 3; // sets the column length for the grid
        const int STARTING_BALANCE = 100; // sets the balance upon starting a new game
        const int LOW_BALANCE = 5; // sets the limit on available lines depending on balance
        public const int MAX_NUMBER = 10; // sets the range of values that can be generated to fill the grid
        const int MAX_LOSS = 5; // sets the balance amount lost when all lines are playes
        public const int SMALL_WIN = 50; // sets the win amount for a single line
        public const int MEDIUM_WIN = 500; // sets the win amount for multiple lines
        public const int LARGE_WIN = 1000; // sets the win amount for a jackpot

        static void Main(string[] args)
        {
            int balance = STARTING_BALANCE;
            char userInput;

            UIMethods.DisplayWelcome();

            while (true) // runs until the user quits the game
            {
                bool jackpotMatch = false;
                bool bigMatch = false;
                bool singleRowMatch = false;
                bool rowsMatch = false;
                bool colsMatch = false;
                bool diagsMatch = false;
                bool winAmount = false;
                int winnings = 0;

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

                // checks if the input is valid or not
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
                int[,] grid = LogicMethods.GenerateSlotMachineArray(userInput);

                if (userInput == '1')
                {
                    UIMethods.DisplaySlotMachineArray(grid);

                    LogicMethods.CheckSingleWinningRow(grid);
                    singleRowMatch = LogicMethods.CheckSingleRowMatch(grid);
                    UIMethods.DisplaySingleRowWin(singleRowMatch);
                }

                List<int> matchingRows = LogicMethods.CheckWinningRows(grid);
                List<int> matchingCols = LogicMethods.CheckWinningCols(grid);
                List<int> matchingDiags = LogicMethods.CheckWinningDiags(grid);

                if (userInput == '3')
                {
                    decreaseBalanceForGameRound = MAX_LOSS;

                    UIMethods.DisplaySlotMachineArray(grid);

                    jackpotMatch = LogicMethods.CheckJackpotMatch(matchingRows, matchingCols); // determines if a jackpot is hit

                    if (jackpotMatch)
                    {
                        UIMethods.DisplayJackpotWin();
                    }
                    else
                    {
                        bigMatch = LogicMethods.CheckBigMatch(matchingRows, matchingCols); // determings if a big match is hit

                        if (bigMatch)
                        {
                            UIMethods.DisplayBigWin();
                        }
                        else
                        {
                            rowsMatch = LogicMethods.CheckRowsMatch(matchingRows);
                            colsMatch = LogicMethods.CheckColsMatch(matchingCols);
                            diagsMatch = LogicMethods.CheckDiagsMatch(matchingDiags);

                            UIMethods.DisplayRowsWin(matchingRows);
                            UIMethods.DisplayColumnsWin(matchingCols);
                            UIMethods.DisplayDiagonalsWin(matchingDiags);
                        }
                    }
                }

                balance -= decreaseBalanceForGameRound;
                winnings = LogicMethods.GrantWins(jackpotMatch, bigMatch, singleRowMatch, rowsMatch, colsMatch, diagsMatch, matchingRows, matchingCols, matchingDiags);
                balance += winnings;
                winAmount = LogicMethods.WinTotal(winnings);
                UIMethods.DisplayTotalWin(winAmount, winnings);

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