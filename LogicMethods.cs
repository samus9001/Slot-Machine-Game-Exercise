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
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <param name="matchingRows"></param>
        public static void matchRows(int amountRows, int[,] slotMachine, int SMALL_WIN, int balance, int matchingRows)
        {
            for (int i = 0; i < amountRows; i++)
            {
                if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
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
        /// <param name="cols"></param>
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <param name="matchingColumns"></param>
        public static void matchColumns(int cols, int[,] slotMachine, int SMALL_WIN, int balance, int matchingColumns)
        {
            for (int j = 0; j < cols; j++)
            {
                if (slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON COLUMN {j + 1}! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                    matchingColumns++;
                }
            }
        }

        /// <summary>
        /// checks for matches on all diagonals
        /// </summary>
        /// <param name="diags"></param>
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <param name="matchingDiagonals"></param>
        public static void matchDiagonals(int diags, int[,] slotMachine, int SMALL_WIN, int balance, int matchingDiagonals)
        {
            for (int i = 0; i < diags; i++)
            {
                if (i == 0 && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON THE LEFT DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                    matchingDiagonals++;
                }
                else if (i == 1 && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    Console.WriteLine($"\nYOU HIT A MATCH ON THE RIGHT DIAGONAL! YOU WIN ${SMALL_WIN}!\n\n");
                    balance += SMALL_WIN;
                    matchingDiagonals++;
                }
            }
        }
    }
}
