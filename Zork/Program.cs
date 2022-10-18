using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = @"Content\Game.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFileName] : defaultGameFilename);
            Game game = Game.Load(gameFilename);
            
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Finished");           
        }
        private enum CommandLineArguments
        {
            GameFileName = 0
        }
    }
}