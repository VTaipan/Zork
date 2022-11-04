using System;
using System.Linq;

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
            Output = output ?? throw new ArgumentNullException(nameof(output));
            Input = input ?? throw new ArgumentNullException(nameof(input));

            Input.InputReceived += Input_InputReceived;
            IsRunning = true;
            Output.WriteLine(Player.CurrentRoom);
            Output.WriteLine(Player.CurrentRoom.LookDescription);
        }

        private void Input_InputReceived(object sender, string inputString)
        {
            Room previousRoom = Player.CurrentRoom;
            Commands command = ToCommand(inputString);
            string outputString;
            string subject = null;

            switch (command)
            {
                case Commands.Quit:
                    IsRunning = false;
                    outputString = "Thank you for playing!";
                    break;

                case Commands.Look:
                    Output.WriteLine(Player.CurrentRoom.LookDescription);                   
                    
                    foreach (Item item in Player.CurrentRoom.Inventory)
                    {
                        Output.WriteLine(item.LookDescription);
                    }
                    outputString = null;
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

                    //while(IsRunning)
                    //{
                    //    char separator = ' ';
                    //    string[] commandTokens = inputString.Split(separator);

                    //    string verb = null;
                    //    if (commandTokens.Length == 0)
                    //    {
                    //        continue;
                    //    }
                    //    else if (commandTokens.Length == 1)
                    //    {
                    //        verb = commandTokens[0];
                    //    }
                    //    else
                    //    {
                    //        verb = commandTokens[0];
                    //        subject = commandTokens[1];
                    //    }
                    //}

                    //const string fieldDelimiter = " ";
                    //const int expectedFieldCount = 2;
                    //var takeCommand = from item in Player.CurrentRoom.Inventory
                    //                  let fields = item.Split(fieldDelimiter)
                    //                  where fields.Length == expectedFieldCount
                    //                  select ();


                    Item itemToTake = null;
                    foreach (Item item in Player.CurrentRoom.Inventory)
                    {
                        if (string.Compare(item.Name, subject, ignoreCase: true) == 0)
                        {
                            itemToTake = item;
                            break;
                        }
                    }

                    if (itemToTake == null)
                    {
                        outputString = "You can not see any such thing";
                    }
                    else
                    {
                        Player.CurrentRoom.Inventory.Remove(itemToTake);
                        Player.Inventory.Add(itemToTake);
                        outputString = $"You have taken the {itemToTake}."; //itemToTake writes Zork.Common.Item
                    }
                    break;

                case Commands.Drop:
                    //TODO
                    Item itemToDrop = null;
                    foreach (Item item in Player.Inventory)
                    {
                        if (string.Compare(item.Name, subject, ignoreCase: true) == 0)
                        {
                            itemToDrop = item;
                            break;
                        }

                        if (itemToDrop == null)
                        {
                            outputString = "You can not see any such thing";
                        }
                        else
                        {
                            Player.CurrentRoom.Inventory.Add(itemToDrop);
                            Player.Inventory.Remove(itemToDrop);
                            outputString = $"You have dropped the {itemToDrop}."; //not executing
                        }
                    }
                    outputString = null;
                    break;

                case Commands.Inventory:
                    //TODO
                    foreach (Item item in Player.Inventory)
                    {
                        if (Player.Inventory.Count != 0) //not executing
                        {
                            outputString = item.LookDescription; //implement InventoryDescription here and add them to json 
                        }

                        else
                        {
                            outputString = "You are empty handed.";
                        }
                    }
                    outputString = null;
                    break;

                default:
                    outputString = "Unknown command.";
                    break;
            }

            if(previousRoom != Player.CurrentRoom)
            {
                Output.WriteLine(outputString = Player.CurrentRoom.LookDescription);
            }

            if (command != Commands.Unknown)
            {
                Player.Moves++;
            }

            Output.WriteLine(outputString);
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}
