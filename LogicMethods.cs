using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public static class LogicMethods
    {
        /// <summary>
        /// generates array values based on amount of lines played
        /// </summary>
        /// <param name="slotMachine"></param>
        /// <param name="amountRows"></param>
        /// <param name="MAX_NUMBER"></param>
        /// <returns></returns>
        public static int[,] GenerateSlotMachineArray(int[,] slotMachine, int amountRows, int MAX_NUMBER)
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
        public static void matchRows(int amountRows, int matchingRows, int matchingColumns, int rows, int cols, int[,] slotMachine, int balance, int SMALL_WIN)
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
        public static void matchColumns(int matchingRows, int matchingColumns, int rows, int cols, int[,] slotMachine, int balance, int SMALL_WIN)
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
    }
}
