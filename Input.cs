using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public static class Input
    {
        enum InputKeys
        {
            Up = 'w',
            Down = 's',
            Left = 'a',
            Right = 'd',
        }

        public static char GetInput()
        {
            char input = Console.ReadKey().KeyChar;
            if (Enum.IsDefined(typeof(InputKeys), input))
                return input;
            return '0';
        }

        public static char GetSelection(int options, bool allowQuit = false)
        {
            while (true)
            {
                Console.Write("> ");
                char input = Console.ReadKey().KeyChar;
                if (allowQuit && input == 'q')
                {
                    return input;
                }
                if (Char.GetNumericValue(input) <= options)
                {
                    Console.WriteLine();
                    return input;
                }
               
                Console.WriteLine("\nInvalid Input");
            }
        }
    }
}
