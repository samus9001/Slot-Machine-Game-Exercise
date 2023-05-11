using System;

namespace SlotMachine
{
    internal class Program
    {
        public const int ROWS = 3; // sets the row length for the grid
        public const int COLS = 3; // sets the column length for the grid
        const int STARTING_BALANCE = 100; // sets the balance upon starting a new game
        const int LOW_BALANCE = 5; // sets the limit on available lines depending on balance
        public const int MAX_NUMBER = 0; // sets the range of values that can be generated to fill the grid
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
                int[,] grid = LogicMethods.GenerateSlotMachineAny();
                List<int> matchingRows = LogicMethods.CheckWinningRows(grid);
                List<int> matchingCols = LogicMethods.CheckWinningCols(grid);
                List<int> matchingDiags = LogicMethods.CheckWinningDiags(grid);

                if (userInput == '1')
                {
                    LogicMethods.GenerateSlotMachineSingleRow(grid);
                    LogicMethods.CheckSingleWinningRow(grid);
                    UIMethods.DisplaySingleRowWin(jackpotMatch, bigMatch, singleRowMatch, SMALL_WIN);
                }

                if (userInput == '3')
                {
                    decreaseBalanceForGameRound = MAX_LOSS;

                    jackpotMatch = LogicMethods.CheckJackpotMatch(userInput, matchingRows, matchingCols);
                    LogicMethods.CheckBigMatch(userInput, jackpotMatch, matchingRows, matchingCols);
                    LogicMethods.CheckSingleRowMatch(jackpotMatch, bigMatch, userInput, grid, matchingRows, matchingCols);
                    LogicMethods.CheckRowsMatch(jackpotMatch, bigMatch, userInput, matchingRows);
                    LogicMethods.CheckColsMatch(jackpotMatch, bigMatch, userInput, matchingCols);
                    LogicMethods.CheckDiagsMatch(jackpotMatch, bigMatch, userInput, matchingDiags);

                    UIMethods.DisplayJackpotWin(jackpotMatch, LARGE_WIN);
                    UIMethods.DisplayBigWin(jackpotMatch, bigMatch, LARGE_WIN);
                    UIMethods.DisplaySingleRowWin(jackpotMatch, bigMatch, singleRowMatch, SMALL_WIN);
                    UIMethods.DisplayRowsWin(matchingRows, jackpotMatch, bigMatch, rowsMatch);
                    UIMethods.DisplayColumnsWin(matchingCols, jackpotMatch, bigMatch, colsMatch);
                    UIMethods.DisplayDiagonalsWin(matchingDiags, jackpotMatch, bigMatch, diagsMatch);
                }

                balance -= decreaseBalanceForGameRound;
                UIMethods.DisplaySlotMachineArray(grid);

                int winnings = LogicMethods.GrantWins(jackpotMatch, bigMatch, singleRowMatch, rowsMatch, colsMatch, diagsMatch, matchingRows, matchingCols, matchingDiags, balance);
                balance = winnings;

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