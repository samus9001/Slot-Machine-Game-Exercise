using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading;

namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10; //sets the upper bound of the array
        const int MAX_LOSS = 5; //largest amount of balance that is lost
        const int SMALL_WIN = 50;
        const int MEDIUM_WIN = 500;
        const int LARGE_WIN = 1000;
        const int STARTING_BALANCE = 100;
        const int LOW_BALANCE = 5; //sets the limit on available lines depending on balance

        static void Main(string[] args)
        {
            //state variables
            var rnd = new Random();
            int index;
            int balance = STARTING_BALANCE;
            char input;

            //creates a 3x3 array
            int[,] slotMachine = new int[3, 3];

            //checks the length of each dimension of the array
            int rows = slotMachine.GetLength(0);
            int cols = slotMachine.GetLength(1);

            Console.WriteLine("WELCOME TO SLOTS!\n\n");

            while (true)
            {
                //counter for matching rows and columns with three lines
                int matchedRows = 0;
                int matchedColumns = 0;

                Console.WriteLine($"BALANCE = ${balance}\n\n");

                if (balance >= LOW_BALANCE)
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE OR '3' TO PLAY THREE LINES\n\n");
                }
                else
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE\n\n");
                }

                Console.WriteLine("ONE LINE COSTS $1\nTHREE LINES COSTS $5\n\n");
                Console.WriteLine("PRESS 'L' TO LEAVE WITH YOUR BALANCE");

                input = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

                //checks if input is out of bounds
                bool validOption = (input == '1' || input == '3' || input == 'L');
                bool insufficientBalance = (balance < LOW_BALANCE && input != '1' && input != 'L');

                if (!validOption || insufficientBalance)
                {
                    Console.WriteLine("THAT IS NOT A VALID OPTION\n");
                    continue;
                }

                if (input == 'L')
                {
                    Console.WriteLine($"THANKS FOR PLAYING! YOUR FINAL BALANCE IS ${balance}!");
                    return;
                }

                if (input == '1')
                {
                    //set values for the first row of the array
                    for (int j = 0; j < cols; j++)
                    {
                        index = rnd.Next(MAX_NUMBER);
                        slotMachine[0, j] = index;
                        Console.Write(slotMachine[0, j] + " ");
                    }
                    Console.WriteLine("\n");

                    //check for a match on the first row
                    if (slotMachine[0, 0] == slotMachine[0, 1] && slotMachine[0, 1] == slotMachine[0, 2])
                    {
                        Console.WriteLine($"\nYOU HIT A MATCH! YOU WIN ${SMALL_WIN}\n\n");
                        balance += SMALL_WIN;
                    }

                    //decrease balance by 1 for playing one line
                    balance--;
                }

                else if (input == '3')
                {
                    //set values for all rows and columns of the array
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            index = rnd.Next(MAX_NUMBER);
                            slotMachine[i, j] = index;
                            Console.Write(slotMachine[i, j] + " ");
                        }
                        Console.WriteLine("\n");
                    }

                    //check for matches on all rows
                    for (int i = 0; i < rows; i++)
                    {
                        if (matchedRows != rows && matchedColumns != cols && slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                        {
                            Console.WriteLine($"\nYOU HIT A MATCH ON ROW {i + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                            balance += SMALL_WIN;
                            matchedRows++;
                        }
                    }

                    //check for matches on all columns
                    for (int j = 0; j < cols; j++)
                    {
                        if (matchedRows != rows && matchedColumns != cols && slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                        {
                            Console.WriteLine($"\nYOU HIT A MATCH ON COLUMN {j + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                            balance += SMALL_WIN;
                            matchedColumns++;
                        }
                    }

                    //checks if the elements in the diagonals are matching with three lines
                    if (matchedRows != rows && matchedColumns != cols && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2] || matchedRows != rows && matchedColumns != cols && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                    {
                        Console.WriteLine($"\nYOU HIT A MATCH ON THE DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                        balance += SMALL_WIN;
                    }

                    //decrease balance by 5 for playing three lines
                    balance -= MAX_LOSS;
                }

                //checks both matching rows and columns for a jackpot win
                if (matchedRows == rows && matchedColumns == cols)
                {
                    Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n\n");
                    balance += LARGE_WIN;
                }
                //checks if all rows or columns are matching for a bigger win
                else if (matchedRows == rows || matchedColumns == cols)
                {
                    Console.WriteLine($"\nYOU HIT A BIG WIN!! YOU WIN ${MEDIUM_WIN}!\n\n");
                    balance += MEDIUM_WIN;
                }

                //used to restart loop
                bool restart;

                if (balance <= 0)
                {
                    Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
                    Console.WriteLine("IF YOU WOULD LIKE TO PLAY AGAIN PRESS 'Y' OR PRESS ANY OTHER KEY TO EXIT");
                    var key = Console.ReadKey().Key;
                    restart = key == ConsoleKey.Y;

                    if (restart)
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