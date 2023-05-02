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
            Console.Clear();
            Console.WriteLine("WELCOME TO SLOTS!\n\n");
        }

        /// <summary>
        /// checks the input key
        /// </summary>
        public static char Input()
        {
            char userInput = Char.ToUpper(Console.ReadKey().KeyChar);
            return userInput;
        }

        /// <summary>
        /// clears the screen when the game starts
        /// </summary>
        public static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// sets the UI balance amount
        /// </summary>
        /// <param name="balance"></param>
        public static void DisplayBalance(int balance)
        {
            Console.WriteLine($"\nBALANCE = ${balance}\n\n");
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
        /// <param name="rowCnt"></param>
        public static void DisplaySlotMachineArray(int[,] slotMachine, int rowCnt)
        {
            int cols = slotMachine.GetLength(1);

            for (int row = 0; row < rowCnt; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(slotMachine[row, col] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// sets the UI message when there is a jackpot match
        /// </summary>
        /// <param name="LARGE_WIN"></param>
        public static void DisplayJackpotWin(int LARGE_WIN)
        {
            Console.WriteLine($"\nJACKPOT!!! YOU WIN ${LARGE_WIN}!\n");
        }

        /// <summary>
        /// sets the UI message when there is a match on eiter all columns or rows
        /// </summary>
        /// <param name="MEDIUM_WIN"></param>
        public static void DisplayBigWin(int MEDIUM_WIN)
        {
            Console.WriteLine($"\nYOU HIT A BIG WIN!! YOU WIN ${MEDIUM_WIN}!\n");
        }

        /// <summary>
        /// sets the UI message when there is a match on the rows
        /// </summary>
        /// <param name="matchingRows"></param>
        public static void DisplayRowsWin(List<int> matchingRows)
        {
            if (matchingRows.Count > 0)
            {
                Console.Write("\nYOU HIT A MATCH ON ROW");
                for (int row = 0; row < matchingRows.Count; row++)
                {
                    Console.Write($" {matchingRows[row]}");
                    if (row < matchingRows.Count - 1)
                    {
                        Console.Write(" AND");
                    }
                }
            }
        }

        /// <summary>
        /// sets the UI message when there is a match on the columns
        /// </summary>
        /// <param name="matchingColumns"></param>
        public static void DisplayColumnsWin(List<int> matchingColumns)
        {
            if (matchingColumns.Count > 0)
            {
                Console.Write("\nYOU HIT A MATCH ON COLUMN");
                for (int col = 0; col < matchingColumns.Count; col++)
                {
                    Console.Write($" {matchingColumns[col]}");
                    if (col < matchingColumns.Count - 1)
                    {
                        Console.Write(" AND");
                    }
                }
            }
        }

        /// <summary>
        /// sets the UI message when there is a match on the diagonals
        /// </summary>
        /// <param name="matchingDiagonals"></param>
        public static void DisplayDiagonalsWin(List<int> matchingDiagonals)
        {
            if (matchingDiagonals.Count > 0)
            {
                Console.Write("\nYOU HIT A MATCH ON DIAGONAL");
                for (int diag = 0; diag < matchingDiagonals.Count; diag++)
                {
                    Console.Write($" {matchingDiagonals[diag]}");
                    if (diag < matchingDiagonals.Count - 1)
                    {
                        Console.Write(" AND");
                    }
                }
            }
        }

        /// <summary>
        /// sets the UI message to show the total amount won each round
        /// </summary>
        /// <param name="winnings"></param>
        public static void DisplayTotalWin(int winnings)
        {
            Console.WriteLine($"\n\nTOTAL WIN = ${winnings}\n\n\n");
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

        /// <summary>
        /// checks if the user wants to restart the game
        /// </summary>
        /// <returns></returns>
        public static bool RestartGame()
        {
            var key = Console.ReadKey().Key;

            return key == ConsoleKey.Y;
        }
    }
}