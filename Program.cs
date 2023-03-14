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
        const int LOW_BALANCE = 5; //sets the limit on spins depending on balance

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
                Console.WriteLine($"BALANCE = ${balance}\n\n");

                if (balance >= LOW_BALANCE)
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE OR '3' TO PLAY THREE LINES\n\n");
                }
                else
                {
                    Console.WriteLine("PRESS '1' TO PLAY ONE LINE\n\n");
                }

                Console.WriteLine("ONE LINE COSTS $1\nTHREE LINES COSTS $5\n");
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

                //function used for looping through all elements in the array and setting their values
                void SlotMachineInput(int[,] slotMachine, int rows, int cols, Random rnd)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            int index = rnd.Next(MAX_NUMBER);
                            slotMachine[i, j] = index;
                            Console.Write(slotMachine[i, j] + " ");
                        }
                        Console.WriteLine("\n");
                    }
                }

                if (input == '1')
                {
                    //loops through each element in the first row of the array and sets the values
                    for (int j = 0; j < cols; j++)
                    {
                        index = rnd.Next(MAX_NUMBER);
                        slotMachine[0, j] = index;
                        Console.Write(slotMachine[0, j] + " ");
                    }
                    balance--;
                    Console.WriteLine("\n");
                }

                //checks if the elements are matching for one line
                if (input == '1' && slotMachine[0, 0] == slotMachine[0, 1] && slotMachine[0, 1] == slotMachine[0, 2])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH! YOU WIN ${SMALL_WIN}\n\n");
                    balance += SMALL_WIN;
                }

                if (input == '3')
                {
                    SlotMachineInput(slotMachine, rows, cols, rnd);
                    balance = balance - MAX_LOSS;
                }

                //counter for matching rows and columns with three lines
                int matchingRows = 0;
                int matchingColumns = 0;

                if (input != '1') //skips match conditions if one line is played
                {
                    for (int i = 0; i < rows; i++)
                    {

                        if (matchingRows != rows && matchingColumns != cols && slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                        {
                            Console.WriteLine($"\nYOU HIT A MATCH ON ROW {i + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                            balance += SMALL_WIN;
                            matchingRows++;
                        }
                    }
                }

                //checks for matching columns with three lines
                if (input != '1')
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

                //checks if the elements in the diagonals are matching with three lines
                if (matchingRows != rows && matchingColumns != cols && input != '1' && input == '3' && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2] || input != '1' && input == '3' && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON THE DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                }

                //checks both matching rows and columns for a jackpot
                if (matchingRows == rows && matchingColumns == cols)
                {
                    Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n\n");
                    balance += LARGE_WIN;
                }
                //checks for matching rows or matching columns
                else if (matchingRows == rows || matchingColumns == cols)
                {
                    Console.WriteLine($"\nYOU HIT A BIG WIN!! YOU WIN ${MEDIUM_WIN}!\n\n");
                    balance += MEDIUM_WIN;
                }

                bool restart = false;

                if (balance <= 0)
                {
                    Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
                    Console.WriteLine("IF YOU WOULD LIKE TO PLAY AGAIN PRESS 'Y' OR PRESS ANY OTHER KEY TO EXIT");
                    var key = Console.ReadKey().Key;
                    restart = key == ConsoleKey.Y;
                }

                if (restart)
                {
                    balance = STARTING_BALANCE;
                    Console.Clear();
                }
                else if (balance <= 0)
                {
                    return;
                }
            }
        }
    }
}