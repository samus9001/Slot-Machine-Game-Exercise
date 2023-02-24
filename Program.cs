using System;
using System.Diagnostics.Metrics;
using System.Threading;

namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10;
        const int MAX_LOSS = 3;
        static void Main(string[] args)
        {
            //sets the random elements value
            var rnd = new Random();
            int index;
            int startingBalance = 100;
            int balance = startingBalance;
            bool endGame = false;
            char restartGame = 'N';

            //creates a 3x3 array
            int[,] slotMachine = new int[3, 3];

            //checks the length of each dimension of the array
            int rows = slotMachine.GetLength(0);
            int cols = slotMachine.GetLength(1);

            Console.WriteLine("Welcome to Slots!\n\n");

            while (true)
            {
                if (balance >= 3)
                {
                    Console.WriteLine("Enter $1 to play one line or $3 to play all three lines!\n\nPress any other key to leave with your balance");
                }

                if (balance > 0 && balance < 3)
                {
                    Console.WriteLine("Enter $1 to play one line!\n\nPress any other key to leave with your balance");
                }

                Console.WriteLine($"\n\nYour balance is ${balance}\n");
                Console.Write("$");
                char input = Console.ReadKey().KeyChar;
                Console.Clear();

                //checks if input is out of bounds
                if (input != '1' && input != '3')
                {
                    Console.WriteLine("\n\nThat is not a valid option.\n");
                    continue;
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
                    Console.WriteLine();

                    //checks if the elements in the first row are matching
                    if (slotMachine[0, 0] == slotMachine[0, 1] && slotMachine[0, 1] == slotMachine[0, 2])
                    {
                        Console.WriteLine("\nYOU WIN!");
                        balance += 10;
                    }
                    else
                    {
                        balance--;
                    }
                }

                if (input == '3')
                {
                    //loops through each element in the array and sets the values
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            index = rnd.Next(MAX_NUMBER);
                            slotMachine[i, j] = index;
                            Console.Write(slotMachine[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                    balance = balance - MAX_LOSS;
                }
                Console.WriteLine("\n");

                if (balance <= 0)
                {
                    Console.WriteLine("\nYOU LOSE!\n");
                    Console.WriteLine("If you would like to play again press Y or press any other key to exit");
                    endGame = true;
                }


                if (endGame)
                {
                    restartGame = Console.ReadKey().KeyChar;
                }

                if (restartGame == 'Y')
                {
                    balance = startingBalance;
                    Console.Clear();
                    endGame = false;
                    restartGame = 'N';

                }
                else if (endGame)
                {
                    return;
                }
            }
        }
    }
}