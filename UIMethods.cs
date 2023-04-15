using System;

namespace SlotMachine
{
    public static class UIMethods
    {
        /// <summary>
        /// sets the UI welcome message upon starting the game
        /// </summary>
        public static void DisplayWelcome()
        {
            Console.WriteLine("WELCOME TO SLOTS!\n\n");
        }

        /// <summary>
        /// sets the UI balance amount
        /// </summary>
        /// <param name="balance"></param>
        public static void DisplayBalance(int balance)
        {
            Console.WriteLine($"BALANCE = ${balance}\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play when balance is above the sufficient threshold
        /// </summary>
        public static void DisplaySufficientBalance()
        {
            Console.WriteLine("PRESS '1' TO PLAY ONE LINE OR '3' TO PLAY THREE LINES\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play when balance is below the sufficient threshold
        /// </summary>
        public static void DisplayLowBalance()
        {
            Console.WriteLine("PRESS '1' TO PLAY ONE LINE\n\n");
        }

        /// <summary>
        /// sets the UI instructions to play
        /// </summary>
        public static void DisplayInstructions()
        {
            Console.WriteLine("ONE LINE COSTS $1\nTHREE LINES COSTS $5\n\n");
            Console.WriteLine("PRESS 'L' TO LEAVE WITH YOUR BALANCE");
        }

        /// <summary>
        /// sets the UI message for invalid input
        /// </summary>
        public static void DisplayInvalidOption()
        {
            Console.WriteLine("THAT IS NOT A VALID OPTION\n");
        }

        /// <summary>
        /// sets the UI message when balance is below the sufficient threshold
        /// </summary>
        public static void DisplayInsufficientBalance()
        {
            Console.WriteLine("INSUFFICIENT BALANCE\n");
        }

        /// <summary>
        /// sets the UI message when the user ends the game
        /// </summary>
        /// <param name="balance"></param>
        public static void DisplayFinalBalance(int balance)
        {
            Console.WriteLine($"THANKS FOR PLAYING! YOUR FINAL BALANCE IS ${balance}!");
        }

        /// <summary>
        /// sets the UI to display the SlotMachine array
        /// </summary>
        /// <param name="slotMachine"></param>
        /// <param name="amountRows"></param>
        public static void DisplaySlotMachineArray(int[,] slotMachine, int amountRows)
        {
            int cols = slotMachine.GetLength(1);

            for (int i = 0; i < amountRows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// sets the UI message when there is a match on the rows
        /// </summary>
        /// <param name="matchingRows"></param>
        /// <param name="SMALL_WIN"></param>
        /// <param name="balance"></param>
        public static void DisplayRowsMatch(int matchingRows, int SMALL_WIN)
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
        public static void DisplayColumnsMatch(int matchingColumns, int SMALL_WIN)
        {
            for (int j = 0; j < matchingColumns; j++)
            {
                Console.WriteLine($"\nYOU HIT A COLUMN MATCH! YOU WIN ${SMALL_WIN}!\n\n");
            }
        }

        public static void DisplayDiagonalsMatch(int matchingDiagonals, int SMALL_WIN)
        {
            for (int k = 0; k < matchingDiagonals; k++)
            {
                Console.WriteLine($"\nYOU HIT A DIAGONAL MATCH! YOU WIN ${SMALL_WIN}!\n\n");
            }
        }

        public static void DisplayJackpotWin(int LARGE_WIN)
        {
            Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n\n");
        }

        public static void DisplayBigWin(int MEDIUM_WIN)
        {
            Console.WriteLine($"\nYOU HIT A BIG WIN!! YOU WIN ${MEDIUM_WIN}!\n\n");
        }

        /// <summary>
        /// sets the UI instructions after the balance runs out
        /// </summary>
        /// 
        public static void DisplayGameOver()
        {
            Console.WriteLine("\nYOU LOSE! BETTER LUCK NEXT TIME\n");
            Console.WriteLine("IF YOU WOULD LIKE TO PLAY AGAIN PRESS 'Y' OR PRESS ANY OTHER KEY TO EXIT");
        }
    }
}
