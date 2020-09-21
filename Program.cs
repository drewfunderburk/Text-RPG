using System;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50);
            Game game = new Game();
            game.Run();
        }
    }
}
