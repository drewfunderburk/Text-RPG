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

        public static char GetCharInput()
        {
            char input = Console.ReadKey().KeyChar;
            if (Enum.IsDefined(typeof(InputKeys), input))
                return input;
            return '0';
        }
    }
}
