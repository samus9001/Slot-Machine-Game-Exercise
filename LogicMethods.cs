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
        /// checks for matches on all rows
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="amountRows"></param>
        /// <param name="slotMachine"></param>
        /// <returns></returns>
        public static int CheckMatchingRows(int amountRows, int[,] slotMachine, int matchingRows, ref int balance, int SMALL_WIN)
        {
            for (int i = 0; i < amountRows; i++)
            {
                if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                {
                    matchingRows++;
                    balance += SMALL_WIN;
                }
            }
            return matchingRows;
        }

        /// <summary>
        /// checks for matches on all columns
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <param name="matchingColumns"></param>
        public static int CheckMatchingColumns(int cols, int[,] slotMachine, int matchingColumns, ref int balance, int SMALL_WIN)
        {
            for (int j = 0; j < cols; j++)
            {
                if (slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                {
                    matchingColumns++;
                    balance += SMALL_WIN;
                }
            }
            return matchingColumns;
        }

        /// <summary>
        /// checks for matches on all diagonals
        /// </summary>
        /// <param name="diags"></param>
        /// <param name="slotMachine"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        /// <param name="matchingDiagonals"></param>
        public static int CheckMatchingDiagonals(int diags, int[,] slotMachine, int matchingDiagonals, ref int balance, int SMALL_WIN)
        {
            for (int i = 0; i < diags; i++)
            {
                if (i == 0 && slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                {
                    matchingDiagonals++; 
                    balance += SMALL_WIN;
                    
                }
                else if (i == 1 && slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                {
                    matchingDiagonals++; 
                    balance += SMALL_WIN;
                }
            }
            return matchingDiagonals;
        }
    }
}