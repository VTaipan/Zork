using System;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return _rooms[_location.Row, _location.Column];
            }
        }

        static void Main(string[] args)
        {
            const string defaultRoomsFilename = @"Content\Room.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFileName] : defaultRoomsFilename);
            InitializeRooms(roomsFilename);
            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = CurrentRoom;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                if (previousRoom != CurrentRoom && CurrentRoom.HasBeenVisited == false)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                    CurrentRoom.HasBeenVisited = true;
                }
                Console.Write("> ");                             
                    command = ToCommand(Console.ReadLine().Trim());

                    switch (command)
                    {
                        case Commands.QUIT:
                            Console.WriteLine("Thank you for playing!");
                            break;

                        case Commands.LOOK:
                            Console.WriteLine(CurrentRoom.Description);
                            break;

                        case Commands.NORTH:
                        case Commands.SOUTH:
                        case Commands.EAST:
                        case Commands.WEST:
                            if (Move(command) == false)
                            {
                                Console.WriteLine("The way is shut!");
                            }
                            break;

                        default:
                            Console.WriteLine("Unknown command.");
                            break;
                    }
                    Console.WriteLine(CurrentRoom);               
            }
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

        private static void InitializeRooms(string roomsFilename)
        {
            _rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
        }

        private static Room[,] _rooms;

        private enum CommandLineArguments
        {
            RoomsFileName = 0
        }

        private static (int Row, int Column) _location = (1, 1);
        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when _location.Row < _rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < _rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.WEST when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;
            }
            return didMove;
        }
    }
}