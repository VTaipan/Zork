using System;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = @"Content\Game.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFileName] : defaultRoomsFilename);
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(@"Content\Game.json"));
            
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