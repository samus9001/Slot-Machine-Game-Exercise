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

            UIMethods.Welcome();

            while (true)
            {
                // counter for matching rows and columns with three lines
                int matchingRows = 0;
                int matchingColumns = 0;
                int matchingDiagonals = 0;

                UIMethods.Balance(balance);

                if (balance >= LOW_BALANCE)
                {
                    UIMethods.sufficientBalance();
                }
                else
                {
                    UIMethods.lowBalance();
                }

                UIMethods.Instructions();

                userInput = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

                // checks if input is out of bounds
                bool validOption = (userInput == '1' || userInput == '3' || userInput == 'L');

                if (!validOption)
                {
                    UIMethods.invalidOption();
                    continue;
                }

                if (userInput == '3' && balance < LOW_BALANCE)
                {
                    UIMethods.insufficientBalance();
                    continue;
                }

                if (userInput == 'L')
                {
                    UIMethods.finalBalance(balance);
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
                balance -= decreaseBalance;

                LogicMethods.matchRows(amountRows, slotMachine, SMALL_WIN, balance, matchingRows);

                if (amountRows == 3)
                {
                    LogicMethods.matchColumns(cols, slotMachine, SMALL_WIN, balance, matchingColumns);
                    LogicMethods.matchDiagonals(diags, slotMachine, SMALL_WIN, balance, matchingDiagonals);

                    // checks if all rows or columns are matching for a big win
                    if (matchingRows == rows || matchingColumns == cols)
                    {
                        Console.WriteLine($"\nYOU HIT A BIG WIN!! YOU WIN ${MEDIUM_WIN}!\n\n");
                        balance += MEDIUM_WIN;
                    }

                    // checks both matching rows and columns for a jackpot win
                    else if (matchingRows == rows && matchingColumns == cols)
                    {
                        Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n\n");
                        balance += LARGE_WIN;
                    }
                }

                // checks if the game is over
                if (balance <= 0)
                {
                    UIMethods.gameOver();
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