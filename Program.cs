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

            // checks matchingDiagonals counter
            int diags = 1;

            Console.WriteLine("WELCOME TO SLOTS!\n\n");

            while (true)
            {
                // counter for matching rows and columns with three lines
                int matchingRows = 0;
                int matchingColumns = 0;
                int matchingDiagonals = 0;

                Console.WriteLine($"BALANCE = ${balance}\n\n");

                if (balance >= LOW_BALANCE)
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE OR '3' TO PLAY THREE LINES\n\n");
                }
                else
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE\n\n");
                }

                Instructions();

                userInput = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

                // checks if input is out of bounds
                bool validOption = (userInput == '1' || userInput == '3' || userInput == 'L');

                if (!validOption)
                {
                    Console.WriteLine("THAT IS NOT A VALID OPTION\n");
                    continue;
                }

                if (userInput == '3' && balance < LOW_BALANCE)
                {
                    Console.WriteLine("INSUFFICIENT BALANCE\n");
                    continue;
                }

                if (userInput == 'L')
                {
                    Console.WriteLine($"THANKS FOR PLAYING! YOUR FINAL BALANCE IS ${balance}!");
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

                slotMachine = GenerateSlotMachineArray(slotMachine, amountRows, MAX_NUMBER);
                balance -= decreaseBalance;

                matchRows(amountRows, matchingRows, matchingColumns, rows, cols, slotMachine, balance);

                if (amountRows == 3)
                {
                    matchColumns(matchingRows, matchingColumns, rows, cols, slotMachine, SMALL_WIN, balance);

                    // checks for a match on either diagonal then increases counter
                    if (slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2] || slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                    {
                        matchingDiagonals++;
                    }

                    // checks if the elements in either diagonals are matching
                    if (matchingRows != rows && matchingColumns != cols && matchingDiagonals == diags)
                    {
                        Console.WriteLine($"\nYOU HIT A MATCH ON THE DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                        balance += SMALL_WIN;
                    }

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
                    gameOver();
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

        /// <summary>
        /// sets the UI instructions 
        /// </summary>
        static void Instructions()
        {
            Console.WriteLine("ONE LINE COSTS $1\nTHREE LINES COSTS $5\n\n");
            Console.WriteLine("PRESS 'L' TO LEAVE WITH YOUR BALANCE");
        }

        /// <summary>
        /// generates array values based on amount of lines played
        /// </summary>
        /// <param name="slotMachine"></param>
        /// <param name="amountRows"></param>
        /// <param name="MAX_NUMBER"></param>
        /// <returns></returns>
        static int[,] GenerateSlotMachineArray(int[,] slotMachine, int amountRows, int MAX_NUMBER)
        {
            var rnd = new Random();
            int index;
            int cols = slotMachine.GetLength(1);

            for (int i = 0; i < amountRows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    index = rnd.Next(MAX_NUMBER);
                    slotMachine[i, j] = index;
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
            return slotMachine;
        }

        /// <summary>
        /// check for matches on all rows
        /// </summary>
        /// <param name="amountRows"></param>
        /// <param name="matchingRows"></param>
        /// <param name="matchingColumns"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="slotMachine"></param>
        /// <param name="balance"></param>
        static void matchRows(int amountRows, int matchingRows, int matchingColumns, int rows, int cols, int[,] slotMachine, int balance)
        {
            for (int i = 0; i < amountRows; i++)
            {
                if (matchingRows != rows && matchingColumns != cols && slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON ROW {i + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                    matchingRows++;
                }
            }
        }

        /// <summary>
        /// check for matches on all columns
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="matchingColumns"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        static void matchColumns(int matchingRows, int matchingColumns, int rows, int cols, int[,] slotMachine, int SMALL_WIN, int balance)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matchingRows != rows && matchingColumns != cols && slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON COLUMN {j + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                    matchingColumns++;
                }
            }
        }

        static void gameOver()
        {
            Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
            Console.WriteLine("IF YOU WOULD LIKE TO PLAY AGAIN PRESS 'Y' OR PRESS ANY OTHER KEY TO EXIT");
        }
    }
}