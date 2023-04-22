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
        const int diags = 2; // sets the amount of diagonals to check for matches

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
                // counter variables used for checking for matches
                int matchingRows = 0;
                int matchingColumns = 0;
                int matchingDiagonals = 0;

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

                userInput = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

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

                // used for setting the array length
                int amountRows = 1;
                int decreaseBalance = 1;

                if (userInput == '3')
                {
                    amountRows = 3;
                    decreaseBalance = MAX_LOSS;
                }

                slotMachine = LogicMethods.GenerateSlotMachineArray(slotMachine, amountRows, MAX_NUMBER);
                UIMethods.DisplaySlotMachineArray(slotMachine, amountRows);
                balance -= decreaseBalance;

                matchingRows = LogicMethods.CheckMatchingRows(amountRows, slotMachine, matchingRows);
                UIMethods.DisplayRowsMatch(matchingRows, SMALL_WIN);
                LogicMethods.SmallBalanceIncrease(matchingRows, rows, matchingColumns, cols, matchingDiagonals, ref balance, SMALL_WIN);

                if (amountRows == 3)
                {
                    matchingColumns = LogicMethods.CheckMatchingColumns(cols, slotMachine, matchingColumns);
                    UIMethods.DisplayColumnsMatch(matchingColumns, SMALL_WIN);

                    matchingDiagonals = LogicMethods.CheckMatchingDiagonals(diags, slotMachine, matchingDiagonals);
                    UIMethods.DisplayDiagonalsMatch(matchingDiagonals, SMALL_WIN);

                    LogicMethods.CheckJackpotOrBigWin(matchingRows, rows, matchingColumns, cols, LARGE_WIN, MEDIUM_WIN, ref balance);
                }

                // checks if the game is over
                if (balance <= 0)
                {
                    UIMethods.DisplayGameOver();

                    var key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Y)
                    {
                        balance = STARTING_BALANCE;
                        Console.Clear();
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