using System;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public IOutputService Output { get; private set; }

        public bool IsRunning { get; private set; }
        public IInputService Input { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Output = output;
            Input = input;

            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Output.WriteLine(Player.CurrentRoom);
                if (previousRoom != Player.CurrentRoom)
                {
                    Output.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                }

                Output.Write("> ");

                string inputString = Console.ReadLine().Trim();
                // might look like:  "LOOK", "TAKE MAT", "QUIT"
                char  separator = ' ';
                string[] commandTokens = inputString.Split(separator);
                
                string verb = null;
                string subject = null;
                if (commandTokens.Length == 0)
                {
                    continue;
                }
                else if (commandTokens.Length == 1)
                {
                    verb = commandTokens[0];
                }
                else
                {
                    verb = commandTokens[0];
                    subject = commandTokens[1];
                }

                Commands command = ToCommand(verb);
                string outputString;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.Look:
                        outputString = Player.CurrentRoom.Description;
                        foreach (Item item in Player.CurrentRoom.Inventory)
                        {
                            outputString = item.Name;
                        }
                        break;

                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        Directions direction = (Directions)command;
                        if (Player.Move(direction))
                        {
                            outputString = $"You moved {direction}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    case Commands.Take:
                        //TODO
                        //foreach (Item item in )
                        //{
                        //   .Remove from room inv
                        //   .Add to player inv
                        //}
                        if (subject == null)
                        {
                            outputString = "You can not see any such thing";
                        }
                        outputString = null;
                        break;

                    case Commands.Drop:
                        //TODO
                        //foreach(Item item in )
                        //{
                        //   .Remove from player inv
                        //   .Add to room inv
                        //}
                        if (subject == null)
                        {
                            outputString = "You can not see any such thing";
                        }
                        outputString = null;
                        break;

                    case Commands.Inventory:
                        foreach(Item items in Player.Inventory)
                        {
                            outputString = $"";
                        }
                        outputString = null;
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }
                
                if(command != Commands.Unknown)
                {
                    Player.Moves++;
                }

                Output.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}
