﻿using System;
using System.Diagnostics.Metrics;
using System.Threading;

namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10; //sets the upper bound of the array
        const int MAX_LOSS = 5; //largest amount of balance that is lost
        const int SMALL_WIN = 100;
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
                    Console.WriteLine("Enter $1 to play one line or $3 to play all three lines!\n\nPress L to leave with your balance\n");
                }
                else
                {
                    Console.WriteLine("Enter $1 to play one line!\n\nPress L to leave with your balance\n");
                }

                Console.Write("$");
                input = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

                //checks if input is out of bounds
                if (input != '1' && input != '3' && input != 'L')
                {
                    Console.WriteLine("That is not a valid option\n");
                    continue;
                }

                if (balance < LOW_BALANCE && input != '1' && input != 'L')
                {
                    Console.WriteLine("That is not a valid option\n");
                    continue;
                }

                if (input == 'L')
                {
                    Console.WriteLine($"Goodbye, thanks for playing. Your final balance is ${balance}!");
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
                if (input == '3')
                {
                    SlotMachineInput(slotMachine, rows, cols, rnd);
                    balance = balance - MAX_LOSS;
                }

                //checks for matching rows
                int matchingRows = 0;
                for (int i = 0; i < rows; i++)
                {

                    if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                    {
                        Console.WriteLine($"\nYOU HIT A MATCH ON ROW {i + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                        balance += SMALL_WIN;
                        matchingRows++;
                    }
                }

                //checks for matching columns
                int matchingColumns = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                    {
                        Console.WriteLine($"\nYOU HIT A MATCH ON COLUMN {j + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                        balance += SMALL_WIN;
                        matchingColumns++;
                    }
                }

                //checks if the elements in the left diagonal are matching
                if (input == '3' && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON THE DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the right diagonal are matching
                if (input == '3' && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON THE DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                }

                //checks for both matching rows and columns for a jackpot
                if (matchingRows == rows && matchingColumns == cols)
                {
                    Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n\n");
                    balance += LARGE_WIN;
                }
                else if (matchingRows == rows || matchingColumns == cols)
                {
                    Console.WriteLine($"\nYOU HIT A BIG WIN! YOU WIN ${MEDIUM_WIN}!\n\n");
                    balance += MEDIUM_WIN;
                }

                bool restart = false;

                if (balance <= 0)
                {
                    Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
                    Console.WriteLine("If you would like to play again press Y or press any other key to exit");
                    var key = Console.ReadKey().KeyChar;
                    restart = Char.ToUpper(key) == 'Y';
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