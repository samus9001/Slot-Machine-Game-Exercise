namespace SlotMachine
{
    internal class Program
    {
        const int MAX_NUMBER = 10;
        static void Main(string[] args)
        {
            //sets the random elements value
            var rnd = new Random();
            int index;

            Console.WriteLine("Welcome to Slots! Enter $1 to play one line, $2 to play two lines, $3 to play three lines\n");

            while (true)
            {
                Console.Write("$");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");

                if (input != '1' && input != '2' && input != '3')
                {
                    Console.WriteLine("\nThat is not a valid option.\n");
                    continue;
                }

                //creates a 3x3 array
                int[,] slotMachine = new int[3, 3];

                //checks the length of each dimension of the array
                int rows = slotMachine.GetLength(0);
                int cols = slotMachine.GetLength(1);

                if (input == '1')
                {
                    for (int j = 0; j < cols; j++)
                    {
                        index = rnd.Next(MAX_NUMBER);
                        slotMachine[0, j] = index;
                        Console.Write(slotMachine[0, j] + " ");
                    }
                    Console.WriteLine("\n");
                }

                if (input == '2')
                {
                    for (int j = 0; j < cols; j++)
                    {
                        index = rnd.Next(MAX_NUMBER);
                        slotMachine[0, j] = index;
                        Console.Write(slotMachine[0, j] + " ");
                        Console.Write(slotMachine[1, j] + " ");
                    }
                    Console.WriteLine("\n");
                }

                if (input == '3')
                {
                    //loops through each element in the array and sets the values randomly
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
                }

                /*
                char restart = Console.ReadKey().KeyChar;

                if (restart == 'r')
                {
                    Console.Clear();
                }
                */
            }
        }
    }
}