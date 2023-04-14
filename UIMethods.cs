using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public static class UIMethods
    {
        /// <summary>
        /// sets the UI welcome message upon starting the game
        /// </summary>
        public static void Welcome()
        {
            Console.WriteLine("WELCOME TO SLOTS!\n\n");
        }

        /// <summary>
        /// sets the UI balance amount
        /// </summary>
        /// <param name="balance"></param>
        public static void Balance(int balance)
        {
            Console.WriteLine($"BALANCE = ${balance}\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play when balance is above the sufficient threshold
        /// </summary>
        public static void SufficientBalance()
        {
            Console.WriteLine("PRESS '1' TO PLAY ONE LINE OR '3' TO PLAY THREE LINES\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play when balance is below the sufficient threshold
        /// </summary>
        public static void LowBalance()
        {
            Console.WriteLine("PRESS '1' TO PLAY ONE LINE\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play
        /// </summary>
        public static void Instructions()
        {
            Console.WriteLine("ONE LINE COSTS $1\nTHREE LINES COSTS $5\n\n");
            Console.WriteLine("PRESS 'L' TO LEAVE WITH YOUR BALANCE");
        }

        /// <summary>
        /// sets the UI message for invalid input
        /// </summary>
        public static void InvalidOption()
        {
            Console.WriteLine("THAT IS NOT A VALID OPTION\n");
        }

        /// <summary>
        /// sets the UI message when the user ends the game
        /// </summary>
        /// <param name="balance"></param>
        public static void FinalBalance(int balance)
        {
            Console.WriteLine($"THANKS FOR PLAYING! YOUR FINAL BALANCE IS ${balance}!");
        }

        /// <summary>
        /// sets the UI message when there is a match on the rows
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        public static void RowsMatch(int matchingRows, int SMALL_WIN)
        {
            for (int i = 0; i < matchingRows; i++)
            {
                Console.WriteLine($"\nYOU HIT A ROW MATCH! YOU WIN ${SMALL_WIN}!\n\n");
            }
        }

        /// <summary>
        /// sets the UI message when there is a match on the columns
        /// </summary>
        /// <param name="matchingColumns"></param>
        /// <param name="SMALL_WIN"></param>
        public static void ColumnsMatch(int matchingColumns, int SMALL_WIN)
        {
            for (int j = 0; j < matchingColumns; j++)
            {
                Console.WriteLine($"\nYOU HIT A COLUMN MATCH! YOU WIN ${SMALL_WIN}!\n\n");
            }
        }

        public static void DiagonalsMatch(int matchingDiagonals, int SMALL_WIN)
        {
            for (int k = 0; k < matchingDiagonals; k++)
            {
                Console.WriteLine($"\nYOU HIT A DIAGONAL MATCH! YOU WIN ${SMALL_WIN}!\n\n");
            }
        }

        /// <summary>
        /// sets the UI message when balance is below the sufficient threshold
        /// </summary>
        public static void InsufficientBalance()
        {
            Console.WriteLine("INSUFFICIENT BALANCE\n");
        }

        /// <summary>
        /// sets the UI instructions after the balance runs out
        /// </summary>
        /// 
        public static void GameOver()
        {
            Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
            Console.WriteLine("IF YOU WOULD LIKE TO PLAY AGAIN PRESS 'Y' OR PRESS ANY OTHER KEY TO EXIT");
        }
    }
}
