using System;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set window size to appropriate dimensions
            Console.SetWindowSize(100, 50);
            Game game = new Game();
            game.Run();
        }
    }
}
