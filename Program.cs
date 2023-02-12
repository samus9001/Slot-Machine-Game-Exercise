namespace SlotMachine
{
    internal class Program
    {
        const int MIN_NUMBER = 0;
        const int MAX_NUMBER = 10;
        static void Main(string[] args)
        {

            var rnd = new Random();
            int index;

            int[,] slotMachine = new int[3, 3];

            int rows = slotMachine.GetLength(0);
            int cols = slotMachine.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    index = rnd.Next(MIN_NUMBER,MAX_NUMBER);
                    slotMachine[i, j] = index;
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}