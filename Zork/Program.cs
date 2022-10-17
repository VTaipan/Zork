using System;
using System.Collections.Generic;
using System.IO;

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
            const string defaultRoomsFilename = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFileName] : defaultRoomsFilename);
            InitializeRoomDescriptions(roomsFilename);
            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = CurrentRoom;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(CurrentRoom);
                if (previousRoom != CurrentRoom && CurrentRoom.HasBeenVisited == false)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                    CurrentRoom.HasBeenVisited = true;
                }
                Console.Write("> ");


                Commands command = Commands.UNKNOWN;
                while (command != Commands.QUIT)
                {
                    command = ToCommand(Console.ReadLine().Trim());

                    switch (command)
                    {
                        case Commands.QUIT:
                            isRunning = false;
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
        }

        private static readonly Dictionary<string, Room> RoomMap;
        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in _rooms)
            {
                RoomMap[room.Name] = room;
            }
        }
        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldDelimiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                RoomMap[name].Description = description;
            }
        }

        private enum Fields
        {
            Name,
            Description
        }

        private static readonly Room[,] _rooms =
            {
                { new Room("Rocky Trail"), new Room ("South of House"), new Room ("Canyon View")},
                { new Room("Forest"), new Room("West of House"), new Room("Behind House")},
                { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing")}
            };


        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

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