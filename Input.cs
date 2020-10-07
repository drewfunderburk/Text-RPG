using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    // Static input class to be used anywhere input is needed. Was intended to be much more robust,
    //  but was pared down towards the end for scope
    public static class Input
    {
        // Get the user's selection. Takes an int to give the number of options available, and a bool
        //  to allow the user to send a quit signal
        public static char GetSelection(int options, bool allowQuit = false)
        {
            while (true)
            {
                Console.Write("> ");
                char input = Console.ReadKey().KeyChar;

                // Run the loop again if the player presses enter
                if (input == '\r')
                    continue;

                // Check if input was quit
                int inputNumber = (int) Char.GetNumericValue(input);
                if (allowQuit && input == 'q')
                {
                    return input;
                }

                // Check if input was a number within the allowed options
                else if (inputNumber >= 0 && inputNumber <= options)
                {
                    Console.WriteLine();
                    return input;
                }
                // If none of the above conditions were met, try again
                else
                {
                    Console.WriteLine("\nInvalid Input");

                }
            }
        }
    }
}
