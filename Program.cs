namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> numbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var rnd = new Random();
            int index;

            int[,] slotMachine = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    index = rnd.Next(numbers.Count);
                    slotMachine[i, j] = index;
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}