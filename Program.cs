using System;
using System.Diagnostics.Metrics;
using System.Threading;

namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10;
        const int MAX_LOSS = 3;
        const int SMALL_WIN = 100;
        const int MEDIUM_WIN = 500;
        const int LARGE_WIN = 1000;
        static void Main(string[] args)
        {
            //state variables
            var rnd = new Random();
            int index;
            int startingBalance = 1000000;
            int balance = startingBalance;
            bool endGame = false;
            char restartGame = 'N';
            char input;

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
                    Console.WriteLine("Enter $1 to play one line or $3 to play all three lines!\n\nPress L to leave with your balance");
                }

                if (balance > 0 && balance < 3)
                {
                    Console.WriteLine("Enter $1 to play one line!\n\nPress L to leave with your balance");
                }

                Console.WriteLine($"\n\nYour balance is ${balance}\n");
                Console.Write("$");
                input = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Clear();

                //checks if input is out of bounds
                if (input != '1' && input != '3' && input != 'L')
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
                    balance--;
                    Console.WriteLine();
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

                //checks if the elements in the first row are matching
                if (slotMachine[0, 0] == slotMachine[0, 1] && slotMachine[0, 1] == slotMachine[0, 2])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE FIRST ROW!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the second row are matching
                if (input == 3 && slotMachine[1, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[1, 2])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE SECOND ROW!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the third row are matching
                if (input == 3 && slotMachine[2, 0] == slotMachine[2, 1] && slotMachine[2, 1] == slotMachine[2, 2])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE THIRD ROW!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in all rows are matching
                bool allRowsMatch = true;
                for (int i = 0; i < rows; i++)
                {
                    if (!(input == 3 && slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2]))
                    {
                        allRowsMatch = false;
                        break;
                    }
                }
                if (allRowsMatch && input == '3')
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON ALL ROWS!\n");
                    balance += MEDIUM_WIN;
                }

                //checks if the elements in the first column are matching
                if (input == '3' && slotMachine[0, 0] == slotMachine[1, 0] && slotMachine[1, 0] == slotMachine[2, 0])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE FIRST COLUMN!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the second column are matching
                if (input == '3' && slotMachine[0, 1] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 1])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE SECOND COLUMN!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the third column are matching
                if (input == '3' && slotMachine[0, 2] == slotMachine[1, 2] && slotMachine[1, 2] == slotMachine[2, 2])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE THIRD COLUMN!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in all columns are matching
                bool allColumnsMatch = true;
                for (int j = 0; j < cols; j++)
                {
                    if (!(slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j]))
                    {
                        allColumnsMatch = false;
                        break;
                    }
                }

                if (allColumnsMatch && input == '3')
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON ALL COLUMNS!\n");
                    balance += MEDIUM_WIN;
                }

                //checks if the elements in the left diagonal are matching
                if (input == '3' && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE DIAGONAL!\n");
                    balance += SMALL_WIN;
                }
                //checks if the elements in the right diagonal are matching
                if (input == '3' && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    Console.WriteLine("\nYOU HIT A MATCH ON THE DIAGONAL!\n");
                    balance += SMALL_WIN;
                }

                //checks if all elements are matching
                if (allRowsMatch && allColumnsMatch && input == '3')
                {
                    Console.WriteLine("\nYOU HIT THE JACKPOT!!!\n");
                    balance += LARGE_WIN;
                }

            if (input == 'L')
            {
                return;
            }

            if (balance <= 0)
            {
                Console.WriteLine("\nYOU LOSE!\n");
                Console.WriteLine("If you would like to play again press Y or press any other key to exit");
                endGame = true;
            }

            if (endGame)
            {
                restartGame = Char.ToUpper(Console.ReadKey().KeyChar);
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